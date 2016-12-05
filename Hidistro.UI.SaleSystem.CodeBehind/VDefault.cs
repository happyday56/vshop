using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ASPNET.WebControls;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Members;
using Hidistro.Entities.VShop;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.Common.Controls;
using System.Text;
using NewLife.Log;

namespace Hidistro.UI.SaleSystem.CodeBehind
{
    [ParseChildren(true)]
    public class VDefault :  VshopTemplatedWebControl
    {
        private DataTable dtpromotion;
        private HtmlImage img;
        //private HtmlImage imgAd;
        private Literal litAllStore;
        private Literal litStoreAd;
        private HiImage imglogo;
        protected int itemcount;
        private Literal litattention;
        private Literal litdescription;
        private Literal litImgae;
        private Literal litItemParams;
        private Literal litstorename;
        private Pager pager;
        private VshopTemplatedRepeater rptCategories;
        private VshopTemplatedRepeater rptProducts;
        private Literal litGood;
        private Literal litActivitiesAd;

        #region "注销代码"
        /*

        protected override void AttachChildControls()
        {
            #region 测试默认登录

            //HttpCookie tCookie = new HttpCookie("Vshop-Member")
            //            {
            //                Value = "9",
            //                Expires = DateTime.Now.AddYears(10)
            //            };
            //HttpContext.Current.Response.Cookies.Add(tCookie);

            #endregion
            
            this.rptCategories = (VshopTemplatedRepeater)this.FindControl("rptCategories");
            this.rptProducts = (VshopTemplatedRepeater)this.FindControl("rptProducts");
            this.rptProducts.ItemDataBound += new RepeaterItemEventHandler(this.rptProducts_ItemDataBound);
            this.rptCategories.ItemDataBound += new RepeaterItemEventHandler(this.rptCategories_ItemDataBound);
            this.img = (HtmlImage)this.FindControl("imgDefaultBg");
            this.pager = (Pager)this.FindControl("pager");
            this.litstorename = (Literal)this.FindControl("litstorename");
            this.litdescription = (Literal)this.FindControl("litdescription");
            this.litattention = (Literal)this.FindControl("litattention");
            this.imglogo = (HiImage)this.FindControl("imglogo");
            this.litImgae = (Literal)this.FindControl("litImgae");
            this.litItemParams = (Literal)this.FindControl("litItemParams");
            this.pager.PageSize = 1000;
            if (string.IsNullOrEmpty(this.Page.Request.QueryString["ReferralId"]))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["Vshop-ReferralId"];
                if ((cookie != null) && !string.IsNullOrEmpty(cookie.Value))
                {
                    this.Page.Response.Redirect("/Vshop/Default.aspx?ReferralId=" + cookie.Value);
                }
            }
            if (this.rptCategories.Visible)
            {
                DataTable brandCategories = CategoryBrowser.GetBrandCategories();
                this.itemcount = brandCategories.Rows.Count;
                if (brandCategories.Rows.Count > 0)
                {
                    this.rptCategories.DataSource = brandCategories;
                    this.rptCategories.DataBind();
                }
            }
            this.Page.Session["stylestatus"] = "3";
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
            PageTitle.AddSiteNameTitle(masterSettings.SiteName);
            this.litstorename.Text = masterSettings.SiteName;
            this.litdescription.Text = masterSettings.ShopIntroduction;
            if (!string.IsNullOrEmpty(masterSettings.DistributorLogoPic))
            {
                this.imglogo.ImageUrl = masterSettings.DistributorLogoPic.Split(new char[] { '|' })[0];
            }
            if (base.referralId <= 0)
            {
                HttpCookie cookie2 = HttpContext.Current.Request.Cookies["Vshop-ReferralId"];
                if ((cookie2 != null) && !string.IsNullOrEmpty(cookie2.Value))
                {
                    base.referralId = int.Parse(cookie2.Value);
                    this.Page.Response.Redirect("/Vshop/Default.aspx?ReferralId=" + this.referralId.ToString(), true);
                }
            }
            else
            {
                HttpCookie cookie3 = HttpContext.Current.Request.Cookies["Vshop-ReferralId"];
                if (((cookie3 != null) && !string.IsNullOrEmpty(cookie3.Value)) && (this.referralId.ToString() != cookie3.Value))
                {
                    this.Page.Response.Redirect("/Vshop/Default.aspx?ReferralId=" + this.referralId.ToString(), true);
                }
            }
            IList<BannerInfo> allBanners = new List<BannerInfo>();
            allBanners = VshopBrowser.GetAllBanners();
            foreach (BannerInfo info in allBanners)
            {
                TplCfgInfo info2 = new NavigateInfo
                {
                    LocationType = info.LocationType,
                    Url = info.Url
                };
                string loctionUrl = "javascript:";
                if (!string.IsNullOrEmpty(info.Url))
                {
                    loctionUrl = info2.LoctionUrl;
                }
                string text = this.litImgae.Text;
                this.litImgae.Text = text + "<a  id=\"ahref\"  href='" + loctionUrl + "'><img width=\"650\" height=\"200\" src=\"" + info.ImageUrl + "\" title=\"" + info.ShortDesc + "\" alt=\"" + info.ShortDesc + "\"  /></a>";
            }
            if (allBanners.Count == 0)
            {
                this.litImgae.Text = "<a id=\"ahref\"  href='javascript:'><img src=\"/Utility/pics/default.jpg\" title=\"\" width=\"650\" height=\"200\"  /></a>";
                
            }
            DistributorsInfo userIdDistributors = new DistributorsInfo();
            userIdDistributors = DistributorsBrower.GetUserIdDistributors(base.referralId);
            if ((userIdDistributors != null) && (userIdDistributors.UserId > 0))
            {
                PageTitle.AddSiteNameTitle(userIdDistributors.StoreName);
                this.litdescription.Text = userIdDistributors.StoreDescription;
                this.litstorename.Text = userIdDistributors.StoreName;
                if (!string.IsNullOrEmpty(userIdDistributors.Logo))
                {
                    this.imglogo.ImageUrl = userIdDistributors.Logo;
                }
                else if (!string.IsNullOrEmpty(masterSettings.DistributorLogoPic))
                {
                    this.imglogo.ImageUrl = masterSettings.DistributorLogoPic.Split(new char[] { '|' })[0];
                }


                if (!string.IsNullOrEmpty(userIdDistributors.BackImage))
                {
                    this.litImgae.Text = "";
                    foreach (string str2 in userIdDistributors.BackImage.Split(new char[] { '|' }))
                    {
                        if (!string.IsNullOrEmpty(str2))
                        {
                            this.litImgae.Text = this.litImgae.Text + "<a ><img src=\"" + str2 + "\" title=\"\"  /></a>";
                        }
                    }
                }
            }
            this.dtpromotion = ProductBrowser.GetAllFull();
            if (this.rptProducts != null)
            {
                ProductQuery query = new ProductQuery
                {
                    PageSize = this.pager.PageSize,
                    PageIndex = this.pager.PageIndex,
                    SortBy = "DisplaySequence",
                    SortOrder = SortAction.Desc
                };
                DbQueryResult homeProduct = ProductBrowser.GetHomeProduct(MemberProcessor.GetCurrentMember(), query);
                this.rptProducts.DataSource = homeProduct.Data;
                this.rptProducts.DataBind();
                this.pager.TotalRecords = homeProduct.TotalRecords;
                if (this.pager.TotalRecords <= this.pager.PageSize)
                {
                    this.pager.Visible = false;
                }
            }
            if (this.img != null)
            {
                this.img.Src = new VTemplateHelper().GetDefaultBg();
            }
            if (!string.IsNullOrEmpty(masterSettings.GuidePageSet))
            {
                this.litattention.Text = masterSettings.GuidePageSet;
            }
            string str3 = "";
            if (!string.IsNullOrEmpty(masterSettings.ShopHomePic))
            {
                str3 = Globals.HostPath(HttpContext.Current.Request.Url) + masterSettings.ShopHomePic;
            }
            string str4 = "";
            string str5 = (userIdDistributors == null) ? masterSettings.SiteName : userIdDistributors.StoreName;
            if (!string.IsNullOrEmpty(masterSettings.DistributorBackgroundPic))
            {
                str4 = Globals.HostPath(HttpContext.Current.Request.Url) + masterSettings.DistributorBackgroundPic.Split(new char[] { '|' })[0];
            }
            this.litItemParams.Text = str3 + "|" + masterSettings.ShopHomeName + "|" + masterSettings.ShopHomeDescription + "$";
            this.litItemParams.Text = string.Concat(new object[] { this.litItemParams.Text, str4, "|好店推荐之", str5, "商城|一个购物赚钱的好去处|", HttpContext.Current.Request.Url });
        }
        */

        #endregion "注销代码"


        //当前首页模板属性
        //顶部背景：没有分销商ID时使用默认背景
        //分销商Logo
        //分销商名称
        //产品分类
        //分类产品


        protected DataTable dtCategories = null;
        protected Literal litCategories;
        protected Literal litProducts;
        protected override void AttachChildControls()
        {

            this.rptCategories = (VshopTemplatedRepeater)this.FindControl("rptCategories");
            //this.rptProducts = (VshopTemplatedRepeater)this.FindControl("rptProducts");
            //this.rptProducts.ItemDataBound += new RepeaterItemEventHandler(this.rptProducts_ItemDataBound);
            this.rptCategories.ItemDataBound += new RepeaterItemEventHandler(this.rptCategories_ItemDataBound);
            //默认顶部背景
            this.img = (HtmlImage)this.FindControl("imgDefaultBg");
            //this.imgAd = (HtmlImage)this.FindControl("imgAd");
            this.litAllStore = (Literal)this.FindControl("litAllStore");
            this.litStoreAd = (Literal)this.FindControl("litStoreAd");
            this.pager = (Pager)this.FindControl("pager");
            //分销商名称
            this.litstorename = (Literal)this.FindControl("litstorename");
            this.litdescription = (Literal)this.FindControl("litdescription");
            //分销商logo
            this.imglogo = (HiImage)this.FindControl("imglogo");
            //this.litImgae = (Literal)this.FindControl("litImgae");
            //微信分享内容
            this.litItemParams = (Literal)this.FindControl("litItemParams");
            this.litActivitiesAd = (Literal)this.FindControl("litActivitiesAd");
            //
            this.litCategories = (Literal)this.FindControl("litCategories");
            this.litProducts = (Literal)this.FindControl("litProducts");
            this.litattention = (Literal)this.FindControl("litattention");
            this.litGood = (Literal)this.FindControl("litGood");
            this.pager.PageSize = 1000;
            // 如果分享过来的链接中不存在ReferralId，则从Cookie中获取
            if (string.IsNullOrEmpty(this.Page.Request.QueryString["ReferralId"]))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["Vshop-ReferralId"];
                if ((cookie != null) && !string.IsNullOrEmpty(cookie.Value))
                {
                    // Cookie中存在ReferralId，则重新跳转页面
                    XTrace.WriteLine("------Cookie中的ReferralId：" + cookie.Value);

                    this.Page.Response.Redirect("/Vshop/Default.aspx?ReferralId=" + cookie.Value);
                }
            }

            this.Page.Session["stylestatus"] = "3";
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);


            PageTitle.AddSiteNameTitle(masterSettings.SiteName);
            //默认名称和描述
            this.litstorename.Text = masterSettings.SiteName;
            this.litdescription.Text = masterSettings.ShopIntroduction;

            if (masterSettings.SiteFlag.EqualIgnoreCase("ls"))
            {
                this.img.Src = "/templates/vshop/default/style/newcss/imgs/index/lsbg.jpg";
                //this.imgAd.Src = "/templates/vshop/default/style/newcss/imgs/index/lsad.png";
                this.litAllStore.Text = "";
                this.litStoreAd.Text = "";
            }
            else
            {
                this.img.Src = "/templates/vshop/default/style/newcss/imgs/index/lasbg.jpg";
                //this.imgAd.Src = "/templates/vshop/default/style/newcss/imgs/index/lasad.jpg";

                this.litAllStore.Text = "<a href=\"\"><em class=\"em4\"></em><b><a href=\"" + masterSettings.StoreUrl + "\">全国门店</a></b></a>";
                this.litStoreAd.Text = "<div class=\"imgwrap\"><a href=\"" + masterSettings.StoreUrl + "\"><img src=\"/templates/vshop/default/style/newcss/imgs/index/1.jpg\"></a></div>";
            }

            if (masterSettings.SiteFlag.EqualIgnoreCase("ls") || masterSettings.SiteFlag.EqualIgnoreCase("las"))
            {
                this.rptCategories.Visible = false;
            }

            if (this.rptCategories.Visible)
            {
                DataTable brandCategories = CategoryBrowser.GetBrandCategories();
                this.itemcount = brandCategories.Rows.Count;
                if (brandCategories.Rows.Count > 0)
                {
                    this.rptCategories.DataSource = brandCategories;
                    this.rptCategories.DataBind();
                }
            }

            // 设置活动首页显示信息
            GetActivities(masterSettings.SiteFlag);

            if (!string.IsNullOrEmpty(masterSettings.DistributorLogoPic))
            {
                this.imglogo.ImageUrl = masterSettings.DistributorLogoPic.Split(new char[] { '|' })[0];
            }

            if (base.referralId <= 0)
            {
                HttpCookie cookie2 = HttpContext.Current.Request.Cookies["Vshop-ReferralId"];
                if ((cookie2 != null) && !string.IsNullOrEmpty(cookie2.Value))
                {
                    base.referralId = int.Parse(cookie2.Value);
                    this.Page.Response.Redirect("/Vshop/Default.aspx?ReferralId=" + this.referralId.ToString(), true);
                }
            }
            else
            {
                HttpCookie cookie3 = HttpContext.Current.Request.Cookies["Vshop-ReferralId"];
                if (((cookie3 != null) && !string.IsNullOrEmpty(cookie3.Value)) && (this.referralId.ToString() != cookie3.Value))
                {
                    this.Page.Response.Redirect("/Vshop/Default.aspx?ReferralId=" + this.referralId.ToString(), true);
                }
            }
            MemberInfo member = MemberProcessor.GetCurrentMember();
            DistributorsInfo userIdDistributors = new DistributorsInfo();
            if (!string.IsNullOrEmpty(this.Page.Request["ReferralId"]))
            {
                int.TryParse(this.Page.Request["ReferralId"], out base.referralId);
            }
            userIdDistributors = DistributorsBrower.GetUserIdDistributors(base.referralId);
            if ((userIdDistributors != null) && (userIdDistributors.UserId > 0))
            {
                // 更新访问次数
                DistributorsBrower.UpdateVisitCounts(userIdDistributors.UserId);

                PageTitle.AddSiteNameTitle(userIdDistributors.StoreName);
                this.litdescription.Text = userIdDistributors.StoreDescription;
                this.litstorename.Text = userIdDistributors.StoreName;
                if (!string.IsNullOrEmpty(userIdDistributors.Logo))
                {
                    this.imglogo.ImageUrl = userIdDistributors.Logo;
                }
                else if (!string.IsNullOrEmpty(masterSettings.DistributorLogoPic))
                {
                    this.imglogo.ImageUrl = masterSettings.DistributorLogoPic.Split(new char[] { '|' })[0];
                }

                this.litGood.Text = userIdDistributors.GoodCounts.ToString();


                if (!string.IsNullOrEmpty(userIdDistributors.BackImage))
                {
                    this.img.Src = userIdDistributors.BackImage;
                }
            }
            else
            {
                this.litGood.Text = masterSettings.MainSiteGoodCounts.ToString();
            }

            //if (this.img != null)
            //{
            //    this.img.Src = new VTemplateHelper().GetDefaultBg();
            //}
            if (!string.IsNullOrEmpty(masterSettings.GuidePageSet))
            {
                this.litattention.Text = masterSettings.GuidePageSet;
            }
            string str3 = "";
            if (!string.IsNullOrEmpty(masterSettings.ShopHomePic))
            {
                str3 = Globals.HostPath(HttpContext.Current.Request.Url) + masterSettings.ShopHomePic;
            }
            string str4 = "";
            string str5 = (userIdDistributors == null) ? masterSettings.SiteName : userIdDistributors.StoreName;
            if (!string.IsNullOrEmpty(masterSettings.DistributorBackgroundPic))
            {
                str4 = Globals.HostPath(HttpContext.Current.Request.Url) + masterSettings.DistributorBackgroundPic.Split(new char[] { '|' })[0];
            }

            string shareUrl = HttpContext.Current.Request.Url.ToString();
            string sharePic = "";
            string shareTitle = "";
            string shareDescription = "";
            string shareGoodsTitle = "";
            string shareGoodsDescription = "";
            string shareGoodsPic = "";

            if (!string.IsNullOrEmpty(masterSettings.ShopHomePic))
            {
                shareGoodsPic = Globals.HostPath(HttpContext.Current.Request.Url) + masterSettings.ShopHomePic;
            }
            if (!string.IsNullOrEmpty(masterSettings.DistributorBackgroundPic))
            {
                sharePic = Globals.HostPath(HttpContext.Current.Request.Url) + masterSettings.DistributorBackgroundPic.Split(new char[] { '|' })[0];
            }
            shareTitle = (null == userIdDistributors) ? masterSettings.SiteName : userIdDistributors.StoreName;
            shareTitle = "好店推荐之" + shareTitle + "商城";
            shareGoodsTitle = shareTitle;
            shareDescription = "一个购物赚钱的好去处";
            shareGoodsDescription = "一个购物赚钱的好去处";

            if (masterSettings.SiteFlag.EqualIgnoreCase("las"))
            {
                shareTitle = masterSettings.SiteName + ( (null == userIdDistributors) ? "" : userIdDistributors.StoreName );
                shareGoodsTitle = shareTitle;
                shareDescription = "专营优质母婴正品，与妈咪一起守护宝宝健康成长！现在购买优惠多多，快进店选购吧！";
                shareGoodsDescription = shareDescription;
                if (null != userIdDistributors)
                {
                    if (!string.IsNullOrEmpty(userIdDistributors.Logo))
                    {
                        sharePic = Globals.HostPath(HttpContext.Current.Request.Url) + userIdDistributors.Logo;
                    }
                    else
                    {
                        if (null != member)
                        {
                            if (!string.IsNullOrEmpty(member.UserHead))
                            {
                                sharePic = member.UserHead;
                            }
                        }
                    }
                }
            }
            
            shareGoodsPic = sharePic;
            

            this.litItemParams.Text = string.Concat(new object[] { shareGoodsPic, "|", shareGoodsTitle, "|", shareGoodsDescription, "$", sharePic, "|", shareTitle, "|", shareDescription, "|", shareUrl });

            //this.litItemParams.Text = str3 + "|" + masterSettings.ShopHomeName + "|" + masterSettings.ShopHomeDescription + "$";
            //this.litItemParams.Text = string.Concat(new object[] { this.litItemParams.Text, str4, "|好店推荐之", str5, "商城|一个购物赚钱的好去处|", HttpContext.Current.Request.Url });


            dtCategories = CategoryBrowser.GetCategoriesByDisplayHome();

            if (litCategories != null)
            {
                if (masterSettings.SiteFlag.EqualIgnoreCase("ls"))
                {
                    this.GetCategoriesListByLS();
                }
                else
                {
                    GetCategoriesList();
                }                
            }
            if (litProducts != null)
            {
                GetCategoriesProducts();
            }

        }

        private void GetActivities(string siteFlag)
        {
            StringBuilder str = new StringBuilder();

            IList<ActivitiesInfo> allActivities = new List<ActivitiesInfo>();
            allActivities = VshopBrowser.GetAllActivities();

            if (allActivities.Count > 0)
            {
                str.Append("<div class=\"imgwrap banner swiper-container\" id=\"d0\">\r\n<div class=\"clearfix swiper-wrapper\">");
                foreach (ActivitiesInfo info in allActivities)
                {
                    str.AppendFormat("\r\n<div class=\"swiper-slide\"><a href=\"{0}\"><img src=\"{1}\" alt=\"{2}\"/></a></div>"
                        , "", info.CoverImg, "");
                }
                str.Append("</div>\r\n<div class=\"swiper-pagination s0\"></div>\r\n</div>\r\n");

                this.litActivitiesAd.Text = str.ToString();

            }
            else
            {
                if (siteFlag.EqualIgnoreCase("ls"))
                {
                    //this.imgAd.Src = "/templates/vshop/default/style/newcss/imgs/index/lsad.png";
                    this.litActivitiesAd.Text = "<div class=\"imgwrap\"><a href=\"\"><img src=\"/templates/vshop/default/style/newcss/imgs/index/lsad.jpg\"></a></div>";
                }
                else
                {
                    //this.imgAd.Src = "/templates/vshop/default/style/newcss/imgs/index/lasad.jpg";
                    this.litActivitiesAd.Text = "<div class=\"imgwrap\"><a href=\"\"><img src=\"/templates/vshop/default/style/newcss/imgs/index/lasad.jpg\"></a></div>";
                }
            }
            
        }

        private void GetCategoriesList()
        {
            StringBuilder str = new StringBuilder();
            string defImgs = "/Storage/master/default.png";
            string defUrl = "/Vshop/ProductList.aspx?categoryId=";
            bool isHalf = false;
         //   int sliderCount = 2;
            int countDown = 0;

            //获取轮播图

            IList<BannerInfo> allBanners = new List<BannerInfo>();
            allBanners = VshopBrowser.GetAllBanners();
            str.Append("<div class=\"imgwrap banner swiper-container\" id=\"d1\">\r\n<div class=\"clearfix swiper-wrapper\">");
            foreach (BannerInfo info in allBanners)
            {
                str.AppendFormat("\r\n<div class=\"swiper-slide\"><a href=\"{0}\"><img src=\"{1}\" alt=\"{2}\"/></a></div>"
                    , info.LoctionUrl, info.ImageUrl, "");
                //TplCfgInfo info2 = new NavigateInfo
                //{
                //    LocationType = info.LocationType,
                //    Url = info.Url
                //};
                //string loctionUrl = "javascript:";
                //if (!string.IsNullOrEmpty(info.Url))
                //{
                //    loctionUrl = info2.LoctionUrl;
                //}
                //string text = this.litImgae.Text;
                //this.litImgae.Text = text + "<a  id=\"ahref\"  href='" + loctionUrl + "'><img width=\"650\" height=\"200\" src=\"" + info.ImageUrl + "\" title=\"" + info.ShortDesc + "\" alt=\"" + info.ShortDesc + "\"  /></a>";
           
            }
            if (allBanners.Count == 0)
            {
                // this.litImgae.Text = "<a id=\"ahref\"  href='javascript:'><img src=\"/Utility/pics/default.jpg\" title=\"\" width=\"650\" height=\"200\"  /></a>";
                str.AppendFormat("<div class=\"swiper-slide\"><a href=\"{0}\"><img src=\"{1}\" alt=\"{2}\"/></a></div>"
                   , "", "/Utility/pics/default.jpg", "");
            }
            str.Append("</div>\r\n<div class=\"swiper-pagination s1\"></div>\r\n</div>\r\n");

            //获取分类
            if (this.dtCategories != null && dtCategories.Rows.Count > 0)
            {

                for (int i = 0; i < dtCategories.Rows.Count; i++)
                {
                    DataRow dr = dtCategories.Rows[i];
                    //图标
                    string iconUrl = defImgs;
                    if (dr["IconUrl"] != DBNull.Value && !string.IsNullOrEmpty(dr["IconUrl"].ToString()))
                    {
                        iconUrl = dr["IconUrl"].ToString();
                    }

                    //判断轮播图数量
                    //if (i < sliderCount)
                    //{
                    //    if (i == 0)
                    //    {
                    //        str.Append("<div class=\"imgwrap banner swiper-container\">\r\n<div class=\"clearfix swiper-wrapper\">\r\n");
                    //    }
                    //    //str.AppendFormat("<div class=\"swiper-slide\"><a href=\"{0}\"><img data-original=\"{1}\" alt=\"{2}\" src=\"/Utility/pics/lazy-ico.gif\"/></a></div>", defUrl + dr["CategoryId"], iconUrl, dr["Name"]);
                    //    str.AppendFormat("<div class=\"swiper-slide\"><a href=\"{0}\"><img src=\"{1}\" alt=\"{2}\"/></a></div>", defUrl + dr["CategoryId"], iconUrl, dr["Name"]);
                    //    //结束或登陆默认轮播图数量
                    //    if (i == dtCategories.Rows.Count - 1 || i == sliderCount - 1)
                    //    {
                    //        str.Append("</div>\r\n<div class=\"swiper-pagination\"></div>\r\n</div>\r\n");
                    //    }
                    //}
                    //else
                    //{
                    //4图和2图循环切换
                    if (countDown == 0)
                    {
                        str.AppendFormat("<div class=\"table {0}\">\r\n", !isHalf ? "four" : "half");
                    }
                    countDown++;

                    //str.AppendFormat("<div class=\"table_cell\">\r\n<a href = \"{0}\" >\r\n<img data-original = \"{1}\" src=\"/Utility/pics/lazy-ico.gif\" />\r\n</a>\r\n</div>", defUrl + dr["CategoryId"], iconUrl);
                    str.AppendFormat("<div class=\"table_cell\">\r\n<a href = \"{0}\" >\r\n<img src = \"{1}\" />\r\n</a>\r\n</div>", defUrl + dr["CategoryId"], iconUrl);
                    //如果已加载4图，则切换至2图
                    if ((countDown % (!isHalf ? 4 : 2) == 0) || (i == dtCategories.Rows.Count - 1))
                    {
                        str.Append("\r\n</div>");
                        countDown = 0;
                        isHalf = !isHalf;
                    }

                    //}
                }

            }

            this.litCategories.Text = str.ToString();

        }

        private void GetCategoriesListByLS()
        {
            StringBuilder str = new StringBuilder();
            string defImgs = "/Storage/master/default.png";
            string defUrl = "/Vshop/ProductList.aspx?categoryId=";

            int countDown = 0;

            // 获取轮播图
            IList<BannerInfo> allBanners = new List<BannerInfo>();
            allBanners = VshopBrowser.GetAllBanners();
            str.Append("<div class=\"imgwrap banner swiper-container\" id=\"d1\">\r\n<div class=\"clearfix swiper-wrapper\">");
            foreach (BannerInfo info in allBanners)
            {
                str.AppendFormat("\r\n<div class=\"swiper-slide\"><a href=\"{0}\"><img src=\"{1}\" alt=\"{2}\"/></a></div>"
                    , info.LoctionUrl, info.ImageUrl, "");
            }
            if (allBanners.Count == 0)
            {
                str.AppendFormat("<div class=\"swiper-slide\"><a href=\"{0}\"><img src=\"{1}\" alt=\"{2}\"/></a></div>"
                   , "", "/Utility/pics/default.jpg", "");
            }
            str.Append("</div>\r\n<div class=\"swiper-pagination s1\"></div>\r\n</div>\r\n");

            // 获取分类处理
            if (null != this.dtCategories && this.dtCategories.Rows.Count > 0)
            {
                for (int i = 0; i < this.dtCategories.Rows.Count; i++)
                {
                    DataRow dr = this.dtCategories.Rows[i];
                    //图标
                    string iconUrl = defImgs;
                    if (dr["IconUrl"] != DBNull.Value && !string.IsNullOrEmpty(dr["IconUrl"].ToString()))
                    {
                        iconUrl = dr["IconUrl"].ToString();
                    }
                    if (countDown == 0)
                    {
                        str.AppendLine("<div class=\"table\">\r\n");
                    }
                    countDown++;
                    
                    str.AppendFormat("<div class=\"table_cell\">\r\n<a href = \"{0}\" >\r\n<img src = \"{1}\" />\r\n</a>\r\n</div>", defUrl + dr["CategoryId"], iconUrl);

                    if ((countDown % 3 == 0) || i == this.dtCategories.Rows.Count - 1)
                    {
                        str.AppendLine("\r\n</div>");
                        countDown = 0;
                    }
                }
            }

            this.litCategories.Text = str.ToString();
        }

        private void GetCategoriesProducts()
        {
            StringBuilder str = new StringBuilder();
            string defImgs = "/Storage/master/default.png";
            string defUrl = Globals.ApplicationPath + "/Vshop/ProductDetails.aspx?ProductId=";
            string plink = Globals.GetCurrentDistributorId() > 0 ? "&&ReferralId=" + Globals.GetCurrentDistributorId() : "";
            bool isFlow = true;
            int countDown = 0;
            int displayIndex = 0;

            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            if (masterSettings.SiteFlag.EqualIgnoreCase("ls") || masterSettings.SiteFlag.EqualIgnoreCase("las"))
            {
                if (this.dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCategories.Rows)
                    {
                        int categoryId = 0;
                        int.TryParse(dr["CategoryId"].ToString(), out categoryId);

                        string iconUrl = defImgs;
                        if (dr["CoverUrl"] != DBNull.Value && !string.IsNullOrEmpty(dr["CoverUrl"].ToString()))
                        {
                            //这里要取分类图2，还没加字段
                            iconUrl = dr["CoverUrl"].ToString();
                        }
                        //分类图2
                        str.AppendFormat("<div class=\"imgwrap\"><a href = \"{0}\" ><img src=\"{1}\"></a></div>", "/Vshop/ProductList.aspx?categoryId=" + dr["CategoryId"], iconUrl);

                        isFlow = true;

                        //暂取12条数据
                        var dtProducts = ProductBrowser.GetProductsByDisplayHome(null, null, categoryId, null, 1, 12, out countDown, " DisplaySequence", "DESC");
                        if (dtProducts != null)
                        {
                            displayIndex = 0;

                            foreach (DataRow row in dtProducts.Rows)
                            {
                                iconUrl = defImgs;
                                if (row["HomePicUrl"] != DBNull.Value && !string.IsNullOrEmpty(row["HomePicUrl"].ToString()))
                                {
                                    iconUrl = row["HomePicUrl"].ToString();
                                }
                                if (isFlow)
                                {
                                    str.AppendFormat("<div class=\"imgwrap\"><a href = \"{0}\" ><img src=\"{1}\"></a></div>", defUrl + row["ProductId"] + plink, iconUrl);
                                }
                                else
                                {
                                    str.AppendFormat("<div class=\"item\"><span><a href=\"{4}\"><img src=\"{0}\" ></a></span><div class=\"flex\"><div class=\"flex_item\">{1}</div><div class=\"right\"><del>￥{2}</del><span><em>￥</em>{3}</span></div></div></div>"
                                      , iconUrl, row["ProductName"], row["MarketPrice"].ToDouble(0).ToString("F2"), row["SalePrice"].ToDouble(0).ToString("F2"), defUrl + row["ProductId"] + plink);

                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (this.dtCategories != null && dtCategories.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCategories.Rows)
                    {
                        int categoryId = 0;
                        int.TryParse(dr["CategoryId"].ToString(), out categoryId);

                        string iconUrl = defImgs;
                        if (dr["CoverUrl"] != DBNull.Value && !string.IsNullOrEmpty(dr["CoverUrl"].ToString()))
                        {
                            //这里要取分类图2，还没加字段
                            iconUrl = dr["CoverUrl"].ToString();
                        }
                        //分类图2
                        //str.AppendFormat("<div class=\"imgwrap\"><a href = \"{0}\" ><img data-original=\"{1}\" src=\"/Utility/pics/lazy-ico.gif\"></a></div>", "/Vshop/ProductList.aspx?categoryId=" + dr["CategoryId"], iconUrl);
                        str.AppendFormat("<div class=\"imgwrap\"><a href = \"{0}\" ><img src=\"{1}\"></a></div>", "/Vshop/ProductList.aspx?categoryId=" + dr["CategoryId"], iconUrl);

                        if (!isFlow)
                        {
                            str.AppendFormat("<div class=\"wrap\"><div class=\"product_list clearfix\">");

                        }
                        //暂取12条数据
                        var dtProducts = ProductBrowser.GetProductsByDisplayHome(null, null, categoryId, null, 1, 12, out countDown, " DisplaySequence", "DESC");
                        if (dtProducts != null)
                        {
                            displayIndex = 0;

                            foreach (DataRow row in dtProducts.Rows)
                            {
                                iconUrl = defImgs;
                                if (row["HomePicUrl"] != DBNull.Value && !string.IsNullOrEmpty(row["HomePicUrl"].ToString()))
                                {
                                    iconUrl = row["HomePicUrl"].ToString();
                                }
                                if (isFlow)
                                {
                                    //str.AppendFormat("<div class=\"imgwrap\"><a href = \"{0}\" ><img data-original=\"{1}\" src=\"/Utility/pics/lazy-ico.gif\"></a></div>", defUrl + row["ProductId"] + plink, iconUrl);
                                    str.AppendFormat("<div class=\"imgwrap\"><a href = \"{0}\" ><img src=\"{1}\"></a></div>", defUrl + row["ProductId"] + plink, iconUrl);
                                }
                                else
                                {

                                    //str.AppendFormat("<div class=\"item\"><span><a href=\"{4}\"><img data-original=\"{0}\" src=\"/Utility/pics/lazy-ico.gif\"></a></span><div class=\"flex\"><div class=\"flex_item\">{1}</div><div class=\"right\"><del>￥{2}</del><span><em>￥</em>{3}</span></div></div></div>"
                                    //   , iconUrl, row["ProductName"], row["MarketPrice"].ToDouble(0).ToString("F2"), row["SalePrice"].ToDouble(0).ToString("F2"), defUrl + row["ProductId"] + plink);
                                    str.AppendFormat("<div class=\"item\"><span><a href=\"{4}\"><img src=\"{0}\" ></a></span><div class=\"flex\"><div class=\"flex_item\">{1}</div><div class=\"right\"><del>￥{2}</del><span><em>￥</em>{3}</span></div></div></div>"
                                      , iconUrl, row["ProductName"], row["MarketPrice"].ToDouble(0).ToString("F2"), row["SalePrice"].ToDouble(0).ToString("F2"), defUrl + row["ProductId"] + plink);

                                    displayIndex = displayIndex + 1;

                                    if (displayIndex % 2 == 0)
                                    {
                                        str.AppendLine("</div><div class=\"product_list clearfix\">");
                                    }
                                }
                            }
                        }

                        if (!isFlow)
                        {
                            str.AppendFormat("</div></div>");
                        }

                        isFlow = !isFlow;
                    }
                }
            }
            
            this.litProducts.Text = str.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VDefault1.html";
            }
            base.OnInit(e);
        }

        private void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                if (((e.Item.ItemIndex + 1) % 4) == 1)
                {
                    Literal literal = (Literal)e.Item.Controls[0].FindControl("litStart");
                    literal.Visible = true;
                }
                else if ((((e.Item.ItemIndex + 1) % 4) == 0) || ((e.Item.ItemIndex + 1) == this.itemcount))
                {
                    Literal literal2 = (Literal)e.Item.Controls[0].FindControl("litEnd");
                    literal2.Visible = true;
                }
                Literal literal3 = (Literal)e.Item.Controls[0].FindControl("litpromotion");
                if (!string.IsNullOrEmpty(literal3.Text))
                {
                    literal3.Text = "<img src='" + literal3.Text + "'/>";
                }
                else
                {
                    literal3.Text = "<img src='/Storage/master/default.png'/>";
                }
            }
        }

        private void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Literal literal = (Literal)e.Item.Controls[0].FindControl("litpromotion");
                string str = "";
                if (DataBinder.Eval(e.Item.DataItem, "MainCategoryPath") != null)
                {
                    str = DataBinder.Eval(e.Item.DataItem, "MainCategoryPath").ToString();
                }
                DataView defaultView = this.dtpromotion.DefaultView;
                if (!string.IsNullOrEmpty(str))
                {
                    defaultView.RowFilter = " ActivitiesType=0 ";
                    if (defaultView.Count > 0)
                    {
                        literal.Text = "<span class=\"sale-favourable\"><i>满" + decimal.Parse(defaultView[0]["MeetMoney"].ToString()).ToString("0") + "</i><i>减" + decimal.Parse(defaultView[0]["ReductionMoney"].ToString()).ToString("0") + "</i></span>";
                    }
                    else
                    {
                        defaultView.RowFilter = " ActivitiesType= " + str.Split(new char[] { '|' })[0].ToString();
                        if (defaultView.Count > 0)
                        {
                            literal.Text = "<span class=\"sale-favourable\"><i>满" + decimal.Parse(defaultView[0]["MeetMoney"].ToString()).ToString("0") + "</i><i>减" + decimal.Parse(defaultView[0]["ReductionMoney"].ToString()).ToString("0") + "</i></span>";
                        }
                    }
                }
            }
        }
    }
}

