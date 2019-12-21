using System.Windows;

namespace GameOOP
{
    /// <summary>
    /// Логика взаимодействия для StartMenu.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void StartBattle_Click(object sender, RoutedEventArgs e)
        {
            PumpBatlle battle = new PumpBatlle();
            battle.Show();
            this.Close();
        }

        private void Editor_Click(object sender, RoutedEventArgs e)
        {
            Editor editor = new Editor();
            editor.Show();
            this.Close();
        }
    }
}
