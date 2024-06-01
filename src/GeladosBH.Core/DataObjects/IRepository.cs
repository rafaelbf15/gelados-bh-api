using GeladosBH.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeladosBH.Core.DataObjects
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
