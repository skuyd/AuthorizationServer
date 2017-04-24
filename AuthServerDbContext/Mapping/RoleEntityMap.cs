namespace AuthServer.Domain.Mapping
{
    class RoleEntityMap : MapBase<RoleEntity>
    {

        public RoleEntityMap() : base()
        {
            this.Property(t => t.RoleName).HasMaxLength(255);

            this.ToTable("RoleEntity");


        }
    }
}
