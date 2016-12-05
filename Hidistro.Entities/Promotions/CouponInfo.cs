namespace Hidistro.Entities.Promotions
{
    using Hidistro.Core;
    using Hishop.Components.Validation;
    using Hishop.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    [HasSelfValidation]
    public class CouponInfo
    {
        public CouponInfo()
        {
        }

        public CouponInfo(string name, DateTime closingTime, DateTime startTime, string description, decimal? amount, decimal discountValue)
        {
            this.Name = name;
            this.ClosingTime = closingTime;
            this.StartTime = startTime;
            this.Description = description;
            this.Amount = amount;
            this.DiscountValue = discountValue;
        }

        public CouponInfo(int couponId, string name, DateTime closingTime, DateTime startTime, string description, decimal? amount, decimal discountValue)
        {
            this.CouponId = couponId;
            this.Name = name;
            this.ClosingTime = closingTime;
            this.StartTime = startTime;
            this.Description = description;
            this.Amount = amount;
            this.DiscountValue = discountValue;
        }

        [SelfValidation(Ruleset="ValCoupon")]
        public void CompareAmount(ValidationResults result)
        {
            decimal? nullable;
            if (this.Amount.HasValue && ((this.DiscountValue > (nullable = this.Amount).GetValueOrDefault()) && nullable.HasValue))
            {
                result.AddResult(new ValidationResult("折扣值不能大于满足金额", this, "", "", null));
            }
        }

        [RangeValidator(typeof(decimal), "0.01", RangeBoundaryType.Inclusive, "10000000", RangeBoundaryType.Inclusive, Ruleset="ValCoupon"), NotNullValidator(Negated=true, Ruleset="ValCoupon"), ValidatorComposition(CompositionType.Or, Ruleset="ValCoupon", MessageTemplate="满足金额，金额大小0.01-1000万之间")]
        public decimal? Amount { get; set; }

        public DateTime ClosingTime { get; set; }

        public int CouponId { get; set; }

        public string Description { get; set; }

        [RangeValidator(typeof(decimal), "0.01", RangeBoundaryType.Inclusive, "10000000", RangeBoundaryType.Inclusive, Ruleset="ValCoupon", MessageTemplate="可抵扣金额不能为空，金额大小0.01-1000万之间")]
        public decimal DiscountValue { get; set; }

        [HtmlCoding, StringLengthValidator(1, 60, Ruleset="ValCoupon", MessageTemplate="优惠券名称不能为空，长度限制在1-60个字符之间")]
        public string Name { get; set; }

        [RangeValidator(0, RangeBoundaryType.Inclusive, 0x2710, RangeBoundaryType.Inclusive, Ruleset="ValCoupon", MessageTemplate="兑换所需积分不能为空，大小0-10000之间")]
        public int NeedPoint { get; set; }

        public int SentCount { get; set; }

        public DateTime StartTime { get; set; }

        public int UsedCount { get; set; }
    }
}

