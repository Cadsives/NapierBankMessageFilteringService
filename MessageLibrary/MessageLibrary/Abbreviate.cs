using System;
using System.IO;
using System.Linq;

namespace MessageLibrary
{
    public class Abbreviate : Abbreviations
    {
        // this method used to search a message to see if it contains textspeak. For example, LOL becomes <Laughing out Loud>
        public string abbreviate(string message)
        {
            // Stores the path to the textwords csv document
            string path = Path.Combine(Environment.CurrentDirectory, @"textwords.csv");
            // Returns any textspeak found and expands them to the relevant word(s) 
            return File.ReadAllLines(Path.Combine(path)).Select(line => line.Split(',')).Aggregate(message, (a, x) => a.Replace(x[0], String.Format("{0}<{1}>", x[0], x[1])));
        }
    }
}
