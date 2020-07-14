using PureGym.Common;
using System;

namespace PureGym.Interfaces.Common
{
    public interface IIsAnEntity : IHasAValue
    {
        Guid Id { get; }

        string Key { get; }

        string Description { get; }

        Money Value { get; }
    }
}
