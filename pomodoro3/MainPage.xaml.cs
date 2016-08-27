/*
 * Author: Martyna Łagożna
 * Created: 9.07.2016
 * Last update: 17.07.2016
 */

using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace pomodoro3 {

    public sealed partial class MainPage : Page
    {

        private DispatcherTimer myTimer = new DispatcherTimer();
               
        private int currentSecond = 59;
        private int currentMinute = 24;
        private bool work = true;                       //to check if it is break or work now
        private int maxValueOfBarForBreak = 297;        //length of progress bar
        private int maxValueOfBarForWork = 1476;
        private int maxValueOfBarForLongBreak = 885;
        private int pomodoroCounter = 1;
        
        private List<String> categories = new List<string>();

        //MediaPlayer player = new MediaPlayer();

       

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            myTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);         //count seconds
            myTimer.Tick += MyTimer_Tick;

            setMaximumForProgressBars(maxValueOfBarForWork);        //initial value for this program
        }

        private void MyTimer_Tick(object sender, object e) {
          

            //display the time
            if (currentSecond < 10)
                timeToEnd.Text = currentMinute.ToString() + ":0" + currentSecond.ToString();
            else
                timeToEnd.Text = currentMinute.ToString() + ":" + currentSecond.ToString();


            //increment progress bars
            myProgressBarLower.Value++;
            myProgressBarUpper.Value++;



            currentSecond--;
            if (currentSecond == 0) {
                if(currentMinute == 0) {                                                        //end of counting down
                    myTimer.Stop();

                    if (work) {                                                                 //it was work so now it's time to break
                        if (pomodoroCounter % 4 == 0 + 1 && pomodoroCounter != 1) {             //long break
                            setNewTime(14, 59, false, "15:00");
                            setMaximumForProgressBars(maxValueOfBarForLongBreak);
                        }
                        else {                                                                  //short break
                            setNewTime(4, 59, false, "5:00");
                            setMaximumForProgressBars(maxValueOfBarForBreak);
                        }
                            setPlayAndPauseImages(Visibility.Visible, Visibility.Collapsed);
                            setProgressBarsToZero();
                            it_sYourTextBlock.Text = "It'll be your";
                            pomodoroCounter++;
                            pomodoroTodayNumber.Text = pomodoroCounter.ToString();
                    }
                    else {                                                                     //it was break, now it'll be worktime
                        setNewTime(24, 59, true, "25:00");
                        setPlayAndPauseImages(Visibility.Visible, Visibility.Collapsed);
                        setMaximumForProgressBars(maxValueOfBarForWork);
                        setProgressBarsToZero();
                        it_sYourTextBlock.Text = "It's your";
                    }
                }
                else {
                    currentMinute--;
                    currentSecond = 59;
                }
            }
        }

        private void setProgressBarsToZero() {
            myProgressBarUpper.Value = 0;
            myProgressBarLower.Value = 0;
        }

        private void setMaximumForProgressBars(int max) {
            myProgressBarUpper.Maximum = max;
            myProgressBarLower.Maximum = max;
        }

        private void setNewTime(int min, int sec, bool workStatus, string timeToShow) {
            currentMinute = min;
            currentSecond = sec;
            work = workStatus;
            timeToEnd.Text = timeToShow;
        }

        private void setPlayAndPauseImages(Visibility play, Visibility pause) {
            playImg.Visibility = play;
            pauseImg.Visibility = pause;
        }

        private void playImg_Tapped(object sender, TappedRoutedEventArgs e) {
            setPlayAndPauseImages(Visibility.Collapsed, Visibility.Visible);
            myTimer.Start();

            if (work)
                motivationText.Text = "Stay focused!";
            else
                motivationText.Text = "Relax";

            motivationText.Visibility = Visibility.Visible;


        }

        private void pauseImg_Tapped(object sender, TappedRoutedEventArgs e) {
            setPlayAndPauseImages(Visibility.Visible, Visibility.Collapsed);
            myTimer.Stop();

            if (work)
                motivationText.Text = "Ohh really? It's just 25min ";
            else
                motivationText.Text = "Don't lenghten the break!";
            
           
        }


        //skip button
        private void button_Click(object sender, RoutedEventArgs e) {      
            myTimer.Stop();
            setPlayAndPauseImages(Visibility.Visible, Visibility.Collapsed);
            setProgressBarsToZero();

            if (work) {                                                    //if work set break now
                if (pomodoroCounter % 4 != 0 + 1 || pomodoroCounter == 1) {
                    setNewTime(4, 59, false, "5:00");                      //set the time
                    it_sYourTextBlock.Text = "It'll be your";
                    if (pomodoroCounter == 1)                              //to avoid incorrect showing current number of pomodoros
                        pomodoroCounter++;
                    pomodoroTodayNumber.Text = pomodoroCounter.ToString();
                    setMaximumForProgressBars(maxValueOfBarForBreak);

                }
                else {
                    setNewTime(14, 59, false, "15:00");
                    pomodoroTodayNumber.Text = pomodoroCounter.ToString();
                    it_sYourTextBlock.Text = "It'll be your";
                    setMaximumForProgressBars(maxValueOfBarForLongBreak);
                }
                pomodoroCounter++;
            }
            else {
                setNewTime(24, 59, true, "25:00");
                it_sYourTextBlock.Text = "It's your";
                setMaximumForProgressBars(maxValueOfBarForWork);
            }

            
        }


        // add category button
        //private void button_Click_1(object sender, RoutedEventArgs e) {     

        //    if (addCategoryButton.Content.ToString() != "Click to accept") {
        //        newCategoryTextBox.Visibility = Visibility.Visible;
        //        addCategoryButton.Content = "Click to accept";
        //    }
        //    else {
        //        addCategoryButton.Content = "Add category";
        //        categories.Add(newCategoryTextBox.Text.ToString());

        //        categoryBox.Items.Add(categories[categories.Count-1]);
        //        newCategoryTextBox.Visibility = Visibility.Collapsed;
               
        //    }


        //}

    }
}
