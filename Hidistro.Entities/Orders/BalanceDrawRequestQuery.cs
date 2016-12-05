namespace Hidistro.Entities.Orders
{
    using Hidistro.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class BalanceDrawRequestQuery : Pagination
    {
        public string CheckTime { get; set; }

        public string CheckStartTime { get; set; }

        public string CheckEndTime { get; set; }

        public string IsCheck { get; set; }

        public string RequestEndTime { get; set; }

        public string RequestStartTime { get; set; }

        public string RequestTime { get; set; }

        public string StoreName { get; set; }

        public string UserId { get; set; }

        public string Mobile { get; set; }

        public int? CheckType { get; set; }
    }
}

