using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ShoppingSimulation
{
    class Player
    {
        private Item[] _inventory;
        private int _gold;

        public float Gold 
        {
            get 
            {
                return _gold;
            }
        }

        //public Item[] Inventory 
        //{
        //    get 
        //    {
        //        return _inventory;
        //    }
        //}

        public Player() 
        {
            _inventory = new Item[0];
            _gold = 25;
        }

        public void Buy(Item item) 
        {
            if (_gold > item.Cost) 
            {
                Item[] tempItems = new Item[_inventory.Length + 1];
                for (int i = 0; i < _inventory.Length; i++)
                    tempItems[i] = _inventory[i];
                tempItems[tempItems.Length - 1] = item;

                _inventory = tempItems;
                _gold -= item.Cost;
                Console.Clear();
                Console.WriteLine("Pleasure doing business with you.");
                Console.ReadKey(true);
                Console.Clear();
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("You don't have enough gold for this item...");
                Console.ReadKey(true);
                Console.Clear();
            }
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

        public void Save(StreamWriter writer) 
        {
            writer.WriteLine(_gold);
            writer.WriteLine(_inventory.Length);
            foreach (Item i in _inventory)
            {
                writer.WriteLine(i.Name);
                writer.WriteLine(i.Cost);
            }
        }

        public bool Load(StreamReader reader) 
        {
            if (!int.TryParse(reader.ReadLine(), out _gold))
                return false;
            int itemAmount;
            if (!int.TryParse(reader.ReadLine(), out itemAmount))
                return false;
            for (int a = 0; a < itemAmount; a++) 
            {
                Item tempItem = new Item();
                tempItem.Name = reader.ReadLine();
                if (!int.TryParse(reader.ReadLine(), out tempItem.Cost))
                    return false;

                Item[] tempArray = new Item[_inventory.Length + 1];

                for (int i = 0; i < _inventory.Length; i++)
                {
                    tempArray[i] = _inventory[i];
                }

                tempArray[tempArray.Length - 1] = tempItem;

                _inventory = tempArray;
            }

            return true;
        }
    }
}
