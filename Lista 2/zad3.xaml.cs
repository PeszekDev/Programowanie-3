using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Lista_2
{
    public partial class zad3 : Window
    {
        const int Player = 0; // to okno jest Graczem 1

        Game GameLogic => (Game)DataContext;

        public zad3()
        {
            InitializeComponent();
            CreateBoard(MyGrid, "Board1", MyCell_Click);     // własna plansza
            CreateBoard(EnemyGrid, "Shots1", EnemyCell_Click); // strzały w przeciwnika
        }

        // zwykły Grid: wiersze i kolumny dodajemy w pętli,
        // a każdy przycisk dostaje bindingi do swojego pola (Cell) w Game
        private void CreateBoard(Grid grid, string boardName, RoutedEventHandler click)
        {
            for (int k = 0; k < Game.Size; k++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < Game.Size * Game.Size; i++)
            {
                Button b = new Button { FontSize = 12, Tag = i, Margin = new Thickness(1) };
                b.SetBinding(Button.BackgroundProperty, new Binding(boardName + "[" + i + "].Color"));
                b.SetBinding(Button.ContentProperty, new Binding(boardName + "[" + i + "].Text"));
                b.Click += click;

                Grid.SetRow(b, i / Game.Size);
                Grid.SetColumn(b, i % Game.Size);
                grid.Children.Add(b);
            }
        }

        private void MyCell_Click(object sender, RoutedEventArgs e)
        {
            int i = (int)((Button)sender).Tag;
            GameLogic.PlaceShip(Player, i);
        }

        private void EnemyCell_Click(object sender, RoutedEventArgs e)
        {
            int i = (int)((Button)sender).Tag;
            GameLogic.Shoot(Player, i);
        }

        private void BtnReady_Click(object sender, RoutedEventArgs e)
        {
            GameLogic.SetReady(Player);
        }
    }
}
