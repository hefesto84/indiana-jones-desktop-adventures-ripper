using System.IO;

namespace indiana_jones_desktop_adventures_ripper.Models
{
    public class Executable : FileStreamModel
    {
        public Executable(BinaryReader execBinaryFileStream) : base(execBinaryFileStream)
        {
            
        }
    }
}