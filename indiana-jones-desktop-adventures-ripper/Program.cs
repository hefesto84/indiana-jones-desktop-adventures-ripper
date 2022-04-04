using System;
using System.IO;
using indiana_jones_desktop_adventures_ripper.Services;

namespace indiana_jones_desktop_adventures_ripper
{
    class Program
    {
        static void Main(string[] args)
        {
            var areFilesValid = true;

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File not found, check the path for DAW file: {args[0]}"); areFilesValid = false;
            }

            if (!File.Exists(args[1]))
            {
                Console.WriteLine($"File not found, check the path for EXE file: {args[1]}");
                areFilesValid = false;
            }

            if (!areFilesValid) return;

            var sectionService = new SectionService();

            var ripperService = new RipperService(
                new BinaryReader(File.OpenRead(args[0])),
                new BinaryReader(File.OpenRead(args[1])),
                sectionService);

            ripperService.Rip();
        }
    }
}
