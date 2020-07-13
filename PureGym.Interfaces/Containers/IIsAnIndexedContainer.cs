using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Interfaces.Containers
{
    public interface IIsAnIndexedContainer<TEntityType> where TEntityType : IIsAnEntity, new()
    {
        TEntityType Find(string key);

        List<TEntityType> Find(Func<TEntityType, bool> expr);

        List<TEntityType> GetAll();

        void Insert(TEntityType obj);
                
        void Remove(string key);

    }
}
