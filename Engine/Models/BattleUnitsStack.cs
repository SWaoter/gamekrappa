using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.Models
{
    public class BattleUnitsStack : UnitsStack, INotifyPropertyChanged
    {
        public int CurrentUnits { get; private set; }
        public double LastHp { get; private set; }
        public double CurrentAttack { get; private set; }
        private double _curdmg;
        public double CurrentDamage
        {
            get
            {
                _curdmg = Type.Damage();
                CurrentDamage = _curdmg;
                return _curdmg;
            }
            private set { _curdmg = value; OnPropertyChanged(nameof(CurrentDamage)); }
        }
        public double CurrentInitiative { get; private set; }
        public double CurrentDefence { get; private set; }
        public bool ActiveAblStatus { get; private set; }
        public bool IsWaiting { get; set; }
        private bool _isAlive;
        public bool IsAlive
        {
            get => _isAlive;
            private set
            {
                _isAlive = value;
                OnPropertyChanged(nameof(IsAlive));
            }
        }
        public bool CounterAttack { get; set; }
        private string _color;
        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
        private string _tfilename;
        public string FileName
        {
            get => _tfilename;
            set
            {
                _tfilename = value;
                OnPropertyChanged(nameof(FileName));
            }
        }
        private string _tstringDef;
        public string StringDef
        {
            get => _tstringDef;
            set
            {
                _tstringDef = value;
                OnPropertyChanged(nameof(StringDef));
            }
        }
        public BattleUnitsStack(Unit type, int count) : base(type, count)
        {
            CurrentUnits = count;
            LastHp = type.Health;
            CurrentAttack = type.Attack;
            CurrentDamage = type.Damage();
            CurrentInitiative = type.Initiative;
            CurrentDefence = type.Defense;
            IsWaiting = false;
            IsAlive = true;
            Color = "Transparent";
            FileName = type.ImageName;
            CounterAttack = true;
            ActiveAblStatus = type.ActiveAbl.WasNotUsed;
            ChangeStringDef();
        }
        public BattleUnitsStack(UnitsStack other)
            : this(other.Type, other.Count) { }
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
        public void TakeDmg(double totalDmg)
        {
            if (totalDmg <= 0)
                return;
            double tmpHp = (CurrentUnits - 1) * Type.Health + LastHp - totalDmg;
            if (tmpHp > 0)
            {
                CurrentUnits = (int)tmpHp / Type.Health + 1;
                LastHp = tmpHp - (CurrentUnits - 1) * Type.Health;
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
        public void TakeAttack(BattleUnitsStack attacker)
        {
            double totalDmg;
            double tmpDef = CurrentDefence;
            if (attacker.Type.Ability.AccurateShot)
                CurrentDefence = 0;
            if (attacker.CurrentAttack > CurrentDefence)
            {
                totalDmg = attacker.CurrentUnits *
                             attacker.CurrentDamage * (1 + 0.05 * (
                             attacker.CurrentAttack - CurrentDefence));
            }
            else
            {
                totalDmg = attacker.CurrentUnits *
                             attacker.CurrentDamage / (1 + 0.05 * (
                             CurrentDefence - attacker.CurrentAttack));
            }
            if (totalDmg <= 0)
                return;
            CurrentDefence = tmpDef;
            TakeDmg(totalDmg);
        }
        public void Heal(double hp)
        {
            if (Type.Ability.Undead)
                hp *= 2;
            if (hp <= 0)           
                return;           
            double tmpHp = (CurrentUnits - 1) * Type.Health + LastHp + hp;
            CurrentUnits = (int)tmpHp / Type.Health + 1;
            LastHp = tmpHp - (CurrentUnits - 1) * Type.Health;
            if (CurrentUnits > Count)
            {
                CurrentUnits = Count;
                LastHp = Type.Health;
            }
            ChangeStringDef();
        }
        private void ChangeStringDef()
        {
            StringDef = $"{Type.Type} {CurrentUnits}\n" +
                $"Hp of last: {LastHp}\n" +
                $"Attack: {CurrentAttack}\n" +
                $"Damage: {CurrentDamage}\n" +
                $"Initiative: {CurrentInitiative}\n" +
                $"Defense: {CurrentDefence}";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
