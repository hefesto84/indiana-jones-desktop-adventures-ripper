using System.IO;
using Color = System.Drawing.Color;

namespace indiana_jones_desktop_adventures_ripper.Models
{
    public class Palette : FileStreamModel
    {
        private const int OffsetPalette = 0x36656;
        private const int PaletteSize = 1024;
        private const int PaletteColors = 256;

        private readonly int[] _colors = new int[PaletteColors];
        
        public Palette(BinaryReader execBinaryFileStream) : base(execBinaryFileStream) { }

        public void Extract()
        {
            Fs.BaseStream.Position = OffsetPalette;
            
            var data = Fs.ReadBytes(PaletteSize);

            var ms = new MemoryStream(data);
            var br = new BinaryReader(ms);

            for (var i = 0; i < PaletteColors; i++)
            {
                var entry = br.ReadInt32();
                _colors[i] = entry;
            }
            
            br.Close();
            ms.Close();
        }

        private Color GetColor(int index)
        {
            return Color.FromArgb(_colors[index]);
        }
    }
}