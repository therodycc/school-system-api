using school_system_api.config;

namespace school_system_api.Helpers
{
    public class Cookies
    {
        private readonly string Domain;

        public Cookies()
        {
            var config = new Config();
            Domain = config.Domain;
        }

        public string Get(HttpRequest req, string name)
        {
            string[] data = (req.Headers["Cookie"].ToString() ?? "").Split(';');
            var result = data.Select(cookie =>
            {
                var c = cookie.Split('=');
                return new { Key = c[0].Trim(), Value = c[1] };
            }).ToDictionary(x => x.Key, x => x.Value);

            return result.ContainsKey(name) ? result[name] : "";
        }

        public void Set(HttpResponse res, string name, string token)
        {
            res.Cookies.Append(
                name,
                token,
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(1),
                    SameSite = SameSiteMode.Lax,
                    Secure = false,
                    Domain = Domain,
                }
            );
        }

        public void Clear(HttpResponse res, string name)
        {
            res.Cookies.Delete(
                name,
                new CookieOptions
                {
                    Domain = Domain,
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Secure = false,
                    Expires = DateTimeOffset.MinValue,
                }
            );
        }
    }

}