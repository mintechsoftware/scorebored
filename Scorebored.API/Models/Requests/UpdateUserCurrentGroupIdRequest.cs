using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Scorebored.API.Requests
{
    public class UpdateUserCurrentGroupIdRequest
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value.Trim(); }
        }

        public long test { get; set; }
    }
}
