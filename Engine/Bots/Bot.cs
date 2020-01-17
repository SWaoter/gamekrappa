using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Bots
{
    abstract class Bot
    {
        public Battle Battle;
        public Random Random = new Random();

        protected Bot(Battle battle)
        {
            Battle = battle;
        }

        public virtual void Decision(int num) { }
        
    }
}
