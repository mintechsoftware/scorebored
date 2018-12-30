namespace Scorebored.API.Requests
{
    public class JoinGroupRequest
    {
        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value.Trim(); }
        }
    }
}
