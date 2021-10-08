using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vlaaieboer
{
    class Employee
    {
        /*

       {get; set;} is shorthand for:

       private string name;                // this is the field
       public String Name                  // this is the property (which is why it has a Capital, it's not a variable
       {
           get
           {
               return this.name;     
           }
           set
           {
               this.name = value;       
           }
       }


       */
        public int RecordCounter { get; set; }
        public string EmployeeID { get; set; }
        // public string JobTitle { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Prefix { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

    }
}
