using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Engine.Models
{
    [Serializable, XmlInclude(typeof(PunishingBlow)), XmlInclude(typeof(Weakening)), XmlInclude(typeof(SpeedBoost)),
     XmlInclude(typeof(Curse)), XmlInclude(typeof(Resurrection))]
    public class ActiveAbl
    {
        public double Defence { get;}
        public double Attack { get; }
        public double Initiative { get; }
        public double Damage { get; }
        public double Hp { get; }
        public double Heal { get; }
        public bool Friendly { get; }
        public bool Aoe { get; }
        public string Name { get; }
        public bool WasNotUsed { get; set; }
        public ActiveAbl(double def, double atk, double ini, double dmg, double hp, double heal, 
                         bool friend, bool aoe, string name)
        {
            Defence = def;
            Attack = atk;
            Initiative = ini;
            Damage = dmg;
            Hp = hp;
            Heal = heal;
            Friendly = friend;
            Aoe = aoe;
            Name = name;
            WasNotUsed = true;
        }
        public ActiveAbl()
        {
            WasNotUsed = false;
        }
    }
}
