using System.ComponentModel;
using System.Windows.Media;

namespace Lista_2
{
    // Jedno pole planszy. Zmiana State odświeża przycisk przez binding.
    public class Cell : INotifyPropertyChanged
    {
        public const int Empty = 0;
        public const int Ship = 1;
        public const int Miss = 2;
        public const int Hit = 3;

        public event PropertyChangedEventHandler PropertyChanged;

        private int state = Empty;
        public int State
        {
            get { return state; }
            set
            {
                state = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
            }
        }

        // kolor przycisku (z tego korzysta binding Background)
        public Brush Color
        {
            get
            {
                if (state == Hit) return Brushes.Red;
                if (state == Miss) return Brushes.LightBlue;
                if (state == Ship) return Brushes.Gray;
                return Brushes.LightGray;
            }
        }

        // napis na przycisku (z tego korzysta binding Content)
        public string Text
        {
            get
            {
                if (state == Hit) return "X";
                if (state == Miss) return "•";
                return "";
            }
        }
    }
}
