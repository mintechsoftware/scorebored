namespace Scorebored.API.Requests
{
    public class UserRequest
    {
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value.Trim(); }
        }

        public string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value?.Trim() ?? null; }
        }
    }
}
