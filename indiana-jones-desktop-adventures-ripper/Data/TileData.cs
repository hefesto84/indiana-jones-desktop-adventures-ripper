using System;
using System.IO;
using indiana_jones_desktop_adventures_ripper.Data.Base;
using indiana_jones_desktop_adventures_ripper.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using Color = System.Drawing.Color;

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
            var ms = new MemoryStream(section.Data);
            var br = new BinaryReader(ms);

            var nTiles = 0;

            if (Directory.Exists("Tiles")) Directory.Delete("Tiles", true);

            Directory.CreateDirectory("Tiles");

            while (ms.Position != section.Data.Length)
            {
                var flags = br.ReadUInt32();
                var tileData = br.ReadBytes((int) 1024);

                var img = CreateBitmap(tileData);

                var a = img.PixelType.AlphaRepresentation;
                img.Save($"Tiles/tile_{nTiles}.bmp");
                
                nTiles++;
                
                Console.WriteLine($"$\\___TILE_{nTiles} ");
            }
        }

        private Image CreateBitmap(byte[] data)
        {
            var ms = new MemoryStream(data);
            var br = new BinaryReader(ms);

            var img = new Image<Rgba32>(32, 32);
            
            for (var j = 0; j < 1024; j++)
            {
                var pixelData = br.ReadByte();
                var x = (int) pixelData;

                var pixelColor = pixelData == 0 ? Color.Transparent : _palette.GetColor(x);
             
                var color = img[j % 32, j / 32];

                color.A = pixelColor.A;
                color.B = pixelColor.B;
                color.G = pixelColor.G;
                color.R = pixelColor.R;

                img[j % 32, j / 32] = color;
            }

            return img;
        }
    }
}