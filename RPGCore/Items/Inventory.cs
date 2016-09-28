using PropertyChanged;
using RPGCore.Economy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCore.Items
{
    [ImplementPropertyChanged]
    public class Inventory
    {
        public Inventory() { items = new ObservableCollection<ItemBase>(); Money = new Currency(); }
        public Inventory(List<ItemBase> _i, Currency _m)
        {
            Items = new ObservableCollection<ItemBase>(_i);
            Money = _m;
        }

        #region Basic Fields
        private ObservableCollection<ItemBase> items;

        public ObservableCollection<ItemBase> Items
        {
            get { return new ObservableCollection<ItemBase>(items.OrderBy(o => o.Name)); }
            set { items = value; }
        }
        private Currency money;

        public Currency Money
        {
            get { return money; }
            set { money = value; }
        }

        #endregion
        #region Inventory Functions
        public void AddItem(ItemBase item)
        {
            Items.Add(item);
        }
        public void RemoveItem(ItemBase item)
        {
            if (Items.Where(o => o == item).Count() >= 1)
                Items.Remove(item);
        }
        public List<T> GetItemsOfType<T>()
        {
            List<T> list = new List<T>();
            list = Items.OfType<T>().ToList<T>();
            return list;
        }
        #endregion

        public override string ToString()
        {
            return string.Format("Items Count = {0}; Money = {1}", items.Count(), Money);
        }
    }
}
