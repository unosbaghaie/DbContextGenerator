using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace SampleProject1.Common.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Products");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ProductTypeId).HasColumnName("ProductTypeId");
        }
    }
}
