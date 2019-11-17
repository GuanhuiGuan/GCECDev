using System;
using System.Collections.Generic;
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
            if (EntryUsername.Text == null || EntryUsername.Text.Equals(""))
            {
                await DisplayAlert("Access Denied", "Your email cannot be empty", "Try again");
                return;
            }

            if (App.User == null )
            {
                App.User = new User();
            }
            App.User.Username = EntryUsername.Text;

            // API call to send email
            await DisplayAlert("Info", EntryUsername.Text, "Jump to Verify page");

            await Navigation.PushModalAsync(new LogoutVerify());
        }

        async void ToLogIn(object sender, EventArgs e)
        {
            App.User = new User();
            await Navigation.PushModalAsync(new Login());
        }
    }
}
