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
        public static readonly BindableProperty AbilityNameProperty = BindableProperty.Create(nameof(AbilityName), typeof(string), typeof(AbilityScoreView), string.Empty);
        public static readonly BindableProperty AbilityKeyProperty = BindableProperty.Create(nameof(AbilityKey), typeof(string), typeof(AbilityScoreView), string.Empty);

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

        public AbilityScoreView()
        {
            InitializeComponent();
        }
    }
}