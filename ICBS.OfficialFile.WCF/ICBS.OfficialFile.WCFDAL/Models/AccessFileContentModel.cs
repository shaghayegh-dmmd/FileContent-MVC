using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ICBS.OfficialFile.WCFDAL.Models
{
    [DataContract]
    public class AccessFileContentModelSVC
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public long FileContentId { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Link { get; set; }

        [DataMember]
        public string UserList { get; set; }


    }
}
