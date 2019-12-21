using System.Xml;

namespace Engine.Factory
{
    public class ExpandData
    {
        public static void AddCast(double defense, double attack, double initiative,
                                   double damage, double hp, double heal,
                                   bool friendly, bool aoe, string name)
        {
            XmlDocument test1 = new XmlDocument();
            test1.Load("activeabl.xml");
            XmlElement xRoot = test1.DocumentElement;
            XmlElement unitElem = test1.CreateElement("activeAbl");
            XmlAttribute strName = test1.CreateAttribute("Defense");
            XmlAttribute ability = test1.CreateAttribute("Attack");
            XmlAttribute activeAbl = test1.CreateAttribute("Initiative");
            XmlAttribute Hp = test1.CreateAttribute("Damage");
            XmlAttribute Attack = test1.CreateAttribute("Hp");
            XmlAttribute Defence = test1.CreateAttribute("Heal");
            XmlAttribute MinDamage = test1.CreateAttribute("Friendly");
            XmlAttribute MaxDamage = test1.CreateAttribute("Aoe");
            XmlAttribute ImageName = test1.CreateAttribute("Name");
            XmlText stN = test1.CreateTextNode($"{defense}");
            XmlText sAbility = test1.CreateTextNode($"{attack}");
            XmlText sActiveAbl = test1.CreateTextNode($"{initiative}");
            XmlText sHp = test1.CreateTextNode($"{damage}");
            XmlText sAttack = test1.CreateTextNode($"{hp}");
            XmlText sDefence = test1.CreateTextNode($"{heal}");
            XmlText sMinDamage = test1.CreateTextNode($"{friendly}");
            XmlText sMaxDamage = test1.CreateTextNode($"{aoe}");
            XmlText sImageName = test1.CreateTextNode($"{name}");
            strName.AppendChild(stN);
            ability.AppendChild(sAbility);
            activeAbl.AppendChild(sActiveAbl);
            Hp.AppendChild(sHp);
            Attack.AppendChild(sAttack);
            Defence.AppendChild(sDefence);
            MinDamage.AppendChild(sMinDamage);
            MaxDamage.AppendChild(sMaxDamage);
            ImageName.AppendChild(sImageName);
            unitElem.Attributes.Append(strName);
            unitElem.Attributes.Append(ability);
            unitElem.Attributes.Append(activeAbl);
            unitElem.Attributes.Append(Hp);
            unitElem.Attributes.Append(Attack);
            unitElem.Attributes.Append(Defence);
            unitElem.Attributes.Append(MinDamage);
            unitElem.Attributes.Append(MaxDamage);
            unitElem.Attributes.Append(ImageName);
            xRoot.AppendChild(unitElem);
            test1.Save("activeabl.xml");
        }

        public static void AddUnit(string Type_, string Ability_, string ActiveAbl_,
                                   int Health_, int Attack_, int Defense_,
                                   int MinDamage_, int MaxDamage_, string ImageName_, double Initiative_)
        {
            XmlDocument test1 = new XmlDocument();
            test1.Load("persons.xml");
            XmlElement xRoot = test1.DocumentElement;
            XmlElement unitElem = test1.CreateElement("unit");
            XmlAttribute strName = test1.CreateAttribute("Type");
            XmlAttribute Ability = test1.CreateAttribute("Ability");
            XmlAttribute ActiveAbl = test1.CreateAttribute("ActiveAbl");
            XmlAttribute Hp = test1.CreateAttribute("Health");
            XmlAttribute Attack = test1.CreateAttribute("Attack");
            XmlAttribute Defence = test1.CreateAttribute("Defense");
            XmlAttribute MinDamage = test1.CreateAttribute("MinDamage");
            XmlAttribute MaxDamage = test1.CreateAttribute("MaxDamage");
            XmlAttribute ImageName = test1.CreateAttribute("ImageName");
            XmlAttribute Initiative = test1.CreateAttribute("Initiative");
            XmlText stN = test1.CreateTextNode($"{Type_}");
            XmlText sAbility = test1.CreateTextNode($"{Ability_}");
            XmlText sActiveAbl = test1.CreateTextNode($"{ActiveAbl_}");
            XmlText sHp = test1.CreateTextNode($"{Health_}");
            XmlText sAttack = test1.CreateTextNode($"{Attack_}");
            XmlText sDefence = test1.CreateTextNode($"{Defense_}");
            XmlText sMinDamage = test1.CreateTextNode($"{MinDamage_}");
            XmlText sMaxDamage = test1.CreateTextNode($"{MaxDamage_}");
            XmlText sImageName = test1.CreateTextNode($"{ImageName_}");
            XmlText sInitiative = test1.CreateTextNode($"{Initiative_}");
            strName.AppendChild(stN);
            Ability.AppendChild(sAbility);
            ActiveAbl.AppendChild(sActiveAbl);
            Hp.AppendChild(sHp);
            Attack.AppendChild(sAttack);
            Defence.AppendChild(sDefence);
            MinDamage.AppendChild(sMinDamage);
            MaxDamage.AppendChild(sMaxDamage);
            ImageName.AppendChild(sImageName);
            Initiative.AppendChild(sInitiative);
            unitElem.Attributes.Append(strName);
            unitElem.Attributes.Append(Ability);
            unitElem.Attributes.Append(ActiveAbl);
            unitElem.Attributes.Append(Hp);
            unitElem.Attributes.Append(Attack);
            unitElem.Attributes.Append(Defence);
            unitElem.Attributes.Append(MinDamage);
            unitElem.Attributes.Append(MaxDamage);
            unitElem.Attributes.Append(ImageName);
            unitElem.Attributes.Append(Initiative);
            xRoot.AppendChild(unitElem);
            test1.Save("persons.xml");
        }
    }
}
