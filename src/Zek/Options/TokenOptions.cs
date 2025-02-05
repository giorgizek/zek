namespace Zek.Options
{
    public class TokenOptions
    {
        public string? ValidIssuer { get; set; }
        public string? ValidAudience { get; set; }
        public string IssuerSigningKey { get; set; } = string.Empty;
        public int AccessTokenExpirationMinutes { get; set; } = 1440;//1 day
        public int RememberMeAccessTokenExpirationMinutes { get; set; } = 43200;//30 days
        public int RefreshTokenExpirationMinutes { get; set; } = 20;
    }
}
