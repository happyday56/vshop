namespace Hidistro.Entities.Store
{
    using Hishop.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class VoteItemInfo
    {
        public int ItemCount { get; set; }

        public decimal Lenth
        {
            get
            {
                return this.Percentage * Convert.ToDecimal((double)4.2);
            }
        }

        public decimal Percentage { get; set; }

        public long VoteId { get; set; }

        public long VoteItemId { get; set; }

        [StringLengthValidator(1, 60, Ruleset = "VoteItem", MessageTemplate = "提供给用户选择的内容，长度限制在60个字符以内")]
        public string VoteItemName { get; set; }
    }
}

