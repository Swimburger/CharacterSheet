using CharacterSheet.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CharacterSheet.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AbilityScoreView : Grid
    {
        public static readonly BindableProperty AbilityNameProperty = BindableProperty.Create(nameof(AbilityName), typeof(string), typeof(AbilityScoreView));
        public static readonly BindableProperty AbilityKeyProperty = BindableProperty.Create(nameof(AbilityKey), typeof(string), typeof(AbilityScoreView));
        public static readonly BindableProperty AbilityModifierValueProperty = BindableProperty.Create(nameof(AbilityModifierValue), typeof(string), typeof(AbilityScoreView));
        public static readonly BindableProperty AbilityScoreValueProperty = BindableProperty.Create(nameof(AbilityScoreValue), typeof(string), typeof(AbilityScoreView));
        public static readonly BindableProperty AbilitySaveValueProperty = BindableProperty.Create(nameof(AbilitySaveValue), typeof(string), typeof(AbilityScoreView));
        private readonly Page page;

        public string AbilityName
        {
            get => (string)GetValue(AbilityNameProperty);
            set => SetValue(AbilityNameProperty, value);
        }

        public string AbilityKey
        {
            get => (string)GetValue(AbilityKeyProperty);
            set => SetValue(AbilityKeyProperty, value);
        }

        public string AbilityModifierValue
        {
            get => (string)GetValue(AbilityModifierValueProperty);
            set => SetValue(AbilityModifierValueProperty, value);
        }

        public string AbilityScoreValue
        {
            get => (string)GetValue(AbilityScoreValueProperty);
            set => SetValue(AbilityScoreValueProperty, value);
        }

        public string AbilitySaveValue
        {
            get => (string)GetValue(AbilitySaveValueProperty);
            set => SetValue(AbilitySaveValueProperty, value);
        }

        public AbilityScoreView(Page page)
        {
            InitializeComponent();
            this.page = page;
        }

        private async void OnModifierTapped(object sender, EventArgs e)
        {
            string result = await page.DisplayPromptAsync($"{AbilityName} Modifier", $"Enter value for {AbilityName} Modifier", initialValue: AbilityModifierValue);
            if (result != null)
            {
                AbilityModifierValue = result;
                using (var keyValueContext = new KeyValueContext())
                {
                    var key = $"{AbilityKey}-modifier";
                    var keyValue = await keyValueContext.KeyValues.SingleOrDefaultAsync(kv => kv.Key == key);
                    if (keyValue == null)
                    {
                        keyValue = new KeyValue { Key = key, Value = result };
                        keyValueContext.KeyValues.Add(keyValue);
                    }
                    else
                    {
                        keyValue.Value = result;
                    }
                    await keyValueContext.SaveChangesAsync();
                }
            }
        }

        private async void OnScoreTapped(object sender, EventArgs e)
        {
            string result = await page.DisplayPromptAsync($"{AbilityName} Score", $"Enter value for {AbilityName} Score", initialValue: AbilityScoreValue);
            if (result != null)
            {
                AbilityScoreValue = result;
                using (var keyValueContext = new KeyValueContext())
                {
                    var key = $"{AbilityKey}-score";
                    var keyValue = await keyValueContext.KeyValues.SingleOrDefaultAsync(kv => kv.Key == key);
                    if (keyValue == null)
                    {
                        keyValue = new KeyValue { Key = key, Value = result };
                        keyValueContext.KeyValues.Add(keyValue);
                    }
                    else
                    {
                        keyValue.Value = result;
                    }
                    await keyValueContext.SaveChangesAsync();
                }
            }
        }

        private async void OnSaveTapped(object sender, EventArgs e)
        {
            string result = await page.DisplayPromptAsync($"{AbilityName} Save", $"Enter value for {AbilityName} Save", initialValue: AbilitySaveValue);
            if (result != null)
            {
                AbilitySaveValue = result;
                using (var keyValueContext = new KeyValueContext())
                {
                    var key = $"{AbilityKey}-save";
                    var keyValue = await keyValueContext.KeyValues.SingleOrDefaultAsync(kv => kv.Key == key);
                    if (keyValue == null)
                    {
                        keyValue = new KeyValue { Key = key, Value = result };
                        keyValueContext.KeyValues.Add(keyValue);
                    }
                    else
                    {
                        keyValue.Value = result;
                    }
                    await keyValueContext.SaveChangesAsync();
                }
            }
        }
    }
}