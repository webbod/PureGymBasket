using PureGym.Common;

namespace PureGym.Interfaces.Common
{
    public interface IIsABasketItem : IIsAnInventoryItem
    {
        int Quantity { get; }

        void Update(IIsAnInventoryItem item = default(IIsAnInventoryItem), int quantity = 1);

        void UpdateQuantity(int quantity);
    }
}
