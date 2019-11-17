using System;
using System.Collections.Generic;
using GCECDev.Models;
using GCECDev.Constants;
using PropertyChanged;

using Xamarin.Forms;
using GCECDev.MasterDetailViews;

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
            BackgroundColor = Constants.Constants.BackgroundColor;
            buttonSignIn.BackgroundColor = Constants.Constants.MainButtonColor;

            // Texts
            labelUsername.TextColor = Constants.Constants.MainTextColor;
            labelPassword.TextColor = Constants.Constants.MainTextColor;
            buttonSignIn.TextColor = Constants.Constants.BackgroundColor;
            buttonSignUp.TextColor = Constants.Constants.MainTextColor;
            buttonLoggedOut.TextColor = Constants.Constants.MainTextColor;
            footer.Text = Constants.Constants.Footer;

            // Icons
            loginIcon.HeightRequest = Constants.Constants.LoginIconHeight;
            activitySpinner.IsVisible = false;
            activitySpinner.Color = Constants.Constants.MainTextColor;

            // Logic
            entryUsername.Completed += (s, e) => entryPassword.Focus();
            entryPassword.Completed += LogInProcess;
        }

        public async void LogInProcess(object sender, EventArgs args)
        {
            try
            {
                activitySpinner.IsVisible = true;

                var user = new User(entryUsername.Text, entryPassword.Text);

                var err = await App.LoginCtrl.LoginAsync(user);

                if (err != null)
                {
                    throw err;
                }

                App.User = user;

                await Navigation.PushModalAsync(new Root());
            }
            catch(Exception err)
            {
                await DisplayAlert("Error occurred when trying to login", err.Message, "Try again");
            }
            finally
            {
                activitySpinner.IsVisible = false;
            }
        }

        public async void LoggedOutProcess(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new LogoutUser());
        }

        public void ShowPass(object sender, EventArgs args)
        {
            if (entryPassword.IsPassword)
            {
                entryPassword.IsPassword = false;
                iconEye.Source = "NoEye";
            }
            else
            {
                entryPassword.IsPassword = true;
                iconEye.Source = "Eye";
            }
        }
    }
}
