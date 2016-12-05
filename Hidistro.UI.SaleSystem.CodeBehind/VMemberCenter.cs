using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hidistro.Core;
using Hidistro.Entities.Members;
using Hidistro.Entities.Orders;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.Common.Controls;
using Hidistro.Core.Entities;
using System.Web;
using NewLife.Log;

namespace Hidistro.UI.SaleSystem.CodeBehind
{
    [ParseChildren(true), WeiXinOAuth(Common.Controls.WeiXinOAuthPage.VMemberCenter)]
    public class VMemberCenter : VWeiXinOAuthTemplatedWebControl
    {
        private Image image;
        private Literal litExpenditure;
        private Literal litMemberGrade;
        private Literal litUserName;
        private Literal litWaitForPayCount;
        private Literal litWaitForRecieveCount;
        private Literal litWaitForReplace;
        private Literal litVirtualPoint;
        private Literal litVPName;

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("会员中心");
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember != null)
            {
                this.litUserName = (Literal)this.FindControl("litUserName");
                this.image = (Image)this.FindControl("image");
                this.litExpenditure = (Literal)this.FindControl("litExpenditure");
                this.litExpenditure.SetWhenIsNotNull(currentMember.Expenditure.ToString("F2"));
                this.litMemberGrade = (Literal)this.FindControl("litMemberGrade");
                this.litVirtualPoint = (Literal)this.FindControl("litVirtualPoint");
                this.litVPName = (Literal)this.FindControl("litVPName");

                MemberGradeInfo memberGrade = MemberProcessor.GetMemberGrade(currentMember.GradeId);
                if (memberGrade != null)
                {
                    this.litMemberGrade.SetWhenIsNotNull(memberGrade.Name);
                }
                this.litUserName.Text = string.IsNullOrEmpty(currentMember.RealName) ? currentMember.UserName : currentMember.RealName;
                if (!string.IsNullOrEmpty(currentMember.UserHead))
                {
                    this.image.ImageUrl = currentMember.UserHead;
                }
                this.Page.Session["stylestatus"] = "1";
                this.litWaitForRecieveCount = (Literal)this.FindControl("litWaitForRecieveCount");
                this.litWaitForPayCount = (Literal)this.FindControl("litWaitForPayCount");
                this.litWaitForReplace = (Literal)this.FindControl("litWaitForReplace");
                OrderQuery query = new OrderQuery
                {
                    Status = OrderStatus.WaitBuyerPay
                };
                int userOrderCount = MemberProcessor.GetUserOrderCount(Globals.GetCurrentMemberUserId(), query);
                this.litWaitForPayCount.SetWhenIsNotNull((userOrderCount > 0) ? ("<i class=\"border-circle\">" + userOrderCount.ToString() + "<i>") : "");
                query.Status = OrderStatus.SellerAlreadySent;
                userOrderCount = MemberProcessor.GetUserOrderCount(Globals.GetCurrentMemberUserId(), query);
                this.litWaitForRecieveCount.SetWhenIsNotNull((userOrderCount > 0) ? ("<i class=\"border-circle\">" + userOrderCount.ToString() + "<i>") : "");
                int userOrderReturnCount = MemberProcessor.GetUserOrderReturnCount(Globals.GetCurrentMemberUserId());
                this.litWaitForReplace.SetWhenIsNotNull((userOrderReturnCount > 0) ? ("<i class=\"border-circle\">" + userOrderReturnCount.ToString() + "<i>") : "");

                this.litVirtualPoint.Text = currentMember.VirtualPoints.ToString("F2");

                SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);
                this.litVPName.Text = siteSettings.VirtualPointName;

                //if (siteSettings.SiteFlag.EqualIgnoreCase("ls"))
                //{
                    // 什么都不做
                //}
                //else
                //{
                 //   #region 邀请用户状态更新
                 //   /*
                 //* 微信付款完成后(wx_Submit.aspx)，最终会进入会员中心页面
                 //* 1.判断是否有邀请码cookie
                 //* 2.有：则判断订单是否已付款完成
                 //* 3.若已完成则修改邀请码状态至已完成(3),设置cookie过期
                 //*/
                 //   var inviteCok = this.Page.Request.Cookies["InviteId"];
                 //   if (inviteCok != null)
                 //   {
                 //       int inviteId = 0;
                 //       int.TryParse(inviteCok.Value, out inviteId);
                 //       if (inviteId > 0)
                 //       {
                 //           var code = InviteBrowser.GetInvoiteCode(inviteId);
                 //           if (code != null)
                 //           {
                 //               if (code.InviteUserId == currentMember.UserId)
                 //               {
                 //                   //判断订单是否付款成功
                 //                   if (InviteBrowser.CheckInviteProductFinished(currentMember.UserId, code.ProductId))
                 //                   {

                 //                       //修改验证码状态
                 //                       code.InviteStatus = 3;
                 //                       code.TimeStamp = DateTime.Now;
                 //                       var ret = InviteBrowser.UpdateInviteStatus(code);
                 //                       if (ret)
                 //                       {
                 //                           //创建邀请码使用记录
                 //                           InviteBrowser.AddInviteRecord(new Hidistro.Entities.VShop.InviteRecord()
                 //                           {
                 //                               InviteId = code.InviteId,
                 //                               InviteStatus = code.InviteStatus,
                 //                               OpenId = currentMember.OpenId,
                 //                               UserId = currentMember.UserId,
                 //                               TimeStamp = code.TimeStamp,
                 //                           });

                 //                           //设置cookie过期
                 //                           inviteCok.Expires = DateTime.Now.AddDays(-1);
                 //                           this.Page.Response.Cookies.Add(inviteCok);

                 //                           //创建分销商记录
                 //                           DistributorsInfo distributorsInfo = new DistributorsInfo()
                 //                           {
                 //                               UserId = currentMember.UserId,
                 //                               GradeId = currentMember.GradeId,
                 //                               //推荐人
                 //                               //ReferralUserId = code.UserId,
                 //                               ParentUserId = code.UserId,
                 //                               RequestAccount = "",
                 //                               StoreName = code.InviteRealName + InviteBrowser.getRandomizer(4, false, false, true, false) + "的店铺",
                 //                               StoreDescription = "",
                 //                               BackImage = "",
                 //                               Logo = "",
                 //                               UserName = currentMember.UserName,
                 //                               RealName = code.InviteRealName,
                 //                               CellPhone = code.InvitePhone,
                 //                               DistriGradeId = DistributorGradeBrower.GetIsDefaultDistributorGradeInfo().GradeId,
                 //                               InvitationNum = siteSettings.DefaultMinInvitationNum,
                 //                               CreateTime = DateTime.Now
                 //                           };


                 //                           if (!DistributorsBrower.AddDistributors(distributorsInfo))
                 //                           {
                 //                               XTrace.WriteLine("\r\n分销商创建失败，用户ID:[{0}],邀请人ID:[{1}],邀请码:[{2}]\r\n", currentMember.UserId, code.UserId, code.Code);
                 //                               //context.Response.Write("{\"success\":false,\"msg\":\"店铺名称已存在，请重新输入店铺名称\"}");
                 //                               //   return;
                 //                           }
                 //                           else
                 //                           {
                 //                               if (HttpContext.Current.Request.Cookies["Vshop-Member"] != null)
                 //                               {
                 //                                   string str = "Vshop-ReferralId";
                 //                                   this.Page.Response.Cookies[str].Expires = DateTime.Now.AddDays(-1);
                 //                                   HttpCookie httpCookie = new HttpCookie(str)
                 //                                   {
                 //                                       Value = Globals.GetCurrentMemberUserId().ToString(),
                 //                                       Expires = DateTime.Now.AddYears(10)
                 //                                   };
                 //                                   this.Page.Response.Cookies.Add(httpCookie);
                 //                               }

                 //                               //（待修改）此处已经完成邀请，按照流程应该跳转至最后注册成功的页面(invite_complated.aspx)
                 //                               //this.Page.Response.Redirect("/vshop/invite_complated.aspx");
                 //                           }
                 //                       }
                 //                   }
                 //               }
                 //           }
                 //       }
                 //   }
                 //   #endregion
                //}                
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VMemberCenter.html";
            }
            base.OnInit(e);
        }
    }
}

