using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GCECDev.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GCECDev.Controllers
{
    public class VerifyCodeRestAPI
    {
        static HttpClient client = new HttpClient { Timeout = TimeSpan.FromMinutes(10) };
        static string serverName = Constants.Constants.AzureMysqlServer;

        ~VerifyCodeRestAPI()
        {
            client.CancelPendingRequests();
        }

        public Task<VerifyCode> Delete(string value)
        {
            throw new NotImplementedException();
        }

        /*
        Return/Throw:
            VC - Code found
            Null - Code not found
            Exception - Connection Error 
        */
        public async Task<VerifyCode> Get(string username)
        {
            try
            {
                var uri = serverName + "api/verify/" + username;
                HttpRequestMessage reqMes = new HttpRequestMessage(HttpMethod.Get, uri);
                HttpResponseMessage res = await client.SendAsync(reqMes);

                try
                {
                    var resStr = await res.Content.ReadAsStringAsync();
                    var vc = JsonConvert.DeserializeObject<VerifyCode>(resStr);
                    return vc.CheckCompleted()? vc: null;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error while parsing GET result received from VerifyCode API: {0}", e);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error while getting verify code for '{0}': {1}", username, e);
                throw new Exception("Connection Error. Please try again");
            }
            return null;
        }

        public IEnumerable<Task<T>> GetAll<T>() where T : class
        {
            throw new NotImplementedException();
        }

        /*
        Return/Throw:
            true - code saved and email sent
            false - code request or save error, email not sent
            Exception - Connection Error or sent error
        */
        public async Task<bool> Post(string username)
        {
            try
            {
                var uri = serverName + "api/verify/" + username;
                HttpRequestMessage reqMes = new HttpRequestMessage(HttpMethod.Post, uri);
                var res = await client.SendAsync(reqMes);
                
                var resStr = await res.Content.ReadAsStringAsync();
                return bool.Parse(resStr);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error while posting request for verify code for '{0}': {1}", username, e);
                throw new Exception("Network error. Please try again");
            }
        }
    }
}
