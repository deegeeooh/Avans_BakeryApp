using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BakeryConsole
{
    internal class JSON
    {
        public static List<T> PopulateList<T>(string aFilename) where T : class
        {
            var getaListFromJSON = DeserializeJSONfile<T>(aFilename);
            return getaListFromJSON;
        }

        private static List<T> DeserializeJSONfile<T>(string aFilename) where T : class
        {
            var getaListFromJSON = new List<T>();                             // define here so method doesn't return NULL
            if (File.Exists(aFilename))                                       // and causes object not defined error
            {                                                                 // when calling employeeList.add from main()
                try
                {
                    getaListFromJSON = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(aFilename));          // JsonConvert will call the default() constructor here
                    return getaListFromJSON;                                                                         // circumvent with  [JsonConstructor] attribute or by using arguments
                }                                                                                                    // on the constructor
                catch (Exception e)
                {
                    IO.SystemMessage($"Error parsing json file{aFilename} {e}", true);
                }
            }
            else
            {
                IO.SystemMessage($"File {aFilename} doesn't exist, creating new file ", false);
            }
            return getaListFromJSON;
        }

        public static void WriteToFile<T>(string aFilename, List<T> aListOfObjects, bool aConsoleMessage) where T : class
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(aListOfObjects, Formatting.Indented);
                File.WriteAllText(aFilename, jsonString);
                if (aConsoleMessage) IO.SystemMessage($"Writing changes to file: \"{aFilename}\"", false);
            }
            catch (Exception e)
            {
                IO.SystemMessage($"Error writing to file {aFilename} {e}", true);
            }
        }


    }
}