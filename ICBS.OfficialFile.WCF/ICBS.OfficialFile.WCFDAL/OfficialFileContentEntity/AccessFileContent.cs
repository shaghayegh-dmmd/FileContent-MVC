namespace ICBS.OfficialFile.WCFDAL.OfficialFileContentEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccessFileContent")]
    public partial class AccessFileContent
    {
        public long Id { get; set; }

        public long FileContentId { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public string Link { get; set; }

        [StringLength(300)]
        public string UserList { get; set; }

        public virtual OfficialFileContent OfficialFileContent { get; set; }
    }
}
