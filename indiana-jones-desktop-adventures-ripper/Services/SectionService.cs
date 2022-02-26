using System;
using System.Collections.Generic;
using System.IO;
using indiana_jones_desktop_adventures_ripper.Data;
using indiana_jones_desktop_adventures_ripper.Data.Base;
using indiana_jones_desktop_adventures_ripper.Models;

namespace indiana_jones_desktop_adventures_ripper.Services
{
    public class SectionService
    {
        public bool IsEndOfFile { get; private set; }
        
        private const string EndOfFile = "ENDF";
        private Dictionary<string, IData> _dataContents;

        public void SetDependencies(Palette palette)
        {
            _dataContents = new Dictionary<string, IData>();
            
            _dataContents.Add(StupData.Tag, new StupData());
            //_dataContents.Add(SndsData.Tag, new SndsData());
            _dataContents.Add(ZoneData.Tag, new ZoneData());
            _dataContents.Add(TileData.Tag, new TileData(palette));
            
        }
        
        public void GetSection(BinaryReader binaryReader)
        {
            var tag = new string(binaryReader.ReadChars(4));
            var sectionSize = binaryReader.ReadUInt32();
            var data = binaryReader.ReadBytes((int) sectionSize);

            if (_dataContents.ContainsKey(tag)) _dataContents[tag].Parse(new Section(tag, data));
            
            IsEndOfFile = tag.Equals(EndOfFile);
        }
    }
}