using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Lista_2
{
    // Logika gry kółko i krzyżyk (bez żadnych okien i przycisków)
    public class TicTacToe : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // 9 pól planszy: "", "X" albo "O" (przyciski mają to zbindowane)
        public ObservableCollection<string> Cells { get; } = new ObservableCollection<string>();

        private bool xTurn = true;
        private bool gameOver = false;

        private string status = "Tura: X";
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
            }
        }

        public TicTacToe()
        {
            for (int i = 0; i < 9; i++)
                Cells.Add("");
        }

        public void Move(int i)
        {
            if (gameOver || Cells[i] != "") return; // koniec gry albo pole zajęte

            Cells[i] = xTurn ? "X" : "O";

            if (CheckWin())
            {
                gameOver = true;
                Status = "Wygrywa " + Cells[i] + "!";
            }
            else if (IsFull())
            {
                gameOver = true;
                Status = "Remis!";
            }
            else
            {
                xTurn = !xTurn;
                Status = "Tura: " + (xTurn ? "X" : "O");
            }
        }

        public void Reset()
        {
            for (int i = 0; i < 9; i++)
                Cells[i] = "";
            xTurn = true;
            gameOver = false;
            Status = "Tura: X";
        }

        private bool IsFull()
        {
            for (int i = 0; i < 9; i++)
                if (Cells[i] == "") return false;
            return true;
        }

        private bool CheckWin()
        {
            int[,] lines =
            {
                {0,1,2}, {3,4,5}, {6,7,8}, // wiersze
                {0,3,6}, {1,4,7}, {2,5,8}, // kolumny
                {0,4,8}, {2,4,6}           // przekątne1
            };

            for (int i = 0; i < 8; i++)
            {
                string a = Cells[lines[i, 0]];
                if (a != "" && a == Cells[lines[i, 1]] && a == Cells[lines[i, 2]])
                    return true;
            }
            return false;
        }
    }
}
