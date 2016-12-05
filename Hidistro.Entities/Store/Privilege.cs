namespace Hidistro.Entities.Store
{
    using System;
using System.Collections.Generic;

    public enum Privilege
    {
        AddMemberGrade = 0x138d,
        AddProductCategory = 0xbce,
        AddProducts = 0xbba,
        AddProductType = 0xbca,
        BrandCategories = 0xbd1,
        ClientActivy = 0x1b61,
        ClientGroup = 0x1b5f,
        ClientNew = 0x1b60,
        ClientSleep = 0x1b62,
        CofimOrderPay = 0xfa4,
        CountDown = 0x1f46,
        Coupons = 0x1f47,
        DeleteMember = 0x138b,
        DeleteMemberGrade = 0x138f,
        DeleteOrder = 0xfa2,
        DeleteProductCategory = 0xbd0,
        DeleteProducts = 0xbbc,
        DeleteProductType = 0xbcc,
        EditMember = 0x138a,
        EditMemberGrade = 0x138e,
        EditOrders = 0xfa3,
        EditProductCategory = 0xbcf,
        EditProducts = 0xbbb,
        EditProductType = 0xbcb,
        ExpressComputerpes = 0x3ef,
        ExpressPrint = 0xfa6,
        ExpressTemplates = 0xfa9,
        Gifts = 0x1f41,
        GroupBuy = 0x1f45,
        InStockProduct = 0xbbd,
        MemberArealDistributionStatistics = 0x2718,
        MemberGrades = 0x138c,
        MemberRanking = 0x2717,
        Members = 0x1389,
        OffShelfProducts = 0xbbf,
        OrderPromotion = 0x1f43,
        OrderRefundApply = 0xfac,
        OrderReplaceApply = 0xfad,
        OrderReturnsApply = 0xfae,
        Orders = 0xfa1,
        OrderSendGoods = 0xfa5,
        OrderStatistics = 0x2712,
        PaymentModes = 0x3ec,
        PictureMange = 0x3f1,
        ProductBatchExport = 0xbd2,
        ProductBatchUpload = 0xbc4,
        ProductCategory = 0xbcd,
        ProductPromotion = 0x1f42,
        Products = 0xbb9,
        ProductSaleRanking = 0x2715,
        ProductSaleStatistics = 0x2716,
        ProductTypes = 0xbc9,
        ProductUnclassified = 0xbc2,
        RemarkOrder = 0xfa8,
        SaleDetails = 0x2713,
        SaleReport = 0x2711,
        SaleTargets = 0x2714,
        Shippers = 0xfaa,
        ShippingModes = 0x3ed,
        ShippingTemplets = 0x3ee,
        SiteSettings = 0x3e9,
        SubjectProducts = 0xbc3,
        Summary = 0x3e8,
        UpShelfProducts = 0xbbe,
        UserIncreaseStatistics = 0x2719,
        Votes = 0x7d9
    }

    //上面对应的编码和js还有xml里面对应的编码不一致，不知道哪个是标准的，而且系统本身没有实现权限控制，
    //暂时由后台写死。
   

    public class PrivilegeModule
    {
        public int Privilege { get; set; }
        public string PrivilegeCode { get; set; }
        public int ParentId { get; set; }
        public string PrivilegeName { get; set; }
        public string TemplateString { get; set; }
        public string Url { get; set; }
    }

    public static class PrivilegeContext
    {
        public static List<PrivilegeModule> GetPrivilegeModule()
        {
            return new List<PrivilegeModule> { 
                new PrivilegeModule{
                    Privilege = 10001,
                    PrivilegeName = "微配置",
                    TemplateString ="<a onclick=\"ShowMenuLeft('微配置',null,null)\">微配置</a>",
                    Url ="vshop/ReplyOnKey.aspx"
                },
                new PrivilegeModule{
                    Privilege = 10002,
                    PrivilegeName = "微会员",
                    TemplateString ="<a onclick=\"ShowMenuLeft('微会员',null,null)\">微会员</a>",
                    Url ="member/managemembers.aspx"
                },
                 new PrivilegeModule{
                    Privilege = 10003,
                    PrivilegeName = "微营销",
                    TemplateString ="<a onclick=\"ShowMenuLeft('微营销',null,null)\">微营销</a>",
                    Url =""
                },
                 new PrivilegeModule{
                    Privilege = 10004,
                    PrivilegeName = "微商品",
                    TemplateString ="<a onclick=\"ShowMenuLeft('微商品',null,null)\">微商品</a>",
                    Url ="product/selectcategory.aspx"
                },
                 new PrivilegeModule{
                    Privilege = 10005,
                    PrivilegeName = "微分销",
                    TemplateString ="<a onclick=\"ShowMenuLeft('微分销',null,null)\">微分销</a>",
                    Url =""
                },
                 new PrivilegeModule{
                    Privilege = 10006,
                    PrivilegeName = "微订单",
                    TemplateString ="<a onclick=\"ShowMenuLeft('微订单',null,null)\">微订单</a>",
                    Url ="sales/manageorder.aspx"
                },
                 new PrivilegeModule{
                    Privilege = 10007,
                    PrivilegeName = "微统计",
                    TemplateString ="<a onclick=\"ShowMenuLeft('微统计',null,null)\">微统计</a>",
                    Url ="sales/salereport.aspx"
                },
                 new PrivilegeModule{
                    Privilege = 10008,
                    PrivilegeName = "系统工具",
                    TemplateString ="<a onclick=\"ShowMenuLeft('系统工具',null,null)\">系统工具</a>",
                    Url =""
                },
                new PrivilegeModule{
                    Privilege = 10009,
                    PrivilegeName = "上架商品",
                    TemplateString = "",
                    Url = ""
                },
                new PrivilegeModule{
                    Privilege = 10010,
                    PrivilegeName = "查看所有商品",
                    TemplateString = "",
                    Url = ""
                },
            };
        }
    }

}

