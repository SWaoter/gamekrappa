using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class PunishingBlow : ActiveAbl
    {
        public PunishingBlow() : base(1, 1.2, 1, 1, 0, 0, true, false, "Attack Boost") { }
    }
    public class Weakening : ActiveAbl
    {
        public Weakening() : base(0.5, 1, 1, 1, 1000, 0, false, false, "Weakening") { }
    }
    public class SpeedBoost : ActiveAbl
    {
        public SpeedBoost() : base(1, 1, 1.4, 1, 0, 0, true, false, "Speed Boost") { }
    }
    public class Curse : ActiveAbl
    {
        public Curse() : base(1, 0.5, 1, 0.5, 0, 0, false, false, "Curse") { }
    }
    public class Resurrection : ActiveAbl
    {
        public Resurrection() : base(1, 1, 1, 1, 0, 100, true, false, "Resurrection") { }
    }
    public class NoneAbl : ActiveAbl
    {
        public NoneAbl() : base() { }
    }
}
