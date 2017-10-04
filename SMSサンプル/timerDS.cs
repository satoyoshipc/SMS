using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moss_AP
{
    public class timerDS
    {
        public String schedule_no { get; set; }
        public String timerid { get; set; }
        public String timername { get; set; }
        public String repeat_type { get; set; }
        public String alert_time { get; set; }
        private string _status;
        public String status
        {
            get
            {
                string retstr = "";
                if (_status == "1")

                    retstr = "有効";

                else if (_status == "0")

                    retstr = "無効";

                return retstr;
            }
            set { this._status = value; }
        }

        public String start_date { get; set; }
        public String end_date { get; set; }
        public String sound { get; set; }
        public String chk_date { get; set; }
        public String chk_name_id { get; set; }

    }
}
