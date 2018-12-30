namespace Scorebored.API.Requests
{
    public class CreateGroupRequest
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value.Trim(); }
        }
    }
}
