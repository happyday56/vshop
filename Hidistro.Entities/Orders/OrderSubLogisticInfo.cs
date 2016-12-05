using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.Entities.Orders
{
    public class OrderSubLogisticInfo
    {
        public string SubOrderId { get; set; }

        public string OrderId { get; set; }

        public int ProductBrandId { get; set; }

        public string SubExpressCompanyAbb { get; set; }

        public string SubExpressCompanyName { get; set; }

        public string SubShipOrderNumber { get; set; }

    }
}
