using System;
using System.Security.Cryptography;

namespace school_system_api.Helpers
{
    public class Secret
    {
        public string GenerateBase64Key()
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