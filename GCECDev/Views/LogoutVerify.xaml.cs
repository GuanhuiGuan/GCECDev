using System;
using System.Collections.Generic;
using System.Diagnostics;
using GCECDev.Controllers;
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

        async void VerifyProcess(object sender, EventArgs e)
        {
            VCLoadingSign.IsRunning = true;
            if (EntryVerify.Text == null || EntryVerify.Text == "")
            {
                VCLoadingSign.IsRunning = false;
                DisplayAlert("Access Denied", "Your verification code is empty", "Try again");
                return;
            }

            VerifyCodeRestAPI vcClient = new VerifyCodeRestAPI();
            try
            {
                VerifyCode vc = await vcClient.Get(App.User.GetUsername());
                if (vc.ExpireTimestamp < new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
                {
                    VCLoadingSign.IsRunning = false;
                    DisplayAlert("Code Expired", "Please tap on 'Resend Email'", "Close");
                    return;
                }
                if (vc.Code != EntryVerify.Text)
                {
                    VCLoadingSign.IsRunning = false;
                    DisplayAlert("Invalid Code", "Your verification code is incorrect", "Try again");
                    return;
                }
            }
            catch (Exception err)
            {
                VCLoadingSign.IsRunning = false;
                Debug.WriteLine("Error verifying code: {0}", err.GetBaseException());
                DisplayAlert("Server Error", err.Message, "Tap 'Verify' again");
                return;
            }

            VCLoadingSign.IsRunning = false;
            DisplayAlert("Success!", "Your email is verified. Please set your password", "Close");
            EntryPassword.IsEnabled = true;
            EntryRePassword.IsEnabled = true;
        }

        async void ResendProcess(object sender, EventArgs e)
        {
            VCLoadingSign.IsRunning = true;
            if (DateTime.UtcNow < App.CanSendAgain)
            {
                VCLoadingSign.IsRunning = false;
                DisplayAlert("Send Denied",
                    string.Format("Please resend {0} minutes after your previous request", Constants.Constants.SendMinInterval), "Close");
                return;
            }
            LoginController lc = new LoginController();
            try
            {
                bool sendRes = await lc.SendEmail(App.User.GetUsername());
                if (!sendRes)
                {
                    VCLoadingSign.IsRunning = false;
                    DisplayAlert("Resent Failure", "Please try again", "Close");
                    return;
                }
            }
            catch (Exception err)
            {
                VCLoadingSign.IsRunning = false;
                Debug.WriteLine("Error resending email: {0}", err.GetBaseException());
                DisplayAlert("Server Error", err.Message, "Close");
                return;
            }
            VCLoadingSign.IsRunning = false;
            DisplayAlert(App.User.GetUsername(), "A new email has been sent", "Close");
        }

        async void SetPasswordProcessAsync(object sender, EventArgs e)
        {
            PasswordLoadingSign.IsRunning = true;
            // Not null and equal to re-entry
            if (EntryPassword.Text == null || EntryPassword.Text.Equals(""))
            {
                PasswordLoadingSign.IsRunning = false;
                await DisplayAlert("Access Denied", "Your password cannot be empty", "Try again");
                return;
            }
            if (!EntryPassword.Text.Equals(EntryRePassword.Text))
            {
                PasswordLoadingSign.IsRunning = false;
                await DisplayAlert("Access Denied", "Your password entry and re-entry must match", "Try again");
                return;
            }

            User user = new User { Username = App.User.GetUsername(), Password = EntryPassword.Text };

            LoginController lc = new LoginController();
            try
            {
                bool res = await lc.SetPassword(user, App.IsNewUser);
                if (!res)
                {
                    PasswordLoadingSign.IsRunning = false;
                    await DisplayAlert("Database Error", "Please try again", "Close");
                    return;
                }
            }
            catch (Exception err)
            {
                PasswordLoadingSign.IsRunning = false;
                Debug.WriteLine("Error setting password: {0}", err.GetBaseException());
                await DisplayAlert("Network Error", err.Message, "Tap 'Submit' again");
                return;
            }

            DisplayAlert("Success", "Now please log in with your new credentials", "To Log In");
            await Navigation.PushModalAsync(new Login());
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
