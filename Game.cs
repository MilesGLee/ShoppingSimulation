using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ShoppingSimulation
{
    public struct Item 
    {
        public string Name;
        public int Cost;
    }
    
    class Game
    {
        Player _player;
        Shop _shop;
        bool gameOver;
        int _currentScene;

        public void Run()
        {
            Start();
            while (!gameOver)
                Update();
            End();
        }

        void Start() 
        {
            gameOver = false;
            _currentScene = 0;
            InitializeItems();
            _player = new Player();
        }

        void Update() 
        {
            DisplayCurrentScene();
        }

        void End() 
        {
            Console.WriteLine("Well, see you next time you open the game.");
            Console.ReadKey(true);
        }

        void InitializeItems() 
        {
            Item item1 = new Item();
            item1.Name = "Wompus' Gun";
            item1.Cost = 5;
            Item item2 = new Item();
            item2.Name = "Big Wand";
            item2.Cost = 7;
            Item item3 = new Item();
            item3.Name = "Essence of Uncle Phil";
            item3.Cost = 10;
            Item[] tempInventory = new Item[3];
            tempInventory[0] = item1;
            tempInventory[1] = item2;
            tempInventory[2] = item3;
            _shop = new Shop(tempInventory);
        }

        int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputReceived = -1;
            while (inputReceived == -1)
            {
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{(i + 1)}. {options[i]}");
                }
                Console.Write(">");
                input = Console.ReadLine();

                if (int.TryParse(input, out inputReceived))
                {
                    inputReceived--;
                    if (inputReceived < 0 || inputReceived >= options.Length)
                    {
                        inputReceived = -1;

                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    inputReceived = -1;
                    Console.WriteLine("Invalid Input.");
                    Console.ReadKey(true);

                    Console.Clear();
                }

                Console.Clear();
            }

            return inputReceived;
        }

        void Save() 
        {
            StreamWriter writer = new StreamWriter("SaveData.txt");
            //only player needs to be saved.
            _player.Save(writer);
            writer.Close();
        }

        public bool Load() 
        {
            bool loadSuccessful = true;
            if (!File.Exists("SaveData.txt"))
                loadSuccessful = false;
            StreamReader reader = new StreamReader("SaveData.txt");
            if (!_player.Load(reader))
                loadSuccessful = false;
            reader.Close();
            return loadSuccessful;
        }

        void DisplayCurrentScene() 
        {
            if (_currentScene == 0)
                DisplayOpeningMenu();
            if (_currentScene == 1)
                DisplayShopMenu();
        }

        void DisplayOpeningMenu()
        {
            Console.WriteLine("Shopping Simulation Game.");
            int choice = GetInput("Please pick an option", "Start", "Load", "Quit");
            if (choice == 0)
            {
                _currentScene++;
            }
            else if (choice == 1)
            {
                _player = new Player();
                Load();
                Console.WriteLine("Loaded Game");
                Console.ReadKey(true);
                Console.Clear();
                return;
            }
            else if (choice == 2) 
            {
                gameOver = true;
            }
        }

        void GetShopMenuOptions() 
        {
            int choice = GetInput("Please select an item to buy.", _shop.GetItemNames());
            _shop.Sell(_player, choice);
            return;
        }

        void DisplayShopMenu() 
        {
            int choice = GetInput($"Welcome to my shop traveler! See anything you like? All my prices are fair.", "Look at shop menu", "Check stats", "Save", "Leave");
            if (choice == 0)
                GetShopMenuOptions();
            else if (choice == 1)
            {
                Console.Clear();
                Console.WriteLine("Your statistics:");
                Console.WriteLine($"-Gold: {_player.Gold}");
                Console.WriteLine($"-=Inventory=-");
                foreach (string i in _player.GetItemNames())
                {
                    Console.WriteLine($"-{i}.");
                }
                Console.ReadKey();
                Console.Clear();
            }
            else if (choice == 2) 
            {
                Save();
                Console.WriteLine("Saved Game");
                Console.ReadKey(true);
                Console.Clear();
                return;
            }
            else if (choice == 3)
            {
                _currentScene--;
            }
        }
    }
}
