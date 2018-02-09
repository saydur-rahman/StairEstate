using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STAIR.Data.Infrastructure;
using STAIR.Model.Models;

namespace STAIR.Data.Repository
{
    public class ActionLogRepository: RepositoryBase<ActionLog>, IActionLogRepository
    {
        public ActionLogRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IActionLogRepository : IRepository<ActionLog>
    {
    }
}
