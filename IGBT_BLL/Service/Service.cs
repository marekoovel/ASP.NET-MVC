using IGBT_DAL.Interfaces;
using IGBT_DAL;
using IGBT880_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGBT_BLL.Models;
using IGBT_BLL.Interfaces;
using IGBT880_DAL.Interfaces;

namespace IGBT_BLL.Service
{
    public class Service:IService
    {
        private readonly IGBT_IUnitOfWork _uowIGBT;
        private readonly IGBT880_IUnitOfWork _uowIGBT880;

        public Service(IGBT_IUnitOfWork uowIGBT, IGBT880_IUnitOfWork uowIGBT880)
        {
            _uowIGBT = uowIGBT;
            _uowIGBT880 = uowIGBT880;

        }

        public IGBTData GetData()
        {
            IGBTData igbtData = new IGBTData();

            DateTime dataFromDate = DateTime.Now.AddDays(-7);
            igbtData.IGBT_dateLabel = dataFromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString();

            DateTime lastWeekStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6);
            DateTime lastWeekEnd = lastWeekStart.AddDays(7);

            // IGBT DATA
            var production_IGBT_thisweek = _uowIGBT.Results.IGBTProductionThisWeek(dataFromDate);

            List<IGBT_DB.Results> res_IGBT = new List<IGBT_DB.Results>();

            foreach (IGBT_DB.Results temp1 in production_IGBT_thisweek)
            {
                if (res_IGBT.FirstOrDefault(x => x.IGBT_AGDR_SAP_barcode.Equals(temp1.IGBT_AGDR_SAP_barcode)) == null)
                    res_IGBT.Add(temp1);
            }
            int manufacturedCount_IGBT = res_IGBT.Count;
            int firstPassCouant_IGBT = res_IGBT.Count(x => x.Approval.Equals("R7+R8"));

            double fpy_IGBT = (float)((float)firstPassCouant_IGBT / (float)manufacturedCount_IGBT * 100.0);

            fpy_IGBT = Math.Round(fpy_IGBT, 2);
            igbtData.IGBT_agdr_count = manufacturedCount_IGBT.ToString();
            igbtData.IGBT_agdr_fpy = fpy_IGBT + " %";


            if (fpy_IGBT >= 97)
            {
                igbtData.IGBT_agdr_fpy_lightUrl = "~/Content/Images/foor_roheline.jpg";
            }
            else if (fpy_IGBT < 90)
            {
                igbtData.IGBT_agdr_fpy_lightUrl = "~/Content/images/foor_punane.jpg";
            }
            else
            {
                igbtData.IGBT_agdr_fpy_lightUrl = "~/Content/images/foor_kollane.jpg";
            }

            //Last week fpy
            var production_IGBT_lastWeek = _uowIGBT.Results.IGBTProductionLastWeek(lastWeekStart, lastWeekEnd);

            List<IGBT_DB.Results> res_IGBT_lastweek = new List<IGBT_DB.Results>();

            foreach (IGBT_DB.Results temp1 in production_IGBT_lastWeek)
            {
                if (res_IGBT_lastweek.FirstOrDefault(x => x.IGBT_AGDR_SAP_barcode.Equals(temp1.IGBT_AGDR_SAP_barcode)) == null) 
                    res_IGBT_lastweek.Add(temp1);
            }

            double fpy_IGBT_lastWeek =
                (float)
                    ((float)res_IGBT_lastweek.Count(x => x.Approval.Equals("R7+R8")) / (float)res_IGBT_lastweek.Count *
                     100.0);

            double fpy_IGBT_diff = Math.Round(fpy_IGBT) - Math.Round(fpy_IGBT_lastWeek);


            if (fpy_IGBT_diff < 0)
                igbtData.IGBT_agdr_fpyColor = "red";
            else if (fpy_IGBT_diff > 0)
                igbtData.IGBT_agdr_fpyColor = "green";
            else
                igbtData.IGBT_agdr_fpyColor = "yellow";



            //IGBT800 DATA
            var production_IGBT880_thisweek = _uowIGBT880.Results.IGBT880ProductionThisWeek(dataFromDate);

            List<IGBT880_DB.Results> res_IGBT880 = new List<IGBT880_DB.Results>();

            foreach (IGBT880_DB.Results temp1 in production_IGBT880_thisweek)
            {
                if (res_IGBT880.FirstOrDefault(x => x.IGBT_AGDR_SAP_barcode.Equals(temp1.IGBT_AGDR_SAP_barcode)) == null)
                    res_IGBT880.Add(temp1);
            }

            int manufacturedCount_IGBT880 = res_IGBT880.Count;
            int firstPassCouant_IGBT880 = res_IGBT880.Count(x => x.Approval.Equals("R7+R8"));

            double fpy_IGBT880 = (float)((float)firstPassCouant_IGBT880 / (float)manufacturedCount_IGBT880 * 100.0);

            fpy_IGBT880 = Math.Round(fpy_IGBT880, 2);
            igbtData.IGBT880_bgda_count = manufacturedCount_IGBT880.ToString();
            igbtData.IGBT880_bgda_fpy = fpy_IGBT880 + " %";

            if (fpy_IGBT880 >= 97)
            {
                igbtData.IGBT880_bgda_fpy_lightUrl = "~/Content/Images/foor_roheline.jpg";
            }
            else if (fpy_IGBT880 < 90)
            {
                igbtData.IGBT880_bgda_fpy_lightUrl = "~/Content/Images/foor_punane.jpg";
            }
            else
            {
                igbtData.IGBT880_bgda_fpy_lightUrl = "~/Content/Images/foor_kollane.jpg";
            }

            //Last week fpy
            var production_IGBT880_lastWeek = _uowIGBT880.Results.IGBT880ProductionLastWeek(lastWeekStart, lastWeekEnd);

            List<IGBT880_DB.Results> res_IGBT880_lastweek = new List<IGBT880_DB.Results>();

            foreach (IGBT880_DB.Results temp1 in production_IGBT880_lastWeek)
            {
                if (
                    res_IGBT880_lastweek.FirstOrDefault(x => x.IGBT_AGDR_SAP_barcode.Equals(temp1.IGBT_AGDR_SAP_barcode)) ==
                    null) res_IGBT880_lastweek.Add(temp1);
            }

            double fpy_IGBT880_lastWeek =
                (float)
                    ((float)res_IGBT880_lastweek.Count(x => x.Approval.Equals("R7+R8")) /
                     (float)res_IGBT880_lastweek.Count * 100.0);

            double fpy_IGBT880_diff = Math.Round(fpy_IGBT880) - Math.Round(fpy_IGBT880_lastWeek);

            if (fpy_IGBT880_diff < 0)
                igbtData.IGBT880_bgda_fpyColor = "red";
            else if (fpy_IGBT880_diff > 0)
                igbtData.IGBT880_bgda_fpyColor = "green";
            else
                igbtData.IGBT880_bgda_fpyColor = "yellow";



            return igbtData;
        }

    }
}
