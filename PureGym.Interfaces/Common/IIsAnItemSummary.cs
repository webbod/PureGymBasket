using System;

namespace PureGym.Interfaces.Common
{
    public interface IIsAnItemSummary
    {
        string Description { get; set; }
        Guid Id { get; set; }
        string Key { get; set; }
    }
}