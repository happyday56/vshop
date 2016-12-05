namespace Hidistro.Entities.Members
{
    using Hidistro.Core.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class DistributorsInfo : MemberInfo
    {
        public DateTime? AccountTime { get; set; }

        public string BackImage { get; set; }

        public DateTime CreateTime { get; set; }

        public DistributorGrade DistributorGradeId { get; set; }

        public int DistriGradeId { get; set; }

        public string Logo { get; set; }

        public decimal OrdersTotal { get; set; }

        public int? ParentUserId { get; set; }

        public decimal ReferralBlance { get; set; }

        public int ReferralOrders { get; set; }

        public string ReferralPath { get; set; }

        public decimal ReferralRequestBalance { get; set; }

        public int ReferralStatus { get; set; }

        public string RequestAccount { get; set; }

        public string StoreDescription { get; set; }

        public string StoreName { get; set; }

        public string BankName { get; set; }

        public string BankAddress { get; set; }

        public string AccountName { get; set; }

        /// <summary>
        /// 邀请限额数
        /// </summary>
        public int InvitationNum { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal AccountBalance { get; set; }

        /// <summary>
        /// 累计收益
        /// </summary>
        public decimal AccumulatedIncome { get; set; }


        /// <summary>
        /// 是否绑定手机号码分销商
        /// </summary>
        public int IsBindMobile { get; set; }

        public int VistiCounts { get; set; }

        public int GoodCounts { get; set; }

        public string RegionAddress { get; set; }

        public int IsTempStore { get; set; }

        public DateTime DeadlineTime { get; set; }

        public DateTime DecasualizationTime { get; set; }

    }
}

