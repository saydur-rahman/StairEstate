using STAIR.Data.Infrastructure;
using STAIR.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace STAIR.Data.Repository
{
    public interface Isys_menuRepository : IRepository<sys_menu>
    {

    }

    internal class sys_menuRepository : RepositoryBase<sys_menu>, Isys_menuRepository
    {
        protected sys_menuRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
    }
}
