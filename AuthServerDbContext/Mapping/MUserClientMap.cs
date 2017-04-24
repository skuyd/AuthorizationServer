using System.Data.Entity.ModelConfiguration;

namespace AuthServer.Domain.Mapping
{
    class MUserClientMap : EntityTypeConfiguration<MUserClient>
    {

        public MUserClientMap()
        {
            this.HasKey(t => new { t.UserId, t.ClientId });
            this.Property(t => t.UserId)
                .HasMaxLength(32)
                .IsFixedLength();
            this.Property(t => t.ClientId)
                .HasMaxLength(32)
                .IsFixedLength();

            this.ToTable("M_User_ClientId");

            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .WillCascadeOnDelete(true);

            this.HasRequired(t => t.Client)
                .WithMany()
                .HasForeignKey(t => t.ClientId)
                .WillCascadeOnDelete(true);


        }
    }
}
