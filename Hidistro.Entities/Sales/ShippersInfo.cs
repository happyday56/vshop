namespace Hidistro.Entities.Sales
{
    using Hidistro.Core;
    using Hishop.Components.Validation.Validators;
    using System;
    using System.Runtime.CompilerServices;

    public class ShippersInfo
    {
        [StringLengthValidator(1, 300, Ruleset="Valshipper", MessageTemplate="详细地址不能为空，长度限制在300个字符以内"), HtmlCoding]
        public string Address { get; set; }

        [StringLengthValidator(0, 20, Ruleset="Valshipper", MessageTemplate="手机号码的长度限制在20个字符以内"), HtmlCoding]
        public string CellPhone { get; set; }

        public int DistributorUserId { get; set; }

        public bool IsDefault { get; set; }

        public int RegionId { get; set; }

        [StringLengthValidator(0, 300, Ruleset="Valshipper", MessageTemplate="备注的长度限制在300个字符以内"), HtmlCoding]
        public string Remark { get; set; }

        public int ShipperId { get; set; }

        [StringLengthValidator(2, 20, Ruleset="Valshipper", MessageTemplate="发货人姓名不能为空，长度在2-20个字符之间"), RegexValidator(@"[\u4e00-\u9fa5a-zA-Z]+[\u4e00-\u9fa5_a-zA-Z0-9]*", Ruleset="Valshipper", MessageTemplate="发货人姓名只能是汉字或字母开头"), HtmlCoding]
        public string ShipperName { get; set; }

        [HtmlCoding, StringLengthValidator(1, 30, Ruleset="Valshipper", MessageTemplate="发货点不能为空，长度限制在30个字符以内")]
        public string ShipperTag { get; set; }

        [StringLengthValidator(0, 20, Ruleset="Valshipper", MessageTemplate="电话号码的长度限制在20个字符以内"), HtmlCoding]
        public string TelPhone { get; set; }

        [StringLengthValidator(0, 20, Ruleset="Valshipper", MessageTemplate="邮编的长度限制在20个字符以内"), HtmlCoding]
        public string Zipcode { get; set; }
    }
}

