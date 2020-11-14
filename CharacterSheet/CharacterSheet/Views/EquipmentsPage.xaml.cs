using CharacterSheet.Data.E5;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CharacterSheet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipmentsPage : ContentPage
    {
        private Equipment[] allEquipments;
        public ObservableRangeCollection<Equipment> Equipments { get; set; } = new ObservableRangeCollection<Equipment>();

        public EquipmentsPage()
        {
            InitializeComponent();
            EquipmentsListView.ItemsSource = Equipments;
        }

        protected override async void OnAppearing()
        {
            var e5Client = new E5Client();
            var equipmentsListResponse = await e5Client.GetEquipments();
            allEquipments = equipmentsListResponse.Results;
            Equipments.AddRange(GetFilteredEquipments(EquipmentsFilterEntry.Text));
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private void EquipmentsFilterEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Equipments.ReplaceRange(GetFilteredEquipments(e.NewTextValue));
        }

        private IEnumerable<Equipment> GetFilteredEquipments(string filter)
        {
            if(allEquipments == null)
            {
                return Enumerable.Empty<Equipment>();
            }

            if (string.IsNullOrEmpty(filter))
            {
                return allEquipments;
            }

            return allEquipments.Where(eq => eq.Name.IndexOf(filter, 0, StringComparison.OrdinalIgnoreCase) != -1);
        }
    }
}
