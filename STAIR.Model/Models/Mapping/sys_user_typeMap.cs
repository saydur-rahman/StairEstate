using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace STAIR.Model.Models.Mapping
{
    public class sys_user_typeMap : EntityTypeConfiguration<sys_user_type>
    {
        public sys_user_typeMap()
        {
            // Primary Key
            this.HasKey(t => t.usr_type_Id);

            // Properties
            this.Property(t => t.type_name)
                .HasMaxLength(50);

            this.Property(t => t.description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("sys_user_type");
            this.Property(t => t.usr_type_Id).HasColumnName("usr_type_Id");
            this.Property(t => t.type_name).HasColumnName("type_name");
            this.Property(t => t.description).HasColumnName("description");
        }
    }
}
