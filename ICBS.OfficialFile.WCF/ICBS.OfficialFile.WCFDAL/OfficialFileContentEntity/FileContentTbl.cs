namespace ICBS.OfficialFile.WCFDAL.OfficialFileContentEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileContentTbl")]
    public partial class FileContentTbl
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public long? IdOfficialFileContent { get; set; }

        public byte[] FileContent { get; set; }

        public virtual OfficialFileContent OfficialFileContent { get; set; }
    }
}
