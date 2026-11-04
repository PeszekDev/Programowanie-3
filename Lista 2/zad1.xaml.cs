using System;
using System.Windows;
using System.Windows.Controls;

namespace Lista_2
{
    public partial class zad1 : Window
    {
        private double firstNumber = 0;
        private string currentOperator = "";
        private bool isNewEntry = true;

        public zad1()
        {
            InitializeComponent();
        }
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                string digit = btn.Content.ToString();

                if (DisplayBox.Text == "0" || isNewEntry)
                {
                    DisplayBox.Text = digit == "," ? "0," : digit;
                    isNewEntry = false;
                }
                else
                {
                    if (digit == "," && DisplayBox.Text.Contains(","))
                        return;

                    DisplayBox.Text += digit;
                }
            }
        }
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (double.TryParse(DisplayBox.Text, out double val))
                {
                    firstNumber = val;
                    currentOperator = btn.Content.ToString();
                    isNewEntry = true;
                }
            }
        }
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(DisplayBox.Text, out double secondNumber))
            {
                double result = 0;

                switch (currentOperator)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "*":
                        result = firstNumber * secondNumber;
                        break;
                    case "/":
                        if (secondNumber != 0)
                        {
                            result = firstNumber / secondNumber;
                        }
                        else
                        {
                            DisplayBox.Text = "Błąd: dzielenie przez 0";
                            isNewEntry = true;
                            return;
                        }
                        break;
                    default:
                        return;
                }

                DisplayBox.Text = result.ToString();
                isNewEntry = true;
                currentOperator = "";
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            DisplayBox.Text = "0";
            firstNumber = 0;
            currentOperator = "";
            isNewEntry = true;
        }
    }
}