using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.Entities.VShop
{
    public class VPGiftInfo
    {
        public int VPGiftId { get; set; }

        public int VPGiftType { get; set; }

        public string VPGiftName { get; set; }

        public int VPGiftCategory { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IList<VPGiftDetailInfo> VPGiftItems { get; set; }

    }
}
