namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ParseChildren(true)]
    public class VInvite_login : VWeiXinOAuthTemplatedWebControl
    {

        protected override void AttachChildControls()
        {
            var currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember != null)
            {
                DistributorsInfo currentDistributor = DistributorsBrower.GetDistributorInfo(currentMember.UserId);
                // 分销商没有被冻结的情况下
                if (null != currentDistributor)
                {
                    // 分销商没有被冻结的情况下
                    if (currentDistributor.ReferralStatus == 2)
                    {
                        base.GotoResourceNotFound("您的店铺已被冻结，请联系官方客服。");
                    }

                    if (currentDistributor.DeadlineTime < DateTime.Now)
                    {
                        base.GotoResourceNotFound("您的店铺已过期，请联系官方客服。");
                    }

                    if (currentDistributor.IsTempStore == 1)
                    {
                        // 钻石会员不允许邀请开店
                        base.GotoResourceNotFound("您没有权限操作，请联系官方客服。");

                        //this.Page.Response.ContentType = "text/json";
                        //this.Page.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = "您没有权限操作！" }));
                        //this.Page.Response.End();
                    }
                    else
                    {
                        if (currentDistributor.IsBindMobile == 1)
                        {
                            //this.Page.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, msg = "/VShop/invite.aspx" }));
                            this.Page.Response.Redirect("invite.aspx", true);
                        }
                        else
                        {
                            string isAjax = this.Page.Request["isAjax"];
                            string phone = this.Page.Request["phone"] ?? "";
                            string code = this.Page.Request["verifycode"] ?? "";
                            if (isAjax == "1" && !string.IsNullOrEmpty(code.Trim()) && !string.IsNullOrEmpty(phone.Trim()))
                            {
                                this.Page.Response.ContentType = "text/json";

                                //获取验证码,十分钟过期
                                string verifycode = MemberProcessor.GetLastVerifyCode(currentMember.UserId, 10);
                                //判断验证码是否过期，与填入的验证码是否一致
                                if (!string.IsNullOrEmpty(verifycode) && code == verifycode)
                                {
                                    // 更新分销商已绑定手机号码信息
                                    bool updateFlag = DistributorsBrower.UpdateIsBindMobile(currentDistributor.UserId, 1);
                                    //验证成功,跳转页面
                                    this.Page.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, msg = "/VShop/invite.aspx" }));
                                }
                                else
                                {
                                    //验证码已过期
                                    this.Page.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = verifycode == string.Empty ? "验证码已过期，请重新获取！" : "验证码错误！" }));
                                }
                                this.Page.Response.End();
                            }
                        }
                    }                    
                }                
            }


            PageTitle.AddSiteNameTitle("邀请登陆");
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "skin-vInvite_login.html";
            }
            base.OnInit(e);
        }
    }
}

