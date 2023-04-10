using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICBS.OfficialFile.BLL.Helper
{
    public class AccountSvcHelper
    {
        public static List<string> GetUserAccessList(string userName)
        {
#if DEBUG
            return new List<string>
            {
                "Managment",
                "Learn & Tech"
            };

#endif
            throw new NotImplementedException();
            //using (var clProx = new LawReportServiceClient())
            //{
            //    return clProx.GetUserAccessList(userName, "OfficialFile" , KeyHelper.GetKey());
            //}
        }
    }
}
