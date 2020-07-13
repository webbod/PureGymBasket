using PureGym.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Interfaces.Common
{
    public interface IIsAVoucherItem : IIsAnEntity, IHasAValue
    {
        string Description { get; }

        Money Value { get; }

        bool Applied { get; }
    }
}
