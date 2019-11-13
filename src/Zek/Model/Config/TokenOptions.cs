namespace Zek.Model.Config
{
    public class TokenOptions
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string IssuerSigningKey { get; set; }
        public int ExpireMinutes { get; set; } 
        public int RememberMeExpireMinutes { get; set; }
    }
}
