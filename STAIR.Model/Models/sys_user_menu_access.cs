using System;
using System.Collections.Generic;

namespace STAIR.Model.Models
{
    public partial class sys_user_menu_access
    {
        public int sys_menu_access_id { get; set; }
        public Nullable<int> menu_id { get; set; }
        public Nullable<int> usr_type_id { get; set; }
        public virtual sys_user_type sys_user_type { get; set; }
    }
}
