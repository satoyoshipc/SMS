using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSサンプル
{
    public class tantouDS
    {
        
        public String userno { get; set; }
        public String username { get; set; }

        public String user_tantou_no { get; set; }
        public String user_tantou_name { get; set; }
        public String user_tantou_name_kana { get; set; }
        public String busho_name { get; set; }
        public String telno1 { get; set; }
        public String telno2 { get; set; }
        public String yakusyoku { get; set; }
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
        public String chk_date { get; set; }
        public String chk_name_id { get; set; }

    }
}
