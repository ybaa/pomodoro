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
               
        private int currentSecond = 60;
        private int currentMinute = 24;
        private int barCounter = 0;
        private bool work = true;           //to check is it is break or work now
        private int maxValueOfBarForBreak = 297;        //length of progress bar
        private int maxValueOfBarForWork = 1474;
        private int maxValueOfBarForLongBreak = 890;
        private int pomodoroCounter = 1;

        private List<String> categories = new List<string>();

        //MediaPlayer player = new MediaPlayer();

       

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;


            myTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);         //count seconds
            myTimer.Tick += MyTimer_Tick;



        }

        private void MyTimer_Tick(object sender, object e) {

            currentSecond--;//.ToString();
            if(currentSecond == 0) {            //end of cycle
                if (currentMinute == 0) {
                    myTimer.Stop();

                    if (work)
                        //endCountdown(5, 0, false);
                        endCountdown(25, 0, true);
                    else {
                        if (pomodoroCounter % 4 == 0 + 1 && pomodoroCounter != 1)
                            endCountdown(15, 0, false);
                        else {
                            endCountdown(5, 0, false);
                            //SystemSounds.Beep.play();
                            //SoundPlayer player = new SoundPlayer("Assets/finish.wav");
                          
                            
                        }


                    }
                }
                else {
                    currentMinute--;
                    currentSecond = 59;
                }
            }

            if (currentSecond < 10)         //display
                timeToEnd.Text = currentMinute.ToString() + ":0" + currentSecond.ToString();
            else
                timeToEnd.Text = currentMinute.ToString() +":"+ currentSecond.ToString();

            myProgressBar.Value++;
            myProgressBar_Copy.Value++;
            barCounter++;

            if (work) {
                if (barCounter >= maxValueOfBarForWork) {
                    setItAfterTheEndOfCounting(false);
                    pomodoroCounter++;
                    pomodoroTodayNumber.Text = pomodoroCounter.ToString();
                }
            }
            else {
                if(barCounter >= maxValueOfBarForBreak) 
                    setItAfterTheEndOfCounting(true);
                
            }


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
          
        }

      

        private void playImg_Tapped(object sender, TappedRoutedEventArgs e) {
            playImg.Visibility = Visibility.Collapsed;
            pauseImg.Visibility = Visibility.Visible;
            myTimer.Start();

            if (work == true)
                motivationText.Text = "Stay focused!";
            else
                motivationText.Text = "Relax";

            motivationText.Visibility = Visibility.Visible;

            //mediaElement media = new MediaElement();
           // media.AutoPlay = false;
            //media.LoadedBehavior = MediaState.Manual;
            //media.Source = new Uri("Assets/finish.wav", UriKind.Relative);
            //media.Play();
            

        }

        private void pauseImg_Tapped(object sender, TappedRoutedEventArgs e) {
            playImg.Visibility = Visibility.Visible;
            pauseImg.Visibility = Visibility.Collapsed;
            myTimer.Stop();

            if (work == true)
                motivationText.Text = "Ohh really? It's just 25min ";
            else
                motivationText.Text = "Don't lenghten the break!";
            
           
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            myTimer.Stop();
            if (work == true) {
                if (pomodoroCounter % 4 != 0 +1 || pomodoroCounter == 1) {
                    setItAfterSkipIsPressed(4, 60, "It's time to break!", false, "5:00", maxValueOfBarForBreak);
                   
                    pomodoroTodayNumber.Text = pomodoroCounter.ToString();
                }
                else {
                    setItAfterSkipIsPressed(14, 60, "It's time to long break!", false, "15:00", maxValueOfBarForLongBreak);
                    pomodoroTodayNumber.Text = pomodoroCounter.ToString();
                }
                pomodoroCounter++;

            }
            else {
                setItAfterSkipIsPressed(24, 60, "One pomodoro more?", true, "25:00", maxValueOfBarForWork);
            }
        }


        private void setItAfterSkipIsPressed(int min, int sec, String motivationTextLocal, bool workStatus, String timeToShow, int maxValueOfBar) {
            currentMinute = min;
            currentSecond = sec;
            motivationText.Text = motivationTextLocal;
            work = workStatus;
            timeToEnd.Text = timeToShow;
            myProgressBar.Maximum = maxValueOfBar;
            myProgressBar_Copy.Maximum = maxValueOfBar;
            playImg.Visibility = Visibility.Visible;
            pauseImg.Visibility = Visibility.Collapsed;
            myProgressBar.Value = 0;
            myProgressBar_Copy.Value = 0;

        }
        private void setItAfterTheEndOfCounting(bool workStatus) {
            barCounter = 0;
            myProgressBar.Value = 0;
            myProgressBar_Copy.Value = 0;
           // playImg.Visibility = Visibility.Collapsed;
           // pauseImg.Visibility = Visibility.Visible;
            work = workStatus;
        }
        private void endCountdown(int min, int sec, bool workStatus) {
          
            currentMinute = min;
            currentSecond = sec;
            work = workStatus;
            //myProgressBar.Value = 0;
            //myProgressBar_Copy.Value = 0;
            playImg.Visibility = Visibility.Visible;
            pauseImg.Visibility = Visibility.Collapsed;
            timeToEnd.Text = currentMinute.ToString() + ":0" + currentSecond.ToString();
        }

        private void categoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void button_Click_1(object sender, RoutedEventArgs e) {

            if (addCategoryButton.Content.ToString() != "Click to accept") {
                newCategoryTextBox.Visibility = Visibility.Visible;
                addCategoryButton.Content = "Click to accept";
            }
            else {
                addCategoryButton.Content = "Add category";
                categories.Add(newCategoryTextBox.Text.ToString());

                categoryBox.Items.Add(categories[categories.Count-1]);
                newCategoryTextBox.Visibility = Visibility.Collapsed;
               
            }


        }


        //private void playSound(string path) {
        //    System.Media.SoundPlayer player =
        //        new System.Media.SoundPlayer();
        //    player.SoundLocation = path;
        //    player.Load();
        //    player.Play();
        //}

        private void newCategoryTextBox_TextChanged(object sender, TextChangedEventArgs e) {
          
        }
    }
}
