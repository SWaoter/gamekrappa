using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Security.Permissions;
using Engine.Models;

namespace Engine.Factory
{
     public class BattleCreation
    {
        private const string GameUnitsFilename = "persons.xml";
        private const string ActiveAbility = "activeabl.xml";
        private const string PassiveAbility = "passiveabl.xml";
        public static readonly List<Unit> GameUnits = new List<Unit>();
        public static readonly List<ActiveAbl> GameActiveAbl = new List<ActiveAbl>();
        public static readonly List<PassiveAbl> GamePassiveAbl = new List<PassiveAbl>();
        public BattleCreation()
        {
            if (File.Exists(ActiveAbility))
            {
                XmlDocument dataAAbl = new XmlDocument();
                dataAAbl.LoadXml(File.ReadAllText(ActiveAbility));

                LoadActiveAblsFromNodes(dataAAbl.SelectNodes("/ActiveAbls/activeAbl"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {ActiveAbility}");
            }
            if (File.Exists(PassiveAbility))
            {
                XmlDocument dataPAbl = new XmlDocument();
                dataPAbl.LoadXml(File.ReadAllText(PassiveAbility));

                LoadPassiveAblsFromNodes(dataPAbl.SelectNodes("/PassiveAbls/passiveAbl"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {PassiveAbility}");
            }
            if (File.Exists(GameUnitsFilename))
            {
                XmlDocument dataUnits = new XmlDocument();
                dataUnits.LoadXml(File.ReadAllText(GameUnitsFilename));

                LoadUnitsFromNodes(dataUnits.SelectNodes("/Units/unit"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GameUnitsFilename}");
            }
        }

        private static void LoadUnitsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                string Passive = GetXmlAttribute(node, "Ability");
                string AblName = GetXmlAttribute(node, "ActiveAbl");
                ActiveAbl uAbility = new ActiveAbl();
                PassiveAbl uPassive = new PassiveAbl();
                if (Passive != "none")
                {
                    for (int i = 0; i < GamePassiveAbl.Count; i++)
                    {
                        if (GamePassiveAbl[i].Name == Passive)
                        {
                            uPassive = GamePassiveAbl[i];
                            break;
                        }
                    }
                }
                if (AblName != "")
                {
                    for (int i = 0; i < GameActiveAbl.Count; i++)
                    {
                        if (GameActiveAbl[i].Name == AblName)
                        {
                            uAbility = GameActiveAbl[i];
                            break;
                        }
                    }
                }
                Unit u = new Unit(GetXmlAttribute(node, "Type"),
                                  uPassive, uAbility,
                                  GetXmlAttributeAsInt(node, "Health"),
                                  GetXmlAttributeAsInt(node, "Attack"),
                                  GetXmlAttributeAsInt(node, "Defence"),
                                  GetXmlAttributeAsInt(node, "MinDamage"),
                                  GetXmlAttributeAsInt(node, "MaxDamage"),
                                  GetXmlAttributeAsDouble(node, "Initiative"),
                                  GetXmlAttribute(node, "ImageName"));
                GameUnits.Add(u);
            }
        }

        private static void LoadActiveAblsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
                return;
            foreach (XmlNode node in nodes)
            {
                ActiveAbl gameItem =
                    new ActiveAbl(GetXmlAttributeAsDouble(node, "Defence"),
                                GetXmlAttributeAsDouble(node, "Attack"),
                                GetXmlAttributeAsDouble(node, "Initiative"),
                                GetXmlAttributeAsDouble(node, "Damage"),
                                GetXmlAttributeAsDouble(node, "Hp"),
                                GetXmlAttributeAsDouble(node, "Heal"),
                                GetXmlAttributeAsBool(node, "Friendly"),
                                GetXmlAttributeAsBool(node, "Aoe"),
                                GetXmlAttribute(node, "Name"));
                GameActiveAbl.Add(gameItem);
            }
        }
        private static void LoadPassiveAblsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
                return;
            foreach (XmlNode node in nodes)
            {
                PassiveAbl gameItem =
                    new PassiveAbl(GetXmlAttribute(node, "Name"),
                        GetXmlAttributeAsBool(node, "Shooter"),
                        GetXmlAttributeAsBool(node, "AccurateShot"),
                        GetXmlAttributeAsBool(node, "Undead"),
                        GetXmlAttributeAsBool(node, "NoResistance"),
                        GetXmlAttributeAsBool(node, "GreatCleave"),
                        GetXmlAttributeAsBool(node, "Reflection"));
                GamePassiveAbl.Add(gameItem);
            }
        }

        private static int GetXmlAttributeAsInt(XmlNode node, string attributeName)
        {
            return Convert.ToInt32(GetXmlAttribute(node, attributeName));
        }

        private static double GetXmlAttributeAsDouble(XmlNode node, string attributeName)
        {
            return Convert.ToDouble(GetXmlAttribute(node, attributeName));
        }
        private static string GetXmlAttributeAsString(XmlNode node, string attributeName)
        {
            return GetXmlAttribute(node, attributeName);
        }
        private static bool GetXmlAttributeAsBool(XmlNode node, string attributeName)
        {
            return Convert.ToBoolean(GetXmlAttribute(node, attributeName));
        }

        private static string GetXmlAttribute(XmlNode node, string attributeName)
        {
            XmlAttribute attribute = node.Attributes?[attributeName];

            if (attribute == null)
            {
                throw new ArgumentException($"The attribute '{attributeName}' does not exist");
            }

            return attribute.Value;
        }

        public List<Unit> GetUnitsData()
        {
            return GameUnits;
        }
    }
}
