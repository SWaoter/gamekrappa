using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.ViewModels
{
    public class GameSession
    {
        public Battle Battle { get; set; }
        public GameSession()
        {
            UnitsStack arm11 = new UnitsStack(new Lich(), 1);
            UnitsStack arm12 = new UnitsStack(new BoneDragon(), 2);
            UnitsStack arm13 = new UnitsStack(new Skeleton(), 500);
            UnitsStack arm14 = new UnitsStack(new Devil(), 2);
            UnitsStack arm15 = new UnitsStack(new Angel(), 5);
            UnitsStack[] arm1 = new UnitsStack[] { arm11, arm12, arm13, arm14, arm15 };
            Army tFirstArmy = new Army(arm1);
            Army tSecondArmy = new Army(new UnitsStack(new Griffin(), 5), new UnitsStack(new Shaman(), 3),
                                      new UnitsStack(new Hydra(), 4), new UnitsStack(new Arbalester(), 500));
            Battle = new Battle(tFirstArmy, tSecondArmy);
        }
        public void Attack(int _id)
        {
            Battle.Attack1(_id);
        }
        public void Wait()
        {
            Battle.Wait();
        }
        public void Defend()
        {
            Battle.Defend();
        }
        public void Cast(int _id)
        {
            Battle.Cast1(_id);
        }
    }
}

