namespace Hidistro.Entities.Members
{
    using System;
    using System.Runtime.CompilerServices;

    public class DistributorGradeInfo
    {
        public decimal CommissionsLimit { get; set; }
        public string Description { get; set; }
        public decimal FirstCommissionRise { get; set; }
        public int GradeId { get; set; }
        public string Ico { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public decimal SecondCommissionRise { get; set; }
        public decimal ThirdCommissionRise { get; set; }

        /// <summary>
        /// 推荐收入
        /// </summary>
        public decimal RecommendedIncome { get; set; }
        /// <summary>
        /// 额外提成
        /// </summary>
        public decimal AdditionalFees { get; set; }
    }
}

