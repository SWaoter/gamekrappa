using System;
using System.Xml.Serialization;

namespace Engine.Models
{
    [Serializable, XmlInclude(typeof(Angel)), XmlInclude(typeof(Arbalester)), XmlInclude(typeof(Skeleton)),
     XmlInclude(typeof(Fury)), XmlInclude(typeof(Hydra)), XmlInclude(typeof(Devil)), XmlInclude(typeof(BoneDragon)), XmlInclude(typeof(Cyclops)),
     XmlInclude(typeof(Griffin)), XmlInclude(typeof(Shaman)), XmlInclude(typeof(Lich))]
    public class Unit
    {
        public string Type { get; set; }
        public PassiveAbl Ability { get; set; }
        public ActiveAbl ActiveAbl { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public string ImageName { get; set; }
        private Random _damage = new Random();
        public double Initiative { get; set; }
        public Unit() { }
        public Unit(string type, PassiveAbl ability, ActiveAbl abl, int health, int attack,
                    int defense, int minDamage,
                    int maxDamage, double initiative,
                    string imageName)
        {
            this.ActiveAbl = abl;
            this.Ability = ability;
            this.Type = type;
            this.Attack = attack;
            this.Health = health;
            this.Defense = defense;
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.Initiative = initiative;
            this.ImageName = imageName;
        }
        public Unit(Unit other)
        {
            ActiveAbl = other.ActiveAbl;
            Ability = other.Ability;
            Type = other.Type;
            Health = other.Health;
            Attack = other.Attack;
            Defense = other.Defense;
            MinDamage = other.MinDamage;
            MaxDamage = other.MaxDamage;
            Initiative = other.Initiative;
            ImageName = other.ImageName;
        }
        public int Damage()
        {
            return _damage.Next(MinDamage, MaxDamage);
        }
    }

    public class Pair<T, K>
    {
        public T First { get; set; }
        public K Second { get; set; }
    }
}
