namespace Hidistro.Entities.Orders
{
    using System;
    using System.Runtime.CompilerServices;

    public class BalanceDrawRequestInfo
    {
        public string AccountName { get; set; }

        public decimal Amount { get; set; }

        public string CellPhone { get; set; }

        public DateTime CheckTime { get; set; }

        public bool IsCheck { get; set; }

        public string MerchanCade { get; set; }

        public string Remark { get; set; }

        public DateTime RequestTime { get; set; }

        public int RequesType { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string BankName { get; set; }

        public string BankAddress { get; set; }

        public string RegionAddress { get; set; }

    }
}

