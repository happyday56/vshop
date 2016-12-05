namespace Hidistro.Entities.Store
{
    using Hishop.Components.Validation.Validators;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class VoteInfo
    {
        public DateTime EndDate { get; set; }

        public string ImageUrl { get; set; }

        public bool IsBackup { get; set; }

        public string Keys { get; set; }

        [RangeValidator(1, RangeBoundaryType.Inclusive, 100, RangeBoundaryType.Inclusive, Ruleset="ValVote", MessageTemplate="最多可选项数不允许为空，范围为1-100之间的整数")]
        public int MaxCheck { get; set; }

        public DateTime StartDate { get; set; }

        public int VoteCounts { get; set; }

        public long VoteId { get; set; }

        public IList<VoteItemInfo> VoteItems { get; set; }

        [StringLengthValidator(1, 60, Ruleset="ValVote", MessageTemplate="投票调查的标题不能为空，长度限制在60个字符以内")]
        public string VoteName { get; set; }
    }
}

