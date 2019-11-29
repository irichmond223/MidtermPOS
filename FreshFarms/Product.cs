using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FreshFarms
{
    class Product
    {
        private string name;
        private string category;
        private string description;
        private double price;
        // public string categoryCode;
        //public string nameCode;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public Product() { }
        public Product(string name, string category, string description, double price)
        {
            Name = name;
            Category = category;
            Description = description;
            Price = price;
           
        }

        public void ProductToFile(List<Product> productList)
        {
            TextWriter tw = new StreamWriter(@"C:..\..\Product.txt");

            for (int i = 0; i < productList.Count; i++)
            {
                //Console.WriteLine($"{i + 1}. {productList[i].Name}");
                tw.WriteLine($"Item: {productList[i].Name,-15} {productList[i].Category,-15} {productList[i].Description,-80} {productList[i].Price,-80}");
            }

            tw.Close();
        }
        
    }
}
