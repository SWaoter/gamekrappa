using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Engine.ViewModels;

namespace GameOOP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public enum Actions
    {
        Attack,
        Cast
    }
    public partial class MainWindow : Window
    {
        private GameSession _gameSession;
        private Actions _current;
        public MainWindow(int [] arm1Id, int[] arm1Count,
                          int [] arm2Id, int[] arm2Count)
        {
            InitializeComponent();
            _gameSession = new GameSession(arm1Id, arm1Count, arm2Id, arm2Count);
            DataContext = _gameSession;
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            _current = Actions.Attack;
        }

        private void Cast_Click(object sender, RoutedEventArgs e)
        {
            _current = Actions.Cast;
        }

        private void Wait_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.Wait();
        }

        private void Defend_Click(object sender, RoutedEventArgs e)
        {
            _gameSession.Defend();
        }

        private void Stack11_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(0);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(0);
            }         
        }

        private void Stack12_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(1);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(1);
            }
        }

        private void Stack13_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(2);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(2);
            }
        }

        private void Stack14_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(3);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(3);
            }
        }

        private void Stack15_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(4);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(4);
            }
        }

        private void Stack16_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(5);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(5);
            }
        }

        private void Stack21_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(0);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(0);
            }
        }

        private void Stack22_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(1);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(1);
            }
        }

        private void Stack23_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(2);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(2);
            }
        }

        private void Stack24_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(3);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(3);
            }
        }

        private void Stack25_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(4);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(4);
            }
        }

        private void Stack26_Click(object sender, RoutedEventArgs e)
        {
            if (_current == Actions.Attack)
            {
                _gameSession.Attack(5);
            }
            if (_current == Actions.Cast)
            {
                _gameSession.Cast(5);
            }
        }
    }
}
