using Hidistro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.Entities.VShop
{
    public class VPGiftQuery : Pagination
    {
        public string VPGiftId { get; set; }

        public string VPGiftName { get; set; }

        public string State { get; set; }

    }
}
