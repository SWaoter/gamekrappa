using System;

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
        public BattleUnitsStack this[int i]
        {
            get
            {
                return _army[i];
            }
        }
    }
}
