namespace UserWebAPI.Models
{
    public class APICredentials
    {
        private string _apiKey;
        private string _secret;
        public APICredentials()
        {
        }
        public APICredentials(string apiKey, string secret)
        {
            _apiKey = apiKey;
            _secret = secret;
        }
        public string Secret { get => _secret; set => _secret = value; }
        public string ApiKey { get => _apiKey; set => _apiKey = value; }
    }
}
