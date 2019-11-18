using System;
using System.Text;
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

        // GetUPString returns a serialized JSON containing username and password
        public string GetUPString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"username\": \"");
            builder.Append(Username);
            builder.Append("\", ");
            builder.Append("\"password\": \"");
            builder.Append(Password);
            builder.Append("\"}");
            return builder.ToString();
        }

        public bool CheckCompleted()
        {
            return !GetUsername().Equals("") && !GetPassword().Equals("");
        }
    }
}
