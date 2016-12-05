namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VWenan : VWeiXinOAuthTemplatedWebControl
    {
        private Literal litProductList;

        protected override void AttachChildControls()
        {

            this.litProductList = (Literal)this.FindControl("litProductList");

            bool isSearchToday = false;

            var currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember != null)
            {
                IList<ProductInfo> productList = ProductBrowser.GetAllProductsByCond(currentMember.UserId, isSearchToday);
                if (null != productList)
                {
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

                    StringBuilder piContent = new StringBuilder();
                    string piTime = "";
                    string piName = "";
                    string piRemark = "";
                    string piImg1 = "";
                    string piImg2 = "";
                    string piImg3 = "";
                    string piImg4 = "";
                    string piImg5 = "";
                    int piGoodCount = 0;
                    int index = 0;

                    string shareUrl = Globals.HostPath(HttpContext.Current.Request.Url);
                    string sharePic = "";
                    string shareTitle = "";
                    string shareDescription = "";
                    string shareGoodsName = masterSettings.GoodsName;
                    string shareGoodsDescription = masterSettings.GoodsDescription;
                    string shareGoodsPic = Globals.HostPath(HttpContext.Current.Request.Url) + masterSettings.GoodsPic;

                    foreach (ProductInfo pi in productList)
                    {
                        index = index + 1;

                        piTime = pi.LetterAddDate.ToString("yyyy-MM-dd HH:mm:ss");
                        piName = pi.ProductName;
                        piRemark = pi.ProductShortLetter;
                        piImg1 = pi.LetterImgUrl1 == null ? "" : pi.LetterImgUrl1;
                        piImg2 = pi.LetterImgUrl2 == null ? "" : pi.LetterImgUrl2;
                        piImg3 = pi.LetterImgUrl3 == null ? "" : pi.LetterImgUrl3;
                        piImg4 = pi.LetterImgUrl4 == null ? "" : pi.LetterImgUrl4;
                        piImg5 = pi.LetterImgUrl5 == null ? "" : pi.LetterImgUrl5;
                        piGoodCount = pi.GoodCounts;

                        shareUrl = Globals.HostPath(HttpContext.Current.Request.Url);
                        sharePic = Globals.HostPath(HttpContext.Current.Request.Url) + pi.ImageUrl1;
                        shareTitle = pi.ProductName;
                        shareDescription = pi.ProductShortLetter;

                        shareUrl = shareUrl + "/Vshop/ProductDetails.aspx?ProductId=" + pi.ProductId;

                        if (null != currentMember)
                        {
                            if (!shareUrl.Contains("ReferralId"))
                            {
                                shareUrl = shareUrl + "&ReferralId=" + currentMember.UserId;
                            }
                            else
                            {
                                shareUrl = shareUrl.Replace("ReferralId", "RefId");
                                shareUrl = shareUrl + "&ReferralId=" + currentMember.UserId;
                            }
                        }
                        shareUrl = shareUrl.Replace("&&", "&");

                        piContent.AppendLine("<div class=\"con_wrap\">");
                        piContent.AppendLine("    <div class=\"hr\"><span class=\"s0\"></span></div>");
                        piContent.AppendLine("    <div class=\"hr1\"></div><br/>");
                        piContent.AppendLine("    <div class=\"time\">" + piTime + "</div>");
                        piContent.AppendLine("    <div class=\"con\">");
                        piContent.AppendLine("        <div class=\"d0\">" + piName + "</div>");
                        piContent.AppendLine("        <div class=\"d1\">" + piRemark + "</div>");
                        piContent.AppendLine("    </div>");
                        //piContent.AppendLine("    <div class=\"hr\"><span class=\"s0\"></span></div>");
                        piContent.AppendLine("</div>");
                        piContent.AppendLine("<div class=\"_wrap\">");
                        piContent.AppendLine("    <div class=\"im clearfix\">");
                        if (!string.IsNullOrEmpty(piImg1))
                        {
                            piContent.AppendLine("        <a href=\"\"><img src=\"" + piImg1 + "\"></a>");
                        }
                        if (!string.IsNullOrEmpty(piImg2))
                        {
                            piContent.AppendLine("        <a href=\"\"><img src=\"" + piImg2 + "\"></a>");
                        }
                        if (!string.IsNullOrEmpty(piImg3))
                        {
                            piContent.AppendLine("        <a href=\"\"><img src=\"" + piImg3 + "\"></a>");
                        }
                        if (!string.IsNullOrEmpty(piImg4))
                        {
                            piContent.AppendLine("        <a href=\"\"><img src=\"" + piImg4 + "\"></a>");
                        }
                        if (!string.IsNullOrEmpty(piImg5))
                        {
                            piContent.AppendLine("        <a href=\"\"><img src=\"" + piImg5 + "\"></a>");
                        }
                        piContent.AppendLine("    </div>");
                        piContent.AppendLine("    <div class=\"boc\"><input type=\"button\" value=\"分享文案\" onClick=\"SwitchShare('#ItemParams" + index + "');\" class=\"btn\"><span>" + piGoodCount + "</span><input type=\"button\" value=\"保存图片\" class=\"btn\"></div>");
                        piContent.AppendLine("    <span id=\"ItemParams" + index + "\" style=\"display: none\">");
                        piContent.AppendLine("        " + string.Concat(new object[] { sharePic, "|", shareTitle, "|", shareDescription, "$", shareGoodsPic, "|", shareGoodsName, "|", shareGoodsDescription, "|", shareUrl }));
                        piContent.AppendLine("    </span>");
                        piContent.AppendLine("</div>");
                    }

                    this.litProductList.Text = piContent.ToString();
                }
            }
            else
            {
                this.Page.Response.Redirect("/Vshop/Default.aspx");
            }

            PageTitle.AddSiteNameTitle("商品文案");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vwenan.html";
            }
            base.OnInit(e);
        }
    }
}

