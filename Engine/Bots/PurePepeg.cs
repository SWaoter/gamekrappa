using Engine.Models;

namespace Engine.Bots
{
    class PurePepeg : Bot
    {
        public PurePepeg(Battle battle) : base(battle)
        {
        }

        public override void Decision(int num)
        {
            int idOfTarget;
            int[] idArray = Battle.First.GetAliveId();
            int[] idArray2 = Battle.Second.GetAliveId();
            if (Battle.Second[num].ActiveAblStatus)
            {
                idOfTarget = Random.Next(0, Battle.Second[num].Type.ActiveAbl.Friendly ? Battle.Second.GetCurrentAliveStacksNum() 
                    : Battle.First.GetCurrentAliveStacksNum());
                Battle.Cast1(Battle.Second[num].Type.ActiveAbl.Friendly ? idArray2[idOfTarget] : idArray[idOfTarget]);
            }
            else
            {
                idOfTarget = Random.Next(0, Battle.First.GetCurrentAliveStacksNum());
                Battle.Attack1(idArray[idOfTarget]);
            }
        }
    }
}
