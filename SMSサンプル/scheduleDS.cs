using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moss_AP
{
    public class scheduleDS
    {
        public String schedule_no { get; set; }
        public String timer_name { get; set; }
        public String schedule_type { get; set; }
        public String start_date { get; set; }
        public String end_date { get; set; }
        private string _status;
        public String status
        {
            get
            {
                string retstr = "";
                if (_status == "1")

                    retstr = "未完了";

                else if (_status == "0")

                    retstr = "完了";

                return retstr;
            }
            set { this._status = value; }
        }

        public String repeat_type { get; set; }
        public String sound { get; set; }
        public String incident_no { get; set; }
        public String kakunin { get; set; }
        public String userno { get; set; }
        public String username { get; set; }
        public String systemno { get; set; }
        public String systemname { get; set; }
        public String siteno { get; set; }
        public String sitename { get; set; }
        public String alerm_message { get; set; }
        public String chk_date { get; set; }
        public String chk_name_id { get; set; }
        public String alertdate { get; set; }

    }
}
