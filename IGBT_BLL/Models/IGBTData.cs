using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGBT_BLL.Models
{
    public class IGBTData
    {
        //IGBT
        public String IGBT_dateLabel {get; set;}
        public String IGBT_agdr_count { get; set; }
        public String IGBT_agdr_fpy { get; set; }
        public String IGBT_agdr_fpy_lightUrl { get; set;}
        public String IGBT_agdr_fpyColor { get; set; }

        //IGBT880
        public String IGBT880_dateLabel { get; set; }
        public String IGBT880_bgda_count { get; set; }
        public String IGBT880_bgda_fpy { get; set; }
        public String IGBT880_bgda_fpy_lightUrl { get; set; }
        public String IGBT880_bgda_fpyColor {get; set;}
    }
}
