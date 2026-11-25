using System.Windows;
using System.Windows.Controls;

namespace Lista_2
{
    public partial class Zad2 : Window
    {
        // logika gry siedzi w osobnej klasie TicTacToe (ustawionej jako DataContext w XAML)
        TicTacToe Game => (TicTacToe)DataContext;

        public Zad2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Game.Move(int.Parse((string)b.Tag));
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Game.Reset();
        }
    }
}
