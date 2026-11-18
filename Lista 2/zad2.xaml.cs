using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Lista_2
{
    public partial class Zad2 : Window
    {
        private bool xTurn = true; 

        public Zad2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Content != null) return; 

            btn.Content = xTurn ? "X" : "O";

            if (CheckWin())
            {
                MessageBox.Show($"Wygrywa {btn.Content}!");
                Reset();
            }
            else if (BoardGrid.Children.Cast<Button>().All(b => b.Content != null))
            {
                MessageBox.Show("Remis!");
                Reset();
            }
            else
            {
                xTurn = !xTurn;
            }
        }

        private bool CheckWin()
        {
            string[] c = BoardGrid.Children.Cast<Button>()
                                  .Select(b => b.Content as string).ToArray();

            int[][] lines =
            {
                new[] {0,1,2}, new[] {3,4,5}, new[] {6,7,8}, // wiersze
                new[] {0,3,6}, new[] {1,4,7}, new[] {2,5,8}, // kolumny
                new[] {0,4,8}, new[] {2,4,6}                 // przekątne
            };

            return lines.Any(l => c[l[0]] != null && c[l[0]] == c[l[1]] && c[l[1]] == c[l[2]]);
        }

        private void Reset()
        {
            foreach (Button b in BoardGrid.Children)
                b.Content = null;
            xTurn = true;
        }
    }
}