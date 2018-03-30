using System;
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
        public String schedule_no { get; set; }
        public String schedule_type { get; set; }

        public String naiyou { get; set; }
        public String repeat_type { get; set; }
        public String timerid { get; set; }
        public String timername { get; set; }
        public String start_date { get; set; }
        public String end_date { get; set; }

        public String sound { get; set; }
        public String alertdatetime { get; set; }

        public String status { get; set; }
        public String opeid { get; set; } //対応者
        public String taioudate { get; set; } //対応日時

        public String chk_date { get; set; }
        public String chk_name_id { get; set; }
    }
}
