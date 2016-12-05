namespace Hidistro.Entities.Members
{
    using System;
    using System.Runtime.CompilerServices;

    public class MemberInfo
    {
        public string Address { get; set; }

        public string CellPhone { get; set; }

        public DateTime CreateDate { get; set; }

        public string Email { get; set; }

        public decimal Expenditure { get; set; }

        public int GradeId { get; set; }

        public string MicroSignal { get; set; }

        public string OpenId { get; set; }

        public int OrderNumber { get; set; }

        public string Password { get; set; }

        public int Points { get; set; }

        public string QQ { get; set; }

        public string RealName { get; set; }

        public int ReferralUserId { get; set; }

        public int RegionId { get; set; }

        public DateTime SessionEndTime { get; set; }

        public string SessionId { get; set; }

        public int TopRegionId { get; set; }

        public string UserHead { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime? VipCardDate { get; set; }

        public string VipCardNumber { get; set; }

        public decimal VirtualPoints { get; set; }

        public string VerifyCode { get; set; }

        /// <summary>
        /// 是否能开店
        /// </summary>
        public int IsStore { get; set; }

    }
}

