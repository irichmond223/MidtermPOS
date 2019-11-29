using System;
using System.Collections.Generic;
using System.Text;

namespace FreshFarms
{
    class Validator
    {

        public static int ValidateNum(string input, List<Product> listInput)
        {


            int item = 0;
            bool again = true;
            string test = "";
            while (again)
            {

                try
                {
                    
                    item = int.Parse(input) - 1;
                    test = listInput[item].Name;
                    again = false;
                }

                catch (FormatException ex)
                {
                    Console.WriteLine("Please enter a number");
                    input = Console.ReadLine();
                    again = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Please enter a number between 1 and {listInput.Count}!");
                    input = Console.ReadLine();
                    again = true;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Please enter a number");
                    input = Console.ReadLine();
                    again = true;
                }
            }
            return item;

        }

        public static int ValidateIndex(string input, List<Product> listInput)
        {


            int item = 0;
            bool again = true;
            string test = "";
            while (again)
            {

                try
                {

                    item = int.Parse(input);
                    test = listInput[item].Name;
                    again = false;
                }

                catch (FormatException ex)
                {
                    Console.WriteLine("Please enter a number");
                    input = Console.ReadLine();
                    again = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Please enter a number between 1 and {listInput.Count}!");
                    input = Console.ReadLine();
                    again = true;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Please enter a number");
                    input = Console.ReadLine();
                    again = true;
                }
            }
            return item;

        }
    }
}
