namespace Hidistro.Entities.Sales
{
    using Hidistro.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class SaleStatisticsQuery : Pagination
    {
        public DateTime? EndDate { get; set; }

        public string QueryKey { get; set; }

        public DateTime? StartDate { get; set; }

        public string OrderId { get; set; }

        public int? OrderTypeId { get; set; }

        public int ReturnStatus { get; set; }

        public int? OrderStatus { get; set; }

        public string OrderStatusName { get; set; }

        public DateTime? OrderStartDate { get; set; }

        public DateTime? OrderEndDate { get; set; }

        public DateTime? PayStartDate { get; set; }

        public DateTime? PayEndDate { get; set; }

        public DateTime? ShippingStartDate { get; set; }

        public DateTime? ShippingEndDate { get; set; }

    }
}

