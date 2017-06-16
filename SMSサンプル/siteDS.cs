using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSサンプル
{
    public class siteDS
    {
        public String siteno { get; set; }
        public String sitename { get; set; }
        public String address1 { get; set; }
        public String address2 { get; set; }
        public String telno { get; set; }

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

        public String biko { get; set; }
        public String userno { get; set; }
        public String systemno { get; set; }
        public String username { get; set; }
        public String systemname { get; set; }
        public String chk_date { get; set; }
        public String chk_name_id { get; set; }
    }

}
