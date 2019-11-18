using System;
namespace GCECDev.Models
{
    public class VerifyCode
    {
        public string Username { get; set; }
        public string Code { get; set; }
        public long ExpireTimestamp { get; set; }

        public bool CheckCompleted()
        {
            if (Username == null || Username.Equals(""))
            {
                return false;
            }
            if (Code == null || Code.Equals(""))
            {
                return false;
            }
            if (ExpireTimestamp <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
