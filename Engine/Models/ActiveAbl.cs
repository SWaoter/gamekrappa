using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class ActiveAbl
    {
        public double Defence { get; private set; }
        public double Attack { get; private set; }
        public double Initiative { get; private set; }
        public double Damage { get; private set; }
        public double Hp { get; private set; }
        public double Heal { get; private set; }
        public bool Friendy { get; private set; }
        public bool Aoe { get; private set; }
        public string Name { get; private set; }
        public bool WasNotUsed { get; set; }
        public ActiveAbl(double _def, double _atk, double _ini, double _dmg, double _hp, double _heal, 
                         bool _friend, bool _aoe, string _name)
        {
            Defence = _def;
            Attack = _atk;
            Initiative = _ini;
            Damage = _dmg;
            Hp = _hp;
            Heal = _heal;
            Friendy = _friend;
            Aoe = _aoe;
            Name = _name;
            WasNotUsed = true;
        }
        public ActiveAbl()
        {
            WasNotUsed = false;
        }
    }
}
