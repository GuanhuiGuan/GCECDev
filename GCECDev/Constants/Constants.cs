using System;
using Xamarin.Forms;

namespace GCECDev.Constants
{
    public static class Constants
    {

        public static bool IsDev = true;

        // public static Color BackgroundColor = Color.FromRgb(58, 153, 215);
        public static Color BackgroundColor = Color.GhostWhite;
        public static Color MainTextColor = Color.DarkSlateGray;

        public static Color MainButtonColor = Color.FromHex("#ff6666");

        public static int LoginIconHeight = 120;

        // SQLite
        public static string SQLiteUsersDBFilename = "Users.db";

        // Azure Mysql DB Server
        public static string AzureMysqlServer = "https://gcecdatabaseapi.azurewebsites.net/";
    }
}
