using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ICBS.OfficialFile.WCFDAL.OfficialFileContentEntity
{
    public partial class OfficialFileEntity : DbContext
    {
        public OfficialFileEntity()
            : base("name=OfficialFileEntity")
        {
        }

        public virtual DbSet<AccessFileContent> AccessFileContents { get; set; }
        public virtual DbSet<FileContentTbl> FileContentTbls { get; set; }
        public virtual DbSet<OfficialFileContent> OfficialFileContents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessFileContent>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<OfficialFileContent>()
                .HasMany(e => e.AccessFileContents)
                .WithRequired(e => e.OfficialFileContent)
                .HasForeignKey(e => e.FileContentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OfficialFileContent>()
                .HasMany(e => e.FileContentTbls)
                .WithOptional(e => e.OfficialFileContent)
                .HasForeignKey(e => e.IdOfficialFileContent);
        }
    }
}
