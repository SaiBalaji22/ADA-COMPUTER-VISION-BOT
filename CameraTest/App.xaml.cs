using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.TextToSpeech;

namespace CameraTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            { BarBackgroundColor = Color.Violet,
            BarTextColor=Color.White,
           
              
            };
        }

        protected override void OnStart()
        {
            
            CrossTextToSpeech.Current.Speak("WELCOME... IAM ADA..I WAS PROGRAMMED BY SAI BALAJI.I CAN DESCRIBE ABOUT OBJECTS.PICK A PHOTO FROM YOUR ALBUM TO DESCRIBE ABOUT IT");
            Application.Current.MainPage.DisplayAlert("WELCOME", "IAM ADA. I WAS PROGRAMMED BY SAI BALAJI.I CAN DESCRIBE ABOUT OBJECTS.PICK A PHOTO FROM YOUR ALBUM TO DESCRIBE ABOUT IT", "OK");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
