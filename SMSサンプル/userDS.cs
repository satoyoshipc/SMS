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


        public String report_status { get; set; }
        public String biko { get; set; }
        public String chk_date { get; set; }
        public String chk_name_id { get; set; }
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
