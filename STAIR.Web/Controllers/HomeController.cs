using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using STAIR.Model;
using System.Web.Mvc;
using STAIR.Model.Models;
using STAIR.Web.Helpers;
using STAIR.Service;
using STAIR.Model.ViewModel;
using STAIR.CachingService;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        //protected long timeZoneOffset = UserSession.GetTimeZoneOffset();
        public readonly ISecurityService securityService;
        public readonly Isys_userService sys_userService;
        private static readonly ICacheProvider cacheProvider = new DefaultCacheProvider();

        public HomeController(ISecurityService securityService,Isys_userService sys_userService)
        {
            this.securityService = securityService;
            this.sys_userService = sys_userService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public JsonResult CheckLogin(string username,string password)
        {
            sys_user user = new sys_user();
            user.user_name = username;
            user.user_password = password;
            string chk = string.Empty;
           // user.user_password = this.securityService.GenerateHashWithSalt(password, username);


            sys_user bUser = new sys_user();
            userVM aUser = new userVM();
            bUser = this.sys_userService.Authenticatesys_user(user);
            if(bUser !=null)
            {
                aUser.branch_id = bUser.branch_id;
                aUser.deleted = bUser.deleted;
                aUser.full_name = bUser.full_name;
                aUser.user_address = bUser.user_address;
                aUser.user_creation = bUser.user_creation;
                aUser.user_email = bUser.user_email;
                aUser.user_id = bUser.user_id;
                aUser.user_name = bUser.user_name;
                aUser.user_password = bUser.user_password;
                aUser.user_phone = bUser.user_phone;
                aUser.usr_type_id = bUser.usr_type_id;
            }
            if (aUser != null)
            {
                var urlpath = string.Empty;

                //if (aUser.RoleId != null)
                //{
                //    if (aUser.Role.RoleSubModuleItems.Count() != 0)
                //    {

                //    }
                //    else
                //    {
                //        aUser.Role.RoleSubModuleItems = null;
                //    }
                //}
                //else
                //{
                //    aUser.Role = null;
                //}

                UserSession.SetUserFromSession(aUser);
                //UserSession.SetTimeZoneOffset(timeZoneOffset);
                UserSession.SetUserFullNameInSession(aUser.full_name);

                return Json(new
                {
                    isSuccess = true,
                    Id = aUser.user_id,
                    username = aUser.user_name,
                    fullname = aUser.full_name,
                    usertype = aUser.usr_type_id,
                    userbranch = aUser.branch_id,
                    //url = urlpath,
                    chk = chk
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                isSuccess = false,

            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult profile()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}