using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STAIR.Data.Infrastructure;
using STAIR.Model.Models;

namespace STAIR.Data.Repository
{
    public class sys_user_typeRepository : RepositoryBase<sys_user_type>, Isys_user_typeRepository
    {
        public sys_user_typeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface Isys_user_typeRepository : IRepository<sys_user_type>
    {
    }
}
