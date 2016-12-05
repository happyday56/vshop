namespace Hidistro.Entities.Members
{
    using Hidistro.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class MemberQuery : Pagination
    {
        public string CharSymbol { get; set; }

        public string ClientType { get; set; }

        public DateTime? EndTime { get; set; }

        public int? GradeId { get; set; }

        public bool? HasVipCard { get; set; }

        public bool? IsApproved { get; set; }

        public decimal? OrderMoney { get; set; }

        public int? OrderNumber { get; set; }

        public string Realname { get; set; }

        public DateTime? StartTime { get; set; }

        public string Username { get; set; }
    }
}

