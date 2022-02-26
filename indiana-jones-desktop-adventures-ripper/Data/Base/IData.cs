using indiana_jones_desktop_adventures_ripper.Models;

namespace indiana_jones_desktop_adventures_ripper.Data.Base
{
    public interface IData
    {
        void Parse(Section section);
        static string Tag { get; }
    }
}