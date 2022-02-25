using System;
using System.Collections.Generic;
using System.IO;
using indiana_jones_desktop_adventures_ripper.Data.Base;
using indiana_jones_desktop_adventures_ripper.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Color = System.Drawing.Color;

namespace indiana_jones_desktop_adventures_ripper.Data
{
    public class TileData : IData
    {
        public static string Tag => "TILE";
        
        private readonly Palette _palette;
        private Dictionary<string, Image> _tiles;
        
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

            _tiles = new Dictionary<string, Image>();
            
            while (ms.Position != section.Data.Length)
            {
                var flags = br.ReadUInt32();
                var tileData = br.ReadBytes((int) 1024);

                var img = CreateBitmap(tileData);
                
                _tiles.Add($"Tiles/tile_{nTiles}.png", img);
                
                nTiles++;
            }
            
            SaveSpritesheet();
        }
        
        private Image CreateBitmap(byte[] data)
        {
            var ms = new MemoryStream(data);
            var br = new BinaryReader(ms);

            var img = new Image<Rgba32>(32, 32);
            
            for (var j = 0; j < 1024; j++)
            {
                var pixelData = br.ReadByte();
                var pd = (int) pixelData;

                var pixelColor = _palette.GetColor(pd);

                var colorRgba32 = new Rgba32(pixelColor.R, pixelColor.G, pixelColor.B, 255);
    
                var x = j % 32;
                var y = j / 32;
                
                if (pixelData == 0)
                {
                    colorRgba32.B = 255;
                    colorRgba32.G = 255;
                    colorRgba32.R = 255;
                    colorRgba32.A = 1;
                }
                
                img[x,y] = colorRgba32;
            }

            return img;
        }

        private void SaveSpritesheet()
        {
            foreach (var (key, value) in _tiles)
            {
                value.SaveAsPng(key, new PngEncoder());
                Console.WriteLine($"Adding {key} to spritesheet");
            }
        }
    }
}