using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCore.Items
{
    [ImplementPropertyChanged]
    public class ItemBase
    {
        public ItemBase() { }
        public ItemBase(string _n, string _d, int _v)
        {
            Name = _n;
            Description = _d;
            Value = _v;
        }
        #region Basic Fields
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion
    }

    [ImplementPropertyChanged]
    public class Consumable : ItemBase
    {
        public Consumable()
        {
        }

        public Consumable(string _n, string _d, int _v, int _HPr, int _MPr)
        {
            Name = _n;
            Description = _d;
            Value = _v;
            HealthRecovery = _HPr;
            ManaRecovery = _MPr;
        }

        #region Basic Fields
        private int healthRecovery;

        public int HealthRecovery
        {
            get { return healthRecovery; }
            set { healthRecovery = value; }
        }
        private int manaRecovery;

        public int ManaRecovery
        {
            get { return manaRecovery; }
            set { manaRecovery = value; }
        }
        #endregion
    }
}
