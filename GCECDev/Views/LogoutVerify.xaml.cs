using System;
using System.Collections.Generic;
using GCECDev.MasterDetailViews;
using GCECDev.Models;
using Xamarin.Forms;

namespace GCECDev.Views
{
    public partial class LogoutVerify : ContentPage
    {
        public LogoutVerify()
        {
            InitializeComponent();

            BackgroundColor = Color.GhostWhite;

            footer.Text = Constants.Constants.Footer;
            ButtonVerify.BackgroundColor = Constants.Constants.MainButtonColor;
            ButtonVerify.TextColor = Color.GhostWhite;
            ButtonResend.BackgroundColor = Constants.Constants.MainButtonColor;
            ButtonResend.TextColor = Color.GhostWhite;
            ButtonSubmit.BackgroundColor = Constants.Constants.MainButtonColor;
            ButtonSubmit.TextColor = Color.GhostWhite;
            ButtonLogIn.TextColor = Constants.Constants.MainTextColor;

            EntryVerify.Completed += VerifyProcess;

            EntryPassword.Completed += (s, e) => EntryRePassword.Focus();
            EntryRePassword.Completed += SetPasswordProcessAsync;
        }

        void VerifyProcess(object sender, EventArgs e)
        {
            if (EntryVerify.Text == null || EntryVerify.Text == "")
            {
                DisplayAlert("Access Denied", "Your verification code is incorrect", "Try again");
                return;
            }
            // API call to verify
            DisplayAlert("Code", EntryVerify.Text, "Back");
            EntryPassword.IsEnabled = true;
            EntryRePassword.IsEnabled = true;
        }

        void ResendProcess(object sender, EventArgs e)
        {
            // API call to send email
            DisplayAlert(App.User.GetUsername(), "A new email has been sent", "Back");
        }

        async void SetPasswordProcessAsync(object sender, EventArgs e)
        {
            // Not null and equal to re-entry
            if (EntryPassword.Text == null || EntryPassword.Text.Equals(""))
            {
                await DisplayAlert("Access Denied", "Your password cannot be empty", "Try again");
                return;
            }
            if (!EntryPassword.Text.Equals(EntryRePassword.Text))
            {
                await DisplayAlert("Access Denied", "Your password entry and re-entry must match", "Try again");
                return;
            }

            // API call to set password

            App.User.Password = EntryPassword.Text;
            await Navigation.PushModalAsync(new Root());
        }

        async void ToLogIn(object sender, EventArgs e)
        {
            App.User = new User();
            await Navigation.PushModalAsync(new Login());
        }

        public void ShowPass(object sender, EventArgs args)
        {
            if (EntryPassword.IsPassword)
            {
                EntryPassword.IsPassword = false;
                IconEye.Source = "NoEye";
            }
            else
            {
                EntryPassword.IsPassword = true;
                IconEye.Source = "Eye";
            }
        }

        public void ShowPassRe(object sender, EventArgs args)
        {
            if (EntryRePassword.IsPassword)
            {
                EntryRePassword.IsPassword = false;
                IconEye.Source = "NoEye";
            }
            else
            {
                EntryRePassword.IsPassword = true;
                IconEye.Source = "Eye";
            }
        }
    }
}
