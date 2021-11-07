using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testcode
{    
    
    enum Winkel
    {
        Klanten,
        Medewerker,
        Bestaklnt,
        Bestwerk,
        Verkoop,
    }
    class Program
    {

        static List<Klant> klanten = new List<Klant>();
        static List<Medewerker> medewerker = new List<Medewerker>();
        static List<BestaandKlant> bestaklnt = new List<BestaandKlant>();
        static List<BestaandMedewerker> bestwerk = new List<BestaandMedewerker>();
        static List<Vlaai> verkoop = new List<Vlaai>();

        static void Main(string[] args)
        {
        
        //    var doorgaan = true;

        //    do
        //    {
        //        Console.WriteLine("maak een keuze (0/1/2/3/4) etc");
        //        Winkel kies = (Winkel)int.Parse(Console.ReadLine());

        //        switch (kies)
        //        {
        //            case Winkel.Klanten:
        //                DoeIetsMetKlanten();
        //                break;
        //            case Winkel.Medewerker:
        //                DoeIetsMetMedewerkers();
        //                break;
        //            case Winkel.Bestaklnt:
        //                break;
        //            case Winkel.Bestwerk:
        //                break;
        //            case Winkel.Verkoop:
        //                break;
        //            default:
        //                break;
        //        }

        //        check(ref doorgaan);

        //    } while (doorgaan);



            var doorgaan = true;
            Console.WriteLine("invoeren klant gegevens");

            while (doorgaan)
            {
                var klant = new Klant();
                klanten.Add(klant);

                check(ref doorgaan);
            }

            doorgaan = true;
            Console.WriteLine("invoeren medewerker gegevens");

            while (doorgaan)
            {
                var werker = new Medewerker();
                medewerker.Add(werker);

                check(ref doorgaan);
            }

            doorgaan = true;
            Console.WriteLine("invoeren soorten vlaai");

            while (doorgaan)
            {
                var verkocht = new Vlaai();
                verkoop.Add(verkocht);

                check(ref doorgaan);
            }

            ToonBestand();
        }
        static void ToonBestand()
        {
            Console.WriteLine("0 voor niewe klant. \n 1 voor nieuwe medewerker. \n 2 voor soorten vlai's.");
            Winkel kies = (Winkel)int.Parse(Console.ReadLine());

            switch (kies)
            {
                case Winkel.Klanten:
                    foreach (var klant in klanten)
                    {
                        klant.toon();
                    }
                    break;

                case Winkel.Medewerker:
                    foreach (var werker in medewerker)
                    {
                        werker.toon();
                    }
                    break;

                case Winkel.Verkoop:
                    foreach (var verkocht in verkoop)
                    {
                        verkocht.toon();
                    }
                    break;

                default:
                    foreach (var klant in klanten)
                    {
                        klant.toon();
                    }
                    foreach (var werker in medewerker)
                    {
                        werker.toon();
                    }
                    foreach (var verkocht in verkoop)
                    {
                        verkocht.toon();
                    }

                    break;
            }
        }

        private static void check(ref bool doorgaan)
        {
            Console.WriteLine("Wilt u doorgaan J/N?");

            if (Console.ReadLine() == "n")
            {
                doorgaan = false;
            }
        }

    }

}

