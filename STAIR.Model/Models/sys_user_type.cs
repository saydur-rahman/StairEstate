using System;
using System.Collections.Generic;

namespace STAIR.Model.Models
{
    public partial class sys_user_type
    {
        public sys_user_type()
        {
            this.sys_user = new List<sys_user>();
            this.sys_user_menu_access = new List<sys_user_menu_access>();
        }

        public int usr_type_Id { get; set; }
        public string type_name { get; set; }
        public string description { get; set; }
        public virtual ICollection<sys_user> sys_user { get; set; }
        public virtual ICollection<sys_user_menu_access> sys_user_menu_access { get; set; }
    }
}
