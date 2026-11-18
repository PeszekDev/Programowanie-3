using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Lista_2
{
    public class TurnColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int tura)
            {
                if (tura == 1) return Brushes.DodgerBlue; 
                if (tura == 2) return Brushes.SeaGreen;   
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}