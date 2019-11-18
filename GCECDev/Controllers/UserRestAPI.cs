using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GCECDev.Models;
using Newtonsoft.Json;

namespace GCECDev.Controllers
{
    public class UserRestAPI
    {
        static HttpClient client = new HttpClient { Timeout = TimeSpan.FromMinutes(10) };
        static string serverName = Constants.Constants.AzureMysqlServer;

        ~UserRestAPI()
        {
            client.CancelPendingRequests();
        }

        public Task<User> Delete(string value)
        {
            throw new NotImplementedException();
        }

        /*
        Return/Throw:
            User - Valid user found
            Null - Not found
            Exception - Connection Error
        */
        public async Task<User> Get(string username)
        {
            try
            {
                var uri = serverName + "api/user/" + username;
                HttpRequestMessage reqMes = new HttpRequestMessage(HttpMethod.Get, uri);
                HttpResponseMessage res = await client.SendAsync(reqMes);

                try
                {
                    var resStr = await res.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(resStr);
                    return user.CheckCompleted() ? user : null;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error while parsing GET result received from User API: {0}", e);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error while calling User API: {0}", e);
                throw new Exception("Error while connecting to the server");
            }
            return null;
        }

        public IEnumerable<Task<T>> GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        // Insert new user
        /*
        Return/Throw:
            True - New User Created
            False - Failed to save

        TODO: Catch the duplicate PK error in UserAPI to let it return false instead
        */
        public async Task<bool> Insert(User user)
        {
            try
            {
                var uri = serverName + "api/user/";
                HttpRequestMessage reqMes = new HttpRequestMessage(HttpMethod.Put, uri);
                reqMes.Content = new StringContent(
                    user.GetUPString(), Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.SendAsync(reqMes);

                var resStr = await res.Content.ReadAsStringAsync();
                return bool.Parse(resStr);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error while creating user '{0}': {1}", user.GetUsername(), e);
            }
            return false;
        }

        // Update existing user
        /*
        Return/Throw:
            True - New User Created
            False - Failed to save
        */
        public async Task<bool> Update(User user)
        {
            try
            {
                var uri = serverName + "api/user/";
                HttpRequestMessage reqMes = new HttpRequestMessage(HttpMethod.Post, uri);
                reqMes.Content = new StringContent(
                    user.GetUPString(), Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.SendAsync(reqMes);

                var resStr = await res.Content.ReadAsStringAsync();
                return bool.Parse(resStr);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error while updating user '{0}': {1}", user.GetUsername(), e);
            }
            return false;
        }
    }
}
