namespace Hidistro.Entities.Members
{
    using Hidistro.Core.Entities;
    using System;
    using System.Runtime.CompilerServices;

    public class CommissionsQuery : Pagination
    {
        public string EndTime { get; set; }

        public string OrderNum { get; set; }

        public int ReferralUserId { get; set; }

        public string StartTime { get; set; }

        public string StoreName { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string OneStoreName { get; set; }

        public int? CommTypeId { get; set; }

        public int? OrderTypeId { get; set; }

        public int? IncomeTypeId { get; set; }
    }
}

