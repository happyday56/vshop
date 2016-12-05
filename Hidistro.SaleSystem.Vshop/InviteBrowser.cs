namespace Hidistro.SaleSystem.Vshop
{
    using Hidistro.Core;
    using Hidistro.Entities;
    using Hidistro.Entities.Commodities;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.VShop;
    using Hidistro.SqlDal.Commodities;
    using Hidistro.SqlDal.VShop;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Web.Caching;
    using NewLife.Log;
    using Hidistro.Core.Enums;

    public static class InviteBrowser
    {
        public static bool AddInviteCode(InviteCode model, out string code)
        {
            code = string.Empty;
            var dao = new InviteDao();

            //XTrace.WriteLine("邀请码用户：" + model.UserId + " --购买产品：" + model.ProductId);

            //判断剩余邀请数量
            if (dao.GetInvitationNum(model.UserId) <= 0)
            {
                return false;
            }

            //判断该产品是否存在
            if (!dao.CheckProductId(model.ProductId))
            {
                return false;
            }

            //暂时不考虑大量并发问题，不清楚目前该系统是否有类似的处理方法，故不使用异步队列的方式进行操作
            do
            {
                //创建邀请码,如果存在则重新生成
                code = getRandomizer(8, true, false, true, true);
            }
            while (dao.CheckInviteCode(code));
            model.Code = code;
            //保存邀请码
            return dao.AddInviteCode(model);

        }

        public static bool AddInviteCode2(InviteCode model, out string code)
        {
            code = string.Empty;
            var dao = new InviteDao();

            //XTrace.WriteLine("邀请码用户：" + model.UserId + " --购买产品：" + model.ProductId);

            ////判断剩余邀请数量
            //if (dao.GetInvitationNum(model.UserId) <= 0)
            //{
            //    return false;
            //}

            //判断该产品是否存在
            if (!dao.CheckProductId(model.ProductId))
            {
                return false;
            }

            //暂时不考虑大量并发问题，不清楚目前该系统是否有类似的处理方法，故不使用异步队列的方式进行操作
            do
            {
                //创建邀请码,如果存在则重新生成
                code = getRandomizer(8, true, false, true, true);
            }
            while (dao.CheckInviteCode(code));
            model.Code = code;
            //保存邀请码
            return dao.AddInviteCode(model);

        }

        

        /// <summary>
        /// 获取剩余邀请码数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetInvitationNum(int userId)
        {
            return new InviteDao().GetInvitationNum(userId);
        }

        /// <summary>
        /// 获取未完成邀请列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<InviteCode> GetInviteCodeList(int userId)
        {
            return new InviteDao().GetInviteCodeList(userId);
        }
        /// <summary>
        /// 获取已完成邀请列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<InviteCode> GetInviteComplatedList(int userId)
        {
            return new InviteDao().GetInviteComplatedList(userId);
        }

        /// <summary>
        /// 获取分销商注册购买商品
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDistributorProduct()
        {
            return new InviteDao().GetDistributorProduct();
        }

        /// <summary>
        /// 获取邀请码实体
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static InviteCode GetInvoiteCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }
            return new InviteDao().GetInviteCodeByCode(code);
        }

        public static InviteCode GetInvoiteCode(int inviteId)
        {
            if (!(inviteId > 0))
            {
                return null;
            }
            return new InviteDao().GetInviteCodeById(inviteId);
        }

        /// <summary>
        /// 锁定邀请码
        /// </summary>
        /// <param name="invitecode"></param>
        /// <param name="inviteUserId"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool LockInviteCode(string invitecode, int inviteUserId, DateTime ts, int inviteId)
        {
            return new InviteDao().LockInviteCode(invitecode, inviteUserId, ts, inviteId);
        }
        /// <summary>
        /// 更新邀请码状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateInviteStatus(InviteCode model)
        {
            return new InviteDao().UpdateInviteCode(model);
        }

        /// <summary>
        /// 新增邀请码记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddInviteRecord(InviteRecord model)
        {
            return new InviteDao().AddInviteRecord(model);
        }

        /// <summary>
        /// 检测用户是否已被邀请成功
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CheckIsInvite(int userId)
        {
            return new InviteDao().CheckIsInvite(userId);
        }

        /// <summary>
        /// 验证用户是否已经购买了受邀请的产品
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static bool CheckInviteProductFinished(int userId, int productId) {
            return new InviteDao().CheckInviteProductFinished(userId, productId);
        }


        /// <summary>
        /// 获取邀请码最后的状态
        /// </summary>
        /// <param name="inviteid"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static InviteRecord GetInviteRecordByInviteId(int inviteid, string openId)
        {
            return new InviteDao().GetInviteRecordByInviteId(inviteid, openId);
        }

        /// <summary>
        /// 申请邀请限额
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddInviteApplyRecord(InviteApply model)
        {
            return new InviteDao().AddInviteApplyRecord(model);
        }
        /// <summary>
        /// 检测是否有申请记录，如果有待审核的申请记录则不予申请
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CheckIsInviteApplyRecord(int userId) {
            return new InviteDao().CheckIsInviteApplyRecord(userId);
        }

        public static bool UpdateInviteApplyAudit(InviteApply model)
        {
            return new InviteDao().UpdateInviteApplyAudit(model);
        }

        public static InviteApply GetInviteApplyRecordById(int applyId) {
            return new InviteDao().GetInviteApplyRecordById(applyId);
        }

        public static List<InviteApply> GetInviteApplyRecord(ref int total, string realname, string cellphone, int auditstatus,
        string sortBy, SortAction sortOrder, int pageIndex, int pageSize)
        {
            return new InviteDao().GetInviteApplyRecord(ref total, realname, cellphone, auditstatus, sortBy, sortOrder, pageIndex, pageSize);
        }
        /// <summary>
        /// 增加次数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool UpdateInvitationNum(int userId, int num)
        {
            return new InviteDao().UpdateInvitationNum(userId, num);
        }

        public static DistributorTree GetDistributorList()
        {
            var cacheDistributorTree = HiCache.Get("DistributorTree");
            if (cacheDistributorTree == null)
            {
                cacheDistributorTree = new InviteDao().GetDistributorList();
                HiCache.Insert("DistributorTree", cacheDistributorTree);
            }
            return cacheDistributorTree as DistributorTree;
        }


        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <param name="intLength">长度</param>
        /// <param name="booNumber">是否数字</param>
        /// <param name="booSign">符号</param>
        /// <param name="booSmallword">小写</param>
        /// <param name="booBigword">大写</param>
        /// <returns></returns>
        public static string getRandomizer(int intLength, bool booNumber, bool booSign, bool booSmallword, bool booBigword)
        {
            //定义
            Random ranA = new Random();
            int intResultRound = 0;
            int intA = 0;
            string strB = "";
            while (intResultRound < intLength)
            {
                //生成随机数A，表示生成类型
                //1=数字，2=符号，3=小写字母，4=大写字母
                intA = ranA.Next(1, 5);
                //如果随机数A=1，则运行生成数字
                //生成随机数A，范围在0-10
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 1 && booNumber)
                {
                    intA = ranA.Next(0, 10);
                    strB = intA.ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
                //如果随机数A=2，则运行生成符号
                //生成随机数A，表示生成值域
                //1：33-47值域，2：58-64值域，3：91-96值域，4：123-126值域
                if (intA == 2 && booSign == true)
                {
                    intA = ranA.Next(1, 5);
                    //如果A=1
                    //生成随机数A，33-47的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 1)
                    {
                        intA = ranA.Next(33, 48);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }
                    //如果A=2
                    //生成随机数A，58-64的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 2)
                    {
                        intA = ranA.Next(58, 65);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }
                    //如果A=3
                    //生成随机数A，91-96的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 3)
                    {
                        intA = ranA.Next(91, 97);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }
                    //如果A=4
                    //生成随机数A，123-126的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 4)
                    {
                        intA = ranA.Next(123, 127);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }
                }
                //如果随机数A=3，则运行生成小写字母
                //生成随机数A，范围在97-122
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 3 && booSmallword == true)
                {
                    intA = ranA.Next(97, 123);
                    strB = ((char)intA).ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
                //如果随机数A=4，则运行生成大写字母
                //生成随机数A，范围在65-90
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 4 && booBigword == true)
                {
                    intA = ranA.Next(65, 89);
                    strB = ((char)intA).ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
            }
            return strB;
        }

    }
}
