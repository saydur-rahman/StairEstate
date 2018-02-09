using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace STAIR.Model.Models.Mapping
{
    public class sys_userMap : EntityTypeConfiguration<sys_user>
    {
        public sys_userMap()
        {
            // Primary Key
            this.HasKey(t => t.user_id);

            // Properties
            this.Property(t => t.user_name)
                .HasMaxLength(50);

            this.Property(t => t.user_password)
                .HasMaxLength(50);

            this.Property(t => t.user_email)
                .HasMaxLength(50);

            this.Property(t => t.user_phone)
                .HasMaxLength(50);

            this.Property(t => t.user_address)
                .HasMaxLength(50);

            this.Property(t => t.full_name)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("sys_user");
            this.Property(t => t.user_id).HasColumnName("user_id");
            this.Property(t => t.user_name).HasColumnName("user_name");
            this.Property(t => t.user_password).HasColumnName("user_password");
            this.Property(t => t.user_email).HasColumnName("user_email");
            this.Property(t => t.user_phone).HasColumnName("user_phone");
            this.Property(t => t.user_address).HasColumnName("user_address");
            this.Property(t => t.user_creation).HasColumnName("user_creation");
            this.Property(t => t.full_name).HasColumnName("full_name");
            this.Property(t => t.usr_type_id).HasColumnName("usr_type_id");
            this.Property(t => t.branch_id).HasColumnName("branch_id");
            this.Property(t => t.deleted).HasColumnName("deleted");

            // Relationships
            this.HasOptional(t => t.sys_user_type)
                .WithMany(t => t.sys_user)
                .HasForeignKey(d => d.usr_type_id);

        }
    }
}
