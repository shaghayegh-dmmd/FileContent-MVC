using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICBS.OfficialFile.BLL.Helper;
using System.ComponentModel;

namespace ICBS.OfficialFile.MVC.Models
{
    public class OfficialFileContentModel
    {
        
        public long Id { get; set; }

        public Guid FileSerial { get; set; }

        public string SystemFileName { get; set; }

        [DisplayName("نام فایل:")]
        public string FileName { get; set; }

     
        public string SystemFileType { get; set; }

      
        public DateTime? CreationDate { get; set; }

       
        public DateTime? UpdateDate { get; set; }

        public string UpdateDateStr
        {
            set
            {
                var val = value.PersianNumbersToEnglish();
                if (!DateHelper.IsValidJalaliDate(val)) return;
                var date = val.ToGeoDateTime();
                if (date != null) UpdateDate = (DateTime)date;
            }
            get
            {
                try
                {
                    return DateHelper.GetJalaliFromDateTimeGregorian(UpdateDate);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
        public long? FileSize { get; set; }

        
        public byte[] FileContent { get; set; }

        [DisplayName(" موضوع فایل:")]
        public string SubjectType { get; set; }

        [DisplayName("توضیحات:")]
        public string Description { get; set; }

      
        public string CreatorUserName { get; set; }

       
        public string UpdateUserName { get; set; }
    }
}