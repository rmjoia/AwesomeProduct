using System;
using System.Collections.Generic;

namespace AwesomeProduct.Persistence
{
    public interface IRepository<T>
    {
        T GetLast();
        int Insert(T item);
    }
}
