using PureGym.Common;
using PureGym.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Interfaces.Common
{
    public interface IIsAnInventoryItem : IIsAnEntity, IHasAValue
    {
        string Description { get; }

        Money Price { get; }

        StockCategory Category { get;  }
    }
}
