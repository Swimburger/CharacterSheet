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
        public static readonly BindableProperty BoxTitleProperty = BindableProperty.Create(nameof(LabelTitle), typeof(string), typeof(EditLabelView));
        public static readonly BindableProperty BoxKeyProperty = BindableProperty.Create(nameof(LabelKey), typeof(string), typeof(EditLabelView));
        public static readonly BindableProperty BoxValueProperty = BindableProperty.Create(nameof(LabelValue), typeof(string), typeof(EditLabelView));
        private readonly Page page;

        public string LabelTitle
        {
            get => (string)GetValue(BoxTitleProperty);
            set => SetValue(BoxTitleProperty, value);
        }

        public string LabelKey
        {
            get => (string)GetValue(BoxKeyProperty);
            set => SetValue(BoxKeyProperty, value);
        }

        public string LabelValue
        {
            get => (string)GetValue(BoxValueProperty);
            set => SetValue(BoxValueProperty, value);
        }

        public EditLabelView(Page page)
        {
            this.page = page;
            InitializeComponent();
        }

        private async void OnTapped(object sender, EventArgs e)
        {
            string result = await page.DisplayPromptAsync(LabelTitle, $"Enter value for {LabelTitle}", initialValue: LabelValue);
            if(result != null)
            {
                LabelValue = result;
            }
        }
    }
}