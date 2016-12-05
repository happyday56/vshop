namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Core.Enums;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using NewLife.Log;
    using Hidistro.Entities.Commodities;

    [ParseChildren(true), WeiXinOAuth(Common.Controls.WeiXinOAuthPage.VDistributorCenter)]
    public class VDistributorCenter : VWeiXinOAuthTemplatedWebControl
    {
        private HyperLink hyrequest;
        private Image imgGrade;
        private HiImage imglogo;
        private Literal litdistirbutors;
        private Literal litOrders;
        private Literal litQRcode;
        private Literal litQRcode2;
        private Literal litStoreNum;
        private Literal litStroeName;
        private Literal litTodayOrdersNum;
        private Literal litVP;
        private FormatedMoneyLabel lblsurpluscommission;
        private FormatedMoneyLabel lblAccountBalance;
        private FormatedMoneyLabel lblAccumulatedIncome;

        private FormatedMoneyLabel refrraltotal;
        private FormatedMoneyLabel saletotal;
        private Literal litRefId;
        private Literal litInviteUrl;
        private Literal litSJName;

        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("店铺中心");
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            int currentMemberUserId = Globals.GetCurrentMemberUserId();
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();

            #region 邀请用户状态更新
            /*
                 * 微信付款完成后(wx_Submit.aspx)，最终会进入会员中心页面
                 * 1.判断是否有邀请码cookie
                 * 2.有：则判断订单是否已付款完成
                 * 3.若已完成则修改邀请码状态至已完成(3),设置cookie过期
                 */

            //MemberInfo currentMember = MemberProcessor.GetCurrentMember();            

            if (null != currentMember)
            {
                
                var inviteCok = this.Page.Request.Cookies["InviteId"];
                
                if (inviteCok != null)
                {
                    int inviteId = 0;
                    int.TryParse(inviteCok.Value, out inviteId);
                    XTrace.WriteLine("------------------------邀请开店注册开始。------------------------");

                    XTrace.WriteLine("邀请码ID：" + inviteId);
                    if (inviteId > 0)
                    {
                        var code = InviteBrowser.GetInvoiteCode(inviteId);
                        if (code != null)
                        {
                            if (code.InviteUserId == currentMember.UserId)
                            {
                                //判断订单是否付款成功
                                if (InviteBrowser.CheckInviteProductFinished(currentMember.UserId, code.ProductId))
                                {
                                    XTrace.WriteLine("查看订单是否已付款成功.");
                                    //修改验证码状态
                                    code.InviteStatus = 3;
                                    code.TimeStamp = DateTime.Now;
                                    var ret = InviteBrowser.UpdateInviteStatus(code);
                                    if (ret)
                                    {
                                        //创建邀请码使用记录
                                        InviteBrowser.AddInviteRecord(new Hidistro.Entities.VShop.InviteRecord()
                                        {
                                            InviteId = code.InviteId,
                                            InviteStatus = code.InviteStatus,
                                            OpenId = currentMember.OpenId,
                                            UserId = currentMember.UserId,
                                            TimeStamp = code.TimeStamp,
                                        });

                                        //设置cookie过期
                                        inviteCok.Expires = DateTime.Now.AddDays(-1);
                                        this.Page.Response.Cookies.Add(inviteCok);

                                        // 判断是店主还是钻石会员
                                        int tempStore = 0;
                                        DateTime tmpDecasualizationTime;
                                        if (code.PTTypeId == 2)
                                        {
                                            // 钻石会员
                                            tempStore = 1;
                                            tmpDecasualizationTime = DateTime.Now.AddYears(1);
                                        }
                                        else
                                        {
                                            tempStore = 0;
                                            tmpDecasualizationTime = DateTime.Now;
                                        }

                                        //创建分销商记录
                                        DistributorsInfo distributorsInfo = new DistributorsInfo()
                                        {
                                            UserId = currentMember.UserId,
                                            GradeId = currentMember.GradeId,
                                            //推荐人
                                            ReferralUserId = code.UserId,
                                            ParentUserId = code.UserId,
                                            RequestAccount = "",
                                            StoreName = code.InviteRealName + InviteBrowser.getRandomizer(4, false, false, true, false) + "的店铺",
                                            StoreDescription = "",
                                            BackImage = "",
                                            Logo = "",
                                            UserName = currentMember.UserName,
                                            RealName = code.InviteRealName,
                                            CellPhone = code.InvitePhone,
                                            DistriGradeId = DistributorGradeBrower.GetIsDefaultDistributorGradeInfo().GradeId,
                                            InvitationNum = masterSettings.DefaultMinInvitationNum,
                                            CreateTime = DateTime.Now,
                                            IsTempStore = tempStore,
                                            DecasualizationTime = tmpDecasualizationTime
                                        };

                                        XTrace.WriteLine("开始创建分销商.");
                                        if (!DistributorsBrower.AddDistributors(distributorsInfo))
                                        {
                                            XTrace.WriteLine("\r\n分销商创建失败，用户ID:[{0}],邀请人ID:[{1}],邀请码:[{2}]\r\n", currentMember.UserId, code.UserId, code.Code);
                                            //context.Response.Write("{\"success\":false,\"msg\":\"店铺名称已存在，请重新输入店铺名称\"}");
                                            //   return;
                                        }
                                        else
                                        {
                                            if (HttpContext.Current.Request.Cookies["Vshop-Member"] != null)
                                            {
                                                string str = "Vshop-ReferralId";
                                                this.Page.Response.Cookies[str].Expires = DateTime.Now.AddDays(-1);
                                                HttpCookie httpCookie = new HttpCookie(str)
                                                {
                                                    Value = Globals.GetCurrentMemberUserId().ToString(),
                                                    Expires = DateTime.Now.AddYears(10)
                                                };
                                                this.Page.Response.Cookies.Add(httpCookie);
                                            }

                                            //（待修改）此处已经完成邀请，按照流程应该跳转至最后注册成功的页面(invite_complated.aspx)
                                            //this.Page.Response.Redirect("/vshop/invite_complated.aspx");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    XTrace.WriteLine("------------------------邀请开店注册结束。------------------------");
                }
                
            }

            #endregion


            DistributorsInfo userIdDistributors = DistributorsBrower.GetUserIdDistributors(currentMemberUserId);

            string txtVP = "";

            
            if (userIdDistributors != null )
            {
                // 分销商没有被冻结的情况下
                if (userIdDistributors.ReferralStatus == 2)
                {
                    base.GotoResourceNotFound("您的店铺已被冻结，请联系官方客服。");
                }

                if (userIdDistributors.DeadlineTime < DateTime.Now)
                {
                    base.GotoResourceNotFound("您的店铺已过期，请联系官方客服。");
                }

                // 处理钻石会员升级成正式会员 2016-02-26
                if (userIdDistributors.IsTempStore == 1)
                {
                    // 钻石会员

                    // 获取升级订单信息
                    OrderInfo orderInfo = DistributorsBrower.GetSJOrderInfo(userIdDistributors.UserId);
                    if (null != orderInfo)
                    {
                        bool updateFlag = false;

                        // 变更店主的属性
                        updateFlag = DistributorsBrower.UpdateDistributorIsTempStoreAndDeadlineTimeById(userIdDistributors.UserId, 0, orderInfo.PayDate.ToDateTime().AddMinutes(-1), orderInfo.PayDate.ToDateTime().AddYears(1));
                        XTrace.WriteLine("升级分销商时的ID和截止有效时间(正):" + userIdDistributors.UserId.ToString() + "---" + orderInfo.PayDate.ToDateTime().AddYears(1) + "---" + updateFlag);

                        if (updateFlag)
                        {
                            // 增加金币
                            updateFlag = DistributorsBrower.AddUserVirtualPoints(userIdDistributors.UserId, masterSettings.DefaultVirtualPoint);
                            XTrace.WriteLine("升级分销商时的ID和默认Points(正):" + userIdDistributors.UserId.ToString() + "---" + masterSettings.DefaultVirtualPoint.ToString() + "---" + updateFlag);

                            // 更改订单状态和相关时间
                            if (orderInfo.OrderStatus == OrderStatus.BuyerAlreadyPaid || orderInfo.OrderStatus == OrderStatus.SellerAlreadySent)
                            {
                                updateFlag = DistributorsBrower.UpdateOrderStatus(orderInfo.OrderId, 5, DateTime.Now);
                            }
                        }                        
                    }
                }

                if (masterSettings.SiteFlag.EqualIgnoreCase("las"))
                {
                    this.Page.Response.Redirect("My.aspx", true);
                }

                this.litTodayOrdersNum = (Literal)this.FindControl("litTodayOrdersNum");
                OrderQuery query = new OrderQuery
                {
                    UserId = new int?(currentMemberUserId),
                    Status = OrderStatus.Today
                };
                this.litTodayOrdersNum.Text = DistributorsBrower.GetDistributorOrderCount(query).ToString();

                if (currentMember != null)
                {
                    txtVP = masterSettings.VirtualPointName + "：" + currentMember.VirtualPoints;
                }

                this.imglogo = (HiImage)this.FindControl("imglogo");
                this.lblsurpluscommission = (FormatedMoneyLabel)this.FindControl("lblsurpluscommission");
                this.lblAccountBalance = (FormatedMoneyLabel)this.FindControl("lblAccountBalance");
                this.lblAccumulatedIncome = (FormatedMoneyLabel)this.FindControl("lblAccumulatedIncome");
                this.imgGrade = (Image)this.FindControl("imgGrade");
                this.hyrequest = (HyperLink)this.FindControl("hyrequest");
                this.litStroeName = (Literal)this.FindControl("litStroeName");
                this.saletotal = (FormatedMoneyLabel)this.FindControl("saletotal");
                this.refrraltotal = (FormatedMoneyLabel)this.FindControl("refrraltotal");
                this.litStoreNum = (Literal)this.FindControl("litStoreNum");
                this.litdistirbutors = (Literal)this.FindControl("litdistirbutors");
                this.litQRcode = (Literal)this.FindControl("litQRcode");
                this.litQRcode2 = (Literal)this.FindControl("litQRcode2");
                this.litOrders = (Literal)this.FindControl("litOrders");
                this.litVP = (Literal)this.FindControl("litVP");
                this.litRefId = (Literal)FindControl("litRefId");
                this.litInviteUrl = (Literal)FindControl("litInviteUrl");
                this.litSJName = (Literal)FindControl("litSJName");

                this.litdistirbutors.Text = "<li><a href=\"ChirldrenDistributors.aspx\" class=\"shop-underling\">我的团队</a></li>";
                this.litQRcode.Text = "<li><a href=\"QRcode.aspx?PTTypeId=1&ReferralUserId=" + userIdDistributors.UserId + "\" class=\"shop-qr\">邀请店主</a></li>";
                this.litQRcode2.Text = "<li><a href=\"QRcode.aspx?PTTypeId=2&ReferralUserId=" + userIdDistributors.UserId + "\" class=\"shop-qr\">邀请钻石会员</a></li>";
                this.litVP.Text = "<li><a href=\"#\" class=\"shop-brokerage\">" + txtVP + "</a></li>";
                this.litStroeName.Text = userIdDistributors.StoreName;
                this.saletotal.Money = userIdDistributors.OrdersTotal;

                if (userIdDistributors.IsTempStore == 1)
                {
                    // 钻石会员不显示推广码
                    this.litQRcode.Visible = false;
                    this.litQRcode2.Visible = false;


                    int tmpProductId = 0;
                    ProductInfo product = DistributorsBrower.GetQRCodeDistProductByPTTypeId(1);
                    if (null != product)
                    {
                        tmpProductId = product.ProductId;
                    }
                    this.litInviteUrl.Text = "/vshop/ProductDetails.aspx?SJCode=1&PTTypeId=1&ReferralId=" + userIdDistributors.ReferralUserId + "&ReferralUserId=" + userIdDistributors.ReferralUserId + "&ProductId=" + tmpProductId;
                    this.litSJName.Text = "升级店主";
                }
                else
                {
                    this.litQRcode.Visible = true;
                    this.litQRcode2.Visible = false;

                    this.litInviteUrl.Text = "/vshop/invite.aspx";
                    this.litSJName.Text = "邀请开店";
                }
                
                DistributorGradeInfo distributorGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(userIdDistributors.DistriGradeId);

                if ((userIdDistributors != null) && (userIdDistributors.UserId > 0))
                {
                    this.lblsurpluscommission.Money = userIdDistributors.ReferralBlance;
                    //this.lblAlreadycommission.Money = userIdDistributors.ReferralRequestBalance;
                    this.lblAccountBalance.Money = userIdDistributors.AccountBalance;
                    this.lblAccumulatedIncome.Money = userIdDistributors.AccumulatedIncome;
                }

                if (DistributorsBrower.IsExitsCommionsRequest())
                {
                    this.hyrequest.Text = "审核中…";
                    this.hyrequest.Enabled = false;
                }
                if (string.IsNullOrEmpty(currentMember.RealName) || string.IsNullOrEmpty(currentMember.CellPhone))
                {
                    this.hyrequest.NavigateUrl = "UserInfo.aspx?edit=true&&returnUrl=" + Globals.UrlEncode(Globals.HostPath(HttpContext.Current.Request.Url) + "/Vshop/RequestCommissions.aspx");
                }

                if ((distributorGradeInfo != null) && (distributorGradeInfo.Ico.Length > 10))
                {
                    this.imgGrade.ImageUrl = distributorGradeInfo.Ico;
                }
                else
                {
                    this.imgGrade.Visible = false;
                }
                if (!string.IsNullOrEmpty(userIdDistributors.Logo))
                {
                    this.imglogo.ImageUrl = userIdDistributors.Logo;
                }
                else
                {
                    if (!string.IsNullOrEmpty(masterSettings.DistributorLogoPic))
                    {
                        this.imglogo.ImageUrl = masterSettings.DistributorLogoPic.Split(new char[] { '|' })[0];
                    }
                }
                this.litStoreNum.Text = DistributorsBrower.GetDistributorNum(DistributorGrade.All).ToString();
                this.refrraltotal.Money = DistributorsBrower.GetUserCommissions(userIdDistributors.UserId, DateTime.Now);
                if (userIdDistributors.ReferralStatus == 1)
                {
                    this.litOrders.Text = "style=\"display:none;\"";
                }

                this.litRefId.Text = "?ReferralId=" + userIdDistributors.UserId;

            }
            else
            {
                // 进入开店流程页面

                XTrace.WriteLine("进入开店流程，当前登录会员是：" + currentMember.UserId);
                if (null != currentMember)
                {
                    //if (masterSettings.SiteFlag.EqualIgnoreCase("ls"))
                    //{
                    //if (currentMember.IsStore > 0)
                    //{
                    //this.Page.Response.Redirect("DistributorValid.aspx", true);                            
                    //}
                    // 不能开店则提示
                    //base.GotoResourceNotFound("您还不是店主，请联系官方客服。");
                    //}
                    //else
                    //{
                    if (DistributorsBrower.IsExistDistributor())
                    {
                        // 不能开店则提示
                        MemberInfo twoMember = MemberProcessor.GetCurrentMember();
                        if (null == twoMember)
                        {
                            base.GotoResourceNotFound("您还不是店主，请联系官方客服。");
                        }
                        DistributorsInfo twoDist = DistributorsBrower.GetUserIdDistributors(twoMember.UserId);
                        if (null == twoDist)
                        {
                            base.GotoResourceNotFound("您还不是店主，请联系官方客服。");
                        }
                    }
                    else
                    {
                        this.Page.Response.Redirect("DistributorValid.aspx", true);
                    }
                    //}
                }
                else
                {
                    MemberInfo twoMember = MemberProcessor.GetCurrentMember();
                    if (null == twoMember)
                    {
                        base.GotoResourceNotFound("您还不是店主，请联系官方客服。");
                    }
                    DistributorsInfo twoDist = DistributorsBrower.GetUserIdDistributors(twoMember.UserId);
                    if (null == twoDist)
                    {
                        base.GotoResourceNotFound("您还不是店主，请联系官方客服。");
                    }                    
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-DistributorCenter.html";
            }
            base.OnInit(e);
        }
    }
}

