namespace Hidistro.Core.Entities
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public class SiteSettings
    {
        public SiteSettings(string siteUrl)
        {
            this.SiteUrl = siteUrl;
            this.Theme = "default";
            this.VTheme = "default";
            this.Disabled = false;
            this.SiteName = "全婴汇";
            this.LogoUrl = "/utility/pics/logo.jpg";
            this.DefaultProductImage = "/utility/pics/none.gif";
            this.DefaultProductThumbnail1 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail2 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail3 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail4 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail5 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail6 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail7 = "/utility/pics/none.gif";
            this.DefaultProductThumbnail8 = "/utility/pics/none.gif";
            this.WeiXinCodeImageUrl = "/Storage/master/WeiXinCodeImageUrl.jpg";
            this.VipCardBG = "/Storage/master/Vipcard/vipbg.png";
            this.VipCardQR = "/Storage/master/Vipcard/vipqr.jpg";
            this.VipCardPrefix = "100000";
            this.VipRequireName = true;
            this.VipRequireMobile = true;
            this.EnablePodRequest = false;
            this.DecimalLength = 2;
            this.PointsRate = 1M;
            this.OrderShowDays = 7;
            this.CloseOrderDays = 3;
            this.FinishOrderDays = 7;
            this.OpenManyService = false;
        }

        public static SiteSettings FromXml(XmlDocument doc)
        {
            XmlNode node = doc.SelectSingleNode("Settings");
            return new SiteSettings(node.SelectSingleNode("SiteUrl").InnerText) { 
                Theme = node.SelectSingleNode("Theme").InnerText, VTheme = node.SelectSingleNode("VTheme").InnerText, DecimalLength = int.Parse(node.SelectSingleNode("DecimalLength").InnerText), DefaultProductImage = node.SelectSingleNode("DefaultProductImage").InnerText, DefaultProductThumbnail1 = node.SelectSingleNode("DefaultProductThumbnail1").InnerText, DefaultProductThumbnail2 = node.SelectSingleNode("DefaultProductThumbnail2").InnerText, DefaultProductThumbnail3 = node.SelectSingleNode("DefaultProductThumbnail3").InnerText, DefaultProductThumbnail4 = node.SelectSingleNode("DefaultProductThumbnail4").InnerText, DefaultProductThumbnail5 = node.SelectSingleNode("DefaultProductThumbnail5").InnerText, DefaultProductThumbnail6 = node.SelectSingleNode("DefaultProductThumbnail6").InnerText, DefaultProductThumbnail7 = node.SelectSingleNode("DefaultProductThumbnail7").InnerText, DefaultProductThumbnail8 = node.SelectSingleNode("DefaultProductThumbnail8").InnerText, CheckCode = node.SelectSingleNode("CheckCode").InnerText, Disabled = bool.Parse(node.SelectSingleNode("Disabled").InnerText), Footer = node.SelectSingleNode("Footer").InnerText, RegisterAgreement = node.SelectSingleNode("RegisterAgreement").InnerText, 
                LogoUrl = node.SelectSingleNode("LogoUrl").InnerText, OrderShowDays = int.Parse(node.SelectSingleNode("OrderShowDays").InnerText), CloseOrderDays = int.Parse(node.SelectSingleNode("CloseOrderDays").InnerText), FinishOrderDays = int.Parse(node.SelectSingleNode("FinishOrderDays").InnerText), TaxRate = decimal.Parse(node.SelectSingleNode("TaxRate").InnerText), PointsRate = decimal.Parse(node.SelectSingleNode("PointsRate").InnerText), SiteName = node.SelectSingleNode("SiteName").InnerText, SiteUrl = node.SelectSingleNode("SiteUrl").InnerText, YourPriceName = node.SelectSingleNode("YourPriceName").InnerText, EmailSender = node.SelectSingleNode("EmailSender").InnerText, EmailSettings = node.SelectSingleNode("EmailSettings").InnerText, SMSSender = node.SelectSingleNode("SMSSender").InnerText, SMSSettings = node.SelectSingleNode("SMSSettings").InnerText, EnabledCnzz = bool.Parse(node.SelectSingleNode("EnabledCnzz").InnerText), CnzzUsername = node.SelectSingleNode("CnzzUsername").InnerText, CnzzPassword = node.SelectSingleNode("CnzzPassword").InnerText, 
                WeixinAppId = node.SelectSingleNode("WeixinAppId").InnerText, WeixinAppSecret = node.SelectSingleNode("WeixinAppSecret").InnerText, WeixinPaySignKey = node.SelectSingleNode("WeixinPaySignKey").InnerText, WeixinPartnerID = node.SelectSingleNode("WeixinPartnerID").InnerText, WeixinPartnerKey = node.SelectSingleNode("WeixinPartnerKey").InnerText, IsValidationService = bool.Parse(node.SelectSingleNode("IsValidationService").InnerText), WeixinToken = node.SelectSingleNode("WeixinToken").InnerText, WeixinNumber = node.SelectSingleNode("WeixinNumber").InnerText, WeixinLoginUrl = node.SelectSingleNode("WeixinLoginUrl").InnerText, WeiXinCodeImageUrl = node.SelectSingleNode("WeiXinCodeImageUrl").InnerText, VipCardLogo = node.SelectSingleNode("VipCardLogo").InnerText, VipCardBG = node.SelectSingleNode("VipCardBG").InnerText, VipCardQR = node.SelectSingleNode("VipCardQR").InnerText, VipCardName = node.SelectSingleNode("VipCardName").InnerText, VipCardPrefix = node.SelectSingleNode("VipCardPrefix").InnerText, VipRequireName = bool.Parse(node.SelectSingleNode("VipRequireName").InnerText), 
                VipRequireMobile = bool.Parse(node.SelectSingleNode("VipRequireMobile").InnerText), VipRequireAdress = bool.Parse(node.SelectSingleNode("VipRequireAdress").InnerText), VipRequireQQ = bool.Parse(node.SelectSingleNode("VipRequireQQ").InnerText), VipEnableCoupon = bool.Parse(node.SelectSingleNode("VipEnableCoupon").InnerText), VipRemark = node.SelectSingleNode("VipRemark").InnerText, EnablePodRequest = bool.Parse(node.SelectSingleNode("EnablePodRequest").InnerText), EnableCommission = bool.Parse(node.SelectSingleNode("EnableCommission").InnerText), EnableAlipayRequest = bool.Parse(node.SelectSingleNode("EnableAlipayRequest").InnerText), EnableWeiXinRequest = bool.Parse(node.SelectSingleNode("EnableWeiXinRequest").InnerText), EnableOffLineRequest = bool.Parse(node.SelectSingleNode("EnableOffLineRequest").InnerText), EnableWapShengPay = bool.Parse(node.SelectSingleNode("EnableWapShengPay").InnerText), OffLinePayContent = node.SelectSingleNode("OffLinePayContent").InnerText, DistributorDescription = node.SelectSingleNode("DistributorDescription").InnerText, DistributorBackgroundPic = node.SelectSingleNode("DistributorBackgroundPic").InnerText, DistributorLogoPic = node.SelectSingleNode("DistributorLogoPic").InnerText, SaleService = node.SelectSingleNode("SaleService").InnerText, 
                MentionNowMoney = node.SelectSingleNode("MentionNowMoney").InnerText, ShopIntroduction = node.SelectSingleNode("ShopIntroduction").InnerText, ApplicationDescription = node.SelectSingleNode("ApplicationDescription").InnerText, GuidePageSet = node.SelectSingleNode("GuidePageSet").InnerText, ManageOpenID = node.SelectSingleNode("ManageOpenID").InnerText, WeixinCertPath = node.SelectSingleNode("WeixinCertPath").InnerText, WeixinCertPassword = node.SelectSingleNode("WeixinCertPassword").InnerText, GoodsPic = node.SelectSingleNode("GoodsPic").InnerText, GoodsName = node.SelectSingleNode("GoodsName").InnerText, GoodsDescription = node.SelectSingleNode("GoodsDescription").InnerText, ShopHomePic = node.SelectSingleNode("ShopHomePic").InnerText, ShopHomeName = node.SelectSingleNode("ShopHomeName").InnerText, ShopHomeDescription = node.SelectSingleNode("ShopHomeDescription").InnerText, ShopSpreadingCodePic = node.SelectSingleNode("ShopSpreadingCodePic").InnerText, ShopSpreadingCodeName = node.SelectSingleNode("ShopSpreadingCodeName").InnerText, ShopSpreadingCodeDescription = node.SelectSingleNode("ShopSpreadingCodeDescription").InnerText,
                OpenManyService = bool.Parse(node.SelectSingleNode("OpenManyService").InnerText),
                IsRequestDistributor = bool.Parse(node.SelectSingleNode("IsRequestDistributor").InnerText),
                FinishedOrderMoney = int.Parse(node.SelectSingleNode("FinishedOrderMoney").InnerText),
                RegisterDistributorsPoints = int.Parse(node.SelectSingleNode("RegisterDistributorsPoints").InnerText),
                OrdersPoints = int.Parse(node.SelectSingleNode("OrdersPoints").InnerText),
                SiteFlag = node.SelectSingleNode("SiteFlag").InnerText,
                DefaultProductId = node.SelectSingleNode("DefaultProductId").InnerText,
                DefaultPartnerGradeId = node.SelectSingleNode("DefaultPartnerGradeId").InnerText,
                DefaultTutorGradeId = node.SelectSingleNode("DefaultTutorGradeId").InnerText,
                DefaultStoreGradeId = node.SelectSingleNode("DefaultStoreGradeId").InnerText,
                DefaultMinInvitationNum = int.Parse(node.SelectSingleNode("DefaultMinInvitationNum").InnerText),
                DefaultMaxInvitationNum = int.Parse(node.SelectSingleNode("DefaultMaxInvitationNum").InnerText),
                DefaultLoginSmsContent = node.SelectSingleNode("DefaultLoginSmsContent").InnerText,
                VirtualPointExchangeRate = decimal.Parse(node.SelectSingleNode("VirtualPointExchangeRate").InnerText),
                DistributorRegSmsContent = node.SelectSingleNode("DistributorRegSmsContent").InnerText,
                DefaultVirtualPoint = decimal.Parse(node.SelectSingleNode("DefaultVirtualPoint").InnerText),
                VirtualPointName = node.SelectSingleNode("VirtualPointName").InnerText,
                ServicePhone = node.SelectSingleNode("ServicePhone").InnerText,
                DefaultCompanyRecommendedIncome = decimal.Parse( node.SelectSingleNode("DefaultCompanyRecommendedIncome").InnerText),
                DefaultCompanyIncomeSeven = decimal.Parse(node.SelectSingleNode("DefaultCompanyIncomeSeven").InnerText),
                DefaultCompanyIncomeEight = decimal.Parse(node.SelectSingleNode("DefaultCompanyIncomeEight").InnerText),
                MainSiteGoodCounts = int.Parse(node.SelectSingleNode("MainSiteGoodCounts").InnerText),
                StoreUrl = node.SelectSingleNode("StoreUrl").InnerText,
                WXDebug = node.SelectSingleNode("WXDebug").InnerText,
                RecruitCnt = int.Parse(node.SelectSingleNode("RecruitCnt").InnerText),
                RecruitCntPoint = decimal.Parse(node.SelectSingleNode("RecruitCntPoint").InnerText),
                IsProcessCommissions = bool.Parse(node.SelectSingleNode("IsProcessCommissions").InnerText),
                TempStorePoint = decimal.Parse(node.SelectSingleNode("TempStorePoint").InnerText),
                TempStoreSaleAmount = decimal.Parse(node.SelectSingleNode("TempStoreSaleAmount").InnerText)
             };
        }

        private static void SetNodeValue(XmlDocument doc, XmlNode root, string nodeName, string nodeValue)
        {
            XmlNode newChild = root.SelectSingleNode(nodeName);
            if (newChild == null)
            {
                newChild = doc.CreateElement(nodeName);
                root.AppendChild(newChild);
            }
            newChild.InnerText = nodeValue;
        }

        public void WriteToXml(XmlDocument doc)
        {
            XmlNode root = doc.SelectSingleNode("Settings");
            SetNodeValue(doc, root, "SiteUrl", this.SiteUrl);
            SetNodeValue(doc, root, "Theme", this.Theme);
            SetNodeValue(doc, root, "VTheme", this.VTheme);
            SetNodeValue(doc, root, "DecimalLength", this.DecimalLength.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "DefaultProductImage", this.DefaultProductImage);
            SetNodeValue(doc, root, "DefaultProductThumbnail1", this.DefaultProductThumbnail1);
            SetNodeValue(doc, root, "DefaultProductThumbnail2", this.DefaultProductThumbnail2);
            SetNodeValue(doc, root, "DefaultProductThumbnail3", this.DefaultProductThumbnail3);
            SetNodeValue(doc, root, "DefaultProductThumbnail4", this.DefaultProductThumbnail4);
            SetNodeValue(doc, root, "DefaultProductThumbnail5", this.DefaultProductThumbnail5);
            SetNodeValue(doc, root, "DefaultProductThumbnail6", this.DefaultProductThumbnail6);
            SetNodeValue(doc, root, "DefaultProductThumbnail7", this.DefaultProductThumbnail7);
            SetNodeValue(doc, root, "DefaultProductThumbnail8", this.DefaultProductThumbnail8);
            SetNodeValue(doc, root, "CheckCode", this.CheckCode);
            SetNodeValue(doc, root, "Disabled", this.Disabled ? "true" : "false");
            SetNodeValue(doc, root, "Footer", this.Footer);
            SetNodeValue(doc, root, "RegisterAgreement", this.RegisterAgreement);
            SetNodeValue(doc, root, "LogoUrl", this.LogoUrl);
            SetNodeValue(doc, root, "OrderShowDays", this.OrderShowDays.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "CloseOrderDays", this.CloseOrderDays.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "FinishOrderDays", this.FinishOrderDays.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "TaxRate", this.TaxRate.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "PointsRate", this.PointsRate.ToString("F"));
            SetNodeValue(doc, root, "SiteName", this.SiteName);
            SetNodeValue(doc, root, "YourPriceName", this.YourPriceName);
            SetNodeValue(doc, root, "EmailSender", this.EmailSender);
            SetNodeValue(doc, root, "EmailSettings", this.EmailSettings);
            SetNodeValue(doc, root, "SMSSender", this.SMSSender);
            SetNodeValue(doc, root, "SMSSettings", this.SMSSettings);
            SetNodeValue(doc, root, "EnabledCnzz", this.EnabledCnzz ? "true" : "false");
            SetNodeValue(doc, root, "CnzzUsername", this.CnzzUsername);
            SetNodeValue(doc, root, "CnzzPassword", this.CnzzPassword);
            SetNodeValue(doc, root, "WeixinAppId", this.WeixinAppId);
            SetNodeValue(doc, root, "WeixinAppSecret", this.WeixinAppSecret);
            SetNodeValue(doc, root, "WeixinPaySignKey", this.WeixinPaySignKey);
            SetNodeValue(doc, root, "WeixinPartnerID", this.WeixinPartnerID);
            SetNodeValue(doc, root, "WeixinPartnerKey", this.WeixinPartnerKey);
            SetNodeValue(doc, root, "IsValidationService", this.IsValidationService ? "true" : "false");
            SetNodeValue(doc, root, "WeixinToken", this.WeixinToken);
            SetNodeValue(doc, root, "WeixinNumber", this.WeixinNumber);
            SetNodeValue(doc, root, "WeixinLoginUrl", this.WeixinLoginUrl);
            SetNodeValue(doc, root, "WeiXinCodeImageUrl", this.WeiXinCodeImageUrl);
            SetNodeValue(doc, root, "VipCardBG", this.VipCardBG);
            SetNodeValue(doc, root, "VipCardLogo", this.VipCardLogo);
            SetNodeValue(doc, root, "VipCardQR", this.VipCardQR);
            SetNodeValue(doc, root, "VipCardPrefix", this.VipCardPrefix);
            SetNodeValue(doc, root, "VipCardName", this.VipCardName);
            SetNodeValue(doc, root, "VipRequireName", this.VipRequireName ? "true" : "false");
            SetNodeValue(doc, root, "VipRequireMobile", this.VipRequireMobile ? "true" : "false");
            SetNodeValue(doc, root, "VipRequireQQ", this.VipRequireQQ ? "true" : "false");
            SetNodeValue(doc, root, "VipRequireAdress", this.VipRequireAdress ? "true" : "false");
            SetNodeValue(doc, root, "VipEnableCoupon", this.VipEnableCoupon ? "true" : "false");
            SetNodeValue(doc, root, "VipRemark", this.VipRemark);
            SetNodeValue(doc, root, "EnablePodRequest", this.EnablePodRequest ? "true" : "false");
            SetNodeValue(doc, root, "EnableCommission", this.EnableCommission ? "true" : "false");
            SetNodeValue(doc, root, "EnableAlipayRequest", this.EnableAlipayRequest ? "true" : "false");
            SetNodeValue(doc, root, "EnableWeiXinRequest", this.EnableWeiXinRequest ? "true" : "false");
            SetNodeValue(doc, root, "EnableOffLineRequest", this.EnableOffLineRequest ? "true" : "false");
            SetNodeValue(doc, root, "EnableWapShengPay", this.EnableWapShengPay ? "true" : "false");
            SetNodeValue(doc, root, "OffLinePayContent", this.OffLinePayContent);
            SetNodeValue(doc, root, "DistributorDescription", this.DistributorDescription);
            SetNodeValue(doc, root, "DistributorBackgroundPic", this.DistributorBackgroundPic);
            SetNodeValue(doc, root, "DistributorLogoPic", this.DistributorLogoPic);
            SetNodeValue(doc, root, "SaleService", this.SaleService);
            SetNodeValue(doc, root, "MentionNowMoney", this.MentionNowMoney);
            SetNodeValue(doc, root, "ShopIntroduction", this.ShopIntroduction);
            SetNodeValue(doc, root, "ApplicationDescription", this.ApplicationDescription);
            SetNodeValue(doc, root, "GuidePageSet", this.GuidePageSet);
            SetNodeValue(doc, root, "ManageOpenID", this.ManageOpenID);
            SetNodeValue(doc, root, "WeixinCertPath", this.WeixinCertPath);
            SetNodeValue(doc, root, "WeixinCertPassword", this.WeixinCertPassword);
            SetNodeValue(doc, root, "GoodsPic", this.GoodsPic);
            SetNodeValue(doc, root, "GoodsName", this.GoodsName);
            SetNodeValue(doc, root, "GoodsDescription", this.GoodsDescription);
            SetNodeValue(doc, root, "ShopHomePic", this.ShopHomePic);
            SetNodeValue(doc, root, "ShopHomeName", this.ShopHomeName);
            SetNodeValue(doc, root, "ShopHomeDescription", this.ShopHomeDescription);
            SetNodeValue(doc, root, "ShopSpreadingCodePic", this.ShopSpreadingCodePic);
            SetNodeValue(doc, root, "ShopSpreadingCodeName", this.ShopSpreadingCodeName);
            SetNodeValue(doc, root, "ShopSpreadingCodeDescription", this.ShopSpreadingCodeDescription);
            SetNodeValue(doc, root, "OpenManyService", this.OpenManyService ? "true" : "false");
            SetNodeValue(doc, root, "IsRequestDistributor", this.IsRequestDistributor ? "true" : "false");
            SetNodeValue(doc, root, "FinishedOrderMoney", this.FinishedOrderMoney.ToString());
            SetNodeValue(doc, root, "RegisterDistributorsPoints", this.RegisterDistributorsPoints.ToString());
            SetNodeValue(doc, root, "OrdersPoints", this.OrdersPoints.ToString());
            SetNodeValue(doc, root, "SiteFlag", this.SiteFlag);
            SetNodeValue(doc, root, "DefaultProductId", this.DefaultProductId);
            SetNodeValue(doc, root, "DefaultPartnerGradeId", this.DefaultPartnerGradeId);
            SetNodeValue(doc, root, "DefaultTutorGradeId", this.DefaultTutorGradeId);
            SetNodeValue(doc, root, "DefaultStoreGradeId", this.DefaultStoreGradeId);
            SetNodeValue(doc, root, "DefaultMinInvitationNum", this.DefaultMinInvitationNum.ToString());
            SetNodeValue(doc, root, "DefaultMaxInvitationNum", this.DefaultMaxInvitationNum.ToString());
            SetNodeValue(doc, root, "DefaultLoginSmsContent", this.DefaultLoginSmsContent);
            SetNodeValue(doc, root, "VirtualPointExchangeRate", this.VirtualPointExchangeRate.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "DistributorRegSmsContent", this.DistributorRegSmsContent);
            SetNodeValue(doc, root, "DefaultVirtualPoint", this.DefaultVirtualPoint.ToString(CultureInfo.InvariantCulture));
            SetNodeValue(doc, root, "VirtualPointName", this.VirtualPointName);
            SetNodeValue(doc, root, "ServicePhone", this.ServicePhone);
            SetNodeValue(doc, root, "DefaultCompanyRecommendedIncome", this.DefaultCompanyRecommendedIncome.ToString());
            SetNodeValue(doc, root, "DefaultCompanyIncomeSeven", this.DefaultCompanyIncomeSeven.ToString());
            SetNodeValue(doc, root, "DefaultCompanyIncomeEight", this.DefaultCompanyIncomeEight.ToString());
            SetNodeValue(doc, root, "MainSiteGoodCounts", this.MainSiteGoodCounts.ToString());
            SetNodeValue(doc, root, "StoreUrl", this.StoreUrl);
            SetNodeValue(doc, root, "WXDebug", this.WXDebug);
            SetNodeValue(doc, root, "RecruitCnt", this.RecruitCnt.ToString());
            SetNodeValue(doc, root, "RecruitCntPoint", this.RecruitCntPoint.ToString());
            SetNodeValue(doc, root, "IsProcessCommissions", this.IsProcessCommissions ? "true" : "false");
            SetNodeValue(doc, root, "TempStorePoint", this.TempStorePoint.ToString());
            SetNodeValue(doc, root, "TempStoreSaleAmount", this.TempStoreSaleAmount.ToString());
        }

        public string ApplicationDescription { get; set; }

        public string CheckCode { get; set; }

        public int CloseOrderDays { get; set; }

        public string CnzzPassword { get; set; }

        public string CnzzUsername { get; set; }

        public int DecimalLength { get; set; }

        public string DefaultProductImage { get; set; }

        public string DefaultProductThumbnail1 { get; set; }

        public string DefaultProductThumbnail2 { get; set; }

        public string DefaultProductThumbnail3 { get; set; }

        public string DefaultProductThumbnail4 { get; set; }

        public string DefaultProductThumbnail5 { get; set; }

        public string DefaultProductThumbnail6 { get; set; }

        public string DefaultProductThumbnail7 { get; set; }

        public string DefaultProductThumbnail8 { get; set; }

        public bool Disabled { get; set; }

        public string DistributorBackgroundPic { get; set; }

        public string DistributorDescription { get; set; }

        public string DistributorLogoPic { get; set; }

        public bool EmailEnabled
        {
            get
            {
                return (((!string.IsNullOrEmpty(this.EmailSender) && !string.IsNullOrEmpty(this.EmailSettings)) && (this.EmailSender.Trim().Length > 0)) && (this.EmailSettings.Trim().Length > 0));
            }
        }

        public string EmailSender { get; set; }

        public string EmailSettings { get; set; }

        public bool EnableAlipayRequest { get; set; }

        public bool EnableCommission { get; set; }

        public bool EnabledCnzz { get; set; }

        public bool EnableOffLineRequest { get; set; }

        public bool EnablePodRequest { get; set; }

        public bool EnableWapShengPay { get; set; }

        public bool EnableWeiXinRequest { get; set; }

        public int FinishedOrderMoney { get; set; }

        public int FinishOrderDays { get; set; }

        public string Footer { get; set; }

        public string GoodsDescription { get; set; }

        public string GoodsName { get; set; }

        public string GoodsPic { get; set; }

        public string GuidePageSet { get; set; }

        public bool IsRequestDistributor { get; set; }

        public bool IsValidationService { get; set; }

        public string LogoUrl { get; set; }

        public string ManageOpenID { get; set; }

        public string MentionNowMoney { get; set; }

        public string OffLinePayContent { get; set; }

        public bool OpenManyService { get; set; }

        public int OrderShowDays { get; set; }

        public int OrdersPoints { get; set; }

        public decimal PointsRate { get; set; }

        public string RegisterAgreement { get; set; }

        public int RegisterDistributorsPoints { get; set; }

        public string SaleService { get; set; }

        public string ShopHomeDescription { get; set; }

        public string ShopHomeName { get; set; }

        public string ShopHomePic { get; set; }

        public string ShopIntroduction { get; set; }

        public string ShopSpreadingCodeDescription { get; set; }

        public string ShopSpreadingCodeName { get; set; }

        public string ShopSpreadingCodePic { get; set; }

        public string SiteName { get; set; }

        public string SiteUrl { get; set; }

        public bool SMSEnabled
        {
            get
            {
                return (((!string.IsNullOrEmpty(this.SMSSender) && !string.IsNullOrEmpty(this.SMSSettings)) && (this.SMSSender.Trim().Length > 0)) && (this.SMSSettings.Trim().Length > 0));
            }
        }

        public string SMSSender { get; set; }

        public string SMSSettings { get; set; }

        public decimal TaxRate { get; set; }

        public string Theme { get; set; }

        public string VipCardBG { get; set; }

        public string VipCardLogo { get; set; }

        public string VipCardName { get; set; }

        public string VipCardPrefix { get; set; }

        public string VipCardQR { get; set; }

        public bool VipEnableCoupon { get; set; }

        public string VipRemark { get; set; }

        public bool VipRequireAdress { get; set; }

        public bool VipRequireMobile { get; set; }

        public bool VipRequireName { get; set; }

        public bool VipRequireQQ { get; set; }

        public string VTheme { get; set; }

        public string WeixinAppId { get; set; }

        public string WeixinAppSecret { get; set; }

        public string WeixinCertPassword { get; set; }

        public string WeixinCertPath { get; set; }

        public string WeiXinCodeImageUrl { get; set; }

        public string WeixinLoginUrl { get; set; }

        public string WeixinNumber { get; set; }

        public string WeixinPartnerID { get; set; }

        public string WeixinPartnerKey { get; set; }

        public string WeixinPaySignKey { get; set; }

        public string WeixinToken { get; set; }

        public string YourPriceName { get; set; }
        
        public string SiteFlag { get; set; }

        public string DefaultProductId { get; set; }

        /// <summary>
        /// 默认合伙人级别ID
        /// </summary>
        public string DefaultPartnerGradeId { get; set; }

        /// <summary>
        /// 默认导师级别ID
        /// </summary>
        public string DefaultTutorGradeId { get; set; }

        /// <summary>
        /// 默认店主级别ID
        /// </summary>
        public string DefaultStoreGradeId { get; set; }

        /// <summary>
        /// 默认的最小邀请码数
        /// </summary>
        public int DefaultMinInvitationNum { get; set; }

        /// <summary>
        /// 默认的最大邀请码数
        /// </summary>
        public int DefaultMaxInvitationNum { get; set; }

        /// <summary>
        /// 分销商登录时验证码短信
        /// </summary>
        public string DefaultLoginSmsContent { get; set; }

        /// <summary>
        /// 邀请分销商注册成功后的短信
        /// </summary>
        public string DistributorRegSmsContent { get; set; }

        /// <summary>
        /// 虚拟币对换比率
        /// </summary>
        public decimal VirtualPointExchangeRate { get; set; }

        /// <summary>
        /// 默认增加的虚拟币
        /// </summary>
        public decimal DefaultVirtualPoint { get; set; }

        /// <summary>
        /// 钻石会员赠送的金币
        /// </summary>
        public decimal TempStorePoint { get; set; }

        /// <summary>
        /// 虚拟币的名称
        /// </summary>
        public string VirtualPointName { get; set; }

        public string ServicePhone { get; set; }

        /// <summary>
        /// 默认公司招募分配推荐收入值
        /// </summary>
        public decimal DefaultCompanyRecommendedIncome { get; set; }

        public decimal DefaultCompanyIncomeSeven { get; set; }

        public decimal DefaultCompanyIncomeEight { get; set; }

        /// <summary>
        /// 主站的点赞数
        /// </summary>
        public int MainSiteGoodCounts { get; set; }

        public string StoreUrl { get; set; }

        public string WXDebug { get; set; }

        public int RecruitCnt { get; set; }

        public decimal RecruitCntPoint { get; set; }

        public bool IsProcessCommissions { get; set; }

        public decimal TempStoreSaleAmount { get; set; }

        

    }
}

