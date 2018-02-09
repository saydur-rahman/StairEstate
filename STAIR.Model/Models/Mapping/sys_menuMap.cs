using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace STAIR.Model.Models.Mapping
{
    public class sys_menuMap : EntityTypeConfiguration<sys_menu>
    {
        public sys_menuMap()
        {
            // Primary Key
            this.HasKey(t => t.menu_id);

            // Properties
            this.Property(t => t.menu_name)
                .HasMaxLength(50);

            this.Property(t => t.menu_link)
                .HasMaxLength(50);

            this.Property(t => t.menu_parent)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("sys_menu");
            this.Property(t => t.menu_id).HasColumnName("menu_id");
            this.Property(t => t.menu_name).HasColumnName("menu_name");
            this.Property(t => t.menu_type).HasColumnName("menu_type");
            this.Property(t => t.menu_link).HasColumnName("menu_link");
            this.Property(t => t.menu_parent).HasColumnName("menu_parent");
        }
    }
}
