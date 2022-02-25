using System;
using System.IO;

namespace indiana_jones_desktop_adventures_ripper.Models
{
    public class Section
    {
        public string Tag { get; }
        public byte[] Data { get; }
        
        public Section(string tag, byte[] data)
        {
            Tag = tag;
            Data = data;
            
            Console.WriteLine($"SECTION: {Tag} DATA SIZE: {Data.Length}");
        }
    }
}