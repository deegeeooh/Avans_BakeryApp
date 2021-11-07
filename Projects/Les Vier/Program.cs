using System;
using System.Collections.Generic;

namespace Les_Vier_Reza
{
    class LesVier
    {
        private enum Show
        {
            Alledocenten,
            Allestudenten,
            Docent,
            Student
        }

        private class Program
        {
            private static List<Student> students = new List<Student>();
            private static List<Docent> docenten = new List<Docent>();

            private static void Main(string[] args)
            {
                Console.WriteLine("Hello World!");
                var doorgaan = true;
                Console.WriteLine("invoeren van docent-gegevens begint");

                while (doorgaan)
                {
                    var docent = new Docent();

                    docenten.Add(docent);

                    Check(ref doorgaan);
                }

                doorgaan = true;
                Console.WriteLine("invoeren van student-gegevens begint");
                while (doorgaan)
                {
                    var st = new Student();

                    students.Add(st);

                    Check(ref doorgaan);
                }

                ShowWhatClientNeeds();
            }

            private static void ShowWhatClientNeeds()
            {
                Console.WriteLine("0 voor alle docenten.\n 1 voor alle studenten.\n 2 voor 1 docent\n 3 voor 1 student");

                Show kies = (Show)int.Parse(Console.ReadLine());

                switch (kies)
                {
                    case Show.Alledocenten:
                        foreach (var docent in docenten)
                        {
                            docent.display();
                        }

                        break;

                    case Show.Allestudenten:
                        foreach (var student in students)
                        {
                            student.display();
                        }
                        break;

                    case Show.Docent:
                        Console.WriteLine("geef aub naam van de docent op: ");
                        var naamDocent = Console.ReadLine();
                        Docent foundDocent = docenten.Find(docent => docent.Name == naamDocent);
                        foundDocent.display();
                        break;

                    case Show.Student:
                        Console.WriteLine("geef aub naam van de student op: ");
                        var naamStudent = Console.ReadLine();
                        Student foundStudent = students.Find(student => student.Name == naamStudent);
                        foundStudent.display();
                        break;

                    default:
                        foreach (var docent in docenten)
                        {
                            docent.display();
                        }
                        foreach (var student in students)
                        {
                            student.display();
                        }

                        break;
                }
            }

            private static void Check(ref bool doorgaan)
            {
                Console.WriteLine("Wilt u doorgaan J/N?");

                if (Console.ReadLine() == "n")
                {
                    doorgaan = false;
                }
            }
        }
    }

    internal class Docent : Persoon
    {
        public static int aantal = 0;
        public string vakgebied;

        public Docent()
        {
            Console.WriteLine("geef aub vakgebied van een docent op:");
            vakgebied = Console.ReadLine();
            Docent.aantal++;
        }

        public void display()
        {
            Console.WriteLine("naam, adres, telnr, cijfer en aantal docenten: {0} {1} {2} {3} {4}", Name, Adres, Phone, vakgebied, Docent.aantal);
        }
    }

    internal class Student : Persoon
    {
        public static int aantal = 0;

        public int Cijfer;

        public Student()
        {
            Console.WriteLine("geef aub cijfer van een student op:");
            Cijfer = int.Parse(Console.ReadLine());
            Student.aantal++;
        }

        public void display()
        {
            Console.WriteLine("naam, adres, telnr, cijfer en aantal studenten: {0} {1} {2} {3} {4}", Name, Adres, Phone, Cijfer, Student.aantal);
        }
    }

    public class Persoon

    {
        public string Name { get; set; }
        public string Adres { get; set; }
        public int Phone { get; set; }
        public string GebDatum { get; set; }

        public Persoon()

        {
            Console.WriteLine("geef aub naam van een persoon op:");

            Name = Console.ReadLine();

            Console.WriteLine("geef aub telnr van een persoon op:");

            Phone = int.Parse(Console.ReadLine());

            Console.WriteLine("geef aub adres van een persoon op:");

            Adres = Console.ReadLine();
        }
    }
}