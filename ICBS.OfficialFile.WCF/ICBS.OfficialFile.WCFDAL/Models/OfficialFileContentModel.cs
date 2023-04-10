using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace ICBS.OfficialFile.WCFDAL.Models
{
    [DataContract]
    public class OfficialFileContentModelSVC
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public Guid FileSerial { get; set; }

        [DataMember]
        public string SystemFileName { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string SystemFileType { get; set; }

        [DataMember]
        public DateTime? CreationDate { get; set; }

        [DataMember]
        public DateTime? UpdateDate { get; set; }

        [DataMember]
        public long? FileSize { get; set; }

        [DataMember]
        public string SubjectType { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string CreatorUserName { get; set; }

        [DataMember]
        public string UpdateUserName { get; set; }

        [DataMember]
        public byte[] FileContent { get; set; }

        
    }
}
