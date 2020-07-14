﻿using System;
using PureGym.Interfaces.Common;

namespace PureGym.Models.Summary
{
    public struct ItemSummary : IIsAnItemSummary
    {
        public string Key { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; }

    }
}
