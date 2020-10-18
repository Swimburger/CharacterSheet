using CharacterSheet.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CharacterSheet.Views
{
    public class HomePage : ContentPage
    {
        public HomePage()
        {
            Content = new ScrollView
            {
                Content = BuildGrid()
            };
        }

        private Grid BuildGrid()
        {
            var grid = new Grid();
            JObject layoutObject = GetLayoutObject();
            AddColumns(grid, layoutObject);
            AddRows(grid, layoutObject);
            AddWidgets(grid, layoutObject);
            return grid;
        }

        private static void AddColumns(Grid grid, JObject layoutObject)
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

        private static void AddRows(Grid grid, JObject layoutObject)
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

        private static Grid AddWidgets(Grid grid, JObject layoutObject)
        {
            var widgets = (JArray)layoutObject["widgets"];
            foreach (JObject widget in widgets)
            {
                var name = widget.Value<string>("name");
                var key = widget.Value<string>("key");
                var column = widget.Value<int>("column");
                var row = widget.Value<int>("row");
                var rowSpan = widget.Value<int?>("rowSpan");
                var widgetView = widget.Value<string>("view");
                View view;
                switch (widgetView)
                {
                    case "label":
                        view = new Label
                        {
                            Text = name
                        };
                        break;
                    case "keyValue":
                        view = new KeyValueView { BoxTitle = name, BoxKey = key };
                        break;
                    case "abilityScore":
                        view = new AbilityScoreView { AbilityName = name, AbilityKey = key };
                        break;
                    default:
                        continue;
                }

                Grid.SetColumn(view, column);
                Grid.SetRow(view, row);
                if (rowSpan != null)
                {
                    Grid.SetRowSpan(view, rowSpan.Value);
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