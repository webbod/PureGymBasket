using PureGym.Common.Enumerations;
using PureGym.Interfaces.Common;

namespace PureGym.Interfaces.ShoppingSystem
{
    public interface IIsAWarehouse
    {
        void Initalise(Currency currency);

        IIsABasketItem GetItem(string key);
    }
}