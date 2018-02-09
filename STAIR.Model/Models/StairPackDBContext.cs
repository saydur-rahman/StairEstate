using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using STAIR.Model.Models.Mapping;

namespace STAIR.Model.Models
{
    public partial class StairPackDBContext : DbContext
    {
        static StairPackDBContext()
        {
            Database.SetInitializer<StairPackDBContext>(null);
        }

        public StairPackDBContext()
            : base("Name=StairPackDBContext")
        {
        }

        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<sys_menu> sys_menu { get; set; }
        public DbSet<sys_user> sys_user { get; set; }
        public DbSet<sys_user_menu_access> sys_user_menu_access { get; set; }
        public DbSet<sys_user_type> sys_user_type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActionLogMap());
            modelBuilder.Configurations.Add(new sys_menuMap());
            modelBuilder.Configurations.Add(new sys_userMap());
            modelBuilder.Configurations.Add(new sys_user_menu_accessMap());
            modelBuilder.Configurations.Add(new sys_user_typeMap());
        }
    }
}
