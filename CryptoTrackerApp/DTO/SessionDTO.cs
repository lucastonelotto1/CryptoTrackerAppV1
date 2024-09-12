namespace CryptoTrackerApp.DTO
{
    public class SessionDTO
    {
        public string AccessToken { get; set; }
        public string Id{ get; set; }

        public string Email { get; set; }

        public SessionDTO(string accessToken, string userId, string email)
        {
            AccessToken = accessToken;
            Id = userId;
            Email = email;
        }
    }
}