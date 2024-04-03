﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MATCHJEU
{
    using System.Windows.Threading;
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");
            if(matchesFound==8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " -play again?";
            }

        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐓" , "🐓",
                "🐁" , "🐁" ,
                "🐅" , "🐅" ,
                "🐅" ,"🐅",
                "🐄" , "🐄" ,
                "🐍" , "🐍" ,
                "🐿️" , "🐿️" ,
                "🐘", "🐘",
            };
                Random random = new Random();
                foreach (TextBlock textblock in mainGrid.Children.OfType<TextBlock>())
                {
                    if (textblock.Name != "timeTextBlock")
                    {
                        textblock.Visibility = Visibility.Visible;
                        int index = random.Next(animalEmoji.Count);
                        string nextEmoji = animalEmoji[index];
                        textblock.Text = nextEmoji;
                        animalEmoji.RemoveAt(index);
                    }
                }

                timer.Start();
                tenthOfSecondsElapsed = 0;
                matchesFound = 0;

        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if(findingMatch==false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if(textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
                
                

            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimerTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchesFound ==8)
            {
                SetUpGame();
            }
        }

       
    }
    
    
    
   

    
}
