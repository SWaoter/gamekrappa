using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
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
            GameUnits.Clear();
            GameActiveAbl.Clear();
            GamePassiveAbl.Clear();
            if (File.Exists(ActiveAbility))
            {
                XmlDocument dataAAbl = new XmlDocument();
                dataAAbl.LoadXml(File.ReadAllText(ActiveAbility));

                LoadActiveAbilitiesFromNodes(dataAAbl.SelectNodes("/ActiveAbls/activeAbl"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {ActiveAbility}");
            }
            if (File.Exists(PassiveAbility))
            {
                XmlDocument dataPAbl = new XmlDocument();
                dataPAbl.LoadXml(File.ReadAllText(PassiveAbility));

                LoadPassiveAbilitiesFromNodes(dataPAbl.SelectNodes("/PassiveAbls/passiveAbl"));
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
                string passive = GetXmlAttribute(node, "Ability");
                string ablName = GetXmlAttribute(node, "ActiveAbl");
                ActiveAbl uAbility = new ActiveAbl();
                PassiveAbl uPassive = new PassiveAbl();
                if (passive != "none")
                {
                    foreach (var t in GamePassiveAbl)
                    {
                        if (t.Name != passive) continue;
                        uPassive = t;
                        break;
                    }
                }
                if (ablName != "")
                {
                    foreach (var t in GameActiveAbl)
                    {
                        if (t.Name != ablName) continue;
                        uAbility = t;
                        break;
                    }
                }
                Unit u = new Unit(GetXmlAttribute(node, "Type"),
                                  uPassive, uAbility,
                                  GetXmlAttributeAsInt(node, "Health"),
                                  GetXmlAttributeAsInt(node, "Attack"),
                                  GetXmlAttributeAsInt(node, "Defense"),
                                  GetXmlAttributeAsInt(node, "MinDamage"),
                                  GetXmlAttributeAsInt(node, "MaxDamage"),
                                  GetXmlAttributeAsDouble(node, "Initiative"),
                                  GetXmlAttribute(node, "ImageName"));
                GameUnits.Add(u);
            }
        }

        private static void LoadActiveAbilitiesFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
                return;
            foreach (XmlNode node in nodes)
            {
                ActiveAbl gameItem =
                    new ActiveAbl(GetXmlAttributeAsDouble(node, "Defense"),
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
        private static void LoadPassiveAbilitiesFromNodes(XmlNodeList nodes)
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
        public List<ActiveAbl> GetAblsData()
        {
            return GameActiveAbl;
        }
        public List<PassiveAbl> GetPassData()
        {
            return GamePassiveAbl;
        }
    }
}
