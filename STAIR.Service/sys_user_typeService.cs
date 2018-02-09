using STAIR.Data.Repository;
using STAIR.Data.Infrastructure;
using STAIR.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STAIR.Core.Common;
using STAIR.LoggerService;

namespace STAIR.Service
{
    public interface Isys_user_typeService
    {

        bool Createsys_user_type(sys_user_type sys_user_type);
        bool Updatesys_user_type(sys_user_type sys_user_type);
        bool Deletesys_user_type(Guid id);
        sys_user_type Getsys_user_type(Guid id);
        
        IEnumerable<sys_user_type> GetAllsys_user_type();
        void SaveRecord();

        bool CheckIsExist(sys_user_type sys_user_type);
    }

    public class sys_user_typeService : Isys_user_typeService
    {
        private readonly Isys_user_typeRepository sys_user_typeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly LoggingService logger = new LoggingService(typeof(sys_user_typeService));

        public sys_user_typeService()
        {
        }
                
        public sys_user_typeService(Isys_user_typeRepository sys_user_typeRepository, IUnitOfWork unitOfWork)
        {
            this.sys_user_typeRepository = sys_user_typeRepository;
            this.unitOfWork = unitOfWork;
        }

        public bool CheckIsExist(sys_user_type sys_user_type)
        {
            return false;
            //return sys_user_typeRepository.Get(chk => chk.Name == sys_user_type.Name) == null ? false : true;
        }

        public bool Createsys_user_type(sys_user_type sys_user_type)
        {
            bool isSuccess = true;
            try
            {
                sys_user_typeRepository.Add(sys_user_type);                
                this.SaveRecord();
                //ServiceUtil<sys_user_type>.WriteActionLog(sys_user_type.usr_type_Id, ENUMOperation.CREATE, sys_user_type);
            }
            catch(Exception ex)
            {
                isSuccess = false;
                logger.Error("Error in creating sys_user_type", ex);
            }
            return isSuccess;
        }

        public bool Updatesys_user_type(sys_user_type sys_user_type)
        {
            bool isSuccess = true;
            try
            {
                sys_user_typeRepository.Update(sys_user_type);
                this.SaveRecord();
                //ServiceUtil<sys_user_type>.WriteActionLog(sys_user_type.usr_type_Id, ENUMOperation.UPDATE, sys_user_type);
            }
            catch(Exception ex)
            {
                isSuccess = false;
                logger.Error("Error in updating sys_user_type", ex);
            }
            return isSuccess;
        }

        public bool Deletesys_user_type(Guid id)
        {
            bool isSuccess = true;
            var sys_user_type = sys_user_typeRepository.GetById(id);
            try
            {
                sys_user_typeRepository.Delete(sys_user_type);
                SaveRecord();
                //ServiceUtil<sys_user_type>.WriteActionLog(id, ENUMOperation.DELETE);
            }
            catch(Exception ex)
            {
                isSuccess = false;
                logger.Error("Error in deleting sys_user_type", ex);
            }
            return isSuccess;
        }

        public sys_user_type Getsys_user_type(Guid id)
        {
            return sys_user_typeRepository.GetById(id);
        }
               
        public IEnumerable<sys_user_type> GetAllsys_user_type()
        {
            return sys_user_typeRepository.GetAll();
        }


        public void SaveRecord()
        {
            unitOfWork.Commit();
        }
    }
}
