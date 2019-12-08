using System;
using System.Xml.Serialization;

namespace Engine.Models
{
    [Serializable]
    [XmlInclude(typeof(AArbalester))]
    [XmlInclude(typeof(AUndead))]
    [XmlInclude(typeof(ANoResistance))]
    [XmlInclude(typeof(AHydra))]
    [XmlInclude(typeof(AShooter))]
    [XmlInclude(typeof(AReflection))]
    [XmlInclude(type: typeof(ALich))]
    public class PassiveAbl
    {
        public string Name { get; }
        public bool Shooter { get; }
        public bool AccurateShot { get; }
        public bool Undead { get; }
        public bool NoResistance { get; }
        public bool GreatCleave { get; }
        public bool Reflection { get; }

        public PassiveAbl(string name, bool shooter, bool accurateShot, bool undead, bool noResistance,
            bool greatCleave, bool reflection)
        {
            Name = name;
            Shooter = shooter;
            AccurateShot = accurateShot;
            Undead = undead;
            NoResistance = noResistance;
            GreatCleave = greatCleave;
            Reflection = reflection;
        }

        public PassiveAbl()
        {
            Name = "none";
            Shooter = false;
            AccurateShot = false;
            Undead = false;
            NoResistance = false;
            GreatCleave = false;
            Reflection = false;
        }
    }
}
