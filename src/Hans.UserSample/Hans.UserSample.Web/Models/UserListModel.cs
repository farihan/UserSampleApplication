using Hans.UserSample.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hans.UserSample.Web.Models
{
    public class UserListModel
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Sort { get; set; }
        public bool IsAsc { get; set; }
        public IList<User> Users { get; set; }

        public UserListModel()
        {
            TotalPages = 0;
            CurrentPage = 0;
            PageSize = 0;
            PageIndex = 0;
            Users = new List<User>();
        }
    }
}
