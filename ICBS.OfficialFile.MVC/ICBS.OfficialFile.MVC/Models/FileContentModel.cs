using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICBS.OfficialFile.MVC.Models
{
    public class FileContentModel
    {

        public Guid Id { get; set; }


        public byte[] ArryContent { get; set; }


        public DateTime LostDate { get; set; }


        public DateTime DateNow
        {
            set
            {
                LostDate = DateNow.AddHours(1);
            }
            get
            {
                return DateTime.Now;
            }


        }
    }
}