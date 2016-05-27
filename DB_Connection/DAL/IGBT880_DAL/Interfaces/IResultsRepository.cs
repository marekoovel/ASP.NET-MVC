using IGBT880_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGBT880_DB;

namespace IGBT880_DAL.Interfaces
{
    public interface IResultsRepository : IEFRepository<Results>
    {
        List<Results> IGBT880ProductionThisWeek(DateTime dataFromDate);
        List<Results> IGBT880ProductionLastWeek(DateTime lastWeekStart, DateTime lastWeekEnd);
    }
}
