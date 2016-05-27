using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGBT880_DB;
using IGBT880_DAL.Repositories;
using IGBT880_DAL.Interfaces;

namespace IGBT880_DAL.Repositories
{
    public class ResultsRepository : EFRepository<Results>, IResultsRepository
    {
        public ResultsRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public List<Results> IGBT880ProductionThisWeek(DateTime dataFromDate)
        {
            return DbSet.Where(x => x.Date_modified > dataFromDate).OrderBy(e => e.TestId).ToList();
        }

        public List<Results> IGBT880ProductionLastWeek(DateTime lastWeekStart, DateTime lastWeekEnd)
        {
            return DbSet.Where(x => x.Date_modified > lastWeekStart && x.Date_modified < lastWeekEnd)
                    .OrderBy(e => e.TestId)
                    .ToList();
        }
    }
}
