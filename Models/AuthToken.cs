namespace RecipeBuilder.Models
{
    public class AuthToken
    {
        public string SessionId { get; set; }
        public string Username { get; set; }
        public DateTime Expiration { get; set; }

        public bool ValidateToken()
        {
            return DateTime.Now < Expiration;
        }
    }
}