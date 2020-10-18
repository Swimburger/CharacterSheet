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
    public partial class TitleIntegerBoxView : StackLayout
    {
        public static readonly BindableProperty BoxTitleProperty = BindableProperty.Create(nameof(BoxTitle), typeof(string), typeof(TitleIntegerBoxView), string.Empty);
        public static readonly BindableProperty BoxKeyProperty = BindableProperty.Create(nameof(BoxKey), typeof(string), typeof(TitleIntegerBoxView), string.Empty);

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

        public TitleIntegerBoxView()
        {
            InitializeComponent();
        }
    }
}