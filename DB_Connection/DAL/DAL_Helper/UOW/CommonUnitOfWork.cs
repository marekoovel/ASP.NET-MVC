using Common_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_DAL.UOW
{
    public class CommonUnitOfWork : ICommonUnitOfWork, IDisposable
    {
        public IGBT_DAL.Interfaces.IGBT_IUnitOfWork igbt{get{return null;}}

        public IGBT880_DAL.Interfaces.IGBT880_IUnitOfWork igbt880 {get{return null;}}


        public CommonUnitOfWork() {

        
        }

        public void Commit()
        {
            igbt.Commit();
            igbt880.Commit();
        }


        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
           
        }

        #endregion

        //TODO Here is common unit of work
    }

}
