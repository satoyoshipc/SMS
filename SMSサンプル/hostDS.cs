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
        public String status { get; set; }
        public String device { get; set; }
        public String location { get; set; }
        public String usefor { get; set; }
        public String kansiStartdate { get; set; }
        public String kansiEndsdate { get; set; }
        public String hosyukanri { get; set; }
        public String hosyuinfo { get; set; }
        public String biko { get; set; }
        public String siteno { get; set; }
        public String userno { get; set; }
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
