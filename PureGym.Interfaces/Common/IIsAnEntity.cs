using System;
using System.Collections.Generic;
using System.Text;

namespace PureGym.Interfaces.Common
{
    public interface IIsAnEntity
    {
        Guid Id { get; }

        string Key { get; }
    }
}
