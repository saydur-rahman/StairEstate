using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STAIR.Data.Infrastructure;
using STAIR.Model.Models;

namespace STAIR.Data.Repository
{
    public class sys_userRepository : RepositoryBase<sys_user>, Isys_userRepository
    {
        public sys_userRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface Isys_userRepository : IRepository<sys_user>
    {
    }
}
