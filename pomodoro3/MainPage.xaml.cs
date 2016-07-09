using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace pomodoro3
{
 
    public sealed partial class MainPage : Page
    {

        DispatcherTimer myTimer = new DispatcherTimer();
        
        int currentSecond = 60;
        int currentMinute = 24;
        int barCounter = 0;
        bool work = true;

        
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;


            myTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            myTimer.Tick += MyTimer_Tick;
            
        }

        private void MyTimer_Tick(object sender, object e) {
            
            currentSecond--.ToString();
            if(currentSecond == 0) {
                if (currentMinute == 0) {
                    myTimer.Stop();
                    currentMinute = 25;
                    currentSecond = 00;
                }

                currentMinute--;
                currentSecond = 59;
            }

            if (currentSecond < 10) 
                timeToEnd.Text = currentMinute.ToString() + ":0" + currentSecond.ToString();
            else
                timeToEnd.Text = currentMinute.ToString() +":"+ currentSecond.ToString();

            myProgressBar.Value++;
            myProgressBar_Copy.Value++;
            barCounter++;
            if (barCounter >= 1500) {
                barCounter = 0;
                myProgressBar.Value = 0;
                myProgressBar_Copy.Value = 0;
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
          
        }

      

        private void playImg_Tapped(object sender, TappedRoutedEventArgs e) {
            playImg.Visibility = Visibility.Collapsed;
            pauseImg.Visibility = Visibility.Visible;
            myTimer.Start();
            motivationText.Text = "Stay focused!";
            motivationText.Visibility = Visibility.Visible;
            
            
        }

        private void pauseImg_Tapped(object sender, TappedRoutedEventArgs e) {
            playImg.Visibility = Visibility.Visible;
            pauseImg.Visibility = Visibility.Collapsed;
            myTimer.Stop();
            motivationText.Text = "Ohh really? It's just 25min ";
            
           
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            myTimer.Stop();
            if (work == true) {
                currentMinute = 4;
                currentSecond = 60;
                motivationText.Text = "It's time to break!";
                timeToEnd.Text = "5:00";
                work = false;
                playImg.Visibility = Visibility.Visible;
                pauseImg.Visibility = Visibility.Collapsed;
                myProgressBar.Value = 0;
                myProgressBar_Copy.Value = 0; 

            }
            else {
                currentMinute = 24;
                currentSecond = 60;
                motivationText.Text = "One pomodoro more?";
                timeToEnd.Text = "25:00";
                work = true;
                playImg.Visibility = Visibility.Visible;
                pauseImg.Visibility = Visibility.Collapsed;
                myProgressBar.Value = 0;
                myProgressBar_Copy.Value = 0;
            }
        }
    }
}
