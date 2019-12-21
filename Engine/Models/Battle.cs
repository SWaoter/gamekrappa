using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.Models
{
    public class Battle : INotifyPropertyChanged
    {
        public BattleArmy First { get; }
        public BattleArmy Second { get; }
        private bool _moveOfFirstArmy;
        private BattleUnitsStack _attacker;
        private int _idOfCurrentStackInArmy;
        private bool _isContinue;
        private Pair<int, int>[] _turnPriority;
        public QueueToShow[] TurnQueue { get; }
        private bool _isEnded;
        public bool IsEnded
        {
            get => _isEnded;
            set
            {
                _isEnded = value;
                OnPropertyChanged(nameof(IsEnded));
            }
        }
        public Army RetArm1 { get; }
        public Army RetArm2 { get; }
        private int _priorPointer;
        private int _waitingUnits;
        private string _ablName;

        public string AblName
        {
            get => _ablName;
            private set
            {
                _ablName = value;
                OnPropertyChanged(nameof(AblName));
            }
        }
        private bool _ablVis;
        public bool AblVis
        {
            get => _ablVis;
            private set
            {
                _ablVis = value;
                OnPropertyChanged(nameof(AblVis));
            }
        }
        public Battle(Army first, Army second)
        {
            IsEnded = false;
            RetArm1 = first;
            RetArm2 = second;
            _isContinue = true;
            First = new BattleArmy(first);
            Second = new BattleArmy(second);
            TurnQueue = new QueueToShow[12];
            for (int i = 0; i < 12; i++)
            {
                TurnQueue[i] = new QueueToShow();
            }
            InitializePriority();
            RecalculatePriority();
            WhoMoves();
        }

        private void QueueChange()
        {
            for (int i = 0; i < 12; i++)
            {
                TurnQueue[i].Visibility = false;
                TurnQueue[i].ColorName = "Transparent";
            }
            int dead = 0;
            for (int i = 0; i < _turnPriority.Length; i++)
            {
                switch (_turnPriority[i].Second)
                {
                    case 1:
                        TurnQueue[i - dead].ImageName = First[_turnPriority[i].First].Type.ImageName;
                        TurnQueue[i - dead].ColorName = First[_turnPriority[i].First].Color;
                        TurnQueue[i - dead].Visibility = First[_turnPriority[i].First].IsAlive;
                        if (!First[_turnPriority[i].First].IsAlive)
                            dead++;
                        break;
                    case 2:
                        TurnQueue[i - dead].ImageName = Second[_turnPriority[i].First].Type.ImageName;
                        TurnQueue[i - dead].ColorName = Second[_turnPriority[i].First].Color;
                        TurnQueue[i - dead].Visibility = Second[_turnPriority[i].First].IsAlive;
                        if (!Second[_turnPriority[i].First].IsAlive)
                            dead++;
                        break;
                }
            }
        }

        private void WhoMoves()
        {
            First.CheckIfDefeated();
            Second.CheckIfDefeated();
            if (First.IsDefeated || Second.IsDefeated)
                EndBattle();
            if (_turnPriority[_priorPointer].Second == 1)
            {
                _moveOfFirstArmy = true;
                _idOfCurrentStackInArmy = _turnPriority[_priorPointer].First;
                First[_idOfCurrentStackInArmy].Color = "Green";
            }
            else
            {
                _moveOfFirstArmy = false;
                _idOfCurrentStackInArmy = _turnPriority[_priorPointer].First;
                Second[_idOfCurrentStackInArmy].Color = "Green";
            }
            _attacker = _moveOfFirstArmy ? First[_idOfCurrentStackInArmy] : Second[_idOfCurrentStackInArmy];
            if (!_attacker.IsAlive)
            {
                QueueChange();
                EndTurn();
            }
            AblName = _attacker.Type.ActiveAbl.Name;
            AblVis = _attacker.ActiveAblStatus;
            QueueChange();
        }
        private void InitializePriority()
        {
            _turnPriority = new Pair<int, int>[First.Size + Second.Size];
            for (int i = 0; i < First.Size + Second.Size; i++)
            {
                _turnPriority[i] = new Pair<int, int>();
                if (i < First.Size)
                {
                    _turnPriority[i].First = i;
                    _turnPriority[i].Second = 1;
                }
                else
                {
                    _turnPriority[i].First = i - First.Size;
                    _turnPriority[i].Second = 2;
                }
            }
        }
        private void RecalculatePriority()
        {
            for (int i = 0; i < First.Size + Second.Size; i++)
            {
                for (int j = First.Size + Second.Size - 1; j > i; j--)
                {
                    var s1 = _turnPriority[j].Second == 1 ? First[_turnPriority[j].First].CurrentInitiative 
                        : Second[_turnPriority[j].First].CurrentInitiative;
                    var s2 = _turnPriority[j - 1].Second == 1 ? First[_turnPriority[j - 1].First].CurrentInitiative 
                        : Second[_turnPriority[j - 1].First].CurrentInitiative;
                    if (s2 < s1)
                    {
                        var tmp = _turnPriority[j];
                        _turnPriority[j] = _turnPriority[j - 1];
                        _turnPriority[j - 1] = tmp;
                    }
                }
            }
        }
        private void EndTurn()
        {
            _priorPointer++;
            if (_moveOfFirstArmy)
            {
                First[_idOfCurrentStackInArmy].Color = "Transparent";
            }
            else
            {
                Second[_idOfCurrentStackInArmy].Color = "Transparent";
            }
            if (_priorPointer > First.Size + Second.Size - 1)
            {
                _priorPointer = 0;
                _waitingUnits = 0;
                InitializePriority();
                RecalculatePriority();
                for (int i = 0; i < First.Size; i++)
                {
                    First[i].IsWaiting = false;
                    First[i].CounterAttack = true;
                }
                for (int i = 0; i < Second.Size; i++)
                {
                    Second[i].IsWaiting = false;
                    Second[i].CounterAttack = true;
                }
            }
            WhoMoves();
        }
        public void Wait()
        {
            for (int i = _priorPointer; i < First.Size + Second.Size - _waitingUnits - 1; i++)
            {
                Pair<int, int> tmp = _turnPriority[i];
                _turnPriority[i] = _turnPriority[i + 1];
                _turnPriority[i + 1] = tmp;
            }
            _attacker.IsWaiting = true;
            _waitingUnits++;
            _priorPointer--;
            EndTurn();
        }
        public void Defend()
        {
            ActiveAbl tmp = new ActiveAbl(1.4, 1, 1, 1, 0, 0, true, false, "def");
            if (_moveOfFirstArmy)
            {
                First[_idOfCurrentStackInArmy].TakeActiveAbl(tmp);
            }
            else
            {
                Second[_idOfCurrentStackInArmy].TakeActiveAbl(tmp);
            }
            EndTurn();
        }
        public void Attack1(int idOfTarget)
        {
            if (_attacker.Type.Ability.GreatCleave)
            {
                _isContinue = false;
                if (_moveOfFirstArmy)
                {
                    for (int i = 0; i < Second.Size; i++)
                    {
                        if (i == Second.Size - 1)
                            _isContinue = true;
                        Attack(i);
                    }
                }
                else
                {
                    for (int i = 0; i < First.Size; i++)
                    {
                        if (i == First.Size - 1)
                            _isContinue = true;
                        Attack(i);
                    }
                }
            }
            else
            {
                Attack(idOfTarget);
            }

        }
        public void Attack(int idOfTarget)
        {
            if (_moveOfFirstArmy)
            {
                Second.TakeAttack(_attacker, idOfTarget);
                if ((!(_attacker.Type.Ability.Shooter) && Second[idOfTarget].CounterAttack &&
                    !(Second[idOfTarget].Type.Ability.Shooter) || Second[idOfTarget].Type.Ability.Reflection) 
                    && !(_attacker.Type.Ability.NoResistance))
                {
                    First.TakeAttack(Second[idOfTarget], _idOfCurrentStackInArmy);
                    Second[idOfTarget].CounterAttack = false;
                }                
            }
            else
            {
                First.TakeAttack(_attacker, idOfTarget);
                if ((!(_attacker.Type.Ability.Shooter) && First[idOfTarget].CounterAttack &&
                    !(First[idOfTarget].Type.Ability.Shooter) || First[idOfTarget].Type.Ability.Reflection)
                    && !(_attacker.Type.Ability.NoResistance))
                {
                    Second.TakeAttack(First[idOfTarget], _idOfCurrentStackInArmy);
                    First[idOfTarget].CounterAttack = false;
                }
            }
            if (_isContinue)
                EndTurn();
        }
        public void Cast1(int idOfTarget)
        {
            if (_attacker.Type.ActiveAbl.Aoe)
            {
                _isContinue = false;
                if (_attacker.Type.ActiveAbl.Friendly)
                {
                    if (_moveOfFirstArmy)
                    {
                        for (int i = 0; i < First.Size; i++)
                        {
                            if (i == First.Size - 1)
                                _isContinue = true;
                            Cast(i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < Second.Size; i++)
                        {
                            if (i == Second.Size - 1)
                                _isContinue = true;
                            Cast(i);
                        }
                    }
                }
                else
                {
                    if (_moveOfFirstArmy)
                    {
                        for (int i = 0; i < Second.Size; i++)
                        {
                            if (i == Second.Size - 1)
                                _isContinue = true;
                            Cast(i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < First.Size; i++)
                        {
                            if (i == First.Size - 1)
                                _isContinue = true;
                            Cast(i);
                        }
                    }
                }
            }
            else
            {
                Cast(idOfTarget);
            }
        }
        private void Cast(int idOfTarget)
        {
            if (_isContinue)
                _attacker.ActiveAblStatusChanged();
            int armyId;
            bool isWaiting;
            double initiative;
            double tmpInitiative;
            if (_attacker.Type.ActiveAbl.Friendly)
            {
                if (_moveOfFirstArmy)
                { 
                    First[idOfTarget].TakeActiveAbl(_attacker.Type.ActiveAbl);
                    armyId = 1;
                    isWaiting = First[idOfTarget].IsWaiting;
                    initiative = First[idOfTarget].CurrentInitiative;
                }
                else
                {
                    Second[idOfTarget].TakeActiveAbl(_attacker.Type.ActiveAbl);
                    armyId = 2;
                    isWaiting = Second[idOfTarget].IsWaiting;
                    initiative = Second[idOfTarget].CurrentInitiative;
                }
            } else
            {
                if (_moveOfFirstArmy)
                {
                    Second[idOfTarget].TakeActiveAbl(_attacker.Type.ActiveAbl);
                    armyId = 2;
                    isWaiting = Second[idOfTarget].IsWaiting;
                    initiative = Second[idOfTarget].CurrentInitiative;
                }
                else
                {
                    First[idOfTarget].TakeActiveAbl(_attacker.Type.ActiveAbl);
                    armyId = 1;
                    isWaiting = First[idOfTarget].IsWaiting;
                    initiative = First[idOfTarget].CurrentInitiative;
                }
            }
            if (_attacker.Type.ActiveAbl.Initiative > 1)
            {
                for (int i = 0; i < First.Size + Second.Size; i++)
                {
                    if (_turnPriority[i].First == idOfTarget &&
                        _turnPriority[i].Second == armyId)
                    {
                        if (!isWaiting && i > _priorPointer + 1)
                        {
                            for (int j = i; j > _priorPointer + 1; j--)
                            {
                                if (_turnPriority[j - 1].Second == 1)
                                {
                                    tmpInitiative = First[_turnPriority[j - 1].First].CurrentInitiative;
                                }
                                else
                                {
                                    tmpInitiative = Second[_turnPriority[j - 1].First].CurrentInitiative;

                                }
                                if (initiative > tmpInitiative)
                                {
                                    Pair<int, int> tmp = _turnPriority[j];
                                    _turnPriority[j] = _turnPriority[j - 1];
                                    _turnPriority[j - 1] = tmp;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        if (isWaiting && i > _priorPointer + 1)
                        {
                            for (int j = i; j < First.Size + Second.Size - 1; j++)
                            {
                                if (_turnPriority[j + 1].Second == 1)
                                {
                                    tmpInitiative = First[_turnPriority[j + 1].First].CurrentInitiative;
                                }
                                else
                                {
                                    tmpInitiative = Second[_turnPriority[j + 1].First].CurrentInitiative;

                                }
                                if (initiative > tmpInitiative)
                                {
                                    Pair<int, int> tmp = _turnPriority[j];
                                    _turnPriority[j] = _turnPriority[j + 1];
                                    _turnPriority[j + 1] = tmp;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            if (_attacker.Type.ActiveAbl.Initiative < 1)
            {
                for (int i = 0; i < First.Size + Second.Size; i++)
                {
                    if (_turnPriority[i].First == idOfTarget &&
                        _turnPriority[i].Second == armyId)
                    {
                        if (!isWaiting && i > _priorPointer)
                        {
                            for (int j = i; j < First.Size + Second.Size - 1 - _waitingUnits; j++)
                            {
                                if (_turnPriority[j + 1].Second == 1)
                                {
                                    tmpInitiative = First[_turnPriority[j + 1].First].CurrentInitiative;
                                }
                                else
                                {
                                    tmpInitiative = Second[_turnPriority[j + 1].First].CurrentInitiative;

                                }
                                if (initiative < tmpInitiative)
                                {
                                    Pair<int, int> tmp = _turnPriority[j];
                                    _turnPriority[j] = _turnPriority[j + 1];
                                    _turnPriority[j + 1] = tmp;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        if (isWaiting && i > _priorPointer + 1)
                        {
                            for (int j = i; j > _priorPointer + 1; j--)
                            {
                                if (_turnPriority[j - 1].Second == 1)
                                {
                                    tmpInitiative = First[_turnPriority[j - 1].First].CurrentInitiative;
                                }
                                else
                                {
                                    tmpInitiative = Second[_turnPriority[j - 1].First].CurrentInitiative;

                                }
                                if (initiative < tmpInitiative)
                                {
                                    Pair<int, int> tmp = _turnPriority[j];
                                    _turnPriority[j] = _turnPriority[j - 1];
                                    _turnPriority[j - 1] = tmp;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            if (_isContinue)
                EndTurn();
        }

        public void EndBattle()
        {
            IsEnded = true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class QueueToShow : INotifyPropertyChanged
    {
        private string _imageName;
        private string _colorName;
        private bool _visibility;
        public string ImageName
        {
            get => _imageName;
            set
            {
                _imageName = value;
                OnPropertyChanged(nameof(ImageName));
            }
        }
        public string ColorName
        {
            get => _colorName;
            set
            {
                _colorName = value;
                OnPropertyChanged(nameof(ColorName));
            }
        }
        public bool Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
