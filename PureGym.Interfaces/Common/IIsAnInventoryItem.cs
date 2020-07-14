using PureGym.Common;
using PureGym.Common.Enumerations;

namespace PureGym.Interfaces.Common
{
    public interface IIsAnInventoryItem : IIsAnEntity
    {
        StockCategory Category { get;  }
    }
}
