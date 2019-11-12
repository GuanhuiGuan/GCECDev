using System;
using Xamarin.Forms;

namespace GCECDev.Constants
{
    public static class Constants
    {

        public static bool IsDev = true;

        public static Color BackgroundColor = Color.FromRgb(58, 153, 215);
        public static Color MainTextColor = Color.White;

        public static int LoginIconHeight = 120;

        // SQLite
        public static string SQLiteUsersDBFilename = "Users.db";

        // Azure Mysql DB Server
        public static string AzureMysqlServer = "https://gcecdatabaseapi.azurewebsites.net/";
    }
}
