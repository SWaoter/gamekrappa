using System;

namespace Engine.Models
{
    public enum UnitsType
    {
        Arbalester,
        Skeleton,
        Fury,
        Hydra,
        Angel,
        BoneDragon,
        Devil,
        Cyclops,
        Griffin,
        Shaman,
        Lich
    }
    [Flags]
    public enum Ability
    {
        None = 0x000,
        Shooter = 0x001, //
        AccurateShot = 0x002, //
        Undead = 0x0004, 
        NoResistance = 0x008, //
        GreatCleave = 0x010, //
        Reflection = 0x100, //
    }
    public class Unit
    {
        public UnitsType Type { get; private set; }
        public Ability Ability { get; private set; }
        public ActiveAbl ActiveAbl { get; private set; }
        public int Health { get; private set; }
        public int Attack { get; private set; }
        public int Defence { get; private set; }
        public int Min_Damage { get; private set; }
        public int Max_Damage { get; private set; }
        public string ImageName { get; }
        private Random _damage = new Random();
        public double Initiative { get; private set; }
        public Unit() { }
        public Unit(UnitsType Type, Ability Ability, ActiveAbl Abl,int Health, int Attack,
                    int Defence, int Min_Damage,
                    int Max_Damage, double Initiative,
                    string ImageName)
        {
            this.ActiveAbl = Abl;
            this.Ability = Ability;
            this.Type = Type;
            this.Attack = Attack;
            this.Health = Health;
            this.Defence = Defence;
            this.Min_Damage = Min_Damage;
            this.Max_Damage = Max_Damage;
            this.Initiative = Initiative;
            this.ImageName = ImageName;
        }
        public Unit(Unit other)
        {
            ActiveAbl = other.ActiveAbl;
            Ability = other.Ability;
            Type = other.Type;
            Health = other.Health;
            Attack = other.Attack;
            Defence = other.Defence;
            Min_Damage = other.Min_Damage;
            Max_Damage = other.Max_Damage;
            Initiative = other.Initiative;
            ImageName = other.ImageName;
        }
        public bool HaveAbility(string pattern)
        {
            return ((int)Ability & Help.AbilityToInt(pattern)) > 0;
        }
        public int Damage()
        {
            return _damage.Next(Min_Damage, Max_Damage);
        }
    }
    public class Help
    {
        public static int AbilityToInt(string _ability)
        {
            switch (_ability)
            {
                case "Shooter":
                    return 1;
                case "AccurateShot":
                    return 2;
                case "Undead":
                    return 4;
                case "NoResistance":
                    return 8;
                case "GreatCleave":
                    return 16;
                case "PunishingBlow":
                    return 32;
                case "Curse":
                    return 64;
                case "Weakening":
                    return 128;
                case "Reflection":
                    return 256;
                case "SpeedBoost":
                    return 512;
                case "Resurrection":
                    return 1024;
                default:
                    return 0;
            }
        }

    }
    public class Pair<T, K>
    {
        public T First { get; set; }
        public K Second { get; set; }
    }
}
