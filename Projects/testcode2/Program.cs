using System;


class Program
    {
        

        static void Main(string[] args)
        {
            bool abool = false;
            
        
            bool flagToggle = abool ? false : true;
        string a = abool ? "abool is Waar" : "abool is onwaar";
        Console.Write("flagToggle is " + flagToggle + " " + a);
        Console.ReadKey();
        }

    }