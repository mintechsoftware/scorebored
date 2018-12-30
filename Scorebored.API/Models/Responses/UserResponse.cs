namespace Scorebored.API.Responses
{
    public class UserResponse
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public long? CurrentGroupId { get; set; }
    }
}
