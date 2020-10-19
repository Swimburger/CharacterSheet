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
    public partial class EditLabelView : ContentView
    {
        public static readonly BindableProperty LabelTitleProperty = BindableProperty.Create(nameof(LabelTitle), typeof(string), typeof(EditLabelView));
        public static readonly BindableProperty LabelKeyProperty = BindableProperty.Create(nameof(LabelKey), typeof(string), typeof(EditLabelView));
        public static readonly BindableProperty LabelValueProperty = BindableProperty.Create(nameof(LabelValue), typeof(string), typeof(EditLabelView));
        private readonly Page page;

        public string LabelTitle
        {
            get => (string)GetValue(LabelTitleProperty);
            set => SetValue(LabelTitleProperty, value);
        }

        public string LabelKey
        {
            get => (string)GetValue(LabelKeyProperty);
            set => SetValue(LabelKeyProperty, value);
        }

        public string LabelValue
        {
            get => (string)GetValue(LabelValueProperty);
            set => SetValue(LabelValueProperty, value);
        }

        public EditLabelView(Page page)
        {
            this.page = page;
            InitializeComponent();
        }

        private async void OnTapped(object sender, EventArgs e)
        {
            string result = await page.DisplayPromptAsync(LabelTitle, $"Enter value for {LabelTitle}", initialValue: LabelValue);
            if (result != null)
            {
                LabelValue = result;
                using (var keyValueContext = new KeyValueContext())
                {
                    var keyValue = await keyValueContext.KeyValues.SingleOrDefaultAsync(kv => kv.Key == LabelKey);
                    if (keyValue == null)
                    {
                        keyValue = new KeyValue { Key = LabelKey, Value = result };
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