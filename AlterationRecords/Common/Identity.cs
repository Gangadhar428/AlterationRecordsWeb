using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlterationRecords.Common
{
    public class Identity
    {
        public string userName;
        public long userID;
        public string name;
        public int roleID;
        public bool isSuperUser;
        public string password;
        public string role;

        public Identity(string userName, long userID, string name, int roleID, bool isSuperUser, string password, string role)
        {
            this.userName = userName;
            this.userID = userID;
            this.roleID = roleID;
            this.name = name;
            this.isSuperUser = isSuperUser;
            this.password = password;
            this.role = role;
        }

        public string UserName
        {
            get { return userName; }
        }

        public string Name
        {
            get { return name; }
        }

        public int RoleID
        {
            get { return roleID; }
        }

        public bool ISSuperUser
        {
            get { return isSuperUser; }
        }

        public long UserID
        {
            get { return userID; }
        }

        public string Password
        {
            get { return password; }
        }

        public string Role
        {
            get { return role; }
        }
    }
}