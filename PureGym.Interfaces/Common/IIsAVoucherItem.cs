using PureGym.Common;

namespace PureGym.Interfaces.Common
{
    public interface IIsAVoucherItem : IIsAnEntity, IHasAValue
    {
        bool Applied { get; }

        void ApplyVoucher();

        void DontApplyVoucher();
    }
}
