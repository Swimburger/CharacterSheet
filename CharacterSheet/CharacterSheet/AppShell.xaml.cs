using System;
using System.Collections.Generic;
using CharacterSheet.ViewModels;
using CharacterSheet.Views;
using Xamarin.Forms;

namespace CharacterSheet
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EquipmentsPage), typeof(EquipmentsPage));
        }
    }
}
