using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Army
    {
        private UnitsStack[] _army;
        public int Size { get; private set; }
        public Army(params UnitsStack[] param)
        {
            if (param.Length > 6)
                throw new Exception("Failed to create army with more than 6 units stacks");
            _army = new UnitsStack[6];
            for (int i = 0; i < param.Length; i++)
            {
                _army[i] = new UnitsStack(param[i]);
            }
            Size = param.Length;
        }
        public Army()
        {
            _army = new UnitsStack[6];
            Size = 0;
        }
        public UnitsStack[] GetArmy()
        {
            if (Size == 0)
                throw new Exception("Army is defeated");
            UnitsStack[] ret = new UnitsStack[Size];
            for (int i = 0; i < Size; i++)
            {
                ret[i] = new UnitsStack(_army[i]);
            }
            return ret;
        }
        public void UpdateArmy(Army other)
        {
            if (other.Size > 6)
            {
                Size = 6;
            }
            else
            {
                Size = other.Size;
            }
            UnitsStack[] tmp = other.GetArmy();
            for (int i = 0; i < Size; i++)
            {
                _army[i] = new UnitsStack(tmp[i]);
            }
        }
        public void UpdateArmy(int i = 0)
        {
            Size = 0;
        }
        public void AddStack(UnitsStack other)
        {
            if (Size > 5)
                throw new Exception("Failed to add Stack to army");
            _army[Size] = new UnitsStack(other);
            Size++;
        }
        public void PrintArmy()
        {
            if (Size == 0)
                Console.WriteLine("Army is defeated");
            for (int i = 0; i < Size; i++)
            {
                Console.WriteLine($"{i + 1} : {_army[i].Type.Type}  {_army[i].Count}");
            }
        }
    }
}
