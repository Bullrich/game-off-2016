using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// By @JavierBullrich
namespace BlueFlow.Util
{
    public class CSVParser
    {
        public Dictionary<string, string> SetDictionary(TextAsset csvFile)
        {

            var csv = csvFile.text;

            var csvItems = csv.Split('\n');
            var dictionary = new Dictionary<string, string>();

            foreach (var item in csvItems)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var itemParts = item.Split(new char[] { ',' }, 2);
                    itemParts[1] = itemParts[1].Replace("\"", "");
                    dictionary.Add(itemParts[0], itemParts[1]);
                }
            }
            return dictionary;
        }
    }
}