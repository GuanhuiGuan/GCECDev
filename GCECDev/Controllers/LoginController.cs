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

        public async Task<Exception> LoginAsync(User user)
        {
            if (!user.CheckCompleted())
            {
                return new Exception("Username and password cannot be empty");
            }

            try
            {
                var userAPI = new UserRestAPI();

                var res = await userAPI.Get<User>(user.GetUsername());

                if (res == null || !res.CheckCompleted())
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
                return new Exception("Exception from API", e);
            }
        }
    }
}
