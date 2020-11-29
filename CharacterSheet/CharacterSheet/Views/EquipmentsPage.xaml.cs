using CharacterSheet.Data.E5;
using MvvmHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CharacterSheet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipmentsPage : ContentPage
    {
        private readonly List<E5Resource> allItems = new List<E5Resource>();
        public ObservableRangeCollection<E5Resource> Items { get; set; } = new ObservableRangeCollection<E5Resource>();

        public EquipmentsPage()
        {
            InitializeComponent();
            EquipmentsListView.ItemsSource = Items;
        }

        protected override async void OnAppearing()
        {
            allItems.Clear();
            Items.Clear();
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await DisplayAlert("No internet", "The app could not connect to the internet.", "OK");
                return;
            }

            var e5Client = new E5Client();
            try
            {
                await Task.WhenAll(LoadEquipment(e5Client), LoadMagicItems(e5Client));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while loading equipments and magic items");
                await DisplayAlert("Unexptected Error", "An error occurred while loading equipments and magic items.", "OK");
            }
        }

        private async Task LoadEquipment(E5Client e5Client)
        {
            var equipmentsList = await e5Client.GetEquipmentsAsync();
            allItems.AddRange(equipmentsList.Results);
            Items.AddRange(GetFilteredEquipments(EquipmentsFilterEntry.Text));
        }

        private async Task LoadMagicItems(E5Client e5Client)
        {
            var magicItemsList = await e5Client.GetMagicItemsAsync();
            allItems.AddRange(magicItemsList.Results);
            Items.AddRange(GetFilteredEquipments(EquipmentsFilterEntry.Text));
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var detailsPage = new EquipmentDetailsPage((E5Resource)e.Item);
            await Navigation.PushAsync(detailsPage);
        }

        private void EquipmentsFilterEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Items.ReplaceRange(GetFilteredEquipments(e.NewTextValue));
        }

        private IEnumerable<E5Resource> GetFilteredEquipments(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return allItems;
            }

            return allItems.Where(eq => eq.Name.IndexOf(filter, 0, StringComparison.OrdinalIgnoreCase) != -1);
        }
    }
}
