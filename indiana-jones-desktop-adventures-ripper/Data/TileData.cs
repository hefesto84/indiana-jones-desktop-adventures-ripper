using System;
using System.Collections.Generic;
using System.IO;
using indiana_jones_desktop_adventures_ripper.Data.Base;
using indiana_jones_desktop_adventures_ripper.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace indiana_jones_desktop_adventures_ripper.Data
{
    public class TileData : IData
    {
        public static string Tag => "TILE";
        
        private readonly Palette _palette;
        private Dictionary<string, Image> _tiles;
        private const int SpriteW = 32;
        private const int SpriteH = 32;
        private const string SpritesFolder = "Tiles/";
        private const string SpritesheetFolder = "Spritesheet/";
        private const string SpriteFilename = "indiana-jones-desktop-adventures";
        
        public TileData(Palette palette)
        {
            _palette = palette;
        }

        public void Parse(Section section)
        {
            var ms = new MemoryStream(section.Data);
            var br = new BinaryReader(ms);

            var nTiles = 0;

            if (Directory.Exists(SpritesFolder)) Directory.Delete(SpritesFolder, true);
            if(Directory.Exists(SpritesheetFolder)) Directory.Delete(SpritesheetFolder, true);
            
            Directory.CreateDirectory(SpritesFolder);
            Directory.CreateDirectory(SpritesheetFolder);

            _tiles = new Dictionary<string, Image>();
            
            while (ms.Position != section.Data.Length)
            {
                var flags = br.ReadUInt32();
                var tileData = br.ReadBytes((int) 1024);

                var img = CreateBitmap(tileData);
                
                _tiles.Add($"{SpriteFilename}_{nTiles}.png", img);
                
                nTiles++;
            }
            
            SaveSpritesheet();
        }
        
        private Image CreateBitmap(byte[] data)
        {
            var ms = new MemoryStream(data);
            var br = new BinaryReader(ms);

            var img = new Image<Rgba32>(SpriteW, SpriteH);
            
            for (var j = 0; j < SpriteW*SpriteH; j++)
            {
                var pixelData = br.ReadByte();
                var pd = (int) pixelData;

                var pixelColor = _palette.GetColor(pd);

                var x = j % SpriteW;
                var y = j / SpriteH;
       
                img[x,y] = pixelData == 0 ? new Rgba32(255,255,255,0) : new Rgba32(pixelColor.R, pixelColor.G, pixelColor.B, 255);
            }

            return img;
        }

        private void SaveSpritesheet()
        {
            var sq = (int)Math.Round(Math.Sqrt(_tiles.Count));
            
            var width = sq * SpriteW;
            var height = sq * SpriteH;
            
            var spriteSheet = new Image<Rgba32>(width, height);
            
            var row = 0;
            var col = 0;
            var index = 0;
            
            foreach (var (key, value) in _tiles)
            {
                var d = value.CloneAs<Rgba32>();

                for (var x = 0; x < SpriteH; x++)
                {
                    for (var y = 0; y < SpriteW; y++)
                    {
                        spriteSheet[x+(row*SpriteH), y+(col*SpriteW)] = d[x, y];
                    }
                }
                
                index ++;

                if (index % sq == 0)
                {
                    row = 0;
                    col++;
                }
                else
                {
                    row++;
                }

                value.SaveAsPng($"{SpritesFolder}/{key}", new PngEncoder());
            }
            
            spriteSheet.SaveAsPng($"{SpritesheetFolder}/{SpriteFilename}.png",new PngEncoder());
        }
    }
}