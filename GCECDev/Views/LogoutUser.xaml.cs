using System;
using System.Collections.Generic;
using System.Diagnostics;
using GCECDev.Controllers;
using GCECDev.Models;
using Xamarin.Forms;

namespace GCECDev.Views
{
    public partial class LogoutUser : ContentPage
    {
        public LogoutUser()
        {
            InitializeComponent();

            BackgroundColor = Color.GhostWhite;

            footer.Text = Constants.Constants.Footer;
            ButtonSubmit.BackgroundColor = Constants.Constants.MainButtonColor;
            ButtonSubmit.TextColor = Color.GhostWhite;
            ButtonLogIn.TextColor = Constants.Constants.MainTextColor;
                
            EntryUsername.Completed += SendEmailProcess;
        }

        async void SendEmailProcess(object sender, EventArgs e)
        {
            LoadingSign.IsRunning = true;
            if (EntryUsername.Text == null || EntryUsername.Text.Equals(""))
            {
                DisplayAlert("Access Denied", "Your email cannot be empty", "Try again");
                LoadingSign.IsRunning = false;
                return;
            }

            App.User = new User { Username = EntryUsername.Text };

            LoginController lc = new LoginController();
            try
            {
                // Verify email
                var valid = await lc.CheckEmail(App.User.GetUsername(), App.IsNewUser);

                if (!valid)
                {
                    string title, msg;
                    if (App.IsNewUser)
                    {
                        title = "Email Exists";
                        msg = "Your email has been registered";
                    }
                    else
                    {
                        title = "Email Not Found";
                        msg = "Your account cannot be found";
                    }
                    DisplayAlert(title, msg, "Close");
                    LoadingSign.IsRunning = false;
                    return;
                }

                // Send email
                var emailSentRes = await lc.SendEmail(App.User.GetUsername());
                if (!emailSentRes)
                {
                    DisplayAlert("Server Error", "Please try again", "Close");
                    LoadingSign.IsRunning = false;
                    return;
                }
                App.CanSendAgain = DateTime.UtcNow.AddMinutes(Constants.Constants.SendMinInterval);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Failed sending email or connection problem", err.GetBaseException());
                DisplayAlert("Server Error", "Please check your email", "Try again");
                LoadingSign.IsRunning = false;
                return;
            }

            await Navigation.PushModalAsync(new LogoutVerify());
            LoadingSign.IsRunning = false;
        }

        async void ToLogIn(object sender, EventArgs e)
        {
            App.User = new User();
            await Navigation.PushModalAsync(new Login());
        }
    }
}
