using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingSimulation
{
    class Shop
    {
        private int _gold;
        private Item[] _inventory;

        //public Item[] Inventory 
        //{
        //    get
        //    {
        //        return _inventory;
        //    }
        //}

        public Shop(Item[] inventory)
        {
            _inventory = inventory;
        }

        public string[] GetItemNames() 
        {
            string[] itemNames = new string[_inventory.Length];

            for (int i = 0; i < _inventory.Length; i++)
            {
                itemNames[i] = _inventory[i].Name;
            }

            return itemNames;
        }

        public bool Sell(Player player, int myInt)
        {
            if (myInt > _inventory.Length)
                return false;
            player.Buy(_inventory[myInt]);
            return true;
        }

    }
}
