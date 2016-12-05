using Hidistro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.Entities.Sales
{
    [Serializable]
    public class SearchStatisticsQuery : Pagination
    {
        public int SearchType { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? GradeId { get; set; }

        public string StoreName { get; set; }

        public string UserName { get; set; }


    }
}
