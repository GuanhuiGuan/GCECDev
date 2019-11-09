using System;
namespace GCECDev.Models
{
    public class User
    {
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

        public (string, Exception) Verify()
        {
            if (this.GetUsername().Equals("") || this.GetPassword().Equals(""))
            {
                return ("Username and password cannot be empty", null);
            }
            if (this.GetPassword().Equals("ILLEGAL"))
            {
                return (null, new InvalidProgramException());
            }
            return (null, null);
        }
    }
}
