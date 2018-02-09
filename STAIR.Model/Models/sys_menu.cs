using System;
using System.Collections.Generic;

namespace STAIR.Model.Models
{
    public partial class sys_menu
    {
        public int menu_id { get; set; }
        public string menu_name { get; set; }
        public Nullable<int> menu_type { get; set; }
        public string menu_link { get; set; }
        public string menu_parent { get; set; }
    }
}
