using System.Windows;
using System.Windows.Controls;

namespace Lista_2
{
    /// <summary>
    /// Logika interakcji dla klasy Zad1.xaml
    /// </summary>
   public partial class Zad1 : Window
    {
        double firstValue = 0;
        string currentOperator = "";

        public Zad1()
        {
            InitializeComponent();
        }


        private void Number_Click(object sender, RoutedEventArgs e)
        {
            double firstValue = 0;
            string currentOperator = "";

            Button button = (Button)sender;
            string digit = button.Content.ToString();

            if (DisplayBox.Text == "0")
            {
                DisplayBox.Text = digit;
            }
            else
            {
                DisplayBox.Text += digit;
            }
        }


        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            firstValue = double.Parse(DisplayBox.Text);
            currentOperator = button.Content.ToString();
            DisplayBox.Text = "";
        }


        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            double secondValue = double.Parse(DisplayBox.Text);
            double result = 0;

            switch (currentOperator)
            {
                case "+": result = firstValue + secondValue; break;
                case "-": result = firstValue - secondValue; break;
                case "*": result = firstValue * secondValue; break;
                case "/":
                    if (secondValue != 0) result = firstValue / secondValue;
                    else { MessageBox.Show("nie można dzielić przez zero"); return; }
                    break;
            }

            DisplayBox.Text = result.ToString();
        }


        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            DisplayBox.Text = "0";
            firstValue = 0;
            currentOperator = "";
        }
    }
}
