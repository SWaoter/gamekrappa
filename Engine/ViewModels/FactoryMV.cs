using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Factory;
using Engine.Models;

namespace Engine.ViewModels
{
    public class FactoryMV
    {
        private BattleCreation _factory;
        public List<Unit> GameUnitsList { get; private set; }
        public List<ActiveAbl> GameActiveList { get; private set; }
        public List<PassiveAbl> GamePassiveList { get; private set; }
        public List<string> GameUnitsStrings { get; private set; }
        public List<string> GameActiveStrings { get; private set; }
        public List<string> GamePassiveStrings { get; private set; }
        public FactoryMV()
        {
            _factory = new BattleCreation();
            GameUnitsList = new List<Unit>(_factory.GetUnitsData());
            GameActiveList = new List<ActiveAbl>(_factory.GetAblsData());
            GamePassiveList = new List<PassiveAbl>(_factory.GetPassData());
            GameUnitsStrings = new List<string>();
            GamePassiveStrings = new List<string>();
            GameActiveStrings = new List<string>();
            foreach (var val in GameUnitsList) GameUnitsStrings.Add(val.Type);
            foreach (var val in GameActiveList) GameActiveStrings.Add(val.Name);
            foreach (var val in GamePassiveList) GamePassiveStrings.Add(val.Name);
        }
    }
}
