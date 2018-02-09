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
    public interface Isys_userService
    {

        bool Createsys_user(sys_user sys_user);
        bool Updatesys_user(sys_user sys_user);
        bool Deletesys_user(int id);
        sys_user Getsys_user(int id);
        sys_user Getsys_userByLoginNameAndPassword(sys_user sys_user);

        sys_user Authenticatesys_user(sys_user sys_user);
        bool ForgetPwdExpired(sys_user user);
        IEnumerable<sys_user> GetAllsys_user();
        sys_user Authenticatesys_userforDifferentRole(sys_user sys_user);

        void SaveRecord();
    }

    public class sys_userService : Isys_userService
    {
        private readonly Isys_userRepository sys_userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly LoggingService logger = new LoggingService(typeof(sys_userService));
                
        public sys_userService(Isys_userRepository sys_userRepository, IUnitOfWork unitOfWork)
        {
            this.sys_userRepository = sys_userRepository;
            this.unitOfWork = unitOfWork;
        }

        public bool Createsys_user(sys_user user)
        {
            bool isSuccess = true;
            try
            {
                sys_userRepository.Add(user);
                this.SaveRecord();
               // ServiceUtil<sys_user>.WriteActionLog(user.user_id, ENUMOperation.CREATE, user);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                logger.Error("Error in creating sys_user", ex);
            }
            return isSuccess;
        }

        public bool Updatesys_user(sys_user user)
        {
            bool isSuccess = true;
            try
            {
                sys_userRepository.Update(user);
                this.SaveRecord();
                //ServiceUtil<sys_user>.WriteActionLog(user.user_id, ENUMOperation.UPDATE, user);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                logger.Error("Error in updating sys_user", ex);
            }
            return isSuccess;
        }

        public bool Deletesys_user(int id)
        {
            bool isSuccess = true;
            try
            {
                sys_user user = Getsys_user(id);
                sys_userRepository.Delete(user);
                SaveRecord();
                //ServiceUtil<sys_user>.WriteActionLog(id, ENUMOperation.DELETE);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                logger.Error("Error in deleting sys_user", ex);
            }
            return isSuccess;
        }

        public sys_user Getsys_user(int id)
        {
            return sys_userRepository.GetById(id);
        }
        public sys_user Getsys_userByLoginNameAndPassword(sys_user user)
        {
            return sys_userRepository.Get(u => u.user_name == user.user_name && u.user_password == user.user_password);
        }


        //public IEnumerable<sys_user> CountSameMobile(sys_user user)
        //{
        //    return userRepository.GetMany(up => up.MobileNo == user.MobileNo);
        //}

        public sys_user Authenticatesys_user(sys_user user)
        {
            sys_user getsys_userInfo = new sys_user();
            try
            {
                getsys_userInfo = sys_userRepository.Get(u => u.user_name == user.user_name && u.user_password == user.user_password);
                if (getsys_userInfo != null)
                {
                    //if (ForgetPwdExpired(getsys_userInfo))
                    //{
                    //    getsys_userInfo = null;
                    //}
                    return getsys_userInfo;
                }
            }
            catch (Exception ex)
            {
                getsys_userInfo = null;
            }
            return getsys_userInfo;
        }

        public sys_user Authenticatesys_userforDifferentRole(sys_user user)
        {
            sys_user getsys_userInfo = new sys_user();
            bool isSuccess = true;
            try
            {
                getsys_userInfo = sys_userRepository.Get(u => (u.user_id == user.user_id && u.user_password == user.user_password));
                if (getsys_userInfo == null)
                {
                    isSuccess = false;
                }
                else  // login after getting new pwd for forget pwd [userinfo not null but timestamp is set 
                {
                    if (ForgetPwdExpired(getsys_userInfo))
                    {
                        isSuccess = false;
                        getsys_userInfo = null;
                    }
                }


            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return getsys_userInfo;
        }

        public bool ForgetPwdExpired(sys_user user)
        {
            bool isExpired = false;
            //if (user.PwdTimeStamp.HasValue)
            //{
            //    DateTime pwdValidtime = user.PwdTimeStamp.Value.AddMinutes(pwdvalidMinute);
            //    if (DateTime.Now > pwdValidtime)
            //    {
            //        isExpired = true; // if current time <=pwd validation time pwd not expired
            //    }
            //}
            return isExpired;
        }

        public IEnumerable<sys_user> GetAllsys_user()
        {
            return sys_userRepository.GetAll();
        }

        public void SaveRecord()
        {
            unitOfWork.Commit();
        }
    }
}
