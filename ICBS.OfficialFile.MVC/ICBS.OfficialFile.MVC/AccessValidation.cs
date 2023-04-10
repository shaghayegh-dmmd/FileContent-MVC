using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICBS.OfficialFile.BLL.Service;
namespace ICBS.OfficialFile.MVC
{
    public static class AccessValidation
    {
        public static bool HasUserAnyAccess(string username)
        {
#if DEBUG
            return true;
#endif
            var accessLst = AccountService.GetUserAccessList(username);
            return accessLst.Any();
        }
        public static bool HasUserAccess(string username, string accessLevel)
        {
#if DEBUG
            return true;
#endif
            var accessLst = AccountService.GetUserAccessList(username);
            if (accessLst == null) return false;
            return accessLst.Any(w => w == accessLevel);
        }
    }
}