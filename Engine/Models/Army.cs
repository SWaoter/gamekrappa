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
        public int _size { get; private set; }
        public Army(params UnitsStack[] param)
        {
            if (param.Length > 6)
                throw new Exception("Failed to create army with more than 6 units stacks");
            _army = new UnitsStack[6];
            for (int i = 0; i < param.Length; i++)
            {
                _army[i] = new UnitsStack(param[i]);
            }
            _size = param.Length;
        }
        public Army()
        {
            _army = new UnitsStack[6];
            _size = 0;
        }
        public UnitsStack[] GetArmy()
        {
            if (_size == 0)
                throw new Exception("Army is defeated");
            UnitsStack[] ret = new UnitsStack[_size];
            for (int i = 0; i < _size; i++)
            {
                ret[i] = new UnitsStack(_army[i]);
            }
            return ret;
        }
        public void UpdateArmy(Army other)
        {
            if (other._size > 6)
            {
                _size = 6;
            }
            else
            {
                _size = other._size;
            }
            UnitsStack[] _tmp = other.GetArmy();
            for (int i = 0; i < _size; i++)
            {
                _army[i] = new UnitsStack(_tmp[i]);
            }
        }
        public void UpdateArmy(int i = 0)
        {
            _size = 0;
        }
        public void AddStack(UnitsStack other)
        {
            if (_size > 5)
                throw new Exception("Failed to add Stack to army");
            _army[_size] = new UnitsStack(other);
            _size++;
        }
        public void PrintArmy()
        {
            if (_size == 0)
                Console.WriteLine("Army is defeated");
            for (int i = 0; i < _size; i++)
            {
                Console.WriteLine($"{i + 1} : {_army[i].Type.Type}  {_army[i].Count}");
            }
        }
    }
}
