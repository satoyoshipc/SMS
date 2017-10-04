using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moss_AP
{
    public class watch_InterfaceDS
    {

        public String watch_Interfaceno { get; set; }
        public String interfacename { get; set; }
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
        public String type { get; set; }
        public String kanshi { get; set; }
        public String start_date { get; set; }
        public String end_date { get; set; }
        public String border { get; set; }
        public String IPaddress { get; set; }
        public String IPaddressNAT { get; set; }
        public String biko { get; set; }
        public String host_no { get; set; }
        public String userno { get; set; }
        public String systemno { get; set; }
        public String siteno { get; set; }
        public String chk_date { get; set; }
        public String chk_name_id { get; set; }

    }
}
