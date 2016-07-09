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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
          
        }

      

        private void playImg_Tapped(object sender, TappedRoutedEventArgs e) {
            playImg.Visibility = Visibility.Collapsed;
            pauseImg.Visibility = Visibility.Visible;
            myTimer.Start();
        }

        private void pauseImg_Tapped(object sender, TappedRoutedEventArgs e) {
            playImg.Visibility = Visibility.Visible;
            pauseImg.Visibility = Visibility.Collapsed;
            myTimer.Stop();
        }
    }
}
