using System;
using indiana_jones_desktop_adventures_ripper.Data.Base;
using indiana_jones_desktop_adventures_ripper.Models;

namespace indiana_jones_desktop_adventures_ripper.Data
{
    public class TileData : IData
    {
        public static string Tag => "TILE";
        private readonly Palette _palette;
        
        public TileData(Palette palette)
        {
            _palette = palette;
        }
        
        public void Parse(Section section)
        {
            Console.WriteLine($"Parsing: {Tag} with palette.");
        }
    }
}