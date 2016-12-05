namespace Hidistro.Entities.VShop
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Hidistro.Entities.Sales;

    public class ActivitiesInfo
    {
        public string CoverImg { get; set; }

        public string ActivitiesDescription { get; set; }

        public int ActivitiesId { get; set; }

        public string ActivitiesName { get; set; }

        public int ActivitiesType { get; set; }

        public string CloseRemark { get; set; }

        public DateTime EndTIme { get; set; }

        public List<ShoppingCartItemInfo> LineItems { get; set; }

        public decimal MeetMoney { get; set; }

        public decimal ReductionMoney { get; set; }

        public DateTime StartTime { get; set; }

        public int Type { get; set; }

        public int IsDisplayHome { get; set; }

        private IList<int> productCategories;

        public IList<int> ProductCategories
        {
            get
            {
                if (null == this.productCategories)
                {
                    this.productCategories = new List<int>();
                }
                return this.productCategories;
            }
            set
            {
                this.productCategories = value;
            }
        }
    }
}

