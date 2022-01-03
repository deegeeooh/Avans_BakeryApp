using System;


class Program
    {
        

        static void Main(string[] args)
        {
            string searchString = "12";
            string toBeSearched = "Hoogstraat 12";
            
            if (toBeSearched.Contains(searchString))
            {
                Console.WriteLine("\"{0}\" contains \"{1}\"",toBeSearched,searchString);
            }else
            {
                Console.WriteLine("\"{0}\" does not contain \"{1}\"",toBeSearched,searchString);
            }
        }

    }