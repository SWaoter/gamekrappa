using Engine.Models;
using Engine.Bots;
namespace Engine.ViewModels
{
    public class GameSession
    {
        public Battle Battle { get; set; }
        private readonly FactoryMV _factory;
        public GameSession(int[] arm1Id, int[] arm1Count,
                           int[] arm2Id, int[] arm2Count)
        {
            _factory = new FactoryMV();
            Army first = new Army();
            for (int i = 0; i < arm1Id.Length; i++)
                first.AddStack(new UnitsStack(new Unit(_factory.GameUnitsList[arm1Id[i]]), arm1Count[i]));
            Army second = new Army();
            for (int i = 0; i < arm2Id.Length; i++)
                second.AddStack(new UnitsStack(new Unit(_factory.GameUnitsList[arm2Id[i]]), arm2Count[i]));
            Battle = new Battle(first, second);
        }
        public void Attack(int id) => Battle.Attack1(id);

        public void Wait() => Battle.Wait();

        public void Defend() => Battle.Defend();

        public void Cast(int id) => Battle.Cast1(id);
    }
}

