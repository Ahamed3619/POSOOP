﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POSoop
{
    class POS
    {
        public Dictionary<int, Item> MyCard = new Dictionary<int, Item>();
        public List<Item> ItemList;
         
        public void AllItem()
        {
            Console.WriteLine("***************** WELCOME TO Online Bazar Developed BY TANVIR *****************\n");
            ItemList = new List<Item>()
            {
                new Item() {ItemQuantity = 5, ItemName = "Pen", ItemPrice=5, BuyingQuantity=0},
                new Item() {ItemQuantity = 8, ItemName ="shirt", ItemPrice=100, BuyingQuantity=0},
                new Item() {ItemQuantity = 15, ItemName = "Pant", ItemPrice=50, BuyingQuantity=0}
            };
        }

        
        public void UserCheck()
        {
            Console.Write("For Admin press 0, For customer press 1: ");
            var UserInput = Convert.ToInt32(Console.ReadLine());
            if (UserInput == 0)
            {
                Admin();
            }
            else if (UserInput == 1)
            {
                Customer();

            }
            else
            {
                Console.WriteLine("Wrong Input.TryAgain");
                UserCheck();
            }
        }

        private void Admin()
        {
            Console.Write("Select 0 for exit, 1 to add new item, 2 to add stock to existing item: ");
            var adminInput = Convert.ToInt32(Console.ReadLine());
            if (adminInput == 0)
            {
                UserCheck();
            }
            else if (adminInput == 1)
            {
                AddItem();
            }
            else if (adminInput == 2)
            {
                AddStock();
            }
            else
            {
                Console.WriteLine("Wrong Input.TryAgain");
                Admin();
            }
        }

        private void ProductShow()
        {
            Console.WriteLine("Serial\tItemName\tItemPrice\tQuantity");
            int i = 0;
            foreach (Item myItem in ItemList)
            {

                Console.WriteLine(++i + "\t" + myItem.ItemName + "\t\t" + myItem.ItemPrice + "\t\t" + myItem.ItemQuantity);
              
            }

            Console.ReadLine();
        }

        private void Customer()
        {
            ProductShow();
            Console.WriteLine("Press 0 to Buy and 1 to Exit ");
            var CustomerChoice = Convert.ToInt32(Console.ReadLine());
            if (CustomerChoice==0)
            {
                CustomerBuy();
            }
            else if (CustomerChoice == 1)
            {
                UserCheck();
            }
            else
            {
                Console.WriteLine("Wrong Choice.Try Again ");
                UserCheck();
            }
        }

        private void AddItem()
        {
            Console.Write("Enter Item Name: ");
            var itemNameInput = Console.ReadLine();
            Console.Write("Enter Item Quantity: ");
            var itemQuantityInput = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Item Price: ");
            var itemPriceInput = Convert.ToInt32(Console.ReadLine());

            ItemList.Add(new Item() { ItemQuantity = itemQuantityInput, ItemName = itemNameInput, ItemPrice = itemPriceInput });

            ProductShow();
        }

        private void AddStock()
        {

          
            ProductShow();
            Console.Write("What You Want to Add? Enter Serial: ");
            var serialInput = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Item Quantity: ");
            var itemNumberAdd = Convert.ToInt32(Console.ReadLine());
            ItemList[serialInput - 1].ItemQuantity += itemNumberAdd;
            ProductShow();


        }

        
        public void CustomerBuy()
        {
            Console.Write("Enter Serial Number to Buy the Product: ");
            var SerialChoice = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Quantity of the Product: ");
            var SerialQuantity = Convert.ToInt32(Console.ReadLine());
            if (!MyCard.ContainsKey(SerialChoice) && ItemList[SerialChoice - 1].ItemQuantity >= SerialQuantity)
            {




                ItemList[SerialChoice - 1].ItemQuantity = ItemList[SerialChoice - 1].ItemQuantity - SerialQuantity;
                
                MyCard.Add(SerialChoice, ItemList[SerialChoice - 1]);

                MyCard[SerialChoice].BuyingQuantity = SerialQuantity;
               

            }
              
            else if(ItemList[SerialChoice - 1].ItemQuantity >= SerialQuantity)
            {
                ItemList[SerialChoice - 1].ItemQuantity = ItemList[SerialChoice - 1].ItemQuantity - SerialQuantity;

                MyCard[SerialChoice].BuyingQuantity =SerialQuantity+ MyCard[SerialChoice].BuyingQuantity;
               
               
            }
            else
            {
                Console.WriteLine("Sorry Quantity is Insufficient. Try Again");
                CustomerBuy();
            }
            MoreBuy();

        }

        public void MoreBuy()
        {
            Console.Write("Press 0 to Buy More or 1 to check Card: ");
            var BuyInput = Convert.ToInt32(Console.ReadLine());
            if (BuyInput==0)
            {
                CustomerBuy();
            }
            else if (BuyInput == 1)
            {
                Console.WriteLine("Serial\tName\tT.Price\tQuantity");

                foreach (KeyValuePair<int, Item> ShowCard in MyCard)
                {

                    Console.WriteLine(ShowCard.Key + "\t" + ShowCard.Value.ItemName + "\t" + ShowCard.Value.ItemPrice * ShowCard.Value.BuyingQuantity + "\t" + ShowCard.Value.BuyingQuantity);
                    
                }
            }
            else
            {
                Console.WriteLine("Sorry Wrong Choice . Try Again");
                MoreBuy();
            }
        }
    }
}
