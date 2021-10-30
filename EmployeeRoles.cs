using System;

namespace Vlaaieboer
{
    internal class EmployeeRoles
    {
        private static int[,] fieldProperties = { { 0, 3, 1 },
                                                  { 1, 45, 1 } };


        private static String[] fieldNames = { "Code: ",                                      //0
                                               "Description: " };

        public int RecordCounter { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}