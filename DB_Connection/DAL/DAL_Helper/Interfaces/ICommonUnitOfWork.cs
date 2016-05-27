using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_DAL.Interfaces
{
   public interface ICommonUnitOfWork
    {
        //save pending changes to the data store
        void Commit();
        //TODO here is common unit of work

 //       IGBT_DAL.Interfaces.IGBT_IUnitOfWork igbt{ get; }
 //       IGBT880_DAL.Interfaces.IGBT880_IUnitOfWork igbt880 { get; }

    }
}
