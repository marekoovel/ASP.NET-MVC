using IGBT880_DAL.Interfaces;
using IGBT880_DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGBT880_DAL.Interfaces
{
    public interface IGBT880_IUnitOfWork
    {
        //save pending changes to the data store
        void Commit();
        void RefreshAllEntities();
        //UOW Methods, that dont fit into specific repo
        //get repository for type
        T GetRepository<T>() where T : class;
        // IEFRepository<Results> Results { get; } //EEMAOOV temporarily commented out 28.04.2016
        IResultsRepository Results { get; }


    }
}
