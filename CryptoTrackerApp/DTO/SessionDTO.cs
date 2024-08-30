namespace CryptoTrackerApp.DTO
{
    public class SessionDTO
    {
        public string AccessToken { get; set; }
        public string Id{ get; set; }

        public SessionDTO(string accessToken, string userId)
        {
            AccessToken = accessToken;
            Id = userId;
        }
    }
}