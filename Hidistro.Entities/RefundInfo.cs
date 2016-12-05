namespace Hidistro.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class RefundInfo
    {
        public string Account { get; set; }

        public string AdminRemark { get; set; }

        public DateTime ApplyForTime { get; set; }

        public string AuditTime { get; set; }

        public Handlestatus HandleStatus { get; set; }

        public DateTime HandleTime { get; set; }

        public string Operator { get; set; }

        public string OrderId { get; set; }

        public int ProductId { get; set; }

        public int RefundId { get; set; }

        public decimal RefundMoney { get; set; }

        public string RefundRemark { get; set; }

        public string RefundTime { get; set; }

        public int RefundType { get; set; }

        public int UserId { get; set; }

        public enum Handlestatus
        {
            Applied = 1,
            AuditNotThrough = 7,
            HasTheAudit = 5,
            NoneAudit = 4,
            NoRefund = 6,
            Refunded = 2,
            Refused = 3,
            RefuseRefunded = 8
        }
    }
}

