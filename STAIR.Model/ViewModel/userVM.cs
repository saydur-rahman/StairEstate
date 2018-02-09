using STAIR.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAIR.Model.ViewModel
{
    public class userVM
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }
        public string user_email { get; set; }
        public string user_phone { get; set; }
        public string user_address { get; set; }
        public Nullable<System.DateTime> user_creation { get; set; }
        public string full_name { get; set; }
        public Nullable<int> usr_type_id { get; set; }
        public Nullable<int> branch_id { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<ActionLog> ActionLogs { get; set; }
        public virtual sys_user_type sys_user_type { get; set; }
    }
}
