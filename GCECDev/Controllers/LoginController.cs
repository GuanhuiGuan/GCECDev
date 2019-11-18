using System;
using System.Net.Http;
using GCECDev.Models;
using System.Threading.Tasks;

namespace GCECDev.Controllers
{
    public class LoginController
    {
        public LoginController()
        {
        }

        public bool FindCurrentUser()
        {
            User user = App.User;
            if (user == null ||!user.CheckCompleted())
            {
                return false;
            }
            return true;
        }

        public async Task<Exception> LoginAsync(User user)
        {
            if (!user.CheckCompleted())
            {
                return new Exception("Username and password cannot be empty");
            }

            try
            {
                var userAPI = new UserRestAPI();
                var res = await userAPI.Get(user.GetUsername());

                if (res == null)
                {
                    return new Exception("User not found");
                }
                if (!res.GetPassword().Equals(user.GetPassword()))
                {
                    return new Exception("Your password is incorrect");
                }

                return null;
            }
            catch (Exception e)
            {
                // Wrapped from UserAPI
                return e;
            }
        }

        // CheckEmail verifies the email when logged out user enters a email
        // Return confirms if the request is valid (new user -> new email; existing user -> existing email)
        public async Task<bool> CheckEmail(string username, bool newUser)
        {
            if (username == null || username.Equals(""))
            {
                throw new Exception("Empty email");
            }

            try
            {
                var userAPI = new UserRestAPI();
                var res = await userAPI.Get(username);

                return (newUser && (res == null)) ||
                    (!newUser && (res != null));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // TODO separate email sent error and connection error
        // SendEmail sends a request to send an email
        public async Task<bool> SendEmail(string username)
        {
            if (username == null || username.Equals(""))
            {
                throw new Exception("Empty email");
            }
            try
            {
                var vcAPI = new VerifyCodeRestAPI();
                var res = await vcAPI.Post(username);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // SetPassword sets the password
        public async Task<bool> SetPassword(User user, bool newUser)
        {
            if (user == null || !user.CheckCompleted())
            {
                throw new Exception("Invalid user data");
            }
            try
            {
                var userAPI = new UserRestAPI();
                if (newUser)
                {
                    return await userAPI.Insert(user);
                }
                else
                {
                    return await userAPI.Update(user);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
