using System;
using SQLite;

namespace GCECDev.Models
{
    public class User
    {
        public string Id { get; set; }
        [PrimaryKey]
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {
        }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string GetUsername()
        {
            if (this.Username != null)
            {
                return this.Username;
            }
            return "";
        }

        public string GetPassword()
        {
            if (this.Password != null)
            {
                return this.Password;
            }
            return "";
        }


        //public (string, Exception) Verify()
        //{
        //    if (!CheckCompleted())
        //    {
        //        return ("Username and password cannot be empty", null);
        //    }

        //    var userFromDB = App.UserDB.GetUser(GetUsername());
        //    if (userFromDB == null)
        //    {
        //        return ("Username is not found", null);
        //    }

        //    if (userFromDB.GetPassword().Equals(GetPassword()))
        //    {
        //        return ("Incorrect password", null);
        //    }
            
        //    return (null, null);
        //}


        //public (string, Exception) Register()
        //{
        //    if (!CheckCompleted())
        //    {
        //        return ("Username and password cannot be empty", null);
        //    }

        //    if (App.UserDB.GetUser(GetUsername()) != null)
        //    {
        //        return ("Username has been registered", null);
        //    }

        //    var res = App.UserDB.SaveUser(this);
        //    return (res.ToString(), null);
        //}


        public bool CheckCompleted()
        {
            return !GetUsername().Equals("") && !GetPassword().Equals("");
        }
    }
}
