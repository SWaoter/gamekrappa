using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Engine.ViewModels;

namespace GameOOP
{
    /// <summary>
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public ObservableCollection<string> Active;
        public ObservableCollection<string> Passive;
        public Editor()
        {
            InitializeComponent();
            var factory = new FactoryMV();
            Active = new ObservableCollection<string>(factory.GameActiveStrings);
            Passive = new ObservableCollection<string>(factory.GamePassiveStrings);
            _u2.ItemsSource = Passive;
            _u3.ItemsSource = Active;
        }
        private void AddCast(object sender, RoutedEventArgs e)
        {
            bool res = double.TryParse(_a1.Text, out var defense);
            if (!res)
                return;
            res = double.TryParse(_a2.Text, out var attack);
            if (!res)
                return;
            res = double.TryParse(_a3.Text, out var initiative);
            if (!res)
                return;
            res = double.TryParse(_a4.Text, out var damage);
            if (!res)
                return;
            res = double.TryParse(_a5.Text, out var hp);
            if (!res)
                return;
            res = double.TryParse(_a6.Text, out var heal);
            if (!res)
                return;
            res = bool.TryParse(((TextBlock)_a7.SelectedItem).Text, out var friendly);
            if (!res)
                return;
            res = bool.TryParse(((TextBlock)_a8.SelectedItem).Text, out var aoe);
            if (!res)
                return;
            var name = _a9.Text;
            ExpandDataMV.AddCast(defense, attack, initiative, damage, hp, heal, friendly, aoe, name);
            Editor refresh = new Editor();
            refresh.Show();
            this.Close();
        }

        private void AddUnit(object sender, RoutedEventArgs e)
        {
            int attack = 10;
            int defense = 10;
            int minDamage = 10;
            int maxDamage = 10;
            double initiative = 10;
            bool res = (int.TryParse(_u4.Text, out var health) &&
                        int.TryParse(_u5.Text, out attack) &&
                        int.TryParse(_u6.Text, out defense) &&
                        int.TryParse(_u7.Text, out minDamage) &&
                        int.TryParse(_u8.Text, out maxDamage) &&
                        double.TryParse(_u10.Text, out initiative));
            if (!res)
                return;
            var type = _u1.Text;
            var imageName = _u9.Text;
            var passive = (string)_u2.SelectedItem;
            var ability = (string)_u3.SelectedItem;
            ExpandDataMV.AddUnit(type, passive, ability, health,
                                 attack, defense, minDamage, maxDamage, imageName, initiative);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartMenu ret = new StartMenu();
            ret.Show();
            this.Close();
        }
    }
}
