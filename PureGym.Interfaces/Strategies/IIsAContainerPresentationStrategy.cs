using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;
using System;
using System.Collections.Generic;

namespace PureGym.Interfaces.Strategies
{
    public interface IIsAContainerPresentationStrategy : IIsAStrategy
    {
        Func<List<TEntityType>, List<TOutputType>> Render<TEntityType, TOutputType>(TypesOfContainer container)
            where TEntityType : class, IIsAnEntity, IHasAValue, new()
            where TOutputType : class;
    }
}
