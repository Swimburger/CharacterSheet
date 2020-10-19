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
    public partial class KeyValueView : StackLayout
    {
        public static readonly BindableProperty BoxTitleProperty = BindableProperty.Create(nameof(BoxTitle), typeof(string), typeof(KeyValueView));
        public static readonly BindableProperty BoxKeyProperty = BindableProperty.Create(nameof(BoxKey), typeof(string), typeof(KeyValueView));
        public static readonly BindableProperty BoxValueProperty = BindableProperty.Create(nameof(BoxValue), typeof(string), typeof(KeyValueView));
        private readonly Page page;

        public string BoxTitle
        {
            get => (string)GetValue(BoxTitleProperty);
            set => SetValue(BoxTitleProperty, value);
        }

        public string BoxKey
        {
            get => (string)GetValue(BoxKeyProperty);
            set => SetValue(BoxKeyProperty, value);
        }

        public string BoxValue
        {
            get => (string)GetValue(BoxValueProperty);
            set => SetValue(BoxValueProperty, value);
        }

        public KeyValueView(Page page)
        {
            this.page = page;
            InitializeComponent();
        }

        private async void OnTapped(object sender, EventArgs e)
        {
            string result = await page.DisplayPromptAsync(BoxTitle, $"Enter value for {BoxTitle}", initialValue: BoxValue);
            if(result != null)
            {
                BoxValue = result;
                using (var keyValueContext = new KeyValueContext())
                {
                    var keyValue = await keyValueContext.KeyValues.SingleOrDefaultAsync(kv => kv.Key == BoxKey);
                    if (keyValue == null)
                    {
                        keyValue = new KeyValue { Key = BoxKey, Value = result };
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