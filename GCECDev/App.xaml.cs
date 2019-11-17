using System;
using System.IO;
using GCECDev.Controllers;
using GCECDev.Data;
using GCECDev.MasterDetailViews;
using GCECDev.Models;
using GCECDev.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GCECDev
{
    public partial class App : Application
    {
        public static LoginController LoginCtrl;
        public static AuthController AuthCtrl;

        public static User User { get; set; }
        public static Token Token { get; set; }

        public static LoginController NewLoginCtrl()
        {
            if (LoginCtrl == null) {
                LoginCtrl = new LoginController();
            }
            return LoginCtrl;
        }

        public App()
        {
            InitializeComponent();

            User = null;

            LoginCtrl = NewLoginCtrl();

            MainPage = LoginCtrl.FindCurrentUser() ? new Root() : (Page)new Login();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            LoginCtrl = NewLoginCtrl();
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
