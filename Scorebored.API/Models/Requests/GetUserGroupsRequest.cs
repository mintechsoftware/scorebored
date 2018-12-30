namespace Scorebored.API.Requests
{
    public class GetUserGroupsRequest
    {
        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set { _userId = value.Trim(); }
        }
    }
}
