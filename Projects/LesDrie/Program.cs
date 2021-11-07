using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesDrie
{
    enum Show
    {
        Alledocenten, 
        Allestudenen,
        Docent,
        Student
    }
    class Program
    {
        static List<Student> students= new List<Student>();
        static List<Teacher> teachers = new List<Teacher>();
        static List<Person> people= new List<Person>();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello again");
            

            Console.WriteLine("invoer Docent begint");
            var doorgaan = true;
            while (doorgaan)
            {
                var teacher = new Teacher();
                
                teachers.Add(teacher);

                Check(ref doorgaan);


                //Console.Write("Meer invoeren? ");

                //if (Console.ReadLine() == "n")
                //{
                //    doorgaan = false;
                //}

            }

            Console.WriteLine("invoer Student begint");
            doorgaan = true;
            while (doorgaan)
            {
                var student = new Student();

                students.Add(student);

                Check(ref doorgaan);


                //Console.Write("Meer invoeren? ");

                //if (Console.ReadLine() == "n")
                //{
                //    doorgaan = false;
                //}

            }

            var a = Console.ReadKey(true);

            

            ShowWhatClientNeeds();
            
        }
        static void Check(ref bool doorgaan)
        {
            Console.WriteLine("Continue Y/N?");

            if (Console.ReadLine() == "n")
            {
                doorgaan = false;
            }


        }

        static void ShowWhatClientNeeds()
        {
            Console.WriteLine("0: alle docenten \n 1: alle studenten \n 2: 1 student\n 3: 1 docent");

            Show kies = (Show)int.Parse(Console.ReadLine());

            switch (kies)
            {
                case Show.Alledocenten:
                    foreach (var state in teachers)
                    {
                        Console.WriteLine("Naam, adres, geboortedatum, telnummer {0} {1} {2} {3} {4}", state.Name, state.Address, state.DateOfBirth, state.Phone, Teacher.aantalRecords);
                    }
                    break;
                case Show.Allestudenen:
                    foreach (var state in students)
                    {
                        Console.WriteLine("Naam, adres, geboortedatum, telnummer en cijfer {0} {1} {2} {3} {4} {5}", state.Name, state.Address, state.DateOfBirth, state.Phone, state.Grade, Student.aantalRecords);
                    }
                    break;
                case Show.Docent:
                    Console.WriteLine("Geef naam Docent");
                    var naamDocent = Console.ReadLine();
                    Teacher foundTeacher = teachers.Find(Teacher => Teacher.Name == naamDocent);
                    Console.WriteLine("Naam, adres, geboortedatum, telnummer {0} {1} {2} {3}", foundTeacher.Name, foundTeacher.Address, foundTeacher.DateOfBirth, foundTeacher.Phone);

                    break;
                case Show.Student:
                    Console.WriteLine("Geef naam Student");
                    var naamStudent = Console.ReadLine();
                    Student foundStudent = students.Find(Student => Student.Name == naamStudent);
                    Console.WriteLine("Naam, adres, geboortedatum, telnummer {0} {1} {2} {3}", foundStudent.Name, foundStudent.Address, foundStudent.DateOfBirth, foundStudent.Phone);

                    break;
                default:
                    break;
            }

        }



    }
}
