namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using NewLife.Log;
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    [ParseChildren(true)]
    public class VMyProducts : VWeiXinOAuthTemplatedWebControl
    {
        private int categoryId;
        private string keyWord = string.Empty;
        private VshopTemplatedRepeater rpCategorys;
        private VshopTemplatedRepeater rpChooseProducts;
        private HtmlInputText txtkeywords;

        protected override void AttachChildControls()
        {
            int.TryParse(this.Page.Request.QueryString["categoryId"], out this.categoryId);
            this.keyWord = this.Page.Request.QueryString["keyWord"];
            if (!string.IsNullOrWhiteSpace(this.keyWord))
            {
                this.keyWord = this.keyWord.Trim();
            }
            this.txtkeywords = (HtmlInputText)this.FindControl("keywords");
            this.rpChooseProducts = (VshopTemplatedRepeater)this.FindControl("rpChooseProducts");
            this.rpCategorys = (VshopTemplatedRepeater)this.FindControl("rpCategorys");
            this.DataBindSoruce();

            this.Page.Session["stylestatus"] = "3";

            SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);

            //if (siteSettings.SiteFlag.EqualIgnoreCase("ls"))
            //{
                // 什么都不做
            //}
            //else
            //{
                //#region 邀请用户状态更新
                ///*
                // * 微信付款完成后(wx_Submit.aspx)，最终会进入会员中心页面
                // * 1.判断是否有邀请码cookie
                // * 2.有：则判断订单是否已付款完成
                // * 3.若已完成则修改邀请码状态至已完成(3),设置cookie过期
                // */

                //MemberInfo currentMember = MemberProcessor.GetCurrentMember();

                //if (null != currentMember)
                //{
                //    var inviteCok = this.Page.Request.Cookies["InviteId"];
                //    if (inviteCok != null)
                //    {
                //        int inviteId = 0;
                //        int.TryParse(inviteCok.Value, out inviteId);
                //        if (inviteId > 0)
                //        {
                //            var code = InviteBrowser.GetInvoiteCode(inviteId);
                //            if (code != null)
                //            {
                //                if (code.InviteUserId == currentMember.UserId)
                //                {
                //                    //判断订单是否付款成功
                //                    if (InviteBrowser.CheckInviteProductFinished(currentMember.UserId, code.ProductId))
                //                    {

                //                        //修改验证码状态
                //                        code.InviteStatus = 3;
                //                        code.TimeStamp = DateTime.Now;
                //                        var ret = InviteBrowser.UpdateInviteStatus(code);
                //                        if (ret)
                //                        {
                //                            //创建邀请码使用记录
                //                            InviteBrowser.AddInviteRecord(new Hidistro.Entities.VShop.InviteRecord()
                //                            {
                //                                InviteId = code.InviteId,
                //                                InviteStatus = code.InviteStatus,
                //                                OpenId = currentMember.OpenId,
                //                                UserId = currentMember.UserId,
                //                                TimeStamp = code.TimeStamp,
                //                            });

                //                            //设置cookie过期
                //                            inviteCok.Expires = DateTime.Now.AddDays(-1);
                //                            this.Page.Response.Cookies.Add(inviteCok);

                //                            //创建分销商记录

                //                            DistributorsInfo distributorsInfo = new DistributorsInfo()
                //                            {
                //                                UserId = currentMember.UserId,
                //                                GradeId = currentMember.GradeId,
                //                                //推荐人
                //                                //ReferralUserId = code.UserId,
                //                                ParentUserId = code.UserId,
                //                                RequestAccount = "",
                //                                StoreName = code.InviteRealName + InviteBrowser.getRandomizer(4, false, false, true, false) + "的店铺",
                //                                StoreDescription = "",
                //                                BackImage = "",
                //                                Logo = "",
                //                                UserName = currentMember.UserName,
                //                                RealName = code.InviteRealName,
                //                                CellPhone = code.InvitePhone,
                //                                DistriGradeId = DistributorGradeBrower.GetIsDefaultDistributorGradeInfo().GradeId,
                //                                InvitationNum = siteSettings.DefaultMinInvitationNum,
                //                                CreateTime = DateTime.Now
                //                            };


                //                            if (!DistributorsBrower.AddDistributors(distributorsInfo))
                //                            {
                //                                XTrace.WriteLine("\r\n分销商创建失败，用户ID:[{0}],邀请人ID:[{1}],邀请码:[{2}]\r\n", currentMember.UserId, code.UserId, code.Code);
                //                                //context.Response.Write("{\"success\":false,\"msg\":\"店铺名称已存在，请重新输入店铺名称\"}");
                //                                //   return;
                //                            }
                //                            else
                //                            {
                //                                if (HttpContext.Current.Request.Cookies["Vshop-Member"] != null)
                //                                {
                //                                    string str = "Vshop-ReferralId";
                //                                    this.Page.Response.Cookies[str].Expires = DateTime.Now.AddDays(-1);
                //                                    HttpCookie httpCookie = new HttpCookie(str)
                //                                    {
                //                                        Value = Globals.GetCurrentMemberUserId().ToString(),
                //                                        Expires = DateTime.Now.AddYears(10)
                //                                    };
                //                                    this.Page.Response.Cookies.Add(httpCookie);
                //                                }

                //                                //（待修改）此处已经完成邀请，按照流程应该跳转至最后注册成功的页面(invite_complated.aspx)
                //                                //this.Page.Response.Redirect("/vshop/invite_complated.aspx");
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}

                //#endregion
            //}

        }

        private void DataBindSoruce()
        {
            int num;
            this.txtkeywords.Value = this.keyWord;
            this.rpCategorys.DataSource = CategoryBrowser.GetCategories();
            this.rpCategorys.DataBind();
            this.rpChooseProducts.DataSource = ProductBrowser.GetProducts(MemberProcessor.GetCurrentMember(), null, new int?(this.categoryId), this.keyWord, 1, 0x2710, out num, "DisplaySequence", "desc", true);
            this.rpChooseProducts.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-MyProducts.html";
            }
            base.OnInit(e);
        }
    }
}

