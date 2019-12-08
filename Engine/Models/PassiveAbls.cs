namespace Engine.Models
{
    public class AArbalester : PassiveAbl
    {
        public AArbalester() : base("Arbalester", true, true, false, false, false, false) { }
    }
    public class AUndead : PassiveAbl
    {
        public AUndead() : base("Undead", true, false, true, false, false, false) { }
    }
    public class ANoResistance : PassiveAbl
    {
        public ANoResistance() : base("Fury", false, false, false, true, false, false) { }
    }
    public class AHydra : PassiveAbl
    {
        public AHydra() : base("Hydra", false, false, false, true, true, false) { }
    }
    public class AShooter : PassiveAbl
    {
        public AShooter() : base("Shooter", true, false, false, false, false, false) { }
    }
    public class AReflection : PassiveAbl
    {
        public AReflection() : base("Reflection", false, false, false, false, false, true) { }
    }
    public class ALich : PassiveAbl
    {
        public ALich() : base("Lich ", true, false, true, false, false, false) { }
    }

}
