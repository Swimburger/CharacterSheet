using CharacterSheet.Data.E5;
using CharacterSheet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CharacterSheet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipmentDetailsPage : ContentPage
    {
        private readonly EquipmentDetailsViewModel viewModel;
        private readonly E5Resource e5Resource;

        public EquipmentDetailsPage(E5Resource e5Resource)
        {
            this.e5Resource = e5Resource;
            Title = e5Resource.Name;
            viewModel = new EquipmentDetailsViewModel
            {
                Name = e5Resource.Name
            };

            BindingContext = viewModel;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var e5Client = new E5Client();
            EquipmentDetails equipmentDetails;
            if (e5Resource.Url.StartsWith("/api/equipment"))
            {
                equipmentDetails = await e5Client.GetEquipmentDetailsAsync(e5Resource.Index);
            }
            else if (e5Resource.Url.StartsWith("/api/magic-items"))
            {
                equipmentDetails = await e5Client.GetMagicItemDetailsAsync(e5Resource.Index);
            }
            else
            {
                return;
            }

            if (equipmentDetails.DescriptionLines != null && equipmentDetails.DescriptionLines.Length != 0)
            {
                viewModel.Description = string.Join($"{Environment.NewLine}{Environment.NewLine}", equipmentDetails.DescriptionLines);
            }

            if (equipmentDetails.Cost != null)
            {
                viewModel.Cost = $"{equipmentDetails.Cost.Quantity}{equipmentDetails.Cost.Unit}";
            }

            viewModel.DamageDice = equipmentDetails.Damage?.DamageDice;
            viewModel.DamageType = equipmentDetails.Damage?.DamageType.Name;

            if (viewModel.Range != null)
            {
                viewModel.Range = $"{equipmentDetails.Range.Normal}";
                if (equipmentDetails.Range.Long != null)
                {
                    viewModel.Range += $"/{equipmentDetails.Range.Long}";
                }
                viewModel.Range += "ft";
            }

            viewModel.Weight = equipmentDetails.Weight?.ToString();

            if (equipmentDetails.Properties != null && equipmentDetails.Properties.Length > 0)
            {
                viewModel.Properties = string.Join(", ", equipmentDetails.Properties.Select(p => p.Name));
            }

            if (equipmentDetails.ArmorClass != null)
            {
                viewModel.ArmorClass = equipmentDetails.ArmorClass.Base.ToString();
            }

            viewModel.StealthDisadvantaged = equipmentDetails.StealthDisadvantage == true;
        }
    }
}