using System.ComponentModel;
using Xamarin.Forms;
using CharacterSheet.ViewModels;

namespace CharacterSheet.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}