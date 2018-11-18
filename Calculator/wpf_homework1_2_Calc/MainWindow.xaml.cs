using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace wpf_homework1_2_Calc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetKey(((Button)sender).Content.ToString());
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) // добавляет возможность ввода параметров с клавиатуры
        {
            switch (e.Key.ToString())
            {
                case "D1":
                case "NumPad1":
                    GetKey("1");
                    break;
                case "D2":
                case "NumPad2":
                    GetKey("2");
                    break;
                case "D3":
                case "NumPad3":
                    GetKey("3");
                    break;
                case "D4":
                case "NumPad4":
                    GetKey("4");
                    break;
                case "D5":
                case "NumPad5":
                    GetKey("5");
                    break;
                case "D6":
                case "NumPad6":
                    GetKey("6");
                    break;
                case "D7":
                case "NumPad7":
                    GetKey("7");
                    break;
                case "D8":
                case "NumPad8":
                    GetKey("8");
                    break;
                case "D9":
                case "NumPad9":
                    GetKey("9");
                    break;
                case "D0":
                case "NumPad0":
                    GetKey("0");
                    break;

                case "Add":
                case "OemPlus":
                    GetKey("+");
                    break;
                case "OemMinus":
                case "Subtract":
                    GetKey("-");
                    break;
                case "Multiply":
                    GetKey("*");
                    break;
                case "Divide":
                    GetKey("/");
                    break;

                case "Delete":
                    GetKey("CE");
                    break;
                case "Escape":
                    GetKey("C");
                    break;
                case "Back":
                    GetKey("<");
                    break;
                case "Decimal":
                case "OemComma":
                case "OemPeriod":
                    GetKey(",");
                    break;

                case "Return":
                    GetKey("=");
                    break;
            }
        }

        private void GetKey(string key) // обработка кнопок или клавиш
        {
            switch (key)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    if (curNumber.Text.Length == 1 && curNumber.Text.StartsWith("0"))
                        curNumber.Text = key != "0" ? key : "0";
                    else
                        curNumber.Text += key;
                    break;

                case "+":
                case "-":
                case "*":
                case "/":
                    if (!string.IsNullOrEmpty(curNumber.Text))
                        history.Text += curNumber.Text + " " + key + " ";
                    else if (!string.IsNullOrEmpty(history.Text))
                        history.Text = history.Text.Substring(0, history.Text.Length - 2) + key + " ";
                    curNumber.Text = "";
                    break;

                case "CE":
                    curNumber.Text = "";
                    break;
                case "C":
                    curNumber.Text = "";
                    history.Text = "";
                    break;
                case "<":
                    if (curNumber.Text.Length > 0)
                        curNumber.Text = curNumber.Text.Remove(curNumber.Text.Length - 1);
                    break;
                case ",":
                    if (curNumber.Text.Length > 0 && !curNumber.Text.Contains(","))
                        curNumber.Text += key;
                    break;

                case "=":
                    if (!string.IsNullOrEmpty(curNumber.Text))
                        history.Text += curNumber.Text;
                    else
                        break;
                    curNumber.Text = Counting(history.Text).ToString();
                    history.Text = "";
                    break;
            }
        }

        private decimal Counting(string str) // подсчёт введённого выражения, в том числе из многих параметров
        {
            string expression = history.Text.Replace(" ", ""); // удаляем пробелы

            if (str.StartsWith("-")) // если первое число отрицательное - добавляем 0 для предотвращения ошибки парсинга строки
                expression = expression.Insert(0, "0");

            List<char> symbols = new List<char>(); // создаём и заполняем массив арифметических знаков
            foreach (char i in str)
            {
                if (i == '+' || i == '-' || i == '*' || i == '/')
                    symbols.Add(i);
            }

            List<string> strNumbers = new List<string>(); // создаём и заполняем массив для хранения чисел
            char[] signs = { '+', '-', '*', '/' };
            strNumbers.AddRange(expression.Split(signs));
            List<decimal> numbers = new List<decimal>(); // парсим массив строк в числа

            try
            {
                foreach (string item in strNumbers)
                    numbers.Add(decimal.Parse(item));

                for (int i = 0; i < symbols.Count; ) // в первую очередь умножаем и делим
                {
                    if (symbols[i] == '*')
                    {
                        numbers[i] = numbers[i] * numbers[i + 1];
                        numbers.RemoveAt(i + 1);
                        symbols.RemoveAt(i);
                    }
                    else if (symbols[i] == '/')
                    {
                        numbers[i] = numbers[i] / numbers[i + 1];
                        numbers.RemoveAt(i + 1);
                        symbols.RemoveAt(i);
                    }
                    else
                        i++;
                }

                if (symbols.Contains('+') || symbols.Contains('-')) // если нужно ещё прибавить или отнять - делаем это
                {
                    decimal result = numbers[0];
                    for (int i = 0; i < symbols.Count; i++)
                    {
                        if (symbols[i] == '+')
                            result += numbers[i + 1];
                        else if (symbols[i] == '-')
                            result -= numbers[i + 1];
                    }
                    return result;
                }
                else
                    return numbers[0];

            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return 0;
        }
    }
}