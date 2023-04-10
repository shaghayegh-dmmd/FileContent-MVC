namespace ICBS.OfficialFile.WCFDAL.OfficialFileContentEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OfficialFileContent")]
    public partial class OfficialFileContent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OfficialFileContent()
        {
            AccessFileContents = new HashSet<AccessFileContent>();
            FileContentTbls = new HashSet<FileContentTbl>();
        }

        public long Id { get; set; }

        [StringLength(150)]
        public string SystemFileName { get; set; }

        [StringLength(150)]
        public string FileName { get; set; }

        [StringLength(150)]
        public string SystemFileType { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public long? FileSize { get; set; }

        [StringLength(50)]
        public string SubjectType { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string CreatorUserName { get; set; }

        [StringLength(50)]
        public string UpdateUserName { get; set; }

        public Guid FileSerial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccessFileContent> AccessFileContents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileContentTbl> FileContentTbls { get; set; }
    }
}
