using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.Entities.VShop
{
    public class DistributorTree
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserHead { get; set; }

        public int ParentId { get; set; }

        public string StoreName { get; set; }

        public string GradeName { get; set; }

        public List<DistributorTree> Childs { get; set; }

    }
}
