using System;
using System.Security.Cryptography;

namespace school_system_api.config
{
    public class Config
    {

        //TODO: IT SHOULD BE IN ENVIRONMENT VARIABLES 
        public string SecretKey = "TdqVNhyjVra7gqxfvMp9ZBIz8wiI/h5Zybxyb7k1Bmc=";
        public int ExpirationToken = 60 * 24;
        public string Issuer = "school_system";
        public string CookieName = "x-access-token";
        public string Domain = "localhost";
    }
}
