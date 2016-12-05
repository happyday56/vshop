namespace Hidistro.Entities.Orders
{
    using Hidistro.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class ReturnsApplyQuery : Pagination
    {
        public int? HandleStatus { get; set; }

        public string OrderId { get; set; }

        public string ReturnsId { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public DateTime? ApplyForStartTime { get; set; }

        public DateTime? ApplyForEndTime { get; set; }

        public int? OrderTypeId { get; set; }

        public int? OrderStatusId { get; set; }
    }
}

