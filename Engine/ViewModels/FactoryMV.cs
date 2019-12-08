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
        public List<string> GameUnitsStrings { get; private set; }

        public FactoryMV()
        {
            _factory = new BattleCreation();
            GameUnitsList = new List<Unit>(_factory.GetUnitsData());
            GameUnitsStrings = new List<string>();
            foreach (var val in GameUnitsList) GameUnitsStrings.Add(val.Type);
        }
    }
}
