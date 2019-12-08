using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using Engine.ViewModels;

namespace GameOOP
{
    /// <summary>
    /// Логика взаимодействия для PumpBatlle.xaml
    /// </summary>
    public partial class PumpBatlle : Window
    {
        private ObservableCollection<string> _units;
        private ObservableCollection<ListBoxInfo> _toShow;
        private ObservableCollection<ListBoxInfo> _toShow2;
        private FactoryMV _factory;
        public PumpBatlle()
        {
            InitializeComponent();
            _factory = new FactoryMV();
            _units = new ObservableCollection<string>(_factory.GameUnitsStrings);
            Unitslist.ItemsSource = _units;
            Unitslist2.ItemsSource = _units;
            test123.Text = "0";
            test1232.Text = "0";
            _toShow = new ObservableCollection<ListBoxInfo>();
            _toShow2 = new ObservableCollection<ListBoxInfo>();
            Data.ItemsSource = _toShow;
            Data2.ItemsSource = _toShow2;
        }

        public int GetId(string pattern)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i] == pattern)
                    return i;
            }

            return 0;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int number;
            bool success = int.TryParse(test123.Text, out number);
            if (!success || number <= 0)
                return;
            ListBoxInfo tmp = new ListBoxInfo((string)Unitslist.SelectedItem, number);
            if (_toShow.Count > 5)
               return;
            _toShow.Add(tmp);
        }
        private void AddButton_Click2(object sender, RoutedEventArgs e)
        {
            int number;
            bool success = int.TryParse(test1232.Text, out number);
            if (!success || number <= 0)
                return;
            ListBoxInfo tmp = new ListBoxInfo((string)Unitslist2.SelectedItem, number);
            if (_toShow2.Count > 5)
                return;
            _toShow2.Add(tmp);
        }

        private void Start123_Click(object sender, RoutedEventArgs e)
        {
            int [] arm1Id = new int[_toShow.Count];
            int[] arm1Count = new int[_toShow.Count];
            int[] arm2Id = new int[_toShow2.Count];
            int[] arm2Count = new int[_toShow2.Count];
            for (int i = 0; i < _toShow.Count; i++)
            {
                arm1Id[i] = GetId(_toShow[i].Type);
                arm1Count[i] = _toShow[i].Count;
            }
            for (int i = 0; i < _toShow2.Count; i++)
            {
                arm2Id[i] = GetId(_toShow2[i].Type);
                arm2Count[i] = _toShow2[i].Count;
            }
            MainWindow Battle = new MainWindow(arm1Id, arm1Count, arm2Id, arm2Count);
            Battle.Show();
            this.Close();
        }
    }

    class ListBoxInfo
    {
        public string Type { get; private set; }
        public int Count { get; private set; }
        private string string_def;
        public ListBoxInfo(string type, int count)
        {
            Type = type;
            Count = count;
            string_def = $"{Type}  {Count}";
        }

        public override string ToString()
        {
            return string_def;
        }
    }
}

