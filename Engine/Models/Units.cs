using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Arbalester : Unit
    {
        public Arbalester() : base(UnitsType.Arbalester, Ability.Shooter | Ability.AccurateShot, new NoneAbl(), 10, 4, 4, 2, 8, 8,
                                   "/Engine;component/Images/Arbalester.png") { } 
    }
    public class Skeleton : Unit
    {
        public Skeleton() : base(UnitsType.Skeleton, Ability.Undead, new NoneAbl(), 5, 1, 2, 1, 1, 10,
                                 "/Engine;component/Images/Skeleton.jpg") { } 
    }
    public class Fury : Unit
    {
        public Fury() : base(UnitsType.Fury, Ability.NoResistance, new NoneAbl(), 16, 5, 3, 5, 7, 16,
                             "/Engine;component/Images/Fury.png") { }
    }
    public class Hydra : Unit
    {
        public Hydra() : base(UnitsType.Hydra, Ability.GreatCleave | Ability.NoResistance, new NoneAbl(), 80, 15, 12, 7, 14, 7,
                              "/Engine;component/Images/Hydra.png") { } 
    }
    public class Angel : Unit
    {
        public Angel() : base(UnitsType.Angel, Ability.Undead, new PunishingBlow(), 180, 27, 27, 45, 45, 11,
                              "/Engine;component/Images/Angel.png") { }
    }
    public class BoneDragon : Unit
    {
        public BoneDragon() : base(UnitsType.BoneDragon, Ability.Undead, new Curse(), 150, 27, 28, 15, 30, 11,
                                   "/Engine;component/Images/BoneDragon.jpg") { } 
    }
    public class Devil : Unit
    {
        public Devil() : base(UnitsType.Devil, Ability.Undead, new Weakening(), 166, 27, 25, 36, 66, 11,
                              "/Engine;component/Images/Devil.png") { } 
    }
    public class Cyclops : Unit
    {
        public Cyclops() : base(UnitsType.Cyclops, Ability.Shooter,  new NoneAbl(), 85, 20, 15, 18, 26, 10,
                                "/Engine;component/Images/Cyclopus.png") { }
    }
    public class Griffin : Unit
    {
        public Griffin() : base(UnitsType.Griffin, Ability.Reflection, new NoneAbl(), 30, 7, 5, 5, 10, 15,
                                "/Engine;component/Images/Griffin.png") { } 
    }
    public class Shaman : Unit
    {
        public Shaman() : base(UnitsType.Shaman, Ability.Undead, new SpeedBoost(), 40, 12, 10, 7, 12, 10.5,
                               "/Engine;component/Images/Shaman.png") { } 
    }
    public class Lich : Unit
    {
        public Lich() : base(UnitsType.Lich, Ability.Undead | Ability.Shooter, new Resurrection(), 50, 15, 15, 12, 17, 10,
                             "/Engine;component/Images/Lich.png") { } 
    }
}
