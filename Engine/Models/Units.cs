namespace Engine.Models
{
    public class Arbalester : Unit
    {
        public Arbalester() : base("Arbalester", new AArbalester(), new ActiveAbl(), 10, 4, 4, 2, 8, 8,
                                   "/Engine;component/Images/Arbalester.png") { } 
    }
    public class Skeleton : Unit
    {
        public Skeleton() : base("Skeleton", new AUndead(), new ActiveAbl(), 5, 1, 2, 1, 1, 10,
                                 "/Engine;component/Images/Skeleton.jpg") { } 
    }
    public class Fury : Unit
    {
        public Fury() : base("Fury", new ANoResistance(), new ActiveAbl(), 16, 5, 3, 5, 7, 16,
                             "/Engine;component/Images/Fury.png") { }
    }
    public class Hydra : Unit
    {
        public Hydra() : base("Hydra", new AHydra(), new ActiveAbl(), 80, 15, 12, 7, 14, 7,
                              "/Engine;component/Images/Hydra.png") { } 
    }
    public class Angel : Unit
    {
        public Angel() : base("Angel", new PassiveAbl(), new PunishingBlow(), 180, 27, 27, 45, 45, 11,
                              "/Engine;component/Images/Angel.png") { }
    }
    public class BoneDragon : Unit
    {
        public BoneDragon() : base("BoneDragon", new AUndead(), new Curse(), 150, 27, 28, 15, 30, 11,
                                   "/Engine;component/Images/BoneDragon.jpg") { } 
    }
    public class Devil : Unit
    {
        public Devil() : base("Devil", new PassiveAbl(), new Weakening(), 166, 27, 25, 36, 66, 11,
                              "/Engine;component/Images/Devil.png") { } 
    }
    public class Cyclops : Unit
    {
        public Cyclops() : base("Cyclops", new AShooter(), new ActiveAbl(), 85, 20, 15, 18, 26, 10,
                                "/Engine;component/Images/Cyclopus.png") { }
    }
    public class Griffin : Unit
    {
        public Griffin() : base("Griffin", new AReflection(), new ActiveAbl(), 30, 7, 5, 5, 10, 15,
                                "/Engine;component/Images/Griffin.png") { } 
    }
    public class Shaman : Unit
    {
        public Shaman() : base("Shaman", new PassiveAbl(), new SpeedBoost(), 40, 12, 10, 7, 12, 10.5,
                               "/Engine;component/Images/Shaman.png") { } 
    }
    public class Lich : Unit
    {
        public Lich() : base("Lich", new ALich(), new Resurrection(), 50, 15, 15, 12, 17, 10,
                             "/Engine;component/Images/Lich.png") { } 
    }
}
