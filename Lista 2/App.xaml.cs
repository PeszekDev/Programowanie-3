using System.Windows;

namespace Lista_2
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
           // new zad1().Show();
            //new Zad2().Show();

            Game sharedGame = new Game();

            zad3 okno1 = new zad3();
            zad3_1 okno2 = new zad3_1();

            okno1.DataContext = sharedGame;
            okno2.DataContext = sharedGame;

            sharedGame.PropertyChanged += (s, args) =>
            {
                if (args.PropertyName == "Status")
                {
                    if (sharedGame.Turn == 1 && sharedGame.P1Ready && sharedGame.P2Ready)
                        okno1.EnemyGrid.IsEnabled = true;
                    else if (sharedGame.Turn == 2 && sharedGame.P1Ready && sharedGame.P2Ready)
                        okno2.EnemyGrid.IsEnabled = true;
                }
            };

            okno1.Left = 30;
            okno1.Top = 100;
            okno2.Left = 910;
            okno2.Top = 100;

            okno1.Show();
            okno2.Show();
        }
    }
}