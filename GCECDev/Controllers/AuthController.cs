using System;
using GCECDev.Models;
using GCECDev.Views;

using Xamarin.Forms;

namespace GCECDev.Controllers
{
    public class AuthController
    {
        public AuthController()
        {
        }

        public bool VerifyStatus()
        {
            User user = App.User;
            if (user == null || user.GetUsername().Equals(""))
            {
                return false;
            }
            return true;
        }
    }
}
