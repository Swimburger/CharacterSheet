using CharacterSheet.Controls;
using CharacterSheet.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CharacterSheet.Views
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            Style = (Style)Application.Current.Resources["PageStyle"];
        }

        protected override async void OnAppearing()
        {
            Content = new ScrollView
            {
                Content = await BuildGridAsync()
            };
        }

        private async Task<Grid> BuildGridAsync()
        {
            var grid = new Grid();
            JObject layoutObject = await Task.Run(() => GetLayoutObject());
            AddColumns(grid, layoutObject);
            AddRows(grid, layoutObject);
            await AddWidgetsAsync(grid, layoutObject);
            return grid;
        }

        private void AddColumns(Grid grid, JObject layoutObject)
        {
            JArray columns = (JArray)layoutObject["grid"]["columns"];
            foreach (var columnValue in columns.Children<JValue>())
            {
                var columnDefinition = new ColumnDefinition();
                if (columnValue.Type == JTokenType.String)
                {
                    var columnString = (string)columnValue.Value;
                    if (columnString == "*")
                    {
                        columnDefinition.Width = new GridLength(1, GridUnitType.Star);
                    }
                    else if (columnString.EndsWith("*"))
                    {
                        double columnWidth = Convert.ToDouble(columnString.TrimEnd('*'));
                        columnDefinition.Width = new GridLength(columnWidth, GridUnitType.Star);
                    }
                    else if (columnString == "auto")
                    {
                        columnDefinition.Width = new GridLength(1, GridUnitType.Auto);
                    }
                    else
                    {
                        double columnWidth = Convert.ToDouble(columnString);
                        columnDefinition.Width = new GridLength(columnWidth, GridUnitType.Absolute);
                    }
                }
                else
                {
                    double columnWidth = Convert.ToDouble(columnValue.Value);
                    columnDefinition.Width = new GridLength(columnWidth, GridUnitType.Absolute);
                }

                grid.ColumnDefinitions.Add(columnDefinition);
            }
        }

        private void AddRows(Grid grid, JObject layoutObject)
        {
            JArray rows = (JArray)layoutObject["grid"]["rows"];
            foreach (var rowValue in rows.Children<JValue>())
            {
                var rowDefinition = new RowDefinition();
                if (rowValue.Type == JTokenType.String)
                {
                    var rowString = (string)rowValue.Value;
                    if (rowString == "*")
                    {
                        rowDefinition.Height = new GridLength(1, GridUnitType.Star);
                    }
                    else if (rowString.EndsWith("*"))
                    {
                        double rowHeight = Convert.ToDouble(rowString.TrimEnd('*'));
                        rowDefinition.Height = new GridLength(rowHeight, GridUnitType.Star);
                    }
                    else if (rowString == "auto")
                    {
                        rowDefinition.Height = new GridLength(1, GridUnitType.Auto);
                    }
                    else
                    {
                        double rowHeight = Convert.ToDouble(rowString);
                        rowDefinition.Height = new GridLength(rowHeight, GridUnitType.Absolute);
                    }
                }
                else
                {
                    double rowHeight = Convert.ToDouble(rowValue.Value);
                    rowDefinition.Height = new GridLength(rowHeight, GridUnitType.Absolute);
                }

                grid.RowDefinitions.Add(rowDefinition);
            }
        }

        private async Task<Grid> AddWidgetsAsync(Grid grid, JObject layoutObject)
        {
            var widgets = (JArray)layoutObject["widgets"];
            var widgetObjects = widgets
                .OfType<JObject>()
                .Select((widget) =>
                {
                    return new
                    {
                        Name = widget.Value<string>("name"),
                        Key = widget.Value<string>("key"),
                        Column = widget.Value<int>("column"),
                        Row = widget.Value<int>("row"),
                        RowSpan = widget.Value<int?>("rowSpan"),
                        WidgetView = widget.Value<string>("view"),
                        Type = widget.Value<string>("type"),
                        DefaultValue = widget.Value<string>("defaultValue"),
                        DefaultModifierValue = widget.Value<string>("defaultModifierValue"),
                        DefaultScoreValue = widget.Value<string>("defaultScoreValue"),
                        DefaultSaveValue = widget.Value<string>("defaultSaveValue")
                    };
                })
                .ToList();

            var widgetKeys = widgetObjects.Select(w => w.Key).ToList();
            var abilityKeys = widgetObjects
                .Where(w => w.Type == "abilityScore")
                .SelectMany((w) => new string[] {
                    $"{w.Key}-modifier",
                    $"{w.Key}-score",
                    $"{w.Key}-save",
                });
            widgetKeys.AddRange(abilityKeys);

            Dictionary<string, string> keyValues;
            using (var keyValueContext = new KeyValueContext())
            {
                keyValues = await keyValueContext.KeyValues
                    .Where(kv => widgetKeys.Contains(kv.Key))
                    .ToDictionaryAsync((kv) => kv.Key, (kv) => kv.Value);
            }

            foreach (var widgetObject in widgetObjects)
            {
                View view;
                switch (widgetObject.WidgetView)
                {
                    case "editLabel":
                        view = new EditLabelView(this)
                        {
                            LabelTitle = widgetObject.Name,
                            LabelKey = widgetObject.Key,
                            LabelValue = keyValues.ContainsKey(widgetObject.Key) ? keyValues[widgetObject.Key] : widgetObject.DefaultValue
                        };
                        break;
                    case "keyValue":
                        view = new KeyValueView(this)
                        {
                            BoxTitle = widgetObject.Name,
                            BoxKey = widgetObject.Key,
                            BoxValue = keyValues.ContainsKey(widgetObject.Key) ? keyValues[widgetObject.Key] : widgetObject.DefaultValue
                        };
                        break;
                    case "abilityScore":
                        var modifierKey = $"{widgetObject.Key}-modifier";
                        var saveKey = $"{widgetObject.Key}-save";
                        var scoreKey = $"{widgetObject.Key}-score";
                        view = new AbilityScoreView(this)
                        {
                            AbilityName = widgetObject.Name,
                            AbilityKey = widgetObject.Key,
                            AbilityModifierValue = keyValues.ContainsKey(modifierKey) ? keyValues[modifierKey] : widgetObject.DefaultModifierValue,
                            AbilitySaveValue = keyValues.ContainsKey(saveKey) ? keyValues[saveKey] : widgetObject.DefaultSaveValue,
                            AbilityScoreValue = keyValues.ContainsKey(scoreKey) ? keyValues[scoreKey] : widgetObject.DefaultScoreValue,
                        };
                        break;
                    default:
                        continue;
                }

                Grid.SetColumn(view, widgetObject.Column);
                Grid.SetRow(view, widgetObject.Row);
                if (widgetObject.RowSpan != null)
                {
                    Grid.SetRowSpan(view, widgetObject.RowSpan.Value);
                }
                grid.Children.Add(view);
            }

            return grid;
        }

        private JObject GetLayoutObject()
        {
            var assembly = GetType().Assembly;
            Stream stream = assembly.GetManifestResourceStream("CharacterSheet.Templates.HomeLayout.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var layoutObject = JsonConvert.DeserializeObject<JObject>(json);
                return layoutObject;
            }
        }
    }
}