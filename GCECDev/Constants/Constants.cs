using System;
using Xamarin.Forms;

namespace GCECDev.Constants
{
    public static class Constants
    {

        public static string Version = "0.1.5";

        public static string Footer = string.Format("Copyright WGCF 2020 | Ver. {0}", Version);

        public static bool IsDev = true;

        // public static Color BackgroundColor = Color.FromRgb(58, 153, 215);
        public static Color BackgroundColor = Color.GhostWhite;
        public static Color MainTextColor = Color.DarkSlateGray;

        public static Color MainButtonColor = Color.FromHex("#ff6666");

        public static int LoginIconHeight = 120;

        internal static string fontFamily = Device.OnPlatform("MarkerFelt-Thin", "Droid Sans Mono", "Comic Sans MS");

        public static double SendMinInterval = 0.5;

        // SQLite
        public static string SQLiteUsersDBFilename = "Users.db";

        // Azure Mysql DB Server
        public static string AzureMysqlServer = "https://gcecdatabaseapi.azurewebsites.net/";
    }
}
