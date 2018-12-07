using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace wpf_homework4_2_Stili
{
    public partial class MainWindow : Window
    {
        List<Rectangle> rectangles = new List<Rectangle>();
        Style noStyle, compStyle, playerStyle;

        public MainWindow()
        {
            InitializeComponent();
            InitializeRectangles();
            InitializeStyles();
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (((Rectangle)sender).Style == noStyle)
            {
                ((Rectangle)sender).Style = playerStyle;
                if (Check_result())
                {
                    AllRadioButtonsEnable(true);
                    return;
                }
                else
                    CompStep();
                if (Check_result())
                    AllRadioButtonsEnable(true);
            }
        }

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            ClearAllSymbols();
            AllRadioButtonsEnable(false);
            if (!isPlayerFirst.IsChecked.Value)
                CompStep();
        }

        private void InitializeRectangles()
        {
            foreach (var item in MainGrid.Children)
            {
                if (item is Rectangle RE)
                    rectangles.Add(RE);
            }
        }

        private void InitializeStyles()
        {
            noStyle = (Style)Resources["RectStyle"];
            compStyle = (Style)Resources["RectStyle_O"];
            playerStyle = (Style)Resources["RectStyle_X"];
        }

        private void ClearAllSymbols()
        {
            foreach (Rectangle item in rectangles)
                item.Style = noStyle;
        }

        private void AllRadioButtonsEnable(bool turnOn)
        {
            groupLevel.IsEnabled = turnOn;
            groupFirst.IsEnabled = turnOn;
        }

        private void CompStep()
        {
            if (isHardLevelOn.IsChecked.Value)
                CompHardStep();
            else
                CompSimpleStep();
        }

        private void CompSimpleStep()
        {
            Random random = new Random();
            int i = random.Next() % 9;

            if (rectangles[i].Style == noStyle)
                rectangles[i].Style = compStyle;
            else
            {
                for (int j = 0; j < 9; j++)
                {
                    i = (i + 1) % 9;
                    if (rectangles[i].Style == noStyle)
                    {
                        rectangles[i].Style = compStyle;
                        return;
                    }
                }
            }
        }

        private void CompHardStep()
        {
            //Старт гри
            if (rectangles[4].Style == noStyle)
                rectangles[4].Style = compStyle;

            //останній пепеможний хід
            else if (rectangles[0].Style == compStyle && rectangles[1].Style == compStyle && rectangles[2].Style == noStyle)
                rectangles[2].Style = compStyle;
            else if (rectangles[0].Style == compStyle && rectangles[2].Style == compStyle && rectangles[1].Style == noStyle)
                rectangles[1].Style = compStyle;
            else if (rectangles[1].Style == compStyle && rectangles[2].Style == compStyle && rectangles[0].Style == noStyle)
                rectangles[0].Style = compStyle;

            else if (rectangles[3].Style == compStyle && rectangles[4].Style == compStyle && rectangles[5].Style == noStyle)
                rectangles[5].Style = compStyle;
            else if (rectangles[3].Style == compStyle && rectangles[5].Style == compStyle && rectangles[4].Style == noStyle)
                rectangles[4].Style = compStyle;
            else if (rectangles[4].Style == compStyle && rectangles[5].Style == compStyle && rectangles[3].Style == noStyle)
                rectangles[3].Style = compStyle;

            else if (rectangles[6].Style == compStyle && rectangles[7].Style == compStyle && rectangles[8].Style == noStyle)
                rectangles[8].Style = compStyle;
            else if (rectangles[6].Style == compStyle && rectangles[8].Style == compStyle && rectangles[7].Style == noStyle)
                rectangles[7].Style = compStyle;
            else if (rectangles[7].Style == compStyle && rectangles[8].Style == compStyle && rectangles[6].Style == noStyle)
                rectangles[6].Style = compStyle;

            else if (rectangles[0].Style == compStyle && rectangles[3].Style == compStyle && rectangles[6].Style == noStyle)
                rectangles[6].Style = compStyle;
            else if (rectangles[0].Style == compStyle && rectangles[6].Style == compStyle && rectangles[3].Style == noStyle)
                rectangles[3].Style = compStyle;
            else if (rectangles[3].Style == compStyle && rectangles[6].Style == compStyle && rectangles[0].Style == noStyle)
                rectangles[0].Style = compStyle;

            else if (rectangles[1].Style == compStyle && rectangles[4].Style == compStyle && rectangles[7].Style == noStyle)
                rectangles[7].Style = compStyle;
            else if (rectangles[1].Style == compStyle && rectangles[7].Style == compStyle && rectangles[4].Style == noStyle)
                rectangles[4].Style = compStyle;
            else if (rectangles[4].Style == compStyle && rectangles[7].Style == compStyle && rectangles[1].Style == noStyle)
                rectangles[1].Style = compStyle;

            else if (rectangles[2].Style == compStyle && rectangles[5].Style == compStyle && rectangles[8].Style == noStyle)
                rectangles[8].Style = compStyle;
            else if (rectangles[2].Style == compStyle && rectangles[8].Style == compStyle && rectangles[5].Style == noStyle)
                rectangles[5].Style = compStyle;
            else if (rectangles[5].Style == compStyle && rectangles[8].Style == compStyle && rectangles[2].Style == noStyle)
                rectangles[2].Style = compStyle;

            else if (rectangles[0].Style == compStyle && rectangles[4].Style == compStyle && rectangles[8].Style == noStyle)
                rectangles[8].Style = compStyle;
            else if (rectangles[0].Style == compStyle && rectangles[8].Style == compStyle && rectangles[4].Style == noStyle)
                rectangles[4].Style = compStyle;
            else if (rectangles[4].Style == compStyle && rectangles[8].Style == compStyle && rectangles[0].Style == noStyle)
                rectangles[0].Style = compStyle;

            else if (rectangles[2].Style == compStyle && rectangles[4].Style == compStyle && rectangles[6].Style == noStyle)
                rectangles[6].Style = compStyle;
            else if (rectangles[2].Style == compStyle && rectangles[6].Style == compStyle && rectangles[4].Style == noStyle)
                rectangles[4].Style = compStyle;
            else if (rectangles[4].Style == compStyle && rectangles[6].Style == compStyle && rectangles[2].Style == noStyle)
                rectangles[2].Style = compStyle;

            //запобігання перемозі суперника
            else if (rectangles[0].Style == playerStyle && rectangles[1].Style == playerStyle && rectangles[2].Style == noStyle)
                rectangles[2].Style = compStyle;
            else if (rectangles[0].Style == playerStyle && rectangles[2].Style == playerStyle && rectangles[1].Style == noStyle)
                rectangles[1].Style = compStyle;
            else if (rectangles[1].Style == playerStyle && rectangles[2].Style == playerStyle && rectangles[0].Style == noStyle)
                rectangles[0].Style = compStyle;

            else if (rectangles[3].Style == playerStyle && rectangles[4].Style == playerStyle && rectangles[5].Style == noStyle)
                rectangles[5].Style = compStyle;
            else if (rectangles[3].Style == playerStyle && rectangles[5].Style == playerStyle && rectangles[4].Style == noStyle)
                rectangles[4].Style = compStyle;
            else if (rectangles[4].Style == playerStyle && rectangles[5].Style == playerStyle && rectangles[3].Style == noStyle)
                rectangles[3].Style = compStyle;

            else if (rectangles[6].Style == playerStyle && rectangles[7].Style == playerStyle && rectangles[8].Style == noStyle)
                rectangles[8].Style = compStyle;
            else if (rectangles[6].Style == playerStyle && rectangles[8].Style == playerStyle && rectangles[7].Style == noStyle)
                rectangles[7].Style = compStyle;
            else if (rectangles[7].Style == playerStyle && rectangles[8].Style == playerStyle && rectangles[6].Style == noStyle)
                rectangles[6].Style = compStyle;

            else if (rectangles[0].Style == playerStyle && rectangles[3].Style == playerStyle && rectangles[6].Style == noStyle)
                rectangles[6].Style = compStyle;
            else if (rectangles[0].Style == playerStyle && rectangles[6].Style == playerStyle && rectangles[3].Style == noStyle)
                rectangles[3].Style = compStyle;
            else if (rectangles[3].Style == playerStyle && rectangles[6].Style == playerStyle && rectangles[0].Style == noStyle)
                rectangles[0].Style = compStyle;

            else if (rectangles[1].Style == playerStyle && rectangles[4].Style == playerStyle && rectangles[7].Style == noStyle)
                rectangles[7].Style = compStyle;
            else if (rectangles[1].Style == playerStyle && rectangles[7].Style == playerStyle && rectangles[4].Style == noStyle)
                rectangles[4].Style = compStyle;
            else if (rectangles[4].Style == playerStyle && rectangles[7].Style == playerStyle && rectangles[1].Style == noStyle)
                rectangles[1].Style = compStyle;

            else if (rectangles[2].Style == playerStyle && rectangles[5].Style == playerStyle && rectangles[8].Style == noStyle)
                rectangles[8].Style = compStyle;
            else if (rectangles[2].Style == playerStyle && rectangles[8].Style == playerStyle && rectangles[5].Style == noStyle)
                rectangles[5].Style = compStyle;
            else if (rectangles[5].Style == playerStyle && rectangles[8].Style == playerStyle && rectangles[2].Style == noStyle)
                rectangles[2].Style = compStyle;

            else if (rectangles[0].Style == playerStyle && rectangles[4].Style == playerStyle && rectangles[8].Style == noStyle)
                rectangles[8].Style = compStyle;
            else if (rectangles[0].Style == playerStyle && rectangles[8].Style == playerStyle && rectangles[4].Style == noStyle)
                rectangles[4].Style = compStyle;
            else if (rectangles[4].Style == playerStyle && rectangles[8].Style == playerStyle && rectangles[0].Style == noStyle)
                rectangles[0].Style = compStyle;

            else if (rectangles[2].Style == playerStyle && rectangles[4].Style == playerStyle && rectangles[6].Style == noStyle)
                rectangles[6].Style = compStyle;
            else if (rectangles[2].Style == playerStyle && rectangles[6].Style == playerStyle && rectangles[4].Style == noStyle)
                rectangles[4].Style = compStyle;
            else if (rectangles[4].Style == playerStyle && rectangles[6].Style == playerStyle && rectangles[2].Style == noStyle)
                rectangles[2].Style = compStyle;
            else
                CompSimpleStep();
        }

        private bool Check_result()
        {
            if (IsWin(playerStyle))
	        {
                MessageBox.Show("Вітаємо!\nВи перемогли", "Перемога", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
	        else if (IsWin(compStyle))
	        {
                MessageBox.Show("Цього разу переміг комп'ютер", "Програш", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
	        else if (NobodyWon())
            {
                MessageBox.Show("Цього разу ніхто не переміг", "Нічия", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
                return false;
        }

        private bool IsWin(Style style)
        {
            if ((rectangles[0].Style == style && rectangles[1].Style == style && rectangles[2].Style == style) ||
                (rectangles[3].Style == style && rectangles[4].Style == style && rectangles[5].Style == style) ||
                (rectangles[6].Style == style && rectangles[7].Style == style && rectangles[8].Style == style) ||
                (rectangles[0].Style == style && rectangles[3].Style == style && rectangles[6].Style == style) ||
                (rectangles[1].Style == style && rectangles[4].Style == style && rectangles[7].Style == style) ||
                (rectangles[2].Style == style && rectangles[5].Style == style && rectangles[8].Style == style) ||
                (rectangles[0].Style == style && rectangles[4].Style == style && rectangles[8].Style == style) ||
                (rectangles[2].Style == style && rectangles[4].Style == style && rectangles[6].Style == style))
                return true;
            else
                return false;
        }

        private bool NobodyWon()
        {
            foreach (var item in rectangles)
            {
                if (item.Style == noStyle)
                    return false;
            }
            return true;
        }
    }
}