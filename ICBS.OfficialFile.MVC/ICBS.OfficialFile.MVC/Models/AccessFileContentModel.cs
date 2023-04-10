using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICBS.OfficialFile.MVC.Models
{
    public class AccessFileContentModel
    {
        
        public long Id { get; set; }

       
        public long FileContentId { get; set; }

       
        public string Status { get; set; }


        public string Link { get; set; }

       
        public string UserList { get; set; }

    }
}