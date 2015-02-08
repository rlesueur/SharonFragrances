using System.Collections.Generic;

namespace SharonFragrances.API
{
    public interface IStack
    {
        List<Bin> Bins { get; set; }
    }

    public interface IBin
    {
    }

    public class Bin : IBin
    {
        public string ContentID;
    }
}