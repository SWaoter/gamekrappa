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
using System.Windows.Media.Animation;
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
            PathGeometry test = new PathGeometry();
            DoubleAnimationUsingPath test2 = new DoubleAnimationUsingPath();
            test2.PathGeometry = test;
            
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

        private void Stack_Click(object sender, RoutedEventArgs e)
        {
            switch (_current)
            {
                case Actions.Attack:
                    _gameSession.Attack(int.Parse(((Button)sender).Name.Substring(6,1)) - 1);
                    break;
            }
        }
    }
}
