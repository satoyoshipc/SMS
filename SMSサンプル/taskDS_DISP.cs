using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moss_AP
{
    public class taskDS_DISP
    {
        public String schedule_no { get; set; }
        public String schedule_type { get; set; }
        public String templeteno { get; set; }
        public String naiyou { get; set; }
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

        public String startdate { get; set; }
        public String enddate { get; set; }
        public String biko { get; set; }
        public String userno { get; set; }
        public String username { get; set; }
        public String ins_date { get; set; }
        public String ins_name_id { get; set; }
        public String chk_date { get; set; }
        public String chk_name_id { get; set; }



    }
}
