using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lista_2
{
    public partial class zad3_1 : Window
    {
        int placed = 0;
        Game GameLogic => (Game)DataContext;
        public zad3_1()
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
            if (GameLogic.P2Ready) return;
            Button b = (Button)sender;
            int i = (int)b.Tag;
            int r = i / Game.Size, c = i % Game.Size;

            if (GameLogic.Player2Ships[r, c])
            {
                GameLogic.Player2Ships[r, c] = false;
                placed--;
                b.ClearValue(Button.BackgroundProperty);
            }
            else if (placed < Game.ShipCount)
            {
                GameLogic.Player2Ships[r, c] = true;
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
            GameLogic.P2Ready = true;
            BtnReady.IsEnabled = false;

            if (GameLogic.P1Ready)
            {
                GameLogic.Status = "Gra rozpoczęta! Tura: Gracz 1";
            }
        }

        private void EnemyCell_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b.Content != null) return;

            int i = (int)b.Tag;
            int r = i / Game.Size, c = i % Game.Size;

            bool hit = GameLogic.Shoot(2, r, c);
            if (hit)
            {
                b.Content = "X";
                b.Background = Brushes.Crimson;
                b.Foreground = Brushes.White;

                if (GameLogic.P1ShipsLeft == 0)
                {
                    EnemyGrid.IsEnabled = false;
                    MessageBox.Show("Gratulacje! Wygrywa Gracz 2!", "Koniec gry");
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