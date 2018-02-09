using System;
using STAIR.Data.Models;

namespace STAIR.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        ApplicationEntities Get();
    }
}
