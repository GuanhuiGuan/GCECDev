using System;
using System.Collections.Generic;
using GCECDev.Models;
using PropertyChanged;

using Xamarin.Forms;

namespace GCECDev.Views
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            // Background
            BackgroundColor = Constants.BackgroundColor;
            buttonSignIn.BackgroundColor = Color.White;

            // Texts
            labelUsername.TextColor = Constants.MainTextColor;
            labelPassword.TextColor = Constants.MainTextColor;

            // Icons
            loginIcon.HeightRequest = Constants.LoginIconHeight;
            activitySpinner.IsVisible = false;

            // Logic
            entryUsername.Completed += (s, e) => entryPassword.Focus();
            entryPassword.Completed += SignInProcess;
        }

        public void SignInProcess(object sender, EventArgs args)
        {
            var user = new User(entryUsername.Text, entryPassword.Text);
            var (res, err) = user.Verify();
            if (err != null)
            {
                DisplayAlert("INTERNAL ERROR", err.Message, "Try again");
                return;
            }
            if (res != null)
            {
                DisplayAlert("ACCESS DENIED", res, "Try again");
                return;
            }
            activitySpinner.IsVisible = true;
        }
    }
}
