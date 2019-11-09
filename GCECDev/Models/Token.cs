using System;
namespace GCECDev.Models
{
    public class Token
    {
        public string Id { get; set; }
        public int AccessToken { get; set; }
        public string ErrDescription { get; set; }
        public DateTime ExpireDate { get; set; }

    }
}
