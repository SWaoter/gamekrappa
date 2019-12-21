using Engine.Factory;

namespace Engine.ViewModels
{
    public class ExpandDataMV
    {
        public static void AddCast(double Defense, double Attack_, double Initiative,
            double Damage, double Hp_, double Heal,
            bool Friendly, bool Aoe, string Name)
        {
            ExpandData.AddCast(Defense, Attack_, Initiative, Damage, Hp_, Heal, Friendly, Aoe, Name);
        }

        public static void AddUnit(string Type_, string Ability_, string ActiveAbl_,
            int Health_, int Attack_, int Defense_,
            int MinDamage_, int MaxDamage_, string ImageName_, double Initiative_)
        {
            ExpandData.AddUnit(Type_, Ability_, ActiveAbl_, Health_, Attack_, Defense_, MinDamage_, MaxDamage_, ImageName_, Initiative_);
        }
    }
}
