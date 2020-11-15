using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CharacterSheet.ViewModels
{
    public class EquipmentDetailsViewModel : INotifyPropertyChanged
    {
        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private string cost;
        public string Cost
        {
            get { return cost; }
            set { SetProperty(ref cost, value); }
        }

        private string damageDice;
        public string DamageDice
        {
            get { return damageDice; }
            set { SetProperty(ref damageDice, value); }
        }

        private string damageType;
        public string DamageType
        {
            get { return damageType; }
            set { SetProperty(ref damageType, value); }
        }

        private string range;
        public string Range
        {
            get { return range; }
            set { SetProperty(ref range, value); }
        }

        private string weight;
        public string Weight
        {
            get { return weight; }
            set { SetProperty(ref weight, value); }
        }

        private string properties;
        public string Properties
        {
            get { return properties; }
            set { SetProperty(ref properties, value); }
        }

        private string armorClass;
        public string ArmorClass
        {
            get { return armorClass; }
            set { SetProperty(ref armorClass, value); }
        }

        private bool stealthDisadvantaged;
        public bool StealthDisadvantaged
        {
            get { return stealthDisadvantaged; }
            set { SetProperty(ref stealthDisadvantaged, value); }
        }             

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
