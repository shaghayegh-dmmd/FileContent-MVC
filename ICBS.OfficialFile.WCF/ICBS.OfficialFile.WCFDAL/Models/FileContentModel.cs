using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace ICBS.OfficialFile.WCFDAL.Models
{
    [DataContract]
    public class FileContentModel
    {
        

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public byte[] ArryContent { get; set; }

        
        public DateTime LostDate { get; set; }

        [DataMember]
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
