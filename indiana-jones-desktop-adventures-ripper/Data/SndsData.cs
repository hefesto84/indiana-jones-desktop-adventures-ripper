using System;
using indiana_jones_desktop_adventures_ripper.Data.Base;
using indiana_jones_desktop_adventures_ripper.Models;

namespace indiana_jones_desktop_adventures_ripper.Data
{
    public class SndsData : IData
    {
        public static string Tag => "SNDS";
        
        public void Parse(Section section)
        {
            Console.WriteLine($"Parsing: {Tag}");
        }
    }
}