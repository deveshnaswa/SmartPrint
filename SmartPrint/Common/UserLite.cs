using SmartPrint.Models;

namespace SmartPrint.Common
{
    public class UserLite
    {
        private string _name;
        private string _searchName;

        public UserLite(Users user)
        {
            UserId = user.UserId;
            FName = user.FName;
            LName = user.LName;
            UserEmail = user.UserEmail;
            UserCode = user.UserCode;
            UserTypeId = UserTypeId;
            SetName();
            SetSearchName();
        }

        public int UserId { get; set; }
        public string FName { get; set; }
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    SetName();
                }
                return _name;
            }
        }

        private void SetName()
        {
            _name = $"{FName} {LName}";
        }
        private void SetSearchName()
        {
            _searchName = Name.ToLower();
        }
        public string SearchName
        {
            get
            {
                if (_searchName == null)
                {
                    SetSearchName();
                }
                return _searchName;
            }
        }
        public string LName { get; set; }
        public string UserEmail { get; set; }
        public int UserTypeId { get; set; }
        public string UserCode { get; set; }
        
    }
}