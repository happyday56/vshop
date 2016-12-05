namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.VShop;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using NewLife.Log;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true), WeiXinOAuth(Common.Controls.WeiXinOAuthPage.VInvite_Open)]
    public class VInvite_open : VWeiXinOAuthTemplatedWebControl
    {
        private Literal litLostTime;

        protected override void AttachChildControls()
        {
            bool isBack = true;

            //获取用户状态，如果未注册先获取微信授权
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (null != currentMember)
            {
                DistributorsInfo currDistributor = DistributorsBrower.GetCurrentDistributors(currentMember.UserId);
                // 判断是否已是店主
                if (null != currDistributor)
                {
                    base.GotoResourceNotFound("您已是店主或钻石会员，不允许再次申请");
                }

                XTrace.WriteLine("邀请会员进入：" + currentMember.UserId);
                //获取邀请码
                var invitecode = this.Page.Request.QueryString["invitecode"] ?? "";

                //判断邀请码是否为空
                if (!string.IsNullOrEmpty(invitecode.Trim()))
                {
                    //0.检测用户是否已被邀请成功(目前情况只要用户没有注册完成则可以占有多个邀请，直至有一个已经完成),未被邀请进入条件1
                    //1.判断邀请码是否已被该用户注册或占有
                    //2.如果不满足条件1则进入3，满足则进入4
                    //3.判断该用户是否被邀请过，如果邀请过则不能再被邀请
                    //4.跳转至邀请码状态所属页面（>1）,状态为2则直接转购买页，3跳转至首页
                    //5.如果状态为1,

                    if (!InviteBrowser.CheckIsInvite(currentMember.UserId))
                    {
                        InviteCode code = InviteBrowser.GetInvoiteCode(invitecode);
                        if (code != null && code.InviteStatus < 3 && code.UserId != currentMember.UserId)
                        {
                            if (code.IsUse)
                            {
                                #region 已接受邀请
                                //如果受邀用户不是当前用户
                                if (currentMember.UserId == code.InviteUserId)
                                {
                                    //如果状态为1，判断是否超时
                                    //2:跳转至购买页
                                    if (code.InviteStatus == 1)
                                    {
                                        if (code.TimeStamp.AddMinutes(600) >= DateTime.Now)
                                        {
                                            //设置过期时间
                                            SetLoseTime(code.TimeStamp.AddMinutes(600));
                                            isBack = false;
                                            this.Page.Response.Write("<script>var inviteCode = '" + code.Code + "'</script>");
                                        }
                                    }
                                    else if (code.InviteStatus == 2)
                                    {
                                        this.Page.Response.Redirect("/vshop/ProductDetails.aspx?ReferralUserId=" + code.UserId + "&InviteCode=" + code.Code + "&ProductId=" + code.ProductId);
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region 未接受邀请
                                DateTime now = DateTime.Now;
                                //立即锁定该条邀请码,防止生成脏数据
                                bool ret = InviteBrowser.LockInviteCode(invitecode, currentMember.UserId, now, code.InviteId);
                                if (ret)
                                {
                                    SetLoseTime(now.AddMinutes(600));
                                    isBack = false;
                                    this.Page.Response.Write("<script>var inviteCode = '" + code.Code + "'</script>");
                                }
                                #endregion
                            }
                        }
                    }
                }
            }

            if (isBack)
            {
                //(待修改)修改跳转到错误页或邀请码失效页,当前给的跳转至首页
                //this.Page.Response.Redirect("/Vshop/Default.aspx");
                if (null != currentMember)
                {
                    XTrace.WriteLine("此邀请已被使用时的当前登录会员VInvite_open：" + currentMember.UserId + "------" + currentMember.UserName + "------" + currentMember.RealName + "------" + currentMember.CellPhone);
                }
                
                base.GotoResourceNotFound("此邀请已被使用");
            }

            SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);

            PageTitle.AddSiteNameTitle(siteSettings.SiteName + "邀请注册");
        }

        protected void SetLoseTime(DateTime losetime)
        {
            if (this.litLostTime == null)
            {
                this.litLostTime = (Literal)this.FindControl("litLostTime");
            }
            this.litLostTime.Text = string.Format("<span id='splosttime' val='{0}'></span>"
                          , losetime.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vInvite_open.html";
            }
            base.OnInit(e);
        }
    }
}

