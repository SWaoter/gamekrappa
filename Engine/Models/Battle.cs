using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Engine.Models
{
    public class Battle : INotifyPropertyChanged
    {
        public BattleArmy _first { get; private set; }
        public BattleArmy _second { get; private set; }
        private bool move_of_first_army;
        private BattleUnitsStack attacker;
        private int id_of_curent_stack_in_army;
        private bool is_continue;
        private Pair<int, int>[] turn_priority;
        private Army ret_arm_1;
        private Army ret_arm_2;
        private int prior_pointer = 0;
        private int waiting_units = 0;
        private string _abl_name;
        public string AblName
        {
            get { return _abl_name; }
            private set
            {
                _abl_name = value;
                OnPropertyChanged(nameof(AblName));
            }
        }
        private bool _ablVis;
        public bool AblVis
        {
            get { return _ablVis; }
            private set
            {
                _ablVis = value;
                OnPropertyChanged(nameof(AblVis));
            }
        }
        public Battle(Army first, Army second)
        {
            ret_arm_1 = first;
            ret_arm_2 = second;
            is_continue = true;
            _first = new BattleArmy(first);
            _second = new BattleArmy(second);
            InitiaizePriority();
            RecalculPrior();
            WhoMoves();
        }
        public void WhoMoves()
        {
            if (turn_priority[prior_pointer].Second == 1)
            {
                move_of_first_army = true;
                id_of_curent_stack_in_army = turn_priority[prior_pointer].First;
                _first[id_of_curent_stack_in_army].Color = "Green";
            }
            else
            {
                move_of_first_army = false;
                id_of_curent_stack_in_army = turn_priority[prior_pointer].First;
                _second[id_of_curent_stack_in_army].Color = "Green";
            }
            if (move_of_first_army)
            {
                attacker = _first[id_of_curent_stack_in_army];
            }
            else
            {
                attacker = _second[id_of_curent_stack_in_army];
            }
            if (!attacker.IsAlive)
            {
                EndTurn();
            }
            AblName = attacker.Type.ActiveAbl.Name;
            AblVis = attacker.ActiveAblStatus;
        }
        private void InitiaizePriority()
        {
            turn_priority = new Pair<int, int>[_first._size + _second._size];
            for (int i = 0; i < _first._size + _second._size; i++)
            {
                turn_priority[i] = new Pair<int, int>();
                if (i < _first._size)
                {
                    turn_priority[i].First = i;
                    turn_priority[i].Second = 1;
                }
                else
                {
                    turn_priority[i].First = i - _first._size;
                    turn_priority[i].Second = 2;
                }
            }
        }
        private void RecalculPrior()
        {
            for (int i = 0; i < _first._size + _second._size; i++)
            {
                for (int j = _first._size + _second._size - 1; j > i; j--)
                {
                    double s1;
                    double s2 = 0;
                    if (turn_priority[j].Second == 1)
                    {
                        s1 = _first[turn_priority[j].First].CurrentInitiative;
                    }
                    else
                    {
                        s1 = _second[turn_priority[j].First].CurrentInitiative;
                    }
                    if (turn_priority[j - 1].Second == 1)
                    {
                        s2 = _first[turn_priority[j - 1].First].CurrentInitiative;
                    }
                    else
                    {
                        s2 = _second[turn_priority[j - 1].First].CurrentInitiative;
                    }
                    if (s2 < s1)
                    {
                        Pair<int, int> tmp = turn_priority[j];
                        turn_priority[j] = turn_priority[j - 1];
                        turn_priority[j - 1] = tmp;
                    }
                }
            }
        }
        private void EndTurn()
        {
            prior_pointer++;
            if (move_of_first_army)
            {
                _first[id_of_curent_stack_in_army].Color = "Transparent";
            }
            else
            {
                _second[id_of_curent_stack_in_army].Color = "Transparent";
            }
            if (prior_pointer > _first._size + _second._size - 1)
            {
                prior_pointer = 0;
                waiting_units = 0;
                InitiaizePriority();
                RecalculPrior();
                for (int i = 0; i < _first._size; i++)
                {
                    _first[i].IsWaiting = false;
                    _first[i].CounterAttack = true;
                }
                for (int i = 0; i < _second._size; i++)
                {
                    _second[i].IsWaiting = false;
                    _second[i].CounterAttack = true;
                }
            }
            WhoMoves();
        }
        public void Wait()
        {
            for (int i = prior_pointer; i < _first._size + _second._size - waiting_units - 1; i++)
            {
                Pair<int, int> tmp = turn_priority[i];
                turn_priority[i] = turn_priority[i + 1];
                turn_priority[i + 1] = tmp;
            }
            attacker.IsWaiting = true;
            waiting_units++;
            prior_pointer--;
            EndTurn();
        }
        public void Defend()
        {
            ActiveAbl tmp = new ActiveAbl(1.4, 1, 1, 1, 0, 0, true, false, "def");
            if (move_of_first_army)
            {
                _first[id_of_curent_stack_in_army].TakeActiveAbl(tmp);
            }
            else
            {
                _second[id_of_curent_stack_in_army].TakeActiveAbl(tmp);
            }
            EndTurn();
        }
        public void Attack1(int id_of_target)
        {
            if ((attacker.Type.Ability & Ability.GreatCleave) > 0)
            {
                is_continue = false;
                if (move_of_first_army)
                {
                    for (int i = 0; i < _second._size; i++)
                    {
                        if (i == _second._size - 1)
                            is_continue = true;
                        Attack(i);
                    }
                }
                else
                {
                    for (int i = 0; i < _first._size; i++)
                    {
                        if (i == _first._size - 1)
                            is_continue = true;
                        Attack(i);
                    }
                }
            }
            else
            {
                Attack(id_of_target);
            }

        }
        public void Attack(int id_of_target)
        {
            if (move_of_first_army)
            {
                _second.TakeAttack(attacker, id_of_target);
                if (!((attacker.Type.Ability & Ability.Shooter) > 0) && _second[id_of_target].CounterAttack &&
                    !((_second[id_of_target].Type.Ability & Ability.Shooter) > 0) && !((attacker.Type.Ability & Ability.NoResistance) > 0) 
                    || ((_second[id_of_target].Type.Ability & Ability.Reflection) > 0))
                {
                    _first.TakeAttack(_second[id_of_target], id_of_curent_stack_in_army);
                    _second[id_of_target].CounterAttack = false;
                }                
            }
            else
            {
                _first.TakeAttack(attacker, id_of_target);
                if (!((attacker.Type.Ability & Ability.Shooter) > 0) && _first[id_of_target].CounterAttack &&
                    !((_first[id_of_target].Type.Ability & Ability.Shooter) > 0) && !((attacker.Type.Ability & Ability.NoResistance) > 0)
                    || ((_first[id_of_target].Type.Ability & Ability.Reflection) > 0))
                {
                    _second.TakeAttack(_first[id_of_target], id_of_curent_stack_in_army);
                    _first[id_of_target].CounterAttack = false;
                }
            }
            if (is_continue)
                EndTurn();
        }
        public void Cast1(int id_of_target)
        {
            if (attacker.Type.ActiveAbl.Aoe)
            {
                is_continue = false;
                if (attacker.Type.ActiveAbl.Friendy)
                {
                    if (move_of_first_army)
                    {
                        for (int i = 0; i < _first._size; i++)
                        {
                            if (i == _first._size - 1)
                                is_continue = true;
                            Cast(i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _second._size; i++)
                        {
                            if (i == _second._size - 1)
                                is_continue = true;
                            Cast(i);
                        }
                    }
                }
                else
                {
                    if (move_of_first_army)
                    {
                        for (int i = 0; i < _second._size; i++)
                        {
                            if (i == _second._size - 1)
                                is_continue = true;
                            Cast(i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _first._size; i++)
                        {
                            if (i == _first._size - 1)
                                is_continue = true;
                            Cast(i);
                        }
                    }
                }
            }
            else
            {
                Cast(id_of_target);
            }
        }
        public void Cast(int id_of_target)
        {
            attacker.ActiveAblStatusChanged();
            int army_id;
            bool is_waiting;
            double initiative;
            double tmp_initiative;
            if (attacker.Type.ActiveAbl.Friendy)
            {
                if (move_of_first_army)
                { 
                    _first[id_of_target].TakeActiveAbl(attacker.Type.ActiveAbl);
                    army_id = 1;
                    is_waiting = _first[id_of_target].IsWaiting;
                    initiative = _first[id_of_target].CurrentInitiative;
                }
                else
                {
                    _second[id_of_target].TakeActiveAbl(attacker.Type.ActiveAbl);
                    army_id = 2;
                    is_waiting = _second[id_of_target].IsWaiting;
                    initiative = _second[id_of_target].CurrentInitiative;
                }
            } else
            {
                if (move_of_first_army)
                {
                    _second[id_of_target].TakeActiveAbl(attacker.Type.ActiveAbl);
                    army_id = 2;
                    is_waiting = _second[id_of_target].IsWaiting;
                    initiative = _second[id_of_target].CurrentInitiative;
                }
                else
                {
                    _first[id_of_target].TakeActiveAbl(attacker.Type.ActiveAbl);
                    army_id = 1;
                    is_waiting = _first[id_of_target].IsWaiting;
                    initiative = _first[id_of_target].CurrentInitiative;
                }
            }
            if (attacker.Type.ActiveAbl.Initiative > 1)
            {
                for (int i = 0; i < _first._size + _second._size; i++)
                {
                    if (turn_priority[i].First == id_of_target &&
                        turn_priority[i].Second == army_id)
                    {
                        if (!is_waiting && i > prior_pointer + 1)
                        {
                            for (int j = i; j > prior_pointer + 1; j--)
                            {
                                if (turn_priority[j - 1].Second == 1)
                                {
                                    tmp_initiative = _first[turn_priority[j - 1].First].CurrentInitiative;
                                }
                                else
                                {
                                    tmp_initiative = _second[turn_priority[j - 1].First].CurrentInitiative;

                                }
                                if (initiative > tmp_initiative)
                                {
                                    Pair<int, int> tmp = turn_priority[j];
                                    turn_priority[j] = turn_priority[j - 1];
                                    turn_priority[j - 1] = tmp;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        if (is_waiting && i > prior_pointer + 1)
                        {
                            for (int j = i; j < _first._size + _second._size - 1; j++)
                            {
                                if (turn_priority[j + 1].Second == 1)
                                {
                                    tmp_initiative = _first[turn_priority[j + 1].First].CurrentInitiative;
                                }
                                else
                                {
                                    tmp_initiative = _second[turn_priority[j + 1].First].CurrentInitiative;

                                }
                                if (initiative > tmp_initiative)
                                {
                                    Pair<int, int> tmp = turn_priority[j];
                                    turn_priority[j] = turn_priority[j + 1];
                                    turn_priority[j + 1] = tmp;
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
            if (attacker.Type.ActiveAbl.Initiative < 1)
            {
                for (int i = 0; i < _first._size + _second._size; i++)
                {
                    if (turn_priority[i].First == id_of_target &&
                        turn_priority[i].Second == army_id)
                    {
                        if (!is_waiting && i > prior_pointer)
                        {
                            for (int j = i; j < _first._size + _second._size - 1 - waiting_units; j++)
                            {
                                if (turn_priority[j + 1].Second == 1)
                                {
                                    tmp_initiative = _first[turn_priority[j + 1].First].CurrentInitiative;
                                }
                                else
                                {
                                    tmp_initiative = _second[turn_priority[j + 1].First].CurrentInitiative;

                                }
                                if (initiative < tmp_initiative)
                                {
                                    Pair<int, int> tmp = turn_priority[j];
                                    turn_priority[j] = turn_priority[j + 1];
                                    turn_priority[j + 1] = tmp;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        if (is_waiting && i > prior_pointer + 1)
                        {
                            for (int j = i; j > prior_pointer + 1; j--)
                            {
                                if (turn_priority[j - 1].Second == 1)
                                {
                                    tmp_initiative = _first[turn_priority[j - 1].First].CurrentInitiative;
                                }
                                else
                                {
                                    tmp_initiative = _second[turn_priority[j - 1].First].CurrentInitiative;

                                }
                                if (initiative < tmp_initiative)
                                {
                                    Pair<int, int> tmp = turn_priority[j];
                                    turn_priority[j] = turn_priority[j - 1];
                                    turn_priority[j - 1] = tmp;
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
            if (is_continue)
                EndTurn();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
