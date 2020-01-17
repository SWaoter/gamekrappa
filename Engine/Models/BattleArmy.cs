using System;
using System.Linq;

namespace Engine.Models
{
    public class BattleArmy
    {
        private BattleUnitsStack[] _army;
        public int Size { get; private set; }
        public bool IsDefeated { get; private set; }
        public BattleArmy(Army pattern)
        {
            IsDefeated = false;
            _army = new BattleUnitsStack[6];
            Size = pattern.Size;
            UnitsStack[] tmp = pattern.GetArmy();
            for (int i = 0; i < Size; i++)
            {
                _army[i] = new BattleUnitsStack(tmp[i]);
            }
            for (int i = Size; i < 6; i++)
            {
                _army[i] = new BattleUnitsStack();
            }
        }
        public void AddStack(UnitsStack other)
        {
            if (Size > 5)
                throw new Exception("Failed to add Stack to army");
            _army[Size] = new BattleUnitsStack(other);
            Size++;
        }

        public void CheckIfDefeated()
        {
            int num = 0;
            for (int i = 0; i < Size; i++)
            {
                if (!_army[i].IsAlive)
                    num++;
            }
            if (num == Size)
                IsDefeated = true;
        }
        public void TakeAttack(BattleUnitsStack attacker, int num)
        {
            _army[num].TakeAttack(attacker);
        }
        public void Heal(int heal, int num)
        {
            _army[num].Heal(heal);
        }

        public int GetCurrentAliveStacksNum()
        {
            return _army.Count(t => t.IsAlive);
        }

        public int[] GetAliveId()
        {
            var ret = new int[GetCurrentAliveStacksNum()];
            var i = 0;
            for (var index = 0; index < _army.Length; index++)
            {
                var s = _army[index];
                if (!s.IsAlive) continue;
                ret[i] = index;
                i++;
            }

            return ret;
        }
        public BattleUnitsStack this[int i] => _army[i];
    }
}
