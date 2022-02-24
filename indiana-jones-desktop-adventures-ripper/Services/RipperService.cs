using System;
using System.IO;

namespace indiana_jones_desktop_adventures_ripper.Services
{
    public class RipperService
    {
        private readonly FileStream _dataBinaryFileStream;
        private readonly FileStream _execBinaryFileStream;
        
        public RipperService(FileStream dataBinaryFileStream , FileStream execBinaryFileStream)
        {
            _dataBinaryFileStream = dataBinaryFileStream;
            _execBinaryFileStream = execBinaryFileStream;
        }

        public void Rip()
        {
            if (_dataBinaryFileStream == null || _execBinaryFileStream == null)
                throw new Exception("Error reading data.");
        }
    }
}