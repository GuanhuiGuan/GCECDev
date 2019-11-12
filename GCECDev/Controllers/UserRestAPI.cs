using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GCECDev.Models;
using Newtonsoft.Json;

namespace GCECDev.Controllers
{
    public class UserRestAPI
    {
        static HttpClient client = new HttpClient();
        static string serverName = Constants.Constants.AzureMysqlServer;

        public UserRestAPI()
        {
        }

        public Task<T> Delete<T>(string value) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get<User>(string username)
        {
            try
            {
                var uri = serverName + "api/user/" + username;
                string responseBody = await client.GetStringAsync(uri);
                Console.WriteLine(responseBody);
                var user = JsonConvert.DeserializeObject<User>(responseBody);
                return user;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Exception getting user {0}: {1} ", username, e.Message);
                return default;
            }
        }

        public IEnumerable<Task<T>> GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> Post<T>(T value) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
