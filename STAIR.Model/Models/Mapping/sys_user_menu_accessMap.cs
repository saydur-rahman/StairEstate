using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace STAIR.Model.Models.Mapping
{
    public class sys_user_menu_accessMap : EntityTypeConfiguration<sys_user_menu_access>
    {
        public sys_user_menu_accessMap()
        {
            // Primary Key
            this.HasKey(t => t.sys_menu_access_id);

            // Properties
            // Table & Column Mappings
            this.ToTable("sys_user_menu_access");
            this.Property(t => t.sys_menu_access_id).HasColumnName("sys_menu_access_id");
            this.Property(t => t.menu_id).HasColumnName("menu_id");
            this.Property(t => t.usr_type_id).HasColumnName("usr_type_id");

            // Relationships
            this.HasOptional(t => t.sys_user_type)
                .WithMany(t => t.sys_user_menu_access)
                .HasForeignKey(d => d.usr_type_id);

        }
    }
}
