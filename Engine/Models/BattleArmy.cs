using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class BattleArmy
    {
        private BattleUnitsStack[] _army;
        public int _size { get; private set; }
        public bool _is_defeated { get; private set; }
        public BattleArmy(Army pattern)
        {
            _is_defeated = false;
            _army = new BattleUnitsStack[6];
            _size = pattern._size;
            UnitsStack[] tmp = pattern.GetArmy();
            for (int i = 0; i < _size; i++)
            {
                _army[i] = new BattleUnitsStack(tmp[i]);
            }
            for (int i = _size; i < 6; i++)
            {
                _army[i] = new BattleUnitsStack();
            }
        }
        public void AddStack(UnitsStack other)
        {
            if (_size > 5)
                throw new Exception("Failed to add Stack to army");
            _army[_size] = new BattleUnitsStack(other);
            _size++;
        }
        public void GetArmy()
        {
            for (int i = 0; i < _size; i++)
            {
                Console.WriteLine($"{i + 1} : {_army[i].Type.Type}  {_army[i].CurrentUnits}");
            }
        }
        public void TakeAttack(BattleUnitsStack attacker, int num)
        {
            _army[num].TakeAttack(attacker);
        }
        public void Heal(int heal, int num)
        {
            _army[num].Heal(heal);
        }
        public BattleUnitsStack this[int i]
        {
            get
            {
                return _army[i];
            }
        }
    }
}
