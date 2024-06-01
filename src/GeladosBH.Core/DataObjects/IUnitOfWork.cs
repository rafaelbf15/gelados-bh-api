using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeladosBH.Core.DataObjects
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
