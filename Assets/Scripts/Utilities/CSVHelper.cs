using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Runnex.Utilities
{
    public static class CSVHelper
    {
        public static string ImportToJson(string dataCSVPath)
        {
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(dataCSVPath);

            foreach (string line in lines)
            {
                csv.Add(line.Split(','));
            }

            var properties = lines[0].Split(',');
            var listObjResult = new List<Dictionary<string, string>>();

            for (int i = 1; i < lines.Length; i++)
            {
                var objResult = new Dictionary<string, string>();
                for (int j = 0; j < properties.Length; j++)
                {
                    objResult.Add(properties[j], csv[i][j]);
                }

                listObjResult.Add(objResult);
            }

            return JsonConvert.SerializeObject(listObjResult);
        }
    }
}