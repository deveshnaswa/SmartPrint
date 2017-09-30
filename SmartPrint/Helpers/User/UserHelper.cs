using SmartPrint.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using SmartPrint.Common;
using System.Collections;

namespace SmartPrint.Helpers.User
{
    public class UserHelper
    {
        private MainDbContext _dbContext;
        private List<UserLite> _allUsers;
        public UserHelper(MainDbContext dbcontext)
        {
            _dbContext = dbcontext;
            if (MemoryCache.Default.Get(Common.Constants.UserListName) == null)
            {
                var userData = dbcontext.Users.Where(x => x.StatusId == (int)RecordStatus.Active).ToList().Select(x => new UserLite(x)).ToList();
                MemoryCache.Default.Add(Constants.UserListName, userData, DateTimeOffset.MaxValue);
            }
            _allUsers = MemoryCache.Default.Get(Constants.UserListName) as List<UserLite>;
        }

        public List<UserLite> SearchUsers(string searchTerm)
        {
            List<UserLite> result = new List<UserLite>();
            searchTerm = searchTerm.ToLower();
            if (_allUsers != null)
            {
                result = _allUsers.Where(x => x.SearchName.IndexOf(searchTerm) >= 0).ToList();
            }
            return result;
        }

        public string GetNameOfUser(int userId)
        {
            var result = string.Empty;
            var userToFind =_allUsers.FirstOrDefault(x => x.UserId == userId);
            if (userToFind != null)
            {
                result = userToFind.Name;
            }
            return result;
        }

        public List<UserLite> GetAllUsers()
        {
            return _allUsers;
        }
    }
}