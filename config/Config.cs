using System;
using System.Security.Cryptography;

namespace school_system_api.config
{
    public class Config
    {
        public string SecretKey;
        public int ExpirationToken = 60 * 24;
        public string CookieName = "x-access-token";
        public string Domain = "localhost";

        public Config()
        {
            SecretKey = this.GenerateBase64Key();
        }

        private string GenerateBase64Key()
        {
            byte[] keyBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes);
        }
    }
}
