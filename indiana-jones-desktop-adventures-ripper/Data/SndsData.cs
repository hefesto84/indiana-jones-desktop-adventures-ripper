using System;
using System.IO;
using indiana_jones_desktop_adventures_ripper.Data.Base;
using indiana_jones_desktop_adventures_ripper.Models;

namespace indiana_jones_desktop_adventures_ripper.Data
{
    public class SndsData : IData
    {
        public static string Tag => "SNDS";
        
        public void Parse(Section section)
        {
            var ms = new MemoryStream(section.Data);
            var br = new BinaryReader(ms);

            var d = br.ReadUInt16();

            while (ms.Position != section.Data.Length)
            {
                var path = new string(br.ReadChars(br.ReadUInt16()));
                Console.WriteLine($"\\__SNDS: {path}");
            }
        }
    }
}