using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace SharonFragrances.API
{
    public class Stack : IStack
    {
        public List<Bin> Bins {get;set;}

        public Stack()
        {
            Bins = new List<Bin>();
            for (var i = 0; i<100; i++)
            { 
                Bins.Add(new Bin());
            }
        }
    }
}