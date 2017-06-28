using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSサンプル
{
    public class hostDS
    {
        public String host_no { get; set; }
        public String hostname { get; set; }
        public String hostname_ja { get; set; }
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

        public String device { get; set; }
        public String location { get; set; }
        public String usefor { get; set; }
        public String kansiStartdate { get; set; }
        public String kansiEndsdate { get; set; }
        public String hosyukanri { get; set; }
        public String hosyuinfo { get; set; }
        public String biko { get; set; }
        public String siteno { get; set; }
        public String sitename { get; set; }
        public String userno { get; set; }
        public String username { get; set; }

        public String systemno { get; set; }
        public String systemname { get; set; }
        public String chk_date { get; set; }
        public String chk_name_id { get; set; }

        public static implicit operator string(hostDS v)
        {
            throw new NotImplementedException();
        }

        internal void add()
        {
            throw new NotImplementedException();
        }
    }
}
