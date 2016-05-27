using IGBT_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGBT_BLL.Interfaces
{
    public interface IService
    {
        IGBTData GetData();
    }
}
