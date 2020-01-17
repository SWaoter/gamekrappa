using Engine.Factory;

namespace Engine.ViewModels
{
    public class ExpandDataMV
    {
        public static void AddCast(double defense, double attack, double initiative,
            double damage, double hp, double heal,
            bool friendly, bool aoe, string name) =>
            ExpandData.AddCast(defense, attack, initiative, damage, hp, heal, friendly, aoe, name);

        public static void AddUnit(string type, string ability, string activeAbl,
            int health, int attack, int defense,
            int minDamage, int maxDamage, string imageName, double initiative) =>
            ExpandData.AddUnit(type, ability, activeAbl, health, attack, defense, minDamage, maxDamage, imageName, initiative);
    }
}
