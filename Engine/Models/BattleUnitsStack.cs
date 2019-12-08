using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.Models
{
    public class BattleUnitsStack : UnitsStack, INotifyPropertyChanged
    {
        public int CurrentUnits { get; private set; }
        public double LastHp { get; private set; }
        public double CurrentAttack { get; private set; }
        public double CurrentDamage { get; private set; }
        public double CurrentInitiative { get; private set; }
        public double CurrentDefence { get; private set; }
        public bool ActiveAblStatus { get; private set; }
        public bool IsWaiting { get; set; }
        private bool _is_alive;
        public bool IsAlive
        {
            get { return _is_alive; }
            private set
            {
                _is_alive = value;
                OnPropertyChanged(nameof(IsAlive));
            }
        }
        public bool CounterAttack { get; set; }
        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        private string tfilename;
        public string FileName
        {
            get { return tfilename; }
            set
            {
                tfilename = value;
                OnPropertyChanged(nameof(FileName));
            }
        }
        private string tstring_def;
        public string string_def
        {
            get { return tstring_def; }
            set
            {
                tstring_def = value;
                OnPropertyChanged(nameof(string_def));
            }
        }
        public BattleUnitsStack(Unit Type, int Count) : base(Type, Count)
        {
            CurrentUnits = Count;
            LastHp = Type.Health;
            CurrentAttack = Type.Attack;
            CurrentDamage = Type.Damage();
            CurrentInitiative = Type.Initiative;
            CurrentDefence = Type.Defence;
            IsWaiting = false;
            IsAlive = true;
            Color = "Transparent";
            FileName = Type.ImageName;
            CounterAttack = true;
            ActiveAblStatus = Type.ActiveAbl.WasNotUsed;
            ChangeStringDef();
        }
        public BattleUnitsStack(UnitsStack other) : this(other.Type, other.Count) { }
        public BattleUnitsStack()
        {
            IsAlive = false;
            CurrentInitiative = 0;
        }
        public void TakeActiveAbl(ActiveAbl abl)
        {
            CurrentDefence = abl.Defence * CurrentDefence;
            CurrentAttack = abl.Attack * CurrentAttack;
            CurrentInitiative = abl.Initiative * CurrentInitiative;
            CurrentDamage = abl.Damage * CurrentDamage;
            Heal(abl.Heal);
            TakeDmg(abl.Hp);
            ChangeStringDef();
        }
        public void ActiveAblStatusChanged()
        {
            ActiveAblStatus = !ActiveAblStatus;
        }
        public void TakeDmg(double total_dmg)
        {
            if (total_dmg <= 0)
                return;
            double _tmp_hp = (CurrentUnits - 1) * Type.Health + LastHp - total_dmg;
            if (_tmp_hp > 0)
            {
                CurrentUnits = (int)_tmp_hp / Type.Health + 1;
                LastHp = _tmp_hp - (CurrentUnits - 1) * Type.Health;
                if (LastHp == 0)
                {
                    CurrentUnits--;
                    LastHp = Type.Health;
                }
            }
            else
            {
                CurrentUnits = 0;
                LastHp = 0;
                IsAlive = false;
                FileName = "/Engine;component/Images/Dead.png";
            }
            ChangeStringDef();
        }
        public void TakeAttack(BattleUnitsStack _attacker)
        {
            double total_dmg = 0;
            double tmp_def = CurrentDefence;
            if (_attacker.Type.Ability.AccurateShot)
                CurrentDefence = 0;
            if (_attacker.CurrentAttack > CurrentDefence)
            {
                total_dmg = _attacker.CurrentUnits *
                             _attacker.CurrentDamage * (1 + 0.05 * (
                             _attacker.CurrentAttack - CurrentDefence));
            }
            else
            {
                total_dmg = _attacker.CurrentUnits *
                             _attacker.CurrentDamage / (1 + 0.05 * (
                             CurrentDefence - _attacker.CurrentAttack));
            }
            if (total_dmg <= 0)
                return;
            CurrentDefence = tmp_def;
            TakeDmg(total_dmg);
        }
        public void Heal(double hp)
        {
            if (Type.Ability.Undead)
                hp *= 2;
            if (hp <= 0)           
                return;           
            double _tmp_hp = (CurrentUnits - 1) * Type.Health + LastHp + hp;
            CurrentUnits = (int)_tmp_hp / Type.Health + 1;
            LastHp = _tmp_hp - (CurrentUnits - 1) * Type.Health;
            if (CurrentUnits > Count)
            {
                CurrentUnits = Count;
                LastHp = Type.Health;
            }
            ChangeStringDef();
        }
        private void ChangeStringDef()
        {
            string_def = $"{Type.Type} {CurrentUnits}\n" +
                $"Hp of last: {LastHp}\n" +
                $"Attack: {CurrentAttack}\n" +
                $"Damage: {CurrentDamage}\n" +
                $"Initiative: {CurrentInitiative}\n" +
                $"Defence: {CurrentDefence}";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
