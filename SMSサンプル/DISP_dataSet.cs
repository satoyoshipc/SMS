using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSサンプル
{
    public class DISP_dataSet
    {
        public List<userDS>             user_L { get; set; }
        public List<systemDS>           system_L { get; set; }
        public List<siteDS>             site_L { get; set; }
        public List<hostDS>             host_L { get; set; }
        public List<watch_InterfaceDS>  watch_L { get; set; }
        public List<kaisenDS>           kaisen_L { get; set; }
    }
}
