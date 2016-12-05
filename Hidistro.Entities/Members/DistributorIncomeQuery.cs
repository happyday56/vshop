using Hidistro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.Entities.Members
{
    public class DistributorIncomeQuery : Pagination
    {
        public string StoreName { get; set; }

        public string CellPhone { get; set; }

    }
}
