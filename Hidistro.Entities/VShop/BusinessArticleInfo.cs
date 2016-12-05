using Hidistro.Core;
using Hishop.Components.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.Entities.VShop
{
    public class BusinessArticleInfo
    {
        public int ArticleId { get; set; }

        [HtmlCoding, StringLengthValidator(1, 60, Ruleset = "ValBusinessArticleInfo", MessageTemplate = "文件标题不能为空，长度限制在60个字符以内")]
        public string Title { get; set; }

        public string Summary { get; set; }

        public string IconUrl { get; set; }

        [StringLengthValidator(1, 0x3b9ac9ff, Ruleset = "ValBusinessArticleInfo", MessageTemplate = "文件内容不能为空")]
        public string ArtContent { get; set; }

        public DateTime AddedDate { get; set; }

        public int ReviewCnt { get; set; }

        public string PublishName { get; set; }

        public int DisplaySequence { get; set; }

    }
}
