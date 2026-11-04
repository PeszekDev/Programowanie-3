using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lista_2
{
    public partial class zad3 : Window
    {
        int placed = 0;
        Game GameLogic => (Game)DataContext;

        public zad3()
        {
            InitializeComponent();
            CreateBoard(MyGrid, MyCell_Click);
            CreateBoard(EnemyGrid, EnemyCell_Click);
            EnemyGrid.IsEnabled = false;
        }

        private void CreateBoard(System.Windows.Controls.Primitives.UniformGrid grid, RoutedEventHandler click)
        {
            for (int i = 0; i < Game.Size * Game.Size; i++)
            {
                Button b = new Button { FontSize = 12, Tag = i, Margin = new Thickness(1) };
                b.Click += click;
                grid.Children.Add(b);
            }
        }

        private void MyCell_Click(object sender, RoutedEventArgs e)
        {
            if (GameLogic.P1Ready) return;
            Button b = (Button)sender;
            int i = (int)b.Tag;
            int r = i / Game.Size, c = i % Game.Size;

            if (GameLogic.Player1Ships[r, c])
            {
                GameLogic.Player1Ships[r, c] = false;
                placed--;
                b.ClearValue(Button.BackgroundProperty);
            }
            else if (placed < Game.ShipCount)
            {
                GameLogic.Player1Ships[r, c] = true;
                placed++;
                b.Background = Brushes.SlateGray;
            }
        }

        private void BtnReady_Click(object sender, RoutedEventArgs e)
        {
            if (placed != Game.ShipCount)
            {
                MessageBox.Show($"Ustaw dokładnie {Game.ShipCount} statków!");
                return;
            }
            GameLogic.P1Ready = true;
            BtnReady.IsEnabled = false;

            if (GameLogic.P2Ready)
            {
                EnemyGrid.IsEnabled = true;
                GameLogic.Status = "Gra rozpoczęta! Tura: Gracz 1";
            }
        }

        private void EnemyCell_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b.Content != null) return;

            int i = (int)b.Tag;
            int r = i / Game.Size, c = i % Game.Size;

            bool hit = GameLogic.Shoot(1, r, c);
            if (hit)
            {
                b.Content = "X";
                b.Background = Brushes.Crimson;
                b.Foreground = Brushes.White;

                if (GameLogic.P2ShipsLeft == 0)
                {
                    EnemyGrid.IsEnabled = false;
                    MessageBox.Show("Dobra robota! Wygrywa Gracz 1!", "Koniec gry");
                }
            }
            else
            {
                b.Content = "•";
                b.Background = Brushes.LightGray;
                EnemyGrid.IsEnabled = false;
            }
        }
    }
}