using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace SampleProject2.Common.Models.Mapping
{
    public class ProductTypeMap : EntityTypeConfiguration<ProductType>
    {
        public ProductTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("ProductTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
