namespace Hidistro.SaleSystem.Vshop
{
    using Hidistro.Core;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.Entities.Sales;
    using Hidistro.Messages;
    using Hidistro.SqlDal.Commodities;
    using Hidistro.SqlDal.Members;
    using Hidistro.SqlDal.Orders;
    using Hidistro.SqlDal.Promotions;
    using Hidistro.SqlDal.Sales;
    using Hidistro.SqlDal.VShop;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Web.Caching;
    using System.Linq;
    using Hishop.Plugins;
    using Hidistro.Core.Entities;
    using NewLife.Log;

    public static class MemberProcessor
    {
        public static int AddShippingAddress(ShippingAddressInfo shippingAddress)
        {
            ShippingAddressDao dao = new ShippingAddressDao();
            int shippingId = dao.AddShippingAddress(shippingAddress);
            if (dao.SetDefaultShippingAddress(shippingId, Globals.GetCurrentMemberUserId()))
            {
                return 1;
            }
            return 0;
        }

        public static bool ConfirmOrderFinish(OrderInfo order)
        {
            bool flag = false;
            if (order.CheckAction(OrderActions.BUYER_CONFIRM_GOODS))
            {
                order.OrderStatus = OrderStatus.Finished;
                order.FinishDate = new DateTime?(DateTime.Now);
                flag = new OrderDao().UpdateOrder(order, null);
                HiCache.Remove(string.Format("DataCache-Member-{0}", order.UserId));
            }
            return flag;
        }

        public static bool ConfirmCloseOrder(OrderInfo order)
        {
            bool flag = false;
            
            order.OrderStatus = OrderStatus.Closed;
            order.CloseReason = "未付款关闭";
            order.FinishDate = new DateTime?(DateTime.Now);
            flag = new OrderDao().UpdateOrder(order, null);
            HiCache.Remove(string.Format("DataCache-Member-{0}", order.UserId));
            
            return flag;
        }

        public static bool CreateMember(MemberInfo member)
        {
            MemberDao dao = new MemberDao();
            return dao.CreateMember(member);
        }

        public static bool Delete(int userId)
        {
            bool flag = new MemberDao().Delete(userId);
            if (flag)
            {
                HiCache.Remove(string.Format("DataCache-Member-{0}", userId));
            }
            return flag;
        }

        public static bool DelShippingAddress(int shippingid, int userid)
        {
            return new ShippingAddressDao().DelShippingAddress(shippingid, userid);
        }

        public static MemberInfo GetCurrentMember()
        {
            return GetMember(Globals.GetCurrentMemberUserId());
        }

        public static int GetDefaultMemberGrade()
        {
            return new MemberGradeDao().GetDefaultMemberGrade();
        }

        public static ShippingAddressInfo GetDefaultShippingAddress()
        {
            IList<ShippingAddressInfo> shippingAddresses = new ShippingAddressDao().GetShippingAddresses(Globals.GetCurrentMemberUserId());
            foreach (ShippingAddressInfo info in shippingAddresses)
            {
                if (info.IsDefault)
                {
                    return info;
                }
            }
            return null;
        }

        public static MemberInfo GetMember()
        {
            return GetMember(Globals.GetCurrentMemberUserId());
        }

        public static MemberInfo GetMember(int userId)
        {
            MemberInfo member = HiCache.Get(string.Format("DataCache-Member-{0}", userId)) as MemberInfo;
            if (member == null)
            {
                member = new MemberDao().GetMember(userId);
                HiCache.Insert(string.Format("DataCache-Member-{0}", userId), member, 360, CacheItemPriority.Normal);
            }
            else
            {
                member = new MemberDao().GetMember(member.UserId);
                if (null != member)
                {
                    HiCache.Insert(string.Format("DataCache-Member-{0}", member.UserId), member, 360, CacheItemPriority.Normal);
                }
            }
            return member;
        }

        public static MemberInfo GetReferralMember(int userId)
        {
            return new MemberDao().GetReferralMember(userId);
        }

        public static MemberInfo GetMember(string sessionId)
        {
            return new MemberDao().GetMember(sessionId);
        }

        public static MemberGradeInfo GetMemberGrade(int gradeId)
        {
            return new MemberGradeDao().GetMemberGrade(gradeId);
        }

        public static MemberInfo GetOpenIdMember(string OpenId)
        {
            MemberDao dao = new MemberDao();
            return dao.GetOpenIdMember(OpenId);
        }

        public static ShippingAddressInfo GetShippingAddress(int shippingId)
        {
            return new ShippingAddressDao().GetShippingAddress(shippingId, Globals.GetCurrentMemberUserId());
        }

        public static int GetShippingAddressCount()
        {
            return new ShippingAddressDao().GetShippingAddresses(Globals.GetCurrentMemberUserId()).Count;
        }

        public static IList<ShippingAddressInfo> GetShippingAddresses()
        {
            return new ShippingAddressDao().GetShippingAddresses(Globals.GetCurrentMemberUserId());
        }

        public static DataTable GetUserCoupons(int userId, int useType = 0)
        {
            return new CouponDao().GetUserCoupons(userId, useType);
        }

        public static int GetUserHistoryPoint(int userId)
        {
            return new PointDetailDao().GetHistoryPoint(userId);
        }

        public static MemberInfo GetusernameMember(string username)
        {
            return new MemberDao().GetusernameMember(username);
        }

        public static DataSet GetUserOrder(int userId, OrderQuery query)
        {
            return new OrderDao().GetUserOrder(userId, query);
        }

        public static int GetUserOrderCount(int userId, OrderQuery query)
        {
            return new OrderDao().GetUserOrderCount(userId, query);
        }

        public static DataSet GetUserOrderReturn(int userId, OrderQuery query)
        {
            return new OrderDao().GetUserOrderReturn(userId, query);
        }

        public static int GetUserOrderReturnCount(int userId)
        {
            return new OrderDao().GetUserOrderReturnCount(userId);
        }

        public static bool SetDefaultShippingAddress(int shippingId, int UserId)
        {
            return new ShippingAddressDao().SetDefaultShippingAddress(shippingId, UserId);
        }

        public static bool SetMemberSessionId(MemberInfo member)
        {
            MemberDao dao = new MemberDao();
            return dao.SetMemberSessionId(member.SessionId, member.SessionEndTime, member.OpenId);
        }

        public static bool SetMemberSessionId(string sessionId, DateTime sessionEndTime, string openId)
        {
            return new MemberDao().SetMemberSessionId(sessionId, sessionEndTime, openId);
        }

        public static bool SetPwd(string userid, string pwd)
        {
            return new MemberDao().SetPwd(userid, pwd);
        }
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="cellPhone">发送号码</param>
        /// <param name="verifyCode">验证码</param>
        /// <returns></returns>
        public static bool UpdateVerifyCode(string userid, string cellPhone, string verifyCode)
        {
            MemberDao dao = new MemberDao();
            SendStatus sendStatus = SendStatus.Fail;
            SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);
            string errMsg = "";
            string smsContent = "";

            // 生成验证码发送记录
            bool updateFlag = dao.UpdateVerifyCode(userid, verifyCode);
            if (updateFlag)
            {
                // 发送短信验证码短信,并加入发送记录表
                smsContent = siteSettings.DefaultLoginSmsContent;
                smsContent = smsContent.Replace("{Code1}", verifyCode);
                sendStatus = Messenger.SendSMS(cellPhone, smsContent, siteSettings, out errMsg);
            }

            //XTrace.WriteLine("发送验证码短信返回结果：" + errMsg + " 发送状态：" + sendStatus.ToString());

            if (sendStatus == SendStatus.Success)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 获取最后一条验证码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="timeoutmins">验证码有效时间（分）</param>
        /// <returns>为空则表示没有验证码或验证码已过期</returns>
        public static string GetLastVerifyCode(int userId, int timeoutmins)
        {
            return new MemberDao().GetLastVerifyCode(userId, timeoutmins);
        }

        public static bool UpdateMember(MemberInfo member)
        {
            HiCache.Remove(string.Format("DataCache-Member-{0}", member.UserId));
            return new MemberDao().Update(member);
        }

        public static bool UpdateOpenid(MemberInfo member)
        {
            return new MemberDao().UpdateOpenid(member);
        }

        public static bool UpdateShippingAddress(ShippingAddressInfo shippingAddress)
        {
            return new ShippingAddressDao().UpdateShippingAddress(shippingAddress);
        }

        public static bool UserPayOrder(OrderInfo order)
        {
            OrderDao dao = new OrderDao();
            order.OrderStatus = OrderStatus.BuyerAlreadyPaid;
            order.PayDate = new DateTime?(DateTime.Now);
            bool flag = dao.UpdateOrder(order, null);
            string str = "";
            if (flag)
            {
                dao.UpdatePayOrderStock(order.OrderId);
                foreach (LineItemInfo info in order.LineItems.Values)
                {
                    ProductDao dao2 = new ProductDao();
                    str = str + "'" + info.SkuId + "',";
                    ProductInfo productDetails = dao2.GetProductDetails(info.ProductId);
                    productDetails.SaleCounts += info.Quantity;
                    productDetails.ShowSaleCounts += info.Quantity;
                    dao2.UpdateProduct(productDetails, null);
                }
                if (!string.IsNullOrEmpty(str))
                {
                    dao.UpdateItemsStatus(order.OrderId, 2, str.Substring(0, str.Length - 1));
                }
                if (!string.IsNullOrEmpty(order.ActivitiesId))
                {
                    new ActivitiesDao().UpdateActivitiesTakeEffect(order.ActivitiesId);
                }
                MemberInfo member = GetMember(order.UserId);
                if (member == null)
                {
                    return flag;
                }
                MemberDao dao4 = new MemberDao();
                PointDetailInfo point = new PointDetailInfo
                {
                    OrderId = order.OrderId,
                    UserId = member.UserId,
                    TradeDate = DateTime.Now,
                    TradeType = PointTradeType.Bounty,
                    Increased = new int?(order.Points),
                    Points = order.Points + member.Points
                };
                if ((point.Points > 0x7fffffff) || (point.Points < 0))
                {
                    point.Points = 0x7fffffff;
                }
                PointDetailDao dao5 = new PointDetailDao();
                dao5.AddPointDetail(point);
                member.Expenditure += order.GetTotal();
                member.OrderNumber++;
                dao4.Update(member);
                Messenger.OrderPayment(member, order.OrderId, order.GetTotal());
                XTrace.WriteLine("支付微信通知3");
                int historyPoint = dao5.GetHistoryPoint(member.UserId);
                MemberGradeInfo memberGrade = GetMemberGrade(member.GradeId);
                if ((memberGrade != null) && (memberGrade.Points > historyPoint))
                {
                    return flag;
                }
                List<MemberGradeInfo> memberGrades = new MemberGradeDao().GetMemberGrades() as List<MemberGradeInfo>;
                foreach (MemberGradeInfo info6 in from item in memberGrades
                                                  orderby item.Points descending
                                                  select item)
                {
                    if (member.GradeId == info6.GradeId)
                    {
                        return flag;
                    }
                    if (info6.Points <= historyPoint)
                    {
                        member.GradeId = info6.GradeId;
                        dao4.Update(member);
                        return flag;
                    }
                }
            }
            return flag;
        }

        public static bool UpdateUserIsStore(int userId, int isStore)
        {
            return new MemberDao().UpdateUserIsStore(userId, isStore);
        }

        public static bool AddUserVirtualPoints(int userId, decimal points)
        {
            return new MemberDao().AddUserVirtualPoints(userId, points);
        }
    }
}

