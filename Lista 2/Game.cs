using System.ComponentModel;

namespace Lista_2
{
    // Cała logika gry w statki. Okna tylko wołają jej metody w Click.
    public class Game : INotifyPropertyChanged
    {
        public const int Size = 10;      // rozmiar planszy
        public const int ShipCount = 5;  // liczba statków (pojedyncze pola)

        public event PropertyChangedEventHandler PropertyChanged;

        // BoardX = własna plansza gracza X (ze statkami)
        // ShotsX = to, co gracz X wie o planszy przeciwnika (tylko trafienia i pudła)
        public Cell[] Board1 { get; } = CreateCells();
        public Cell[] Shots1 { get; } = CreateCells();
        public Cell[] Board2 { get; } = CreateCells();
        public Cell[] Shots2 { get; } = CreateCells();

        private Cell[][] boards;   // indeks 0 = Gracz 1, 1 = Gracz 2
        private Cell[][] shots;

        private bool[] ready = { false, false };
        private int[] placed = { 0, 0 };
        private int[] shipsLeft = { ShipCount, ShipCount };
        private int turn = 0;
        private bool gameOver = false;

        private string status = "Ustaw 5 statków i kliknij Gotowe";
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
            }
        }

        public Game()
        {
            boards = new Cell[][] { Board1, Board2 };
            shots = new Cell[][] { Shots1, Shots2 };
        }

        private static Cell[] CreateCells()
        {
            Cell[] cells = new Cell[Size * Size];
            for (int i = 0; i < cells.Length; i++)
                cells[i] = new Cell();
            return cells;
        }

        // klik na własnej planszy: postaw / zdejmij statek
        public void PlaceShip(int player, int i)
        {
            if (ready[player]) return;

            Cell c = boards[player][i];
            if (c.State == Cell.Ship)
            {
                c.State = Cell.Empty;
                placed[player]--;
            }
            else if (placed[player] < ShipCount)
            {
                c.State = Cell.Ship;
                placed[player]++;
            }
            Status = "Gracz " + (player + 1) + ": ustawiono " + placed[player] + "/" + ShipCount + " statków";
        }

        // przycisk "Gotowe"
        public void SetReady(int player)
        {
            if (ready[player]) return;

            if (placed[player] != ShipCount)
            {
                Status = "Gracz " + (player + 1) + ": musisz ustawić dokładnie " + ShipCount + " statków!";
                return;
            }

            ready[player] = true;

            if (ready[0] && ready[1])
            {
                turn = 0;
                Status = "Gra się zaczęła! Tura: Gracz 1";
            }
            else
            {
                Status = "Gracz " + (player + 1) + " jest gotowy, czekamy na przeciwnika...";
            }
        }

        // strzał gracza "player" w planszę przeciwnika
        public void Shoot(int player, int i)
        {
            if (!ready[0] || !ready[1]) return; // bitwa jeszcze się nie zaczęła
            if (gameOver) return;
            if (turn != player) return;         // nie twoja tura

            int enemy = 1 - player;
            Cell target = boards[enemy][i];
            if (target.State == Cell.Hit || target.State == Cell.Miss) return; // tu już strzelano

            if (target.State == Cell.Ship)
            {
                target.State = Cell.Hit;
                shots[player][i].State = Cell.Hit;
                shipsLeft[enemy]--;

                if (shipsLeft[enemy] == 0)
                {
                    gameOver = true;
                    Status = "Koniec gry! Wygrywa Gracz " + (player + 1) + "!";
                }
                else
                {
                    Status = "Trafiony! Gracz " + (player + 1) + " strzela dalej";
                }
            }
            else
            {
                target.State = Cell.Miss;
                shots[player][i].State = Cell.Miss;
                turn = enemy;
                Status = "Pudło! Tura: Gracz " + (enemy + 1);
            }
        }
    }
}
