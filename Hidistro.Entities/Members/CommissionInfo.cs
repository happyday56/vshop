using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.Entities.Members
{
    public class CommissionInfo
    {
        public int CommId { get; set; }

        public int UserId { get; set; }

        public int ReferralUserId { get; set; }

        public string OrderId { get; set; }

        public DateTime TradeTime { get; set; }

        public decimal OrderTotal { get; set; }

        public decimal CommTotal { get; set; }

        public int CommType { get; set; }

        public bool State { get; set; }

        public string CommRemark { get; set; }

        public int OrderFromStoreId { get; set; }

        public int CommOrderStatus { get; set; }
    }
}
