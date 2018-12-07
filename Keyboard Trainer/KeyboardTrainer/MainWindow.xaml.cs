using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainer
{
    internal sealed partial class MainWindow : Window
    {
        Dictionary<int, string> lowerCase = new Dictionary<int, string>(48);
        Dictionary<int, string> upperCase = new Dictionary<int, string>(48);
        List<TextBlock> pressedKeys = new List<TextBlock>();
        bool isUppercase = Keyboard.Modifiers == ModifierKeys.Shift || Console.CapsLock;
        DateTime startTyping = new DateTime();

        public MainWindow()
        {
            InitializeComponent();
            InitializeLowerCaseDictionary();
            InitializeUpperCaseDictionary();
            if (isUppercase)
                ChangeLetterSize();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int keyEnum = KeyInterop.VirtualKeyFromKey(e.Key);
            IsUpperCaseChanged();
            SelectPressedKey(keyEnum);
            if (PrintSymbol(keyEnum, isUppercase) && IsErrorLetter())
                failsNumber.Text = (int.Parse(failsNumber.Text) + 1).ToString();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            IsUpperCaseChanged();
            DeselectPressedKeys();
            CalculateSpeed();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            CreateExampleString();
            EnableSomeControls(false);
            startTyping = DateTime.Now;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            EnableSomeControls(true);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            difficultyValue.Text = difficultySlider.Value.ToString("00");
        }

        private void OnlyText_Click(object sender, RoutedEventArgs e)
        {
            if (onlyText.IsChecked.Value)
                difficultySlider.Maximum = 52;
            else
                difficultySlider.Maximum = 95;
        }

        private bool PrintSymbol(int keyEnum, bool isUppercase)
        {
            if (startButton.IsEnabled || exampleLine.Text.Length == 0)
            {
                EnableSomeControls(true);
                return false;
            }

            if (upperCase.ContainsKey(keyEnum))
            {
                if (isUppercase)
                    userLine.Text += upperCase[keyEnum];
                else
                    userLine.Text += lowerCase[keyEnum];

                exampleLineGreen.Text += exampleLine.Text[0];
                exampleLine.Text = exampleLine.Text.Remove(0, 1);

                return true;
            }
            return false;
        }

        private void InitializeLowerCaseDictionary()
        {
            lowerCase.Add(32, " ");

            for (int i = 48; i <= 57; i++)
                lowerCase.Add(i, Convert.ToChar(i).ToString());

            for (int i = 65; i <= 90; i++)
                lowerCase.Add(i, Convert.ToChar(i).ToString().ToLower());

            lowerCase.Add(186, ";");
            lowerCase.Add(187, "=");
            lowerCase.Add(188, ",");
            lowerCase.Add(189, "-");
            lowerCase.Add(190, ".");
            lowerCase.Add(191, "/");
            lowerCase.Add(192, "`");
            lowerCase.Add(219, "[");
            lowerCase.Add(220, "\\");
            lowerCase.Add(221, "]");
            lowerCase.Add(222, "'");
        }

        private void InitializeUpperCaseDictionary()
        {
            upperCase.Add(32, " ");

            upperCase.Add(48, ")");
            upperCase.Add(49, "!");
            upperCase.Add(50, "@");
            upperCase.Add(51, "#");
            upperCase.Add(52, "$");
            upperCase.Add(53, "%");
            upperCase.Add(54, "^");
            upperCase.Add(55, "&");
            upperCase.Add(56, "*");
            upperCase.Add(57, "(");

            for (int i = 65; i <= 90; i++)
                upperCase.Add(i, Convert.ToChar(i).ToString());

            upperCase.Add(186, ":");
            upperCase.Add(187, "+");
            upperCase.Add(188, "<");
            upperCase.Add(189, "_");
            upperCase.Add(190, ">");
            upperCase.Add(191, "?");
            upperCase.Add(192, "~");
            upperCase.Add(219, "{");
            upperCase.Add(220, "|");
            upperCase.Add(221, "}");
            upperCase.Add(222, "\"");
        }

        private void SelectPressedKey(int keyEnum)
        {
            TextBlock pressedKey = (TextBlock)FindName("key" + keyEnum.ToString());
            if (pressedKey != null)
            {
                pressedKey.Foreground = new SolidColorBrush(Colors.Red);
                pressedKey.FontWeight = FontWeights.Bold;
                pressedKeys.Add(pressedKey);
            }
        }

        private void DeselectPressedKeys()
        {
            foreach (var item in pressedKeys)
            {
                item.Foreground = new SolidColorBrush(Colors.Black);
                item.FontWeight = FontWeights.Normal;
            }
        }

        private void ChangeLetterSize()
        {
            TextBlock keyForChange = null;

            if (isUppercase)
            {
                foreach (KeyValuePair<int, string> keyValue in upperCase)
                {
                    keyForChange = (TextBlock)FindName("key" + keyValue.Key.ToString());
                    keyForChange.Text = keyValue.Value;
                }
            }
            else
            {
                foreach (KeyValuePair<int, string> keyValue in lowerCase)
                {
                    keyForChange = (TextBlock)FindName("key" + keyValue.Key.ToString());
                    keyForChange.Text = keyValue.Value;
                }
            }
        }

        private void IsUpperCaseChanged()
        {
            if (isUppercase != (Keyboard.Modifiers == ModifierKeys.Shift || Console.CapsLock))
            {
                isUppercase = !isUppercase;
                ChangeLetterSize();
                key32.Text = "Space";
            }
        }

        private void CreateExampleString()
        {
            StringBuilder exampleText = new StringBuilder(65);
            Random random = new Random();

            if (onlyText.IsChecked.Value)
            {
                int randNum = 0;
                for (int i = 0; i < 65; i++)
                {
                    randNum = random.Next(65, 65 + (int)difficultySlider.Value);
                    if (randNum > 90 && randNum < 97)
                        randNum += 6;
                    exampleText.Append(Convert.ToChar(randNum).ToString());
                }
            }
            else
            {
                for (int i = 0; i < 65; i++)
                    exampleText.Append(Convert.ToChar(random.Next(32, 32 + (int)difficultySlider.Value)).ToString());
            }

            exampleLineGreen.Text = "";
            if (caseSensitive.IsChecked.Value)
                exampleLine.Text = exampleText.ToString();
            else
                exampleLine.Text = exampleText.ToString().ToLower();
        }

        private bool IsErrorLetter()
        {
            if (userLine.Text.Length > 0 && exampleLine.Text.Length > 0)
            {
                int positionOfCurrentSymbol = userLine.Text.Length - 1;
                string LetterFormExampleString = exampleLineGreen.Text[positionOfCurrentSymbol].ToString();
                string lastLetterFormUserString = userLine.Text[positionOfCurrentSymbol].ToString();

                if (!caseSensitive.IsChecked.Value)
                    lastLetterFormUserString = lastLetterFormUserString.ToLower();
                if (LetterFormExampleString != lastLetterFormUserString)
                    return true;
            }
            return false;
        }

        private void EnableSomeControls(bool isStopped)
        {
            startButton.IsEnabled = isStopped;
            caseSensitive.IsEnabled = isStopped;
            onlyText.IsEnabled = isStopped;
            difficultySlider.IsEnabled = isStopped;
            stopButton.IsEnabled = !isStopped;
            if (!isStopped)
            {
                failsNumber.Text = "0";
                speedTyping.Text = "0";
                userLine.Text = "";
            }
        }

        private void CalculateSpeed()
        {
            if (!startButton.IsEnabled)
            {
                double totalSeconds = (DateTime.Now - startTyping).TotalSeconds;
                int symbols = userLine.Text.Length - int.Parse(failsNumber.Text);
                speedTyping.Text = ( 60 / totalSeconds * symbols).ToString("N0");
            }
        }
    }
}