using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


/*
informatie systeem Multivlaai
1. onderhouden van medewerkersgegevens; 6 medewerkers totaal. 1 Mananger 2 inkopers 3verkopers
2. onderhouden van klantgegevens; NAW gegevens
3. onderhouden van productgegevens; Limburgse vlaaien  
*/
namespace PresentatieEindopdrachtAvans
{
   
    class Program
    {
        static void Main(string[] args)
        {   //Intro en keuzemenu
            Console.WriteLine("Welkom bij Multivlaai!");
            Console.WriteLine("----------------------");
            Console.WriteLine("Geef uw wachtwoord op alstublieft");

            bool doorgaan = true;

            var wachtwoord = Console.ReadLine();
            var i = 0;
                        
            while (wachtwoord != "123")
            
			{
                i++;            // verzin iets leuks om op aantal invoer te checken ;)
                if (wachtwoord == "123")
                {
                    Console.WriteLine("Inloggen gelukt");
                }
                else
                {
                    Console.WriteLine("Uw wachtwoord is onjuist");
                    Console.WriteLine("Geef nogmaals uw wachtwoord op alstublieft");
                    wachtwoord = Console.ReadLine();
                }

            }

            //Nieuwe lijsten
            //var LijstKlanten = new List<Klanten>();
            //var LijstPersoneel = new List<Personeel>();
            //var LijstProducten = new List<Producten>();
            //var LijstKlanten1 = new List<KlantenBestand>();
            //var LijstPersoneel1 = new List<PersoneelBestand>();

            //Keuze menu
            Console.WriteLine("\n Kies: \n 1. Personeel \n 2. Klanten \n 3. Producten \n 4. (...) \n 5. (...) \n 6. Stoppen ");
            var Choice = int.Parse(Console.ReadLine());


            while (doorgaan)
            {
                if (Choice == 1)
                {
                    Console.WriteLine("U heeft gekozen voor Personeel. \n Type A om personeel toe te voegen. \n Type B om personeel te wijzigen?\n Type X Om te stoppen\n");

                    string input = Console.ReadLine();

                    while (doorgaan)
                    {

                        if (input == "A")
                        {
                            Console.WriteLine("Personeel toevoegen");
                            // doe iets 
                        }
                        else if (input == "B")
                        {
                            Console.WriteLine("Personeel wijzigen");
                            // doe iets anders leuks

                        }

                        else if (input == "X")
                        {
                            doorgaan = false;
                        }

                        if (doorgaan)
                        {
                            input = Console.ReadLine();
                            Console.WriteLine("U heeft gekozen voor Personeel. \n Type A om personeel toe te voegen. \n Type B om personeel te wijzigen?");
                        }

                        ////// OF switch kan ook;//////

                        //switch (input)
                        //{
                        //    case "A":
                        //        Console.WriteLine("Personeel toevoegen");
                        //        break;

                        //    case "B":
                        //        Console.WriteLine("Personeel wijzigen");
                        //        break;
                        //    case "X":
                        //        Console.WriteLine("Doei!");
                        //        doorgaan = false; 
                        //        break;
                        //    default:
                                                               
                        //        break;
                        //}
                        //if (doorgaan)
                        //{
                        //    input = Console.ReadLine();
                        //    Console.WriteLine("U heeft gekozen voor Personeel. \n Type A om personeel toe te voegen. \n Type B om personeel te wijzigen?");
                        //}

                    }
                    doorgaan = true;        // omdat we var doorgaan ook in main menu loop gebruiken terug op 'true' 
                }

                //Keuze 2
                else if (Choice == 2)
                {
                    Console.WriteLine("U heeft gekozen voor Klanten. \n Wilt u een bestelling plaatsen? Type: Ja / Nee");
                    if (Console.ReadLine() == "Ja")
                    {
                        Console.WriteLine("Bestelling plaatsen");

                        while (doorgaan)
                        {
                            //Klanten toevoegen en bestelling plaatsen
                            //var Bestelling = new Klanten();    
                            //LijstKlanten.Add(Bestelling);

                            Console.Write("Wilt u nog een bestelling plaatsen? Type Ja / Nee \n");
                            if (Console.ReadLine() == "Nee")
                            {
                                doorgaan = false;
                                Console.WriteLine("\n Kies: \n 1. Personeel \n 2. Klanten \n 3. Producten \n 4. (...) \n 5. (...) \n 6. (...)");
                                Choice = int.Parse(Console.ReadLine());
                            }
                        }
                    }
                }


                //Keuze 3
                else if (Choice == 3)
                {
                    Console.WriteLine("U heeft gekozen voor Producten. \n Type: A om nieuwe producten toe te voegen. \n Type: B om het assortiment in te zien.");
                    //Submenu
                    if (Console.ReadLine() == "A")
                    {
                        Console.WriteLine("Nieuwe producten toevoegen");

                        while (doorgaan)
                        {
                            //var vlaaien = new Producten();
                            //LijstProducten.Add(vlaaien);

                            Console.Write("Wilt u nog een product toevoegen? Type Ja / Nee \n");
                            if (Console.ReadLine() == "Nee")
                            {
                                doorgaan = false;
                                Console.WriteLine("Kies: \n 1. Personeel \n 2. Klanten \n 3. Producten \n 4. (...) \n 5. (...) \n 6. (...) ");
                                Choice = int.Parse(Console.ReadLine());
                            }
                        }
                    }
                    //Submenu
                    if (Console.ReadLine() == "B")
                    {
                        Console.WriteLine("Assortiment inzien \n (...)");
                        //Assortiment laten zien
                    }
                }

                //Keuze 4
                else if (Choice == 4)
                {
                    Console.WriteLine("U heeft gekozen voor (...) ");
                    if (Console.ReadLine() == "x")
                    {

                    }

                }


                //Keuze 5
                else if (Choice == 5)
                {
                    Console.WriteLine("Gegevens inzien van het klantenbestand");
                    Console.WriteLine("--------------------------------------");

                    //var bestand1 = new KlantenBestand();
                    //LijstKlanten1.Add(bestand1);

                }

                else if (Choice == 6)
                {
                    doorgaan = false;
                }
                
                if (doorgaan)
                {
                    Choice = int.Parse(Console.ReadLine());
                    Console.WriteLine("\n Kies: \n 1. Personeel \n 2. Klanten \n 3. Producten \n 4. (...) \n 5. (...) \n 6. (...) ");
                }

            }
        }
    }
}

