using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;

namespace PureGym.Interfaces.Strategies
{
    public interface IIsAContainerPersistenceStrategy : IIsAStrategy
    {
        Func<List<TEntityType>, TOutputType> ExportData<TEntityType, TOutputType>(TypesOfContainer container) where TOutputType : class;
        Func<TImportType, List<TEntityType>> ImportData<TEntityType, TImportType>(TypesOfContainer container) where TImportType : class;
    }
}
