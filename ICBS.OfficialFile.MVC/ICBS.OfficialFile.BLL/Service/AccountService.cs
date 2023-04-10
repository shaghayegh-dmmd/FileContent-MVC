using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICBS.OfficialFile.BLL.Helper;

namespace ICBS.OfficialFile.BLL.Service
{
   public class AccountService
    {
        public static List<string> GetUserAccessList(string userName)
        {

            return AccountSvcHelper.GetUserAccessList(userName);

        }
    }
}
