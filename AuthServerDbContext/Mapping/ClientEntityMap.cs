
namespace AuthServer.Domain.Mapping
{
    class ClientEntityMap : MapBase<ClientEntity>
    {
        public ClientEntityMap() : base()
        {
            this.Property(t => t.Address).HasMaxLength(255);
            this.Property(t => t.Code).HasMaxLength(255);
            this.Property(t => t.DomainNamePrefix).HasMaxLength(255).IsRequired();

            this.ToTable("ClientEntity");
             
        }
    }
}
