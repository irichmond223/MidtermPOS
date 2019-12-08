using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FreshFarms
{
    class TextFile
    {
        public static void ReadsTextFile()
        {
            StreamReader sr = new StreamReader(@"C:..\..\..\Inventory.txt");
            List<string> tempList = new List<string>();
            //List<Product> productList = new List<Product>();

            string line = "";


            while (line != null)
            {

                line = sr.ReadLine();
                if (line != null)
                {
                    tempList.Add(line);
                    //productList.Add(line);
                }
            }

            sr.Close();
        }
        public static void WritesTextFile(List<Product> productList)
        {

            // create a writer and open the file
            TextWriter tw = new StreamWriter(@"C:..\..\..\Inventory.txt");

            tw.WriteLine($"\n{"#",-3} {"Name",-15} {"Category",-15} {"Description",-90} {"Price",-70}\n");
            for (int i = 0; i < productList.Count; i++)
            {

                tw.WriteLine($"{i + 1,-3} {productList[i].Name,-15} {productList[i].Category,-15} {productList[i].Description,-90} ${productList[i].Price,-70}");

            }
            tw.WriteLine();
            tw.Close();
        }
    }
}
