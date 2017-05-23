using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSサンプル
{
    public class userDS
    {
        public String userno { get; set; }
        public String username { get; set; }
        public String username_kana { get; set; }
        public String username_sum { get; set; }


        private string _report_status;
        public String report_status
        {
            get
            {
                string retstr = "";
                if (_report_status == "0")

                    retstr = "無効";

                else if (_report_status == "1")

                    retstr = "有効";

                return retstr;
            }
            set { this._report_status = value; }
        }

        public String biko { get; set; }
        public String chk_date { get; set; }
        public String chk_name_id { get; set; }
        public String lastname { get; set; }
        public String fastname { get; set; }
        private string _status;
        public String status
        {
            get
            {
                string retstr = "";
                if (_status == "0")

                    retstr = "無効";

                else if (_status == "1")

                    retstr = "有効";

                return retstr;
            }
            set { this._status = value; }
        }
    }
}
