using System.ComponentModel;

namespace Lista_2
{
    public class Game : INotifyPropertyChanged
    {
        public const int Size = 10;
        public const int ShipCount = 5;

        public bool[,] Player1Ships = new bool[Size, Size];
        public bool[,] Player2Ships = new bool[Size, Size];

        public bool P1Ready = false;
        public bool P2Ready = false;

        private int _turn = 1;
        public int Turn
        {
            get { return _turn; }
            set
            {
                _turn = value;
                OnPropertyChanged("Turn");
            }
        }

        public int P1ShipsLeft = ShipCount;
        public int P2ShipsLeft = ShipCount;

        private string _status = "Ustawcie po 5 statków i kliknijcie Gotowe";
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool Shoot(int shooter, int r, int c)
        {
            if (P1ShipsLeft == 0 || P2ShipsLeft == 0) return false;
            if (Turn != shooter) return false;

            bool hit = false;
            if (shooter == 1)
            {
                hit = Player2Ships[r, c];
                if (hit)
                {
                    P2ShipsLeft--;
                    Status = P2ShipsLeft == 0 ? "Koniec gry! Wygrywa Gracz 1!" : "Trafiony! Gracz 1 strzela dalej.";
                }
                else
                {
                    Turn = 2;
                    Status = "Pudło! Tura: Gracz 2";
                }
            }
            else
            {
                hit = Player1Ships[r, c];
                if (hit)
                {
                    P1ShipsLeft--;
                    Status = P1ShipsLeft == 0 ? "Koniec gry! Wygrywa Gracz 2!" : "Trafiony! Gracz 2 strzela dalej.";
                }
                else
                {
                    Turn = 1;
                    Status = "Pudło! Tura: Gracz 1";
                }
            }
            return hit;
        }
    }
}