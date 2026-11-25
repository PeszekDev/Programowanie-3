using System.Windows;

namespace Lista_2
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            
            //  Zadanie 1 calc
            // new zad1().Show();

            // Zadanie 2 tictactoe
            // new Zad2().Show();

            //Zadanie 3 statki
            Game game = new Game(); // jedna wspólna gra dla obu okien
            zad3 okno1 = new zad3();
            zad3_1 okno2 = new zad3_1();
            okno1.DataContext = game;
            okno2.DataContext = game;
            okno1.Show();
            okno2.Show();
        }
    }
}