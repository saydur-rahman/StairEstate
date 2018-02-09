using System.Web.SessionState;
using STAIR.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using STAIR.Model.ViewModel;

namespace STAIR.Web.Helpers
{
    public static class UserSession
    {
        public static userVM GetUserFromSession()
        {
            return (userVM)HttpContext.Current.Session["LoggedInUser"];
        }
        
        public static void SetUserFromSession(userVM user)
        {
            HttpContext.Current.Session["LoggedInUser"] = user;
        }

        public static void SetUserFullNameInSession(string name)
        {
            HttpContext.Current.Session["LoggedInUserFullName"] = name;
        }

        public static string GetUserFullNameFromSession()
        {
            return (string)HttpContext.Current.Session["LoggedInUserFullName"];
        }

        public static void DestroySessionAfterUserLogout()
        {
            HttpContext.Current.Session.Clear();
        }

        public static bool IsAdmin()
        {
            var isAdminRole = false;
            if (STAIR.Web.Helpers.UserSession.GetUserFromSession() != null)
            {
                var defaultRoleId = STAIR.Web.Helpers.UserSession.GetUserFromSession().usr_type_id;
                if (defaultRoleId == 1)
                    isAdminRole = true;
            }
            return isAdminRole;
        }

        public static void SetSession(string id)
        {
            HttpContext.Current.Session["LoggedInUser"] = id;
        }

        public static void SetTimeZoneOffset(long offset)
        {
            HttpContext.Current.Session["TimezoneOffset"] = offset;
        }

        public static long GetTimeZoneOffset()
        {
            return (long)HttpContext.Current.Session["TimezoneOffset"];
        }

        public static void SetModuleClicked(string id)
        {
            HttpContext.Current.Session["ModuleId"] = id;
        }

        public static string GetModuleId()
        {
            return (string)HttpContext.Current.Session["ModuleId"];
        }
    }
}