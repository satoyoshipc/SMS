﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moss_AP
{
    public class alermDS
    {
        public String userno { get; set; }
        public String username { get; set; }
        public String systemno { get; set; }
        public String systemname { get; set; }
        public String siteno { get; set; }
        public String sitename { get; set; }

        public String timer_name { get; set; }
        public String sound { get; set; }
        public String incident_no { get; set; }
        public String alerm_message { get; set; }
        public String schedule_no { get; set; }
        public String schedule_type { get; set; }
        public String alertdatetime { get; set; }

        public String opeid { get; set; } //対応者
        public String taiou_date { get; set; } //対応日時
        public String repeat_type { get; set; }

        public String chk_date { get; set; }
        public String chk_name_id { get; set; }
    }
}
