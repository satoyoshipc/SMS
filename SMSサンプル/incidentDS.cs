using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSサンプル
{
    public class incidentDS
    {
        public String incident_no { get; set; }
        public String mpms_incident { get; set; }
        public String s_cube_id { get; set; }
        public String incident_type { get; set; }
        public String content { get; set; }
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
        public String matflg { get; set; }
        public String matcommand { get; set; }
        public String uketukedate { get; set; }
        public String tehaidate { get; set; }
        public String fukyudate { get; set; }
        public String enddate { get; set; }
        public String timer { get; set; }
        public String kakuninmsg { get; set; }

        public String user_tantou_no { get; set; }
        public String opeid { get; set; }
        public String userno { get; set; }
        public String systemno { get; set; }
        public String siteno { get; set; }
        public String hostno { get; set; }
        public String opename { get; set; }
        public String user_tantou_name { get; set; }
        public String username { get; set; }
        public String systemname { get; set; }
        public String sitename { get; set; }
        public String hostname { get; set; }

        public String chk_date { get; set; }
        public String chk_name_id { get; set; }
    }
}
