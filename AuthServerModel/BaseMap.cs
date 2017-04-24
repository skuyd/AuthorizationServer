using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;

namespace AuthServer.Domain
{
    public abstract class MapBase<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        protected MapBase()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.Id)
                .HasColumnName("Id")
                .HasMaxLength(32)
                .IsFixedLength();

            this.HasOptional(t => t.Creator)
                .WithMany()
                .HasForeignKey(t => t.CreatorId);

            SetIdColumns();

            this.HasRequired(t => t.Creator)
               .WithMany()
               .HasForeignKey(t => t.CreatorId)
               .WillCascadeOnDelete(false);
        }


        /// <summary>
        /// 
        /// </summary>
        protected virtual void SetIdColumns()
        {
            var type = this.GetType();
            var properties =
                type.GetProperties().Where(p => p.PropertyType.FullName.StartsWith("System.")); ;
            foreach (var property in properties)
            {
                SetPropertyMap(property);

                if (property.Name == "Id" || !property.Name.EndsWith("Id"))
                {
                    continue;
                }

                var lambda = DynamicExpression.ParseLambda<T, string>(property.Name, null);
                this.Property(lambda)
                 .HasColumnName(property.Name)
                  .HasMaxLength(32)
                 .IsFixedLength();
            }
        }

        /// <summary>
        /// 重写字段映射
        /// </summary>
        /// <param name="propertyInfo"></param>
        protected virtual void SetPropertyMap(PropertyInfo propertyInfo)
        {
            //var lambda = DynamicExpression.ParseLambda<T, string>(propertyInfo.Name, null); 
            //this.Property(lambda)
            //    .HasColumnName(propertyInfo.Name);
        }
    }
}
