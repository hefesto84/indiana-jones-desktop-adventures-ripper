using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using indiana_jones_desktop_adventures_ripper.Data.Base;
using indiana_jones_desktop_adventures_ripper.Models;

namespace indiana_jones_desktop_adventures_ripper.Data
{
    public class ZoneData : IData
    {
        public static string Tag => "ZONE";
        
        public void Parse(Section section)
        {
            var ms = new MemoryStream(section.Data);
            var br = new BinaryReader(ms);

            var a = br.ReadBytes(2);

            var izon = br.ReadChars(4);

            var siz = new string(izon);

            var length = br.ReadInt32() - 8;
            
            var lengthStart = ms.Position;

            //var ss = br.ReadBytes(4);
            var width = br.ReadInt16();
            var height = br.ReadInt16();
            var type = br.ReadInt16();

            br.ReadInt16(); // SKIP

            var mapLength = width * height * 3;
            var mapData = new short[width * height*3];
            var tiles = new short[width * height];
            
            for (var i = 0; i < mapData.Length; i++)
            {
                mapData[i] = br.ReadInt16();
            }

            var k = 0;
            var x = 0;

            var s = new StringBuilder();
            
            while (k != mapData.Length)
            {
               
                tiles[x] = mapData[k];
                s.Append($"{tiles[x]};");
                //s+=(tiles[x]);
                k += 3;
                x++;
                
            }

            
            
            File.WriteAllText("map.txt",s.ToString());
   
            Console.WriteLine("S: "+siz);
        }
    }
}