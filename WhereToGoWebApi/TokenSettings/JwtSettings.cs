
namespace WhereToGoWebApi.TokenSettings
{
    public class JwtSettings
    {
        public string Site { get; set; }
        public string SigningKey { get; set; }
        public string ExpiryInMinutes { get; set; }
    }
}
