using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class UnitsStack
    {
        public Unit Type { get; private set; }
        public int Count { get; private set; }
        public UnitsStack(Unit Type, int Count)
        {
            this.Type = new Unit(Type);
            this.Count = Count;
        }
        public UnitsStack(UnitsStack other)
        {
            Type = new Unit(other.Type);
            Count = other.Count;
        }
        public UnitsStack() { }
    }
}
