using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSサンプル
{
    public class MailaddressDS
    {
        //ユーザ区分
        public String kubun { get; set; } //ユーザ区分
        public String opetantouno { get; set; } 
        public String user_tantou_name { get; set; } 
        public String userno { get; set; } 
        public String username { get; set; } 

        public String addressNo { get; set; }   //アドレス番号
        public String mailAddress { get; set; }
        public String addressname { get; set; }
        public String chk_date { get; set; }
        public String chk_name_id { get; set; }

    }
}
