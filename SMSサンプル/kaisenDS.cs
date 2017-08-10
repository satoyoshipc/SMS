using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSサンプル
{
    public class kaisenDS
    {
        public String kaisenno { get; set; }
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


        public String career { get; set; }


        public String telno1 { get; set; }
        public String telno2 { get; set; }
        public String telno3 { get; set; }
        public String biko { get; set; }


        public String type { get; set; }
        public String kaisenid { get; set; }
        public String isp { get; set; }
        public String servicetype { get; set; }
        public String serviceid { get; set; }
        public String host_no { get; set; }
        public String userno { get; set; }
        public String systemno { get; set; }
        public String siteno { get; set; }

        public String chk_date { get; set; }
        public String chk_name_id { get; set; }
    }
}
