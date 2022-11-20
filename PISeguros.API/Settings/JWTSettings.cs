namespace PISeguros.API.Settings
{
    public class JWTSettings
    {
        public string IssuerSigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}
