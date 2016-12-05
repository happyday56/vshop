using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities;
using Hidistro.Entities.Comments;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Members;
using Hidistro.Entities.Orders;
using Hidistro.Entities.Promotions;
using Hidistro.Entities.Sales;
using Hidistro.Entities.VShop;
using Hidistro.Messages;
using Hidistro.SaleSystem.Vshop;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using NewLife.Log;
using Hidistro.ControlPanel.Members;
using Hidistro.ControlPanel.Sales;
using Hidistro.ControlPanel.Store;

namespace Hidistro.UI.Web.API
{
    public class VshopProcess : System.Web.IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(System.Web.HttpContext context)
        {
            string text = context.Request["action"];
            switch (text)
            {
                case "AddToCartBySkus":
                    this.ProcessAddToCartBySkus(context);
                    break;
                case "GetSkuByOptions":
                    this.ProcessGetSkuByOptions(context);
                    break;
                case "DeleteCartProduct":
                    this.ProcessDeleteCartProduct(context);
                    break;
                case "ChageQuantity":
                    this.ProcessChageQuantity(context);
                    break;
                case "Submmitorder"://提交订单
                    this.ProcessSubmmitorder(context);
                    break;
                case "SubmitMemberCard":
                    this.ProcessSubmitMemberCard(context);
                    break;
                case "AddShippingAddress":
                    this.AddShippingAddress(context);
                    break;
                case "DelShippingAddress":
                    this.DelShippingAddress(context);
                    break;
                case "SetDefaultShippingAddress":
                    this.SetDefaultShippingAddress(context);
                    break;
                case "UpdateShippingAddress":
                    this.UpdateShippingAddress(context);
                    break;
                case "GetPrize":
                    this.GetPrize(context);
                    break;
                case "Vote":
                    this.Vote(context);
                    break;
                case "SubmitActivity":
                    this.SubmitActivity(context);
                    break;
                case "AddSignUp":
                    this.AddSignUp(context);
                    break;
                case "AddTicket":
                    this.AddTicket(context);
                    break;
                case "FinishOrder":
                    this.FinishOrder(context);
                    break;
                case "AddUserPrize":
                    this.AddUserPrize(context);
                    break;
                case "SubmitWinnerInfo":
                    this.SubmitWinnerInfo(context);
                    break;
                case "SetUserName":
                    this.SetUserName(context);
                    break;
                case "AddProductConsultations":
                    this.AddProductConsultations(context);
                    break;
                case "AddProductReview":
                    this.AddProductReview(context);
                    break;
                case "AddFavorite":
                    this.AddFavorite(context);
                    break;
                case "AddGoodCounts":
                    this.AddGoodCounts(context);
                    break;
                case "AddProductGoodCounts":
                    this.AddProductGoodCounts(context);
                    break;
                case "DelFavorite":
                    this.DelFavorite(context);
                    break;
                case "CheckFavorite":
                    this.CheckFavorite(context);
                    break;
                case "Logistic":
                    this.SearchExpressData(context);
                    break;
                case "GetShippingTypes":
                    this.GetShippingTypes(context);
                    break;
                case "UserLogin":
                    this.UserLogin(context);
                    break;
                case "RegisterUser":
                    this.RegisterUser(context);
                    break;
                case "AddDistributor"://申请开店
                    this.AddDistributor(context);
                    break;
                case "SetDistributorMsg":
                    this.SetDistributorMsg(context);
                    break;
                case "DeleteProducts":
                    this.DeleteDistributorProducts(context);
                    break;
                case "AddDistributorProducts":
                    this.AddDistributorProducts(context);
                    break;
                case "UpdateDistributor":
                    this.UpdateDistributor(context);
                    break;
                case "AddCommissions":
                    this.AddCommissions(context);
                    break;
                case "SendRegiestVerifyCode":
                    this.SendRegiestVerifyCode(context);
                    break;
                //创建邀请
                case "AddInviteCode":
                    this.AddInviteCode(context);
                    break;
                //分享成功
                case "ShareInviteSuccess":
                    this.ShareInviteSuccess(context);
                    break;
                //用户接受邀请后注册
                case "RegInviteOpen":
                    this.RegInviteOpen(context);
                    break;
                case "ApplyInvite":
                    this.ApplyInviteCode(context);
                    break;
                case "CloseOrder":
                    this.CloseOrder(context);
                    break;
                // 订单付款完成后
                case "SubmitCalCommission":
                    this.SubmitCalCommission(context);
                    break;
                case "AttendActivity":
                    this.AttendActivity(context);
                    break;
                case "RequestReturn":
                    this.RequestReturn(context);
                    break;
                case "SetPrintBatch":
                    this.SetPrintBatch(context);
                    break;
            }
        }

        private void SetPrintBatch(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string batchNo = "";

            if (!string.IsNullOrEmpty(context.Request["orderIds"]))
            {
                string orderId = context.Request["orderIds"].ToString();

                if (!string.IsNullOrWhiteSpace(orderId) && orderId.EndsWith(","))
                {
                    orderId = orderId.Substring(0, orderId.Length - 1);
                }

                XTrace.WriteLine("设置打印批次的订单ID：" + orderId);

                string[] orderIds = orderId.Split(new char[]
                {
                    ','
                });

                long printBatch = OrderHelper.GetMaxPrintBatch();
                DateTime printBatchDate = DateTime.Now;

                foreach (string str in orderIds)
                {
                    OrderHelper.UpdateSetPrintBatch(str, printBatch, printBatchDate, true);
                }

                batchNo = printBatch.ToString();
            }

            context.Response.Write("{\"success\":\"true\",\"msg\":\"" + batchNo + "\"}");

            context.Response.End();
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        private void RequestReturn(HttpContext context)
        {
            context.Response.ContentType = "text/json";

            string status = "OK";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (null != currentMember)
            {

                //退款账户
                string account = context.Request["Account"];
                //退款金额
                decimal money = 0;
                decimal.TryParse(context.Request["Money"], out money);
                int prodid = 0;
                int.TryParse(context.Request["productid"], out prodid);
                //退款原因
                string reason = context.Request["Reason"];
                //订单ID
                string orderid = context.Request["orderid"];

                /**
                * 1.APP页面申请退款，判断是否已经申请
                * 2.若没有申请，则创建退货记录，并修改订单状态至申请退货(已修改订单列表页面将列表中的处理退货退款申请已取消)
                * 3.后台在退货申请单页面中处理
                * 4.处理通过后进入退款申请页面
                * 5.处理通过则完成退款
                 * 
                 * 
                * 6.退佣金的地方不清楚在哪里还没处理！！！！！！！！！！！！！
                *
                 * 
                */

                //检测账户是否为空
                if (string.IsNullOrWhiteSpace(account))
                {
                    status = "Mesg";
                }
                else
                {
                    //验证订单合法信息

                    var order = OrderHelper.GetOrderInfo(orderid);
                    if (order.UserId != currentMember.UserId)
                    {
                        status = "Faild";
                    }
                    else
                    {
                        //检测是否重复提交
                        var refund = Hidistro.ControlPanel.Sales.RefundHelper.GetRefundInfoByOrderId(order.OrderId);
                        if (refund != null && refund.RefundId > 0)
                        {
                            status = "Repeat";
                        }
                        else
                        {
                            //创建退款申请
                            RefundInfo refundInfo = new RefundInfo()
                            {
                                OrderId = order.OrderId,
                                ApplyForTime = DateTime.Now,
                                RefundType = 1,
                                RefundMoney = money,
                                RefundRemark = reason,
                                HandleStatus = RefundInfo.Handlestatus.NoneAudit,
                                HandleTime = DateTime.Now,
                                Account = account,
                                ProductId = prodid,
                                UserId = currentMember.UserId,
                                //  RefundTime = DateTime.Now
                            };

                            var refund2 = Hidistro.ControlPanel.Sales.RefundHelper.GetRefundInfoByOrderId(order.OrderId);
                            if (null != refund2 && refund2.RefundId > 0)
                            {
                                status = "Repeat";
                            }
                            else
                            {
                                //创建退货申请
                                if (Hidistro.ControlPanel.Sales.RefundHelper.AddRefund(refundInfo))
                                {
                                    //修改订单状态至申请退货
                                    Hidistro.ControlPanel.Sales.OrderHelper.SetOrderState(order.OrderId, OrderStatus.ApplyForReturns);

                                    status = "OK";
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                status = "Mesg";
            }

            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { Status = status }));

            context.Response.End();
        }

        /// <summary>
        /// 创建邀请码
        /// </summary>
        /// <param name="context"></param>
        private void AddInviteCode(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            bool ret = false;
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (null != currentMember)
            {
                int prod = 0;
                Int32.TryParse(context.Request["ProductId"] ?? "", out prod);

                InviteCode model = new InviteCode()
                {
                    UserId = currentMember.UserId,
                    Code = "",
                    InviteStatus = 0,
                    ProductId = prod
                };
                string code = string.Empty;
                ret = InviteBrowser.AddInviteCode(model, out code);
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = ret, msg = code }));
            }
            else
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = ret, msg = "请重新登陆!" }));
            }

            context.Response.End();
        }

        /// <summary>
        /// 分享邀请码成功后修改邀请码状态
        /// </summary>
        /// <param name="context"></param>
        private void ShareInviteSuccess(HttpContext context)
        {
            string invitecode = context.Request["InviteCode"] ?? "";

            if (!string.IsNullOrEmpty(invitecode.Trim()))
            {
                InviteCode code = InviteBrowser.GetInvoiteCode(invitecode);
                if (code != null)
                {
                    //更新邀请码状态至待激活(1)
                    code.InviteStatus = 1;
                    code.TimeStamp = DateTime.Now;
                    InviteBrowser.UpdateInviteStatus(code);

                    InviteBrowser.AddInviteRecord(new InviteRecord()
                    {
                        InviteId = code.InviteId,
                        InviteStatus = 1,
                        //分享出去，没有点击所以为空
                        OpenId = "",
                        UserId = 0,
                        TimeStamp = code.TimeStamp,
                    });
                }
            }
        }

        /// <summary>
        /// 根据邀请码注册
        /// </summary>
        /// <param name="context"></param>
        private void RegInviteOpen(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            bool ret = false;
            //验证需要是微信登陆
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (null != currentMember)
            {
                string realName = context.Request["realname"] ?? "";
                string mobile = context.Request["mobile"] ?? "";
                string InviteCode = context.Request["hdInviteCode"] ?? "";
                if (!string.IsNullOrEmpty(InviteCode.Trim()) && !string.IsNullOrEmpty(realName.Trim()) && !string.IsNullOrEmpty(mobile.Trim()))
                {
                    var code = InviteBrowser.GetInvoiteCode(InviteCode);
                    // 2015-11-27 注销时间限制
                    //判断邀请码是否超时过期
                    if (code.TimeStamp.AddMinutes(600) < DateTime.Now)
                    {
                        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = "邀请码已过期!" }));
                        context.Response.End();
                    }

                    //如果不是邀请用户则退出
                    if (code.InviteUserId != currentMember.UserId)
                    {
                        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = "请重新登陆!" }));
                    }
                    else
                    {

                        //更新用户状态
                        currentMember.RealName = realName;
                        currentMember.CellPhone = mobile;
                        currentMember.ReferralUserId = code.UserId;
                        ret = MemberProcessor.UpdateMember(currentMember);
                        if (ret)
                        {
                            //创建邀请码记录Cookie
                            HttpCookie httpCookie = new HttpCookie("InviteId")
                            {
                                Value = code.InviteId.ToString(),
                                Expires = DateTime.Now.AddYears(10)
                            };
                            HttpContext.Current.Response.Cookies.Add(httpCookie);

                            //更新邀请码状态
                            code.InviteStatus = 2;
                            code.TimeStamp = DateTime.Now;
                            ret = InviteBrowser.UpdateInviteStatus(code);

                            //创建邀请码使用记录
                            InviteBrowser.AddInviteRecord(new InviteRecord()
                            {
                                InviteId = code.InviteId,
                                InviteStatus = code.InviteStatus,
                                OpenId = currentMember.OpenId,
                                UserId = currentMember.UserId,
                                TimeStamp = code.TimeStamp,
                            });


                            //成功则跳转到购买产品页面
                            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = ret, msg = ret ? "/vshop/ProductDetails.aspx?ReferralUserId=" + code.UserId + "&InviteCode=" + code.Code + "&ProductId=" + code.ProductId : "" }));
                        }
                        else
                        {
                            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = "用户更新失败请重试!" }));
                        }
                    }
                }
                else
                {
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = "参数错误!" }));
                }
            }
            else
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = "请重新登陆!" }));
            }


            context.Response.End();
        }

        /// <summary>
        /// 申请增加邀请码限额
        /// </summary>
        /// <param name="context"></param>
        private void ApplyInviteCode(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            bool ret = false;
            //验证需要是微信登陆
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (null != currentMember)
            {
                SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);
                //检测是否有申请记录，如果有待审核的申请记录则不予申请
                if (!InviteBrowser.CheckIsInviteApplyRecord(currentMember.UserId))
                {
                    ret = InviteBrowser.AddInviteApplyRecord(new InviteApply()
                     {
                         UserId = currentMember.UserId,
                         ApplyNum = siteSettings.DefaultMinInvitationNum
                     });

                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = ret, msg = "" }));
                }
                else
                {
                    context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = ret, msg = "已存在待审核的申请记录！" }));
                }
            }
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void SendRegiestVerifyCode(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "text/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();

            if (null != currentMember)
            {
                DistributorsInfo currentDistributor = DistributorsBrower.GetDistributorInfo(currentMember.UserId);
                if (null != currentDistributor)
                {
                    //暂用phone为key来获取
                    string phone = httpContext.Request["phone"];
                    if (currentMember.CellPhone == phone)
                    {
                        //生成4位数字
                        string code = InviteBrowser.getRandomizer(4, true, false, false, false);

                        var ret = MemberProcessor.UpdateVerifyCode(currentMember.UserId.ToString(), phone, code);
                        httpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = ret, msg = "" }));
                    }
                    else
                    {
                        //与账户原有手机号不一致
                        httpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = "账户手机号码不一致！" }));
                    }
                }
                else
                {
                    httpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = "您还没有成为店主，请联系官方客服！" }));
                }
            }

            httpContext.Response.End();
        }

        private void AddCommissions(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "text/json";
            string str = "";
            if (DistributorsBrower.IsExitsCommionsRequest())
            {
                str = "{\"success\":false,\"msg\":\"您的申请正在审核中！\"}";
            }
            else if (this.CheckAddCommissions(httpContext, ref str))
            {
                string str1 = httpContext.Request["account"].Trim();
                string bankname = httpContext.Request["bankname"].Trim();
                string bankaddress = httpContext.Request["bankaddress"].Trim();
                string accountname = httpContext.Request["accountname"].Trim();
                decimal num = decimal.Parse(httpContext.Request["commissionmoney"].Trim());
                string regionaddress = httpContext.Request["regionaddress"].Trim();
                int num1 = 0;
                int.TryParse(httpContext.Request["requesttype"].Trim(), out num1);

                string newRegionAddress = "";

                if (!string.IsNullOrEmpty(regionaddress))
                {
                    //XTrace.WriteLine("提现开户行地址1：" + regionaddress);
                    newRegionAddress = RegionHelper.GetFullRegion(int.Parse(regionaddress), "，");
                    //XTrace.WriteLine("提现开户行地址2：" + newRegionAddress);
                    newRegionAddress = newRegionAddress.Replace("省", "").Replace("市", "");
                    //XTrace.WriteLine("提现开户行地址3：" + newRegionAddress);
                    string[] regionArray = newRegionAddress.Split("，");
                    if (regionArray.Length == 3)
                    {
                        newRegionAddress = regionArray[0] + " " + regionArray[1];
                    }
                    else if (regionArray.Length == 2)
                    {
                        newRegionAddress = regionArray[0] + " " + regionArray[1];
                    }
                    //XTrace.WriteLine("提现开户行地址4：" + newRegionAddress);
                }
                //XTrace.WriteLine("提现开户行地址5：" + newRegionAddress);

                BalanceDrawRequestInfo balanceDrawRequestInfo = new BalanceDrawRequestInfo()
                {
                    MerchanCade = str1,
                    Amount = num,
                    AccountName = accountname,
                    BankName = bankname,
                    BankAddress = bankaddress,
                    RequesType = num1,
                    RegionAddress = newRegionAddress
                };

                str = (!DistributorsBrower.AddBalanceDrawRequest(balanceDrawRequestInfo, regionaddress) ? "{\"success\":false,\"msg\":\"真实姓名或手机号未填写！\"}" : "{\"success\":true,\"msg\":\"申请成功！\"}");
            }
            httpContext.Response.Write(str);
            httpContext.Response.End();
        }

        //private void AddCommissions(System.Web.HttpContext context)
        //{
        //    context.Response.ContentType = "text/json";
        //    string msg = "";
        //    if (this.CheckAddCommissions(context, ref msg))
        //    {
        //        string str2 = context.Request["account"].Trim();
        //        decimal num = decimal.Parse(context.Request["commissionmoney"].Trim());
        //        BalanceDrawRequestInfo balancerequestinfo = new BalanceDrawRequestInfo
        //        {
        //            MerchanCade = str2,
        //            Amount = num
        //        };
        //        if (DistributorsBrower.AddBalanceDrawRequest(balancerequestinfo))
        //        {
        //            msg = "{\"success\":true,\"msg\":\"申请成功！\"}";
        //        }
        //        else
        //        {
        //            msg = "{\"success\":false,\"msg\":\"真实姓名或手机号未填写！\"}";
        //        }
        //    }
        //    context.Response.Write(msg);
        //    context.Response.End();
        //}
        //public void AddDistributor(System.Web.HttpContext context)
        //{
        //    context.Response.ContentType = "text/plain";
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    if (this.CheckRequestDistributors(context, sb))
        //    {
        //        DistributorsInfo distributors = new DistributorsInfo
        //        {
        //            RequestAccount = context.Request["acctount"].Trim(),
        //            StoreName = context.Request["stroename"].Trim(),
        //            StoreDescription = context.Request["descriptions"].Trim(),
        //            BackImage = context.Request["backimg"].Trim()
        //        };
        //        System.Web.HttpPostedFile file = context.Request.Files["logo"];
        //        distributors.Logo = "";
        //        if (file != null && !string.IsNullOrEmpty(file.FileName))
        //        {
        //            distributors.Logo = this.UploadFileImages(context, file);
        //        }
        //        if (DistributorsBrower.AddDistributors(distributors))
        //        {
        //            if (System.Web.HttpContext.Current.Request.Cookies["Vshop-Member"] != null)
        //            {
        //                string name = "Vshop-ReferralId";
        //                System.Web.HttpContext.Current.Response.Cookies[name].Expires = System.DateTime.Now.AddDays(-1.0);
        //                System.Web.HttpCookie cookie = new System.Web.HttpCookie(name)
        //                {
        //                    Value = Globals.GetCurrentMemberUserId().ToString(),
        //                    Expires = System.DateTime.Now.AddYears(10)
        //                };
        //                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        //            }
        //            context.Response.Write("OK");
        //            context.Response.End();
        //        }
        //        else
        //        {
        //            context.Response.Write("添加失败");
        //            context.Response.End();
        //        }
        //    }
        //    else
        //    {
        //        context.Response.Write(sb.ToString() ?? "");
        //        context.Response.End();
        //    }
        //}

        //   private bool CheckRequestDistributors(System.Web.HttpContext context, System.Text.StringBuilder sb)
        //{
        //    bool result;
        //    if (string.IsNullOrEmpty(context.Request["stroename"]))
        //    {
        //        sb.AppendFormat("请输入店铺名称", new object[0]);
        //        result = false;
        //    }
        //    else
        //    {
        //        if (context.Request["stroename"].Length > 20)
        //        {
        //            sb.AppendFormat("请输入店铺名称字符不多于20个字符", new object[0]);
        //            result = false;
        //        }
        //        else
        //        {
        //            if (string.IsNullOrEmpty(context.Request["acctount"]))
        //            {
        //                sb.AppendFormat("请输入提现账号", new object[0]);
        //                result = false;
        //            }
        //            else
        //            {
        //                if (string.IsNullOrEmpty(context.Request["backimg"]))
        //                {
        //                    sb.AppendFormat("请选择店铺背景", new object[0]);
        //                    result = false;
        //                }
        //                else
        //                {
        //                    if (!string.IsNullOrEmpty(context.Request["descriptions"]) && context.Request["descriptions"].Trim().Length > 30)
        //                    {
        //                        sb.AppendFormat("店铺描述字不能多于30个字", new object[0]);
        //                        result = false;
        //                    }
        //                    else
        //                    {
        //                        result = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}

        private bool CheckRequestDistributors(HttpContext httpContext, StringBuilder stringBuilder)
        {

            if (string.IsNullOrEmpty(httpContext.Request["stroename"]))
            {
                stringBuilder.AppendFormat("请输入店铺名称", new object[0]);
                return false;
            }

            if (httpContext.Request["stroename"].Length > 20)
            {
                stringBuilder.AppendFormat("请输入店铺名称字符不多于20个字符", new object[0]);
                return false;
            }

            if (string.IsNullOrEmpty(httpContext.Request["descriptions"]) || httpContext.Request["descriptions"].Trim().Length <= 30)
            {
                return true;
            }

            stringBuilder.AppendFormat("店铺描述字不能多于30个字", new object[0]);

            return false;

        }

        public void AddDistributor(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            StringBuilder stringBuilder = new StringBuilder();
            if (!this.CheckRequestDistributors(context, stringBuilder))
            {
                context.Response.Write(string.Concat("{\"success\":false,\"msg\":\"", stringBuilder.ToString(), "\"}"));
                return;
            }

            SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);

            DistributorsInfo distributorsInfo = new DistributorsInfo()
            {
                RequestAccount = context.Request["acctount"].Trim(),
                StoreName = context.Request["stroename"].Trim(),
                StoreDescription = context.Request["descriptions"].Trim(),
                BackImage = context.Request["BackImage"].Trim(),
                Logo = context.Request["logo"].Trim(),
                UserName = context.Request["realname"].Trim(),
                RealName = context.Request["realname"].Trim(),
                CellPhone = context.Request["phone"].Trim(),
                DistriGradeId = DistributorGradeBrower.GetIsDefaultDistributorGradeInfo().GradeId,
                InvitationNum = siteSettings.DefaultMinInvitationNum
            };
            if (!DistributorsBrower.AddDistributors(distributorsInfo))
            {
                context.Response.Write("{\"success\":false,\"msg\":\"店铺名称已存在，请重新输入店铺名称\"}");
                return;
            }
            if (HttpContext.Current.Request.Cookies["Vshop-Member"] != null)
            {
                string str = "Vshop-ReferralId";
                HttpContext.Current.Response.Cookies[str].Expires = DateTime.Now.AddDays(-1);
                HttpCookie httpCookie = new HttpCookie(str)
                {
                    Value = Globals.GetCurrentMemberUserId().ToString(),
                    Expires = DateTime.Now.AddYears(10)
                };
                HttpContext.Current.Response.Cookies.Add(httpCookie);
            }
            context.Response.Write("{\"success\":true}");
        }

        //分销商下加商品
        private void DeleteDistributorProducts(System.Web.HttpContext context)
        {

            if (!string.IsNullOrEmpty(context.Request["Params"]))
            {
                string json = context.Request["Params"];

                JObject source = JObject.Parse(json);

                if (source.Count > 0)
                {
                    List<int> productIds = (from s in source.Values() select (int)s).ToList<int>();
                    DistributorsBrower.DeleteDistributorProductIds(productIds);
                    // DistributorsBrower.DeleteDistributorProductIds((
                    //    from s in source.Values()
                    //   select System.Convert.ToInt32(s)).ToList<int>());
                }

            }

            context.Response.Write("{\"success\":\"true\",\"msg\":\"保存成功\"}");

            context.Response.End();

        }

        private void SubmitCalCommission(System.Web.HttpContext context)
        {
            context.Response.ContentType = "text/json";
            if (!string.IsNullOrEmpty(context.Request["Params"]))
            {
                string orderId = context.Request["Params"].ToString();
                XTrace.WriteLine("计算佣金时的订单ID：" + orderId);
                OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(orderId);
                if (null != orderInfo)
                {
                    // 订单付款完成后计算提成
                    //DistributorsBrower.CalcCommissionByBuy(orderInfo);
                    XTrace.WriteLine("开始计算订单佣金SubmitCalCommission------订单ID：" + orderId);
                    DistributorsBrower.UpdateCalculationCommissionNew(orderInfo);
                }

            }

            context.Response.Write("{\"success\":\"true\",\"msg\":\"保存成功\"}");

            context.Response.End();

        }

        //分销商上架商品
        private void AddDistributorProducts(System.Web.HttpContext context)
        {
            //无法将类型为“Newtonsoft.Json.Linq.JValue”的对象强制转换为类型“System.IConvertible”。

            if (!string.IsNullOrEmpty(context.Request["Params"]))
            {

                string json = context.Request["Params"];

                JObject source = JObject.Parse(json);

                if (source.Count > 0)
                {

                    List<int> productIds = (from s in source.Values() select (int)s).ToList<int>();

                    DistributorsBrower.AddDistributorProductId(productIds);

                }

            }

            context.Response.Write("{\"success\":\"true\",\"msg\":\"保存成功\"}");
            context.Response.End();
        }
        private void AddFavorite(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember == null)
            {
                context.Response.Write("{\"success\":false, \"msg\":\"请先登录才可以收藏商品\"}");
            }
            else
            {
                if (ProductBrowser.AddProductToFavorite(System.Convert.ToInt32(context.Request["ProductId"]), currentMember.UserId))
                {
                    context.Response.Write("{\"success\":true}");
                }
                else
                {
                    context.Response.Write("{\"success\":false, \"msg\":\"提交失败\"}");
                }
            }
        }

        private void AddGoodCounts(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";

            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            int userId = 0;
            if (!string.IsNullOrEmpty(context.Request["ReferralId"]))
            {
                userId = System.Convert.ToInt32(context.Request["ReferralId"]);
                if (userId > 0)
                {
                    DistributorsBrower.UpdateGoodCounts(userId);
                }
                else
                {
                    masterSettings.MainSiteGoodCounts = masterSettings.MainSiteGoodCounts + 1;
                    SettingsManager.Save(masterSettings);
                }
            }
            else
            {
                masterSettings.MainSiteGoodCounts = masterSettings.MainSiteGoodCounts + 1;
                SettingsManager.Save(masterSettings);
            }
            context.Response.Write("{\"success\":true}");

        }

        private void AddProductGoodCounts(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";

            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            int productId = 0;
            if (!string.IsNullOrEmpty(context.Request["ProductId"]))
            {
                productId = System.Convert.ToInt32(context.Request["ProductId"]);
                if (productId > 0)
                {
                    DistributorsBrower.UpdateProductGoodCounts(productId);
                }
            }
            context.Response.Write("{\"success\":true}");

        }

        private void AddProductConsultations(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            ProductConsultationInfo productConsultation = new ProductConsultationInfo
            {
                ConsultationDate = System.DateTime.Now,
                ConsultationText = context.Request["ConsultationText"],
                ProductId = System.Convert.ToInt32(context.Request["ProductId"]),
                UserEmail = currentMember.Email,
                UserId = currentMember.UserId,
                UserName = currentMember.UserName
            };
            if (ProductBrowser.InsertProductConsultation(productConsultation))
            {
                context.Response.Write("{\"success\":true}");
            }
            else
            {
                context.Response.Write("{\"success\":false, \"msg\":\"提交失败\"}");
            }
        }
        private void AddProductReview(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            int productId = System.Convert.ToInt32(context.Request["ProductId"]);
            int num2;
            int num3;
            ProductBrowser.LoadProductReview(productId, currentMember.UserId, out num2, out num3);
            if (num2 == 0)
            {
                context.Response.Write("{\"success\":false, \"msg\":\"您没有购买此商品(或此商品的订单尚未完成)，因此不能进行评论\"}");
            }
            else
            {
                if (num3 >= num2)
                {
                    context.Response.Write("{\"success\":false, \"msg\":\"您已经对此商品进行过评论(或此商品的订单尚未完成)，因此不能再次进行评论\"}");
                }
                else
                {
                    ProductReviewInfo review = new ProductReviewInfo
                    {
                        ReviewDate = System.DateTime.Now,
                        ReviewText = context.Request["ReviewText"],
                        ProductId = productId,
                        UserEmail = currentMember.Email,
                        UserId = currentMember.UserId,
                        UserName = currentMember.UserName
                    };
                    if (ProductBrowser.InsertProductReview(review))
                    {
                        context.Response.Write("{\"success\":true}");
                    }
                    else
                    {
                        context.Response.Write("{\"success\":false, \"msg\":\"提交失败\"}");
                    }
                }
            }
        }
        private void AddShippingAddress(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember == null)
            {
                context.Response.Write("{\"success\":false}");
            }
            else
            {
                ShippingAddressInfo shippingAddress = new ShippingAddressInfo
                {
                    Address = context.Request.Form["address"],
                    CellPhone = context.Request.Form["cellphone"],
                    ShipTo = context.Request.Form["shipTo"],
                    Zipcode = "12345",
                    IsDefault = true,
                    UserId = currentMember.UserId,
                    RegionId = System.Convert.ToInt32(context.Request.Form["regionSelectorValue"])
                };
                if (MemberProcessor.AddShippingAddress(shippingAddress) > 0)
                {
                    context.Response.Write("{\"success\":true}");
                }
                else
                {
                    context.Response.Write("{\"success\":false}");
                }
            }
        }
        private void AddSignUp(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            int activityid = System.Convert.ToInt32(context.Request["id"]);
            string str = System.Convert.ToString(context.Request["code"]);
            LotteryTicketInfo lotteryTicket = VshopBrowser.GetLotteryTicket(activityid);
            if (!string.IsNullOrEmpty(lotteryTicket.InvitationCode) && lotteryTicket.InvitationCode != str)
            {
                context.Response.Write("{\"success\":false, \"msg\":\"邀请码不正确\"}");
            }
            else
            {
                if (lotteryTicket.EndTime < System.DateTime.Now)
                {
                    context.Response.Write("{\"success\":false, \"msg\":\"活动已结束\"}");
                }
                else
                {
                    if (lotteryTicket.OpenTime < System.DateTime.Now)
                    {
                        context.Response.Write("{\"success\":false, \"msg\":\"报名已结束\"}");
                    }
                    else
                    {
                        if (VshopBrowser.GetUserPrizeRecord(activityid) == null)
                        {
                            PrizeRecordInfo model = new PrizeRecordInfo
                            {
                                ActivityID = activityid
                            };
                            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
                            model.UserID = currentMember.UserId;
                            model.UserName = currentMember.UserName;
                            model.IsPrize = true;
                            model.Prizelevel = "已报名";
                            model.PrizeTime = new System.DateTime?(System.DateTime.Now);
                            VshopBrowser.AddPrizeRecord(model);
                            context.Response.Write("{\"success\":true, \"msg\":\"报名成功\"}");
                        }
                        else
                        {
                            context.Response.Write("{\"success\":false, \"msg\":\"你已经报名了，请不要重复报名！\"}");
                        }
                    }
                }
            }
        }
        private void AddTicket(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            int activityid = System.Convert.ToInt32(context.Request["activityid"]);
            LotteryTicketInfo lotteryTicket = VshopBrowser.GetLotteryTicket(activityid);
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember != null && !lotteryTicket.GradeIds.Contains(currentMember.GradeId.ToString()))
            {
                context.Response.Write("{\"success\":false, \"msg\":\"您的会员等级不在此活动范内\"}");
            }
            else
            {
                if (lotteryTicket.EndTime < System.DateTime.Now)
                {
                    context.Response.Write("{\"success\":false, \"msg\":\"活动已结束\"}");
                }
                else
                {
                    if (System.DateTime.Now < lotteryTicket.OpenTime)
                    {
                        context.Response.Write("{\"success\":false, \"msg\":\"抽奖还未开始\"}");
                    }
                    else
                    {
                        if (VshopBrowser.GetCountBySignUp(activityid) < lotteryTicket.MinValue)
                        {
                            context.Response.Write("{\"success\":false, \"msg\":\"还未达到人数下限\"}");
                        }
                        else
                        {
                            PrizeRecordInfo userPrizeRecord = VshopBrowser.GetUserPrizeRecord(activityid);
                            try
                            {
                                if (!lotteryTicket.IsOpened)
                                {
                                    VshopBrowser.OpenTicket(activityid);
                                    userPrizeRecord = VshopBrowser.GetUserPrizeRecord(activityid);
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(userPrizeRecord.RealName) && !string.IsNullOrWhiteSpace(userPrizeRecord.CellPhone))
                                    {
                                        context.Response.Write("{\"success\":false, \"msg\":\"您已经抽过奖了\"}");
                                        return;
                                    }
                                }
                                if (userPrizeRecord == null || string.IsNullOrEmpty(userPrizeRecord.PrizeName))
                                {
                                    context.Response.Write("{\"success\":false, \"msg\":\"很可惜,你未中奖\"}");
                                    return;
                                }
                                if (!userPrizeRecord.PrizeTime.HasValue)
                                {
                                    userPrizeRecord.PrizeTime = new System.DateTime?(System.DateTime.Now);
                                    VshopBrowser.UpdatePrizeRecord(userPrizeRecord);
                                }
                            }
                            catch (System.Exception exception)
                            {
                                context.Response.Write("{\"success\":false, \"msg\":\"" + exception.Message + "\"}");
                                return;
                            }
                            context.Response.Write("{\"success\":true, \"msg\":\"恭喜你获得" + userPrizeRecord.Prizelevel + "\"}");
                        }
                    }
                }
            }
        }
        private void AddUserPrize(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            int result = 1;
            int.TryParse(context.Request["activityid"], out result);
            string str = context.Request["prize"];
            LotteryActivityInfo lotteryActivity = VshopBrowser.GetLotteryActivity(result);
            PrizeRecordInfo model = new PrizeRecordInfo
            {
                PrizeTime = new System.DateTime?(System.DateTime.Now),
                UserID = Globals.GetCurrentMemberUserId(),
                ActivityName = lotteryActivity.ActivityName,
                ActivityID = result,
                Prizelevel = str
            };
            string text = str;
            switch (text)
            {
                case "一等奖":
                    model.PrizeName = lotteryActivity.PrizeSettingList[0].PrizeName;
                    model.IsPrize = true;
                    goto IL_216;
                case "二等奖":
                    model.PrizeName = (model.PrizeName = lotteryActivity.PrizeSettingList[1].PrizeName);
                    model.IsPrize = true;
                    goto IL_216;
                case "三等奖":
                    model.PrizeName = lotteryActivity.PrizeSettingList[2].PrizeName;
                    model.IsPrize = true;
                    goto IL_216;
                case "四等奖":
                    model.PrizeName = lotteryActivity.PrizeSettingList[3].PrizeName;
                    model.IsPrize = true;
                    goto IL_216;
                case "五等奖":
                    model.PrizeName = lotteryActivity.PrizeSettingList[4].PrizeName;
                    model.IsPrize = true;
                    goto IL_216;
                case "六等奖":
                    model.PrizeName = lotteryActivity.PrizeSettingList[5].PrizeName;
                    model.IsPrize = true;
                    goto IL_216;
            }
            model.IsPrize = false;
        IL_216:
            VshopBrowser.AddPrizeRecord(model);
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("{");
            builder.Append("\"Status\":\"OK\"");
            builder.Append("}");
            context.Response.Write(builder);
        }

        private bool CheckAddCommissions(HttpContext httpContext, ref string strPointers)
        {
            int num = 0;
            int.TryParse(httpContext.Request["requesttype"].Trim(), out num);
            if (num != 0)
            {
                num = 1;
            }
            if (num == 1 && string.IsNullOrEmpty(httpContext.Request["account"].Trim()))
            {
                strPointers = "{\"success\":false,\"msg\":\"银行账号不允许为空！\"}";
                return false;
            }
            if (num == 1 && string.IsNullOrEmpty(httpContext.Request["bankname"].Trim()))
            {
                strPointers = "{\"success\":false,\"msg\":\"提现银行不允许为空！\"}";
                return false;
            }
            if (num == 1 && string.IsNullOrEmpty(httpContext.Request["bankaddress"].Trim()))
            {
                strPointers = "{\"success\":false,\"msg\":\"开户行不允许为空！\"}";
                return false;
            }
            if (num == 1 && string.IsNullOrEmpty(httpContext.Request["accountname"].Trim()))
            {
                strPointers = "{\"success\":false,\"msg\":\"持卡人姓名不允许为空！\"}";
                return false;
            }
            if (num == 1 && string.IsNullOrEmpty(httpContext.Request["regionaddress"].Trim()))
            {
                strPointers = "{\"success\":false,\"msg\":\"开户行所在地不允许为空！\"}";
                return false;
            }

            if (string.IsNullOrEmpty(httpContext.Request["commissionmoney"].Trim()))
            {
                strPointers = "{\"success\":false,\"msg\":\"提现金额不允许为空！\"}";
                return false;
            }
            if (decimal.Parse(httpContext.Request["commissionmoney"].Trim()) <= new decimal(0))
            {
                strPointers = "{\"success\":false,\"msg\":\"提现金额必须大于0的纯数字！\"}";
                return false;
            }
            if (!(new System.Text.RegularExpressions.Regex("^[0-9]*[1-9][0-9]*$").IsMatch(httpContext.Request["commissionmoney"].Trim())))
            {
                strPointers = "{\"success\":false,\"msg\":\"请输入正整数！\"}";
                return false;
            }
            decimal num1 = new decimal(0);
            decimal.TryParse(SettingsManager.GetMasterSettings(false).MentionNowMoney, out num1);
            if (num1 > new decimal(0) && decimal.Parse(httpContext.Request["commissionmoney"].Trim()) < new decimal(0))
            {
                strPointers = string.Concat("{\"success\":false,\"msg\":\"提现金额必须大于等于", num1.ToString(), "元！\"}");
                return false;
            }
            DistributorsInfo currentDistributors = DistributorsBrower.GetCurrentDistributors();
            if (decimal.Parse(httpContext.Request["commissionmoney"].Trim()) <= currentDistributors.ReferralBlance)
            {
                return true;
            }
            strPointers = "{\"success\":false,\"msg\":\"提现金额必须为小于现有佣金余额！\"}";
            return false;
        }

        //private bool CheckAddCommissions(System.Web.HttpContext context, ref string msg)
        //{
        //    bool result2;
        //    if (string.IsNullOrEmpty(context.Request["account"].Trim()))
        //    {
        //        msg = "{\"success\":false,\"msg\":\"支付宝账号不允许为空！\"}";
        //        result2 = false;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(context.Request["commissionmoney"].Trim()))
        //        {
        //            msg = "{\"success\":false,\"msg\":\"提现金额不允许为空！\"}";
        //            result2 = false;
        //        }
        //        else
        //        {
        //            if (decimal.Parse(context.Request["commissionmoney"].Trim()) <= 0m)
        //            {
        //                msg = "{\"success\":false,\"msg\":\"提现金额必须大于0的纯数字！\"}";
        //                result2 = false;
        //            }
        //            else
        //            {
        //                decimal result = 0m;
        //                decimal.TryParse(SettingsManager.GetMasterSettings(false).MentionNowMoney, out result);
        //                if (result > 0m && decimal.Parse(context.Request["commissionmoney"].Trim()) % result != 0m)
        //                {
        //                    msg = "{\"success\":false,\"msg\":\"提现金额必须为" + result.ToString() + "的倍数！\"}";
        //                    result2 = false;
        //                }
        //                else
        //                {
        //                    DistributorsInfo currentDistributors = DistributorsBrower.GetCurrentDistributors();
        //                    if (decimal.Parse(context.Request["commissionmoney"].Trim()) > currentDistributors.ReferralBlance)
        //                    {
        //                        msg = "{\"success\":false,\"msg\":\"提现金额必须为小于现有佣金余额！\"}";
        //                        result2 = false;
        //                    }
        //                    else
        //                    {
        //                        result2 = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return result2;
        //}
        private void CheckFavorite(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember == null)
            {
                context.Response.Write("{\"success\":false}");
            }
            else
            {
                if (ProductBrowser.ExistsProduct(System.Convert.ToInt32(context.Request["ProductId"]), currentMember.UserId))
                {
                    context.Response.Write("{\"success\":true}");
                }
                else
                {
                    context.Response.Write("{\"success\":false}");
                }
            }
        }

        private bool CheckUpdateDistributors(HttpContext httpContext, StringBuilder stringBuilder)
        {

            if (string.IsNullOrEmpty(httpContext.Request["stroename"]))
            {
                stringBuilder.Append("请输入店铺名称");
                return false;
            }

            if (httpContext.Request["stroename"].Length > 20)
            {
                stringBuilder.Append("请输入店铺名称字符不多于20个字符");
                return false;
            }

            if (string.IsNullOrEmpty(httpContext.Request["descriptions"]) || httpContext.Request["descriptions"].Trim().Length <= 30)
            {
                return true;
            }

            stringBuilder.Append("店铺描述字不能多于30个字");

            return false;

        }

        //private bool CheckUpdateDistributors(System.Web.HttpContext context, System.Text.StringBuilder sb)
        //{
        //    bool result;
        //    if (string.IsNullOrEmpty(context.Request["VDistributorInfo$txtstorename"]))
        //    {
        //        sb.AppendFormat("请输入店铺名称", new object[0]);
        //        result = false;
        //    }
        //    else
        //    {
        //        if (context.Request["VDistributorInfo$txtstorename"].Length > 20)
        //        {
        //            sb.AppendFormat("请输入店铺名称字符不多于20个字符", new object[0]);
        //            result = false;
        //        }
        //        else
        //        {
        //            if (string.IsNullOrEmpty(context.Request["VDistributorInfo$hdbackimg"]))
        //            {
        //                sb.AppendFormat("请选择店铺背景", new object[0]);
        //                result = false;
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrEmpty(context.Request["VDistributorInfo$txtdescription"]) && context.Request["VDistributorInfo$txtdescription"].Trim().Length > 30)
        //                {
        //                    sb.AppendFormat("店铺描述字不能多于30个字", new object[0]);
        //                    result = false;
        //                }
        //                else
        //                {
        //                    result = true;
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}

        private void DelFavorite(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            if (ProductBrowser.DeleteFavorite(System.Convert.ToInt32(context.Request["favoriteId"])) == 1)
            {
                context.Response.Write("{\"success\":true}");
            }
            else
            {
                context.Response.Write("{\"success\":false, \"msg\":\"取消失败\"}");
            }
        }
        private void DelShippingAddress(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember == null)
            {
                context.Response.Write("{\"success\":false}");
            }
            else
            {
                int userId = currentMember.UserId;
                if (MemberProcessor.DelShippingAddress(System.Convert.ToInt32(context.Request.Form["shippingid"]), userId))
                {
                    context.Response.Write("{\"success\":true}");
                }
                else
                {
                    context.Response.Write("{\"success\":false}");
                }
            }
        }

        private void FinishOrder(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            bool flag = false;
            string str = Convert.ToString(httpContext.Request["orderId"]);
            OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(str);
            Dictionary<string, LineItemInfo> lineItems = orderInfo.LineItems;
            LineItemInfo lineItemInfo = new LineItemInfo();
            foreach (KeyValuePair<string, LineItemInfo> lineItem in lineItems)
            {
                lineItemInfo = lineItem.Value;
                if (lineItemInfo.OrderItemsStatus != OrderStatus.ApplyForRefund && lineItemInfo.OrderItemsStatus != OrderStatus.ApplyForReturns)
                {
                    continue;
                }
                flag = true;
            }
            if (flag)
            {
                httpContext.Response.Write("{\"success\":false, \"msg\":\"订单中商品有退货(款)不允许完成\"}");
                return;
            }
            if (orderInfo == null || !MemberProcessor.ConfirmOrderFinish(orderInfo))
            {
                httpContext.Response.Write("{\"success\":false, \"msg\":\"订单当前状态不允许完成\"}");
                return;
            }
            // 订单完成时不再计算佣金，只做订单相关数据状态的更新
            //DistributorsBrower.UpdateCalculationCommission(orderInfo);
            // 更新订单佣金的结算状态（1：表示已确认收货结算）
            DistributorsBrower.UpdateCommissionOrderStatus(orderInfo.OrderId, 1);

            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
            int num = 0;
            // 注销满足开店的请求处理
            //if (masterSettings.IsRequestDistributor && !string.IsNullOrEmpty(masterSettings.FinishedOrderMoney.ToString()) && currentMember.Expenditure >= masterSettings.FinishedOrderMoney)
            //{
            //    num = 1;
            //}
            foreach (LineItemInfo value in orderInfo.LineItems.Values)
            {
                if (value.OrderItemsStatus.ToString() != OrderStatus.SellerAlreadySent.ToString())
                {
                    continue;
                }
                ShoppingProcessor.UpdateOrderGoodStatu(orderInfo.OrderId, value.SkuId, 5);
            }
            DistributorsInfo distributorsInfo = new DistributorsInfo();
            distributorsInfo = DistributorsBrower.GetUserIdDistributors(orderInfo.UserId);
            if (distributorsInfo != null && distributorsInfo.UserId > 0)
            {
                num = 0;
            }

            // 判断是否有购买默认的分销商购买商品，有，则用户可以申请开店
            if (DistributorsBrower.CheckIsDistributoBuyProduct(orderInfo.OrderId))
            {
                bool isStoreFlag = MemberProcessor.UpdateUserIsStore(orderInfo.UserId, 1);
            }

            httpContext.Response.Write(string.Concat("{\"success\":true,\"isapply\":", num, "}"));
        }

        private void CloseOrder(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            bool flag = false;
            string str = Convert.ToString(httpContext.Request["orderId"]);
            OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(str);
            Dictionary<string, LineItemInfo> lineItems = orderInfo.LineItems;
            LineItemInfo lineItemInfo = new LineItemInfo();

            if (orderInfo == null || !MemberProcessor.ConfirmCloseOrder(orderInfo))
            {
                httpContext.Response.Write("{\"success\":false, \"msg\":\"订单当前状态不允许完成\"}");
                return;
            }

            if (orderInfo.RedPagerID == 1)
            {
                MemberProcessor.AddUserVirtualPoints(orderInfo.UserId, orderInfo.RedPagerAmount);
            }

            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
            int num = 0;

            foreach (LineItemInfo value in orderInfo.LineItems.Values)
            {
                ShoppingProcessor.UpdateOrderGoodStatu(orderInfo.OrderId, value.SkuId, 4);
            }

            httpContext.Response.Write(string.Concat("{\"success\":true,\"isapply\":", num, "}"));
        }

        //private void FinishOrder(System.Web.HttpContext context)
        //{
        //    context.Response.ContentType = "application/json";
        //    OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(System.Convert.ToString(context.Request["orderId"]));
        //    if (orderInfo != null && MemberProcessor.ConfirmOrderFinish(orderInfo))
        //    {
        //        DistributorsBrower.UpdateCalculationCommission(orderInfo);
        //        SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
        //        //MemberProcessor.RemoveUserCache(orderInfo.UserId);
        //        MemberInfo currentMember = MemberProcessor.GetCurrentMember();
        //        DistributorsInfo userIdDistributors = new DistributorsInfo();
        //        userIdDistributors = DistributorsBrower.GetUserIdDistributors(orderInfo.UserId);
        //        int num = 0;
        //        if (masterSettings.IsRequestDistributor && (userIdDistributors == null || userIdDistributors.UserId == 0) && !string.IsNullOrEmpty(masterSettings.FinishedOrderMoney.ToString()) && currentMember.Expenditure >= masterSettings.FinishedOrderMoney)
        //        {
        //            num = 1;
        //        }
        //        context.Response.Write("{\"success\":true,\"isapply\":" + num + "}");
        //    }
        //    else
        //    {
        //        context.Response.Write("{\"success\":false, \"msg\":\"订单当前状态不允许完成\"}");
        //    }
        //}
        private string GenerateOrderId()
        {
            string str = string.Empty;
            System.Random random = new System.Random();
            for (int i = 0; i < 7; i++)
            {
                int num = random.Next();
                str += ((char)(48 + (ushort)(num % 10))).ToString();
            }
            return System.DateTime.Now.ToString("yyyyMMdd") + str;
        }
        private void GetPrize(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            int result = 1;
            int.TryParse(context.Request["activityid"], out result);
            LotteryActivityInfo lotteryActivity = VshopBrowser.GetLotteryActivity(result);
            int userPrizeCount = VshopBrowser.GetUserPrizeCount(result);
            if (MemberProcessor.GetCurrentMember() == null)
            {
                MemberInfo member = new MemberInfo();
                string generateId = Globals.GetGenerateId();
                member.GradeId = MemberProcessor.GetDefaultMemberGrade();
                member.UserName = "";
                member.OpenId = "";
                member.CreateDate = System.DateTime.Now;
                member.SessionId = generateId;
                member.SessionEndTime = System.DateTime.Now;
                MemberProcessor.CreateMember(member);
                member = MemberProcessor.GetMember(generateId);
                System.Web.HttpCookie cookie = new System.Web.HttpCookie("Vshop-Member")
                {
                    Value = member.UserId.ToString(),
                    Expires = System.DateTime.Now.AddYears(10)
                };
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("{");
            if (userPrizeCount >= lotteryActivity.MaxNum)
            {
                builder.Append("\"No\":\"-1\"");
                builder.Append("}");
                context.Response.Write(builder.ToString());
            }
            else
            {
                if (System.DateTime.Now < lotteryActivity.StartTime || System.DateTime.Now > lotteryActivity.EndTime)
                {
                    builder.Append("\"No\":\"-3\"");
                    builder.Append("}");
                    context.Response.Write(builder.ToString());
                }
                else
                {
                    PrizeQuery page = new PrizeQuery
                    {
                        ActivityId = result
                    };
                    System.Collections.Generic.List<PrizeRecordInfo> prizeList = VshopBrowser.GetPrizeList(page);
                    int num3 = 0;
                    int num4 = 0;
                    int num5 = 0;
                    if (prizeList != null && prizeList.Count > 0)
                    {
                        num3 = prizeList.Count((PrizeRecordInfo a) => a.Prizelevel == "一等奖");
                        num4 = prizeList.Count((PrizeRecordInfo a) => a.Prizelevel == "二等奖");
                        num5 = prizeList.Count((PrizeRecordInfo a) => a.Prizelevel == "三等奖");
                    }
                    PrizeRecordInfo model = new PrizeRecordInfo
                    {
                        PrizeTime = new System.DateTime?(System.DateTime.Now),
                        UserID = Globals.GetCurrentMemberUserId(),
                        ActivityName = lotteryActivity.ActivityName,
                        ActivityID = result,
                        IsPrize = true
                    };
                    System.Collections.Generic.List<PrizeSetting> prizeSettingList = lotteryActivity.PrizeSettingList;
                    decimal num6 = prizeSettingList[0].Probability * 100m;
                    decimal num7 = prizeSettingList[1].Probability * 100m;
                    decimal num8 = prizeSettingList[2].Probability * 100m;
                    int num9 = new System.Random(System.Guid.NewGuid().GetHashCode()).Next(1, 10001);
                    if (prizeSettingList.Count > 3)
                    {
                        decimal num10 = prizeSettingList[3].Probability * 100m;
                        decimal num11 = prizeSettingList[4].Probability * 100m;
                        decimal num12 = prizeSettingList[5].Probability * 100m;
                        int num13 = prizeList.Count((PrizeRecordInfo a) => a.Prizelevel == "四等奖");
                        int num14 = prizeList.Count((PrizeRecordInfo a) => a.Prizelevel == "五等奖");
                        int num15 = prizeList.Count((PrizeRecordInfo a) => a.Prizelevel == "六等奖");
                        if (num9 < num6 && prizeSettingList[0].PrizeNum > num3)
                        {
                            builder.Append("\"No\":\"9\"");
                            model.Prizelevel = "一等奖";
                            model.PrizeName = prizeSettingList[0].PrizeName;
                        }
                        else
                        {
                            if (num9 < num7 && prizeSettingList[1].PrizeNum > num4)
                            {
                                builder.Append("\"No\":\"11\"");
                                model.Prizelevel = "二等奖";
                                model.PrizeName = prizeSettingList[1].PrizeName;
                            }
                            else
                            {
                                if (num9 < num8 && prizeSettingList[2].PrizeNum > num5)
                                {
                                    builder.Append("\"No\":\"1\"");
                                    model.Prizelevel = "三等奖";
                                    model.PrizeName = prizeSettingList[2].PrizeName;
                                }
                                else
                                {
                                    if (num9 < num10 && prizeSettingList[3].PrizeNum > num13)
                                    {
                                        builder.Append("\"No\":\"3\"");
                                        model.Prizelevel = "四等奖";
                                        model.PrizeName = prizeSettingList[3].PrizeName;
                                    }
                                    else
                                    {
                                        if (num9 < num11 && prizeSettingList[4].PrizeNum > num14)
                                        {
                                            builder.Append("\"No\":\"5\"");
                                            model.Prizelevel = "五等奖";
                                            model.PrizeName = prizeSettingList[4].PrizeName;
                                        }
                                        else
                                        {
                                            if (num9 < num12 && prizeSettingList[5].PrizeNum > num15)
                                            {
                                                builder.Append("\"No\":\"7\"");
                                                model.Prizelevel = "六等奖";
                                                model.PrizeName = prizeSettingList[5].PrizeName;
                                            }
                                            else
                                            {
                                                model.IsPrize = false;
                                                builder.Append("\"No\":\"0\"");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (num9 < num6 && prizeSettingList[0].PrizeNum > num3)
                        {
                            builder.Append("\"No\":\"9\"");
                            model.Prizelevel = "一等奖";
                            model.PrizeName = prizeSettingList[0].PrizeName;
                        }
                        else
                        {
                            if (num9 < num7 && prizeSettingList[1].PrizeNum > num4)
                            {
                                builder.Append("\"No\":\"11\"");
                                model.Prizelevel = "二等奖";
                                model.PrizeName = prizeSettingList[1].PrizeName;
                            }
                            else
                            {
                                if (num9 < num8 && prizeSettingList[2].PrizeNum > num5)
                                {
                                    builder.Append("\"No\":\"1\"");
                                    model.Prizelevel = "三等奖";
                                    model.PrizeName = prizeSettingList[2].PrizeName;
                                }
                                else
                                {
                                    model.IsPrize = false;
                                    builder.Append("\"No\":\"0\"");
                                }
                            }
                        }
                    }
                    builder.Append("}");
                    if (context.Request["activitytype"] != "scratch")
                    {
                        VshopBrowser.AddPrizeRecord(model);
                    }
                    context.Response.Write(builder.ToString());
                }
            }
        }
        public void GetShippingTypes(System.Web.HttpContext context)
        {
            int regionId = System.Convert.ToInt32(context.Request["regionId"]);
            int groupbuyId = (!string.IsNullOrWhiteSpace(context.Request["groupBuyId"])) ? System.Convert.ToInt32(context.Request["groupBuyId"]) : 0;
            int num2;
            ShoppingCartInfo shoppingCart;
            if (int.TryParse(context.Request["buyAmount"], out num2) && !string.IsNullOrWhiteSpace(context.Request["productSku"]))
            {
                string productSkuId = System.Convert.ToString(context.Request["productSku"]);
                if (groupbuyId > 0)
                {
                    shoppingCart = ShoppingCartProcessor.GetGroupBuyShoppingCart(groupbuyId, productSkuId, num2);
                }
                else
                {
                    shoppingCart = ShoppingCartProcessor.GetShoppingCart(productSkuId, num2);
                }
            }
            else
            {
                shoppingCart = ShoppingCartProcessor.GetShoppingCart();
            }
            System.Collections.Generic.IEnumerable<int> source =
                from item in ShoppingProcessor.GetShippingModes()
                select item.ModeId;
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            if (source != null && source.Count<int>() > 0)
            {
                foreach (int num3 in source)
                {
                    ShippingModeInfo shippingMode = ShoppingProcessor.GetShippingMode(num3, true);
                    decimal num4 = 0m;
                    if (shoppingCart.LineItems.Count != shoppingCart.LineItems.Count((ShoppingCartItemInfo a) => a.IsfreeShipping))
                    {
                        num4 = ShoppingProcessor.CalcFreight(regionId, shoppingCart.Weight, shippingMode);
                    }
                    builder.Append(string.Concat(new string[]
					{
						",{\"modelId\":\"",
						shippingMode.ModeId.ToString(),
						"\",\"text\":\"",
						shippingMode.Name,
						"： ￥",
						num4.ToString("F2"),
						"\",\"freight\":\"",
						num4.ToString("F2"),
						"\"}"
					}));
                }
                if (builder.Length > 0)
                {
                    builder.Remove(0, 1);
                }
            }
            builder.Insert(0, "{\"data\":[").Append("]}");
            context.Response.ContentType = "application/json";
            context.Response.Write(builder.ToString());
        }
        private void ProcessAddToCartBySkus(System.Web.HttpContext context)
        {
            if (MemberProcessor.GetCurrentMember() == null)
            {
                context.Response.Write("{\"Status\":-1}");
            }
            else
            {
                context.Response.ContentType = "application/json";
                XTrace.WriteLine("SKUID:--" + context.Request["productSkuId"]);
                XTrace.WriteLine("quantity:--" + context.Request["quantity"]);
                XTrace.WriteLine("productId:--" + context.Request["productId"]);
                XTrace.WriteLine("categoryid:--" + context.Request["categoryid"]);


                int quantity = int.Parse(context.Request["quantity"], System.Globalization.NumberStyles.None);
                string skuId = string.IsNullOrEmpty(context.Request["productSkuId"]) ? "" : context.Request["productSkuId"];
                int productId = int.Parse(context.Request["productId"], System.Globalization.NumberStyles.None);
                int categoryId = int.Parse(context.Request["categoryid"], NumberStyles.None);
                
                int skuStock = ShoppingCartProcessor.GetSkuStock(skuId);

                int skuIdIsBuyNum = ProductBrowser.CheckProductSkuIsBuy(productId, MemberProcessor.GetCurrentMember().UserId, skuId);

                if (skuIdIsBuyNum > 0)
                {
                    context.Response.Write(string.Concat(new string[]
				    {
					    "{\"Status\":\"5\",\"rnt\":\"",
					    skuStock.ToString(),
					    "\"}"
				    }));
                }
                else
                {
                    if (quantity > skuStock)
                    {
                        context.Response.Write(string.Concat(new string[]
				        {
					        "{\"Status\":\"3\",\"rnt\":\"",
					        skuStock.ToString(),
					        "\"}"
				        }));

                    }
                    else
                    {
                        ShoppingCartProcessor.AddLineItem(skuId, quantity, categoryId);
                        ShoppingCartInfo shoppingCart = ShoppingCartProcessor.GetShoppingCart();
                        context.Response.Write(string.Concat(new string[]
				        {
					        "{\"Status\":\"OK\",\"TotalMoney\":\"",
					        shoppingCart.GetTotal().ToString(".00"),
					        "\",\"Quantity\":\"",
					        shoppingCart.GetQuantity().ToString(),
					        "\"}"
				        }));
                    }
                }
            }
        }
        private void ProcessChageQuantity(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string skuId = context.Request["skuId"];
            int result = 1;
            int.TryParse(context.Request["quantity"], out result);
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("{");
            int skuStock = ShoppingCartProcessor.GetSkuStock(skuId);
            if (result > skuStock)
            {
                builder.AppendFormat("\"Status\":\"{0}\"", skuStock);
                result = skuStock;
            }
            else
            {
                builder.Append("\"Status\":\"OK\",");
                ShoppingCartProcessor.UpdateLineItemQuantity(skuId, (result > 0) ? result : 1);
                builder.AppendFormat("\"TotalPrice\":\"{0}\"", ShoppingCartProcessor.GetShoppingCart().GetAmount());
            }
            builder.Append("}");
            context.Response.ContentType = "application/json";
            context.Response.Write(builder.ToString());
        }
        private void ProcessDeleteCartProduct(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string skuId = context.Request["skuId"];
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            ShoppingCartProcessor.RemoveLineItem(skuId);
            builder.Append("{");
            builder.Append("\"Status\":\"OK\"");
            builder.Append("}");
            context.Response.ContentType = "application/json";
            context.Response.Write(builder.ToString());
        }
        private void ProcessGetSkuByOptions(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            int productId = int.Parse(context.Request["productId"], System.Globalization.NumberStyles.None);
            string str = context.Request["options"];
            if (string.IsNullOrEmpty(str))
            {
                context.Response.Write("{\"Status\":\"0\"}");
            }
            else
            {
                if (str.EndsWith(","))
                {
                    str = str.Substring(0, str.Length - 1);
                }
                SKUItem item = ShoppingProcessor.GetProductAndSku(MemberProcessor.GetCurrentMember(), productId, str);
                if (item == null)
                {
                    context.Response.Write("{\"Status\":\"1\"}");
                }
                else
                {
                    System.Text.StringBuilder builder = new System.Text.StringBuilder();
                    builder.Append("{");
                    builder.Append("\"Status\":\"OK\",");
                    builder.AppendFormat("\"SkuId\":\"{0}\",", item.SkuId);
                    builder.AppendFormat("\"SKU\":\"{0}\",", item.SKU);
                    builder.AppendFormat("\"Weight\":\"{0}\",", item.Weight);
                    builder.AppendFormat("\"Stock\":\"{0}\",", item.Stock);
                    builder.AppendFormat("\"SalePrice\":\"{0}\"", item.SalePrice.ToString("F2"));
                    builder.Append("}");
                    context.Response.ContentType = "application/json";
                    context.Response.Write(builder.ToString());
                }
            }
        }

        private void ProcessSubmitMemberCard(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember == null)
            {
                context.Response.Write("{\"success\":false}");
            }
            else
            {
                currentMember.Address = context.Request.Form.Get("address");
                currentMember.RealName = context.Request.Form.Get("name");
                currentMember.CellPhone = context.Request.Form.Get("phone");
                currentMember.QQ = context.Request.Form.Get("qq");
                if (!string.IsNullOrEmpty(currentMember.QQ))
                {
                    currentMember.Email = currentMember.QQ + "@qq.com";
                }
                currentMember.VipCardNumber = SettingsManager.GetMasterSettings(true).VipCardPrefix + currentMember.UserId.ToString();
                currentMember.VipCardDate = new System.DateTime?(System.DateTime.Now);
                string s = MemberProcessor.UpdateMember(currentMember) ? "{\"success\":true}" : "{\"success\":false}";
                context.Response.Write(s);
            }
        }
        private void ProcessSubmmitorder(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("{");
            int result = 0;
            int num2 = 0;
            string str = context.Request["couponCode"];
            decimal points = decimal.Parse(context.Request["points"] ?? "0");
            int shippingId = int.Parse(context.Request["shippingId"]);
            int num3;
            bool flag = int.TryParse(context.Request["groupbuyId"], out num3);
            string str2 = context.Request["remark"];
            int num4;
            string idCard = context.Request["idcard"];
            XTrace.WriteLine("IdCard：" + idCard);

            SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);

            ShoppingCartInfo shoppingCart;
            if (int.TryParse(context.Request["buyAmount"], out num4) && !string.IsNullOrEmpty(context.Request["productSku"]) && !string.IsNullOrEmpty(context.Request["from"]) && (context.Request["from"] == "signBuy" || context.Request["from"] == "groupBuy"))
            {
                string productSkuId = context.Request["productSku"];
                if (context.Request["from"] == "signBuy")
                {
                    shoppingCart = ShoppingCartProcessor.GetShoppingCart(productSkuId, num4);
                }
                else
                {
                    shoppingCart = ShoppingCartProcessor.GetGroupBuyShoppingCart(num3, productSkuId, num4);
                }
            }
            else
            {
                shoppingCart = ShoppingCartProcessor.GetShoppingCart();
            }

            bool isCrossProduct = DistributorsBrower.CheckProductIsCrossByProductId(DistributorsBrower.GetProductId(shoppingCart.LineItems));
            if (isCrossProduct && string.IsNullOrEmpty(idCard))
            {
                builder.Append("\"Status\":\"Error\"");
                builder.AppendFormat(",\"ErrorMsg\":\"{0}\"", "请输入跟收货人姓名一致的身份证号码!");
            }
            else
            {
                OrderInfo orderInfo = ShoppingProcessor.ConvertShoppingCartToOrder(shoppingCart, false, false);
                if (orderInfo == null)
                {
                    builder.Append("\"Status\":\"None\"");
                }
                else
                {
                    orderInfo.OrderId = this.GenerateOrderId();
                    orderInfo.OrderDate = System.DateTime.Now;
                    MemberInfo currentMember = MemberProcessor.GetCurrentMember();
                    orderInfo.UserId = currentMember.UserId;
                    orderInfo.Username = currentMember.UserName;
                    orderInfo.EmailAddress = currentMember.Email;
                    orderInfo.RealName = currentMember.RealName;
                    orderInfo.QQ = currentMember.QQ;
                    orderInfo.Remark = str2;
                    if (flag)
                    {
                        GroupBuyInfo groupBuy = GroupBuyBrowser.GetGroupBuy(num3);
                        orderInfo.GroupBuyId = num3;
                        orderInfo.NeedPrice = groupBuy.NeedPrice;
                        orderInfo.GroupBuyStatus = groupBuy.Status;
                    }
                    orderInfo.OrderStatus = OrderStatus.WaitBuyerPay;
                    orderInfo.RefundStatus = RefundStatus.None;
                    orderInfo.ShipToDate = context.Request["shiptoDate"];
                    if (System.Web.HttpContext.Current.Request.Cookies["Vshop-ReferralId"] != null)
                    {
                        int rUserId = int.Parse(System.Web.HttpContext.Current.Request.Cookies.Get("Vshop-ReferralId").Value);
                        XTrace.WriteLine("1 订单推荐人ID(" + orderInfo.OrderId + ")：" + rUserId);
                        if (rUserId > 0)
                        {
                            MemberInfo tmpMember = MemberHelper.GetMember(rUserId);
                            if (null != tmpMember && tmpMember.UserId > 0)
                            {

                                //if (tmpMember.UserId == orderInfo.UserId)
                                //{
                                //    orderInfo.ReferralUserId = 0;
                                //}
                                //else
                                //{
                                orderInfo.ReferralUserId = tmpMember.UserId;
                                //}
                                XTrace.WriteLine("2 订单推荐人ID(" + orderInfo.OrderId + ")：" + orderInfo.ReferralUserId);

                            }
                            else
                            {
                                orderInfo.ReferralUserId = MemberHelper.GetMember(orderInfo.UserId).ReferralUserId;
                                XTrace.WriteLine("3 订单推荐人ID(" + orderInfo.OrderId + ")：" + orderInfo.ReferralUserId);
                            }
                        }
                        else
                        {

                            orderInfo.ReferralUserId = MemberHelper.GetMember(orderInfo.UserId).ReferralUserId;
                            XTrace.WriteLine("4 订单推荐人ID(" + orderInfo.OrderId + ")：" + orderInfo.ReferralUserId);
                        }
                        //orderInfo.ReferralUserId = int.Parse(System.Web.HttpContext.Current.Request.Cookies.Get("Vshop-ReferralId").Value);
                        //if (orderInfo.ReferralUserId == 0)
                        //    orderInfo.ReferralUserId = MemberHelper.GetMember(orderInfo.UserId).ReferralUserId;
                    }
                    else
                    {
                        orderInfo.ReferralUserId = MemberHelper.GetMember(orderInfo.UserId).ReferralUserId;
                        XTrace.WriteLine("5 订单推荐人ID(" + orderInfo.OrderId + ")：" + orderInfo.ReferralUserId);
                    }
                    ShippingAddressInfo shippingAddress = MemberProcessor.GetShippingAddress(shippingId);
                    if (shippingAddress != null)
                    {
                        orderInfo.ShippingRegion = RegionHelper.GetFullRegion(shippingAddress.RegionId, "，");
                        orderInfo.RegionId = shippingAddress.RegionId;
                        orderInfo.Address = shippingAddress.Address;
                        orderInfo.ZipCode = shippingAddress.Zipcode;
                        orderInfo.ShipTo = shippingAddress.ShipTo;
                        orderInfo.TelPhone = shippingAddress.TelPhone;
                        orderInfo.CellPhone = shippingAddress.CellPhone;
                        MemberProcessor.SetDefaultShippingAddress(shippingId, MemberProcessor.GetCurrentMember().UserId);
                    }
                    if (int.TryParse(context.Request["shippingType"], out result))
                    {
                        ShippingModeInfo shippingMode = ShoppingProcessor.GetShippingMode(result, true);
                        if (shippingMode != null)
                        {
                            orderInfo.ShippingModeId = shippingMode.ModeId;
                            orderInfo.ModeName = shippingMode.Name;
                            if (shoppingCart.LineItems.Count != shoppingCart.LineItems.Count((ShoppingCartItemInfo a) => a.IsfreeShipping))
                            {
                                orderInfo.AdjustedFreight = (orderInfo.Freight = ShoppingProcessor.CalcFreight(orderInfo.RegionId, shoppingCart.Weight, shippingMode));
                            }
                            else
                            {
                                orderInfo.AdjustedFreight = (orderInfo.Freight = 0m);
                            }
                        }
                    }
                    if (int.TryParse(context.Request["paymentType"], out num2))
                    {
                        orderInfo.PaymentTypeId = num2;
                        int num5 = num2;
                        switch (num5)
                        {
                            case -1:
                            case 0:
                                orderInfo.PaymentType = "货到付款";
                                orderInfo.Gateway = "hishop.plugins.payment.podrequest";
                                break;
                            default:
                                if (num5 != 88)
                                {
                                    if (num5 != 99)
                                    {
                                        PaymentModeInfo paymentMode = ShoppingProcessor.GetPaymentMode(num2);
                                        if (paymentMode != null)
                                        {
                                            orderInfo.PaymentTypeId = paymentMode.ModeId;
                                            orderInfo.PaymentType = paymentMode.Name;
                                            orderInfo.Gateway = paymentMode.Gateway;
                                        }
                                    }
                                    else
                                    {
                                        orderInfo.PaymentType = "线下付款";
                                        orderInfo.Gateway = "hishop.plugins.payment.offlinerequest";
                                    }
                                }
                                else
                                {
                                    orderInfo.PaymentType = "微信支付";
                                    orderInfo.Gateway = "hishop.plugins.payment.weixinrequest";
                                }
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(str))//优惠券
                    {
                        CouponInfo info8 = ShoppingProcessor.UseCoupon(shoppingCart.GetTotal(), str);
                        orderInfo.CouponName = info8.Name;
                        if (info8.Amount.HasValue)
                        {
                            orderInfo.CouponAmount = info8.Amount.Value;
                        }
                        orderInfo.CouponCode = str;
                        orderInfo.CouponValue = info8.DiscountValue;
                    }

                    if (points > 0)//金贝数
                    {
                        orderInfo.RedPagerID = 1;
                        orderInfo.RedPagerAmount = points;
                        orderInfo.RedPagerActivityName = siteSettings.VirtualPointName;
                    }


                    // 添加身份证号码
                    orderInfo.IdCard = idCard;

                    bool isDefaultProduct = DistributorsBrower.CheckCartIsDistributoBuyProduct(DistributorsBrower.GetProductId(shoppingCart.LineItems));
                    if (!isDefaultProduct)
                    {
                        // 普通订单
                        orderInfo.OrderType = 1;

                        DataTable type = ProductBrowser.GetType();//满减活动
                        if (type.Rows.Count > 0)
                        {
                            var allFull = ProductBrowser.GetAllFullBySubmitOrder((int)type.Rows[0]["ActivitiesType"]).AsEnumerable().Select(n => new
                            {
                                ActivitiesId = n.Field<int>("ActivitiesId").ToString(),
                                ActivitiesName = n.Field<string>("ActivitiesName"),
                                MeetMoney = n.Field<decimal>("MeetMoney"),
                                ReductionMoney = n.Field<decimal>("ReductionMoney")
                            }).Where(n => n.MeetMoney <= orderInfo.GetTotal()).FirstOrDefault();
                            if (allFull != null)
                            {
                                orderInfo.ActivitiesId = allFull.ActivitiesId;//活动名称
                                orderInfo.ActivitiesName = allFull.ActivitiesName;//活动Id
                                orderInfo.DiscountAmount = allFull.ReductionMoney; ;//折扣金额

                                //XTrace.WriteLine("优惠减免－－－－活动名称：" + orderInfo.ActivitiesName + "(" + orderInfo.ActivitiesId + ")   折扣金额：" + orderInfo.DiscountAmount);
                            }
                        }

                        // 满减活动处理2015-11-19
                        decimal distAmount = 0;
                        distAmount = ShoppingCartProcessor.DiscountMoney(shoppingCart.LineItems);
                        orderInfo.DiscountAmount = distAmount;
                        XTrace.WriteLine("优惠减免－－－－活动名称：" + orderInfo.ActivitiesName + "(" + orderInfo.ActivitiesId + ")   折扣金额：" + orderInfo.DiscountAmount);

                    }
                    else
                    {
                        // 开店订单
                        orderInfo.OrderType = 2;
                    }

                    #region "虚拟币赠送处理"

                    // 2015-12-05 新增购买赠送虚拟币和首单赠送虚拟币

                    if (!isDefaultProduct)
                    {
                        bool isGiftNextProcess = true;
                        int currStoreGiftId = 0;
                        int currStoreGiftDetailId = 0;
                        decimal currStoreGiftMoney = 0;
                        int currMemberGiftId = 0;
                        int currMemberGiftDetailId = 0;
                        decimal currMemberGiftMoney = 0;

                        #region "首单赠送"

                        // 1.首单赠送虚拟币处理,判断店主是否是首单
                        bool isFirstOrder = DistributorsBrower.CheckIsFirstOrder(orderInfo.ReferralUserId.ToString());
                        if (isFirstOrder)
                        {
                            XTrace.WriteLine("1.1.首单赠送");
                            IList<VPGiftInfo> vpGiftList = VShopHelper.GetVPGiftInfoByType(1);
                            if (null != vpGiftList && vpGiftList.Count > 0)
                            {
                                foreach (VPGiftInfo vpGift in vpGiftList)
                                {
                                    if (!isGiftNextProcess)
                                    {
                                        break;
                                    }

                                    XTrace.WriteLine("1.2.首单赠送" + vpGift.VPGiftId);
                                    IList<VPGiftDetailInfo> vpGiftDetailList = VShopHelper.GetVPGiftDetailById(vpGift.VPGiftId);
                                    if (null != vpGiftDetailList && vpGiftDetailList.Count > 0)
                                    {
                                        XTrace.WriteLine("1.2.1.首单赠送" + vpGift.VPGiftId);
                                        foreach (VPGiftDetailInfo vpd in vpGiftDetailList)
                                        {
                                            XTrace.WriteLine("1.2.2.首单赠送" + orderInfo.GetTotal() + "------" + vpd.MeetMoney);
                                            if (orderInfo.GetTotal() >= vpd.MeetMoney)
                                            {
                                                XTrace.WriteLine("1.3.首单赠送");
                                                currStoreGiftId = vpGift.VPGiftId;
                                                currStoreGiftDetailId = vpd.VPGiftDetailId;

                                                if (vpGift.VPGiftCategory == 1)
                                                {
                                                    XTrace.WriteLine("1.4.首单赠送");
                                                    // 固定金额处理
                                                    currStoreGiftMoney = vpd.GiftMoney;
                                                    isGiftNextProcess = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    // 百分比处理
                                                    XTrace.WriteLine("1.5.首单赠送");
                                                    currStoreGiftMoney = orderInfo.GetTotal() * (vpd.GiftMoney / 100);
                                                    isGiftNextProcess = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            orderInfo.StoreVPGiftId = currStoreGiftId;
                            orderInfo.StoreVPGiftDetailId = currStoreGiftDetailId;
                            orderInfo.StoreGiftMoney = currStoreGiftMoney;
                        }

                        #endregion "首单赠送"

                        #region "购买赠送"

                        // 2.购买赠送虚拟币处理
                        isGiftNextProcess = true;

                        IList<VPGiftInfo> vpGiftBuyList = VShopHelper.GetVPGiftInfoByType(2);
                        if (null != vpGiftBuyList && vpGiftBuyList.Count > 0)
                        {
                            XTrace.WriteLine("2.1.购买赠送");
                            foreach (VPGiftInfo vpGift in vpGiftBuyList)
                            {
                                if (!isGiftNextProcess)
                                {
                                    break;
                                }

                                XTrace.WriteLine("2.2.购买赠送" + vpGift.VPGiftId);
                                IList<VPGiftDetailInfo> vpGiftDetailList = VShopHelper.GetVPGiftDetailById(vpGift.VPGiftId); ;
                                if (null != vpGiftDetailList && vpGiftDetailList.Count > 0)
                                {
                                    XTrace.WriteLine("2.2.1.购买赠送" + vpGift.VPGiftId);
                                    foreach (VPGiftDetailInfo vpd in vpGiftDetailList)
                                    {
                                        XTrace.WriteLine("2.2.2.购买赠送" + orderInfo.GetTotal() + "------" + vpd.MeetMoney);
                                        if (orderInfo.GetTotal() >= vpd.MeetMoney)
                                        {
                                            XTrace.WriteLine("2.3.购买赠送");
                                            currMemberGiftId = vpGift.VPGiftId;
                                            currMemberGiftDetailId = vpd.VPGiftDetailId;

                                            if (vpGift.VPGiftCategory == 1)
                                            {
                                                XTrace.WriteLine("2.4.购买赠送");
                                                // 固定金额处理
                                                currMemberGiftMoney = vpd.GiftMoney;
                                                isGiftNextProcess = false;
                                                break;
                                            }
                                            else
                                            {
                                                XTrace.WriteLine("2.5.购买赠送");
                                                // 百分比处理
                                                currMemberGiftMoney = orderInfo.GetTotal() * (vpd.GiftMoney / 100);
                                                isGiftNextProcess = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        orderInfo.MemberVPGiftId = currMemberGiftId;
                        orderInfo.MemberVPGiftDetailId = currMemberGiftDetailId;
                        orderInfo.MemberGiftMoney = currMemberGiftMoney;

                        #endregion "购买赠送"

                        XTrace.WriteLine("虚拟币赠送处理:" + orderInfo.MemberGiftMoney + "------" + orderInfo.StoreGiftMoney);

                    }
                    else
                    {
                        orderInfo.StoreVPGiftId = 0;
                        orderInfo.StoreVPGiftDetailId = 0;
                        orderInfo.StoreGiftMoney = 0;
                        orderInfo.MemberVPGiftId = 0;
                        orderInfo.MemberVPGiftDetailId = 0;
                        orderInfo.MemberGiftMoney = 0;
                    }

                    #endregion "虚拟币赠送处理"

                    try
                    {
                        // 2015-11-24 添加判断购买是否来自主站，如果是，则不允许创建订单
                        if (orderInfo.ReferralUserId <= 0)
                        {
                            // 不存在店铺主
                            builder.Append("\"Status\":\"Error\"");
                            builder.AppendFormat(",\"ErrorMsg\":\"{0}\"", "此商品暂时不能购买!");
                        }
                        else
                        {
                            // 判断是普通订单还是开店订单

                            if (ShoppingProcessor.CreatOrder(orderInfo))
                            {
                                // 2015-12-08 根据订单上的商品品牌来拆分子订单物流信息
                                DataTable subOrder = ShoppingProcessor.GetOrderBrand(orderInfo.OrderId);
                                if (null != subOrder && subOrder.Rows.Count > 0)
                                {
                                    string subOrderId = "";
                                    string brandId = "";
                                    foreach (DataRow dr in subOrder.Rows)
                                    {
                                        brandId = dr["BrandId"].ToString();
                                        subOrderId = orderInfo.OrderId + "-" + brandId;

                                        ShoppingProcessor.AddSubOrderData(orderInfo.OrderId, subOrderId, brandId);
                                    }
                                }

                                ShoppingCartProcessor.ClearShoppingCart();
                                // 订单提交时不发送微信和短信消息
                                //Messenger.OrderCreated(orderInfo, currentMember);
                                // 订单提交时不计算提成（账户余额，不算真正的佣金）
                                //DistributorsBrower.CalcCommissionByBuy(orderInfo);

                                builder.Append("\"Status\":\"OK\",");
                                builder.AppendFormat("\"OrderId\":\"{0}\"", orderInfo.OrderId);
                            }
                            else
                            {
                                builder.Append("\"Status\":\"Error\"");
                            }
                        }
                    }
                    catch (OrderException exception)
                    {
                        builder.Append("\"Status\":\"Error\"");
                        builder.AppendFormat(",\"ErrorMsg\":\"{0}\"", exception.Message);
                    }
                }
            }

            builder.Append("}");
            context.Response.ContentType = "application/json";
            context.Response.Write(builder.ToString());
        }
        public void RegisterUser(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string username = context.Request["userName"];
            string sourceData = context.Request["password"];
            string str3 = context.Request["passagain"];
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("{");
            if (sourceData == str3)
            {
                MemberInfo info = new MemberInfo();
                if (MemberProcessor.GetusernameMember(username) == null)
                {
                    MemberInfo member = new MemberInfo();
                    string generateId = Globals.GetGenerateId();
                    member.GradeId = MemberProcessor.GetDefaultMemberGrade();
                    member.UserName = username;
                    member.CreateDate = System.DateTime.Now;
                    member.SessionId = generateId;
                    member.SessionEndTime = System.DateTime.Now.AddYears(10);
                    member.Password = HiCryptographer.Md5Encrypt(sourceData);
                    MemberProcessor.CreateMember(member);
                    MemberInfo info2 = MemberProcessor.GetMember(generateId);
                    if (System.Web.HttpContext.Current.Request.Cookies["Vshop-Member"] != null)
                    {
                        System.Web.HttpContext.Current.Response.Cookies["Vshop-Member"].Expires = System.DateTime.Now.AddDays(-1.0);
                        System.Web.HttpCookie cookie = new System.Web.HttpCookie("Vshop-Member")
                        {
                            Value = info2.UserId.ToString(),
                            Expires = System.DateTime.Now.AddYears(10)
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        System.Web.HttpCookie cookie2 = new System.Web.HttpCookie("Vshop-Member")
                        {
                            Value = info2.UserId.ToString(),
                            Expires = System.DateTime.Now.AddYears(10)
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie2);
                    }
                    context.Session["userid"] = info2.UserId.ToString();
                    builder.Append("\"Status\":\"OK\"");
                }
                else
                {
                    builder.Append("\"Status\":\"-1\"");
                }
            }
            else
            {
                builder.Append("\"Status\":\"-2\"");
            }
            builder.Append("}");
            context.Response.Write(builder.ToString());
        }
        private void SearchExpressData(System.Web.HttpContext context)
        {
            string s = string.Empty;
            if (!string.IsNullOrEmpty(context.Request["OrderId"]))
            {
                string orderId = context.Request["OrderId"];
                OrderInfo orderInfo = ShoppingProcessor.GetOrderInfo(orderId);
                if (orderInfo != null && (orderInfo.OrderStatus == OrderStatus.SellerAlreadySent || orderInfo.OrderStatus == OrderStatus.Finished) && !string.IsNullOrEmpty(orderInfo.ExpressCompanyAbb))
                {
                    s = Express.GetExpressData(orderInfo.ExpressCompanyAbb, orderInfo.ShipOrderNumber, 0);
                }
            }
            context.Response.ContentType = "application/json";
            context.Response.Write(s);
            //context.Response.Close();
            context.Response.End();
        }
        private void SetDefaultShippingAddress(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember == null)
            {
                context.Response.Write("{\"success\":false}");
            }
            else
            {
                int userId = currentMember.UserId;
                if (MemberProcessor.SetDefaultShippingAddress(System.Convert.ToInt32(context.Request.Form["shippingid"]), userId))
                {
                    context.Response.Write("{\"success\":true}");
                }
                else
                {
                    context.Response.Write("{\"success\":false}");
                }
            }
        }
        public void SetDistributorMsg(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            currentMember.VipCardDate = new System.DateTime?(System.DateTime.Now);
            currentMember.CellPhone = context.Request["CellPhone"];
            currentMember.MicroSignal = context.Request["MicroSignal"];
            currentMember.RealName = context.Request["RealName"];
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("{");
            if (MemberProcessor.UpdateMember(currentMember))
            {
                builder.Append("\"Status\":\"OK\"");
            }
            else
            {
                builder.Append("\"Status\":\"Error\"");
            }
            builder.Append("}");
            context.Response.Write(builder.ToString());
        }
        public void SetUserName(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            currentMember.UserName = context.Request["userName"];
            currentMember.VipCardDate = new System.DateTime?(System.DateTime.Now);
            currentMember.CellPhone = context.Request["CellPhone"];
            currentMember.QQ = context.Request["QQ"];
            if (!string.IsNullOrEmpty(currentMember.QQ))
            {
                currentMember.Email = currentMember.QQ + "@qq.com";
            }
            currentMember.RealName = context.Request["RealName"];
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("{");
            if (MemberProcessor.UpdateMember(currentMember))
            {
                builder.Append("\"Status\":\"OK\"");
            }
            else
            {
                builder.Append("\"Status\":\"Error\"");
            }
            builder.Append("}");
            context.Response.Write(builder.ToString());
        }
        private void SubmitActivity(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember == null)
            {
                context.Response.Write("{\"success\":false}");
            }
            else
            {
                ActivityInfo activity = VshopBrowser.GetActivity(System.Convert.ToInt32(context.Request.Form.Get("id")));
                if (System.DateTime.Now < activity.StartDate || System.DateTime.Now > activity.EndDate)
                {
                    context.Response.Write("{\"success\":false, \"msg\":\"报名还未开始或已结束\"}");
                }
                else
                {
                    ActivitySignUpInfo info = new ActivitySignUpInfo
                    {
                        ActivityId = System.Convert.ToInt32(context.Request.Form.Get("id")),
                        Item1 = context.Request.Form.Get("item1"),
                        Item2 = context.Request.Form.Get("item2"),
                        Item3 = context.Request.Form.Get("item3"),
                        Item4 = context.Request.Form.Get("item4"),
                        Item5 = context.Request.Form.Get("item5"),
                        RealName = currentMember.RealName,
                        SignUpDate = System.DateTime.Now,
                        UserId = currentMember.UserId,
                        UserName = currentMember.UserName
                    };
                    string s = string.IsNullOrEmpty(VshopBrowser.SaveActivitySignUp(info)) ? "{\"success\":true}" : "{\"success\":false, \"msg\":\"你已经报过名了,请勿重复报名\"}";
                    context.Response.Write(s);
                }
            }
        }
        private void SubmitWinnerInfo(System.Web.HttpContext context)
        {
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember == null)
            {
                context.Response.Write("{\"success\":false}");
            }
            else
            {
                int activityId = System.Convert.ToInt32(context.Request.Form.Get("id"));
                string realName = context.Request.Form.Get("name");
                string cellPhone = context.Request.Form.Get("phone");
                string s = VshopBrowser.UpdatePrizeRecord(activityId, currentMember.UserId, realName, cellPhone) ? "{\"success\":true}" : "{\"success\":false}";
                context.Response.ContentType = "application/json";
                context.Response.Write(s);
            }
        }

        private void UpdateDistributor(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "text/json";
            StringBuilder stringBuilder = new StringBuilder();
            if (!this.CheckUpdateDistributors(httpContext, stringBuilder))
            {
                httpContext.Response.Write(string.Concat("{\"success\":false,\"msg\":\"", stringBuilder.ToString(), "\"}"));
                return;
            }
            DistributorsInfo currentDistributors = DistributorsBrower.GetCurrentDistributors(Globals.GetCurrentMemberUserId());
            currentDistributors.StoreName = httpContext.Request["stroename"].Trim();
            currentDistributors.StoreDescription = httpContext.Request["descriptions"].Trim();
            //currentDistributors.BackImage = httpContext.Request["backimg"].Trim();
            if (!string.IsNullOrEmpty(httpContext.Request["accountname"].Trim()))
            {
                currentDistributors.RequestAccount = httpContext.Request["accountname"].Trim();
            }
            currentDistributors.Logo = httpContext.Request["logo"].Trim();
            if (DistributorsBrower.UpdateDistributorMessage(currentDistributors))
            {
                httpContext.Response.Write("{\"success\":true}");
                return;
            }
            httpContext.Response.Write("{\"success\":false,\"msg\":\"店铺名称已存在，请重新命名!\"}");
        }

        //private void UpdateDistributor(System.Web.HttpContext context)
        //{
        //    context.Response.ContentType = "text/plain";
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    if (this.CheckUpdateDistributors(context, sb))
        //    {
        //        DistributorsInfo currentDistributors = DistributorsBrower.GetCurrentDistributors(Globals.GetCurrentMemberUserId());
        //        currentDistributors.StoreName = context.Request["VDistributorInfo$txtstorename"].Trim();
        //        currentDistributors.StoreDescription = context.Request["VDistributorInfo$txtdescription"].Trim();
        //        currentDistributors.BackImage = context.Request["VDistributorInfo$hdbackimg"].Trim();
        //        System.Web.HttpPostedFile file = context.Request.Files["logo"];
        //        if (file != null && !string.IsNullOrEmpty(file.FileName))
        //        {
        //            currentDistributors.Logo = this.UploadFileImages(context, file);
        //        }
        //        if (DistributorsBrower.UpdateDistributor(currentDistributors))
        //        {
        //            context.Response.Write("OK");
        //            context.Response.End();
        //        }
        //        else
        //        {
        //            context.Response.Write("添加失败");
        //            context.Response.End();
        //        }
        //    }
        //    else
        //    {
        //        context.Response.Write(sb.ToString() ?? "");
        //        context.Response.End();
        //    }
        //}

        private void UpdateShippingAddress(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            if (currentMember == null)
            {
                context.Response.Write("{\"success\":false}");
            }
            else
            {
                ShippingAddressInfo shippingAddress = new ShippingAddressInfo
                {
                    Address = context.Request.Form["address"],
                    CellPhone = context.Request.Form["cellphone"],
                    ShipTo = context.Request.Form["shipTo"],
                    Zipcode = "12345",
                    UserId = currentMember.UserId,
                    ShippingId = System.Convert.ToInt32(context.Request.Form["shippingid"]),
                    RegionId = System.Convert.ToInt32(context.Request.Form["regionSelectorValue"])
                };
                if (MemberProcessor.UpdateShippingAddress(shippingAddress))
                {
                    context.Response.Write("{\"success\":true}");
                }
                else
                {
                    context.Response.Write("{\"success\":false}");
                }
            }
        }
        private string UploadFileImages(System.Web.HttpContext context, System.Web.HttpPostedFile file)
        {
            string virtualPath = string.Empty;
            string result;
            if (file != null && !string.IsNullOrEmpty(file.FileName))
            {
                string str2 = Globals.GetStoragePath() + "/Logo";
                string str3 = System.Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.InvariantCulture) + System.IO.Path.GetExtension(file.FileName);
                virtualPath = str2 + "/" + str3;
                string str4 = System.IO.Path.GetExtension(file.FileName).ToLower();
                if (!str4.Equals(".gif") && !str4.Equals(".jpg") && !str4.Equals(".png") && !str4.Equals(".bmp"))
                {
                    context.Response.Write("你上传的文件格式不正确！上传格式有(.gif、.jpg、.png、.bmp)");
                    context.Response.End();
                }
                if (file.ContentLength > 1048576)
                {
                    context.Response.Write("你上传的文件不能大于1048576KB!请重新上传！");
                    context.Response.End();
                }
                file.SaveAs(context.Request.MapPath(virtualPath));
                result = virtualPath;
            }
            else
            {
                context.Response.Write("图片上传失败!");
                context.Response.End();
                result = virtualPath;
            }
            return result;
        }
        public void UserLogin(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            MemberInfo usernameMember = new MemberInfo();
            string username = context.Request["userName"];
            string sourceData = context.Request["password"];
            usernameMember = MemberProcessor.GetusernameMember(username);
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("{");
            if (usernameMember == null)
            {
                builder.Append("\"Status\":\"-1\"");
                builder.Append("}");
                context.Response.Write(builder.ToString());
            }
            else
            {
                if (usernameMember.Password == HiCryptographer.Md5Encrypt(sourceData))
                {
                    DistributorsInfo userIdDistributors = new DistributorsInfo();
                    userIdDistributors = DistributorsBrower.GetUserIdDistributors(usernameMember.UserId);
                    if (userIdDistributors != null && userIdDistributors.UserId > 0)
                    {
                        System.Web.HttpContext.Current.Response.Cookies["Vshop-ReferralId"].Expires = System.DateTime.Now.AddDays(-1.0);
                        System.Web.HttpCookie cookie = new System.Web.HttpCookie("Vshop-ReferralId")
                        {
                            Value = Globals.UrlEncode(userIdDistributors.UserId.ToString()),
                            Expires = System.DateTime.Now.AddYears(1)
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    if (System.Web.HttpContext.Current.Request.Cookies["Vshop-Member"] != null)
                    {
                        System.Web.HttpContext.Current.Response.Cookies["Vshop-Member"].Expires = System.DateTime.Now.AddDays(-1.0);
                        System.Web.HttpCookie cookie2 = new System.Web.HttpCookie("Vshop-Member")
                        {
                            Value = usernameMember.UserId.ToString(),
                            Expires = System.DateTime.Now.AddYears(10)
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie2);
                    }
                    else
                    {
                        System.Web.HttpCookie cookie3 = new System.Web.HttpCookie("Vshop-Member")
                        {
                            Value = Globals.UrlEncode(usernameMember.UserId.ToString()),
                            Expires = System.DateTime.Now.AddYears(1)
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie3);
                    }
                    context.Session["userid"] = usernameMember.UserId.ToString();
                    builder.Append("\"Status\":\"OK\"");
                }
                else
                {
                    builder.Append("\"Status\":\"-2\"");
                }
                builder.Append("}");
                context.Response.Write(builder.ToString());
            }
        }
        private void Vote(System.Web.HttpContext context)
        {
            context.Response.ContentType = "application/json";
            int result = 1;
            int.TryParse(context.Request["voteId"], out result);
            string itemIds = context.Request["itemIds"];
            itemIds = itemIds.Remove(itemIds.Length - 1);
            if (MemberProcessor.GetCurrentMember() == null)
            {
                MemberInfo member = new MemberInfo();
                string generateId = Globals.GetGenerateId();
                member.GradeId = MemberProcessor.GetDefaultMemberGrade();
                member.UserName = "";
                member.OpenId = "";
                member.CreateDate = System.DateTime.Now;
                member.SessionId = generateId;
                member.SessionEndTime = System.DateTime.Now;
                MemberProcessor.CreateMember(member);
                member = MemberProcessor.GetMember(generateId);
                System.Web.HttpCookie cookie = new System.Web.HttpCookie("Vshop-Member")
                {
                    Value = member.UserId.ToString(),
                    Expires = System.DateTime.Now.AddYears(10)
                };
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("{");
            if (VshopBrowser.Vote(result, itemIds))
            {
                builder.Append("\"Status\":\"OK\"");
            }
            else
            {
                builder.Append("\"Status\":\"Error\"");
            }
            builder.Append("}");
            context.Response.Write(builder.ToString());
        }

        private void AttendActivity(System.Web.HttpContext context)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("{");
            if (MemberProcessor.GetCurrentMember() != null)
            {
                if (VshopBrowser.AttendActivity(int.Parse(context.Request["activityId"] ?? "0"), MemberProcessor.GetCurrentMember().UserId))
                {
                    builder.Append("\"Status\":\"OK\"");
                }
                else
                {
                    builder.Append("\"Status\":\"Error\"");
                }
            }
            builder.Append("}");
            context.Response.Write(builder.ToString());
        }
    }
}
