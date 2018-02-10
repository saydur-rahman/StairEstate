using STAIR.Data.Infrastructure;
using STAIR.Data.Repository;
using STAIR.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAIR.Service
{
    public class sys_menuService : Isys_menuService
    {
        private readonly Isys_menuRepository sys_MenuRepository;
        private readonly IUnitOfWork unitOfWork;

        //Empty Constructor
        public sys_menuService()
        {

        }


        //For Dependency Injection
        public sys_menuService(Isys_menuRepository sys_MenuRepository, IUnitOfWork unitOfWork)
        {
            this.sys_MenuRepository = sys_MenuRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<sys_menu> GetAllMenuForUser(int id)
        {
            return sys_MenuRepository.GetAll().ToList();
        }
    }

    public interface Isys_menuService
    {
        IEnumerable<sys_menu> GetAllMenuForUser(int id);
    }
}
