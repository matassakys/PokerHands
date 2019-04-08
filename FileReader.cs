using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PokerHands
{
    public class FileReader
    {
        public static List<String> GetAllLines()
        {
            string filePath = (@"C:\Users\Matas\source\repos\PokerHands\pokerHands.txt");
            var file = new System.IO.StreamReader(filePath);
            string line;
            List<string> lines = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;
        }
    }
}
