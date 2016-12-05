namespace Hidistro.SqlDal.VShop
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Core.Enums;
    using Hidistro.Entities.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using NewLife.Log;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class InviteDao
    {
        /**
         *1.创建邀请码
         *2.获取邀请码列表
         *3.获取邀请记录表
         *4.更新邀请码状态
         *5.增加邀请记录
         *6.获取邀请码状态
         *
         */

        private Database database = DatabaseFactory.CreateDatabase();
        /// <summary>
        /// 创建邀请码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddInviteCode(InviteCode model)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO aspnet_invitecodes(UserId,Code,ProductId,InviteStatus,ts, IsUse) VALUES (@UserId,@Code,@ProductId,@InviteStatus,@ts,0)");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, model.UserId);
            this.database.AddInParameter(sqlStringCommand, "Code", DbType.String, model.Code);
            this.database.AddInParameter(sqlStringCommand, "ProductId", DbType.Int32, model.ProductId);
            this.database.AddInParameter(sqlStringCommand, "InviteStatus", DbType.Int32, model.InviteStatus);
            this.database.AddInParameter(sqlStringCommand, "ts", DbType.DateTime, model.TimeStamp);
            try
            {
                bool ret = (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
                if (ret)
                {
                    UpdateInvitationNum(model.UserId);
                }
                return ret;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 检测邀请码是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CheckInviteCode(string code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select count(*) from aspnet_invitecodes where code = @code");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "code", DbType.String, code);
            int ret = 0;
            Int32.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out ret);
            return ret > 0;
        }
        /// <summary>
        /// 检测产品是否存在
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        public bool CheckProductId(int prod)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select count(*) from Hishop_Products where IsDistributorBuy=1 and ProductId = @prod");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "prod", DbType.Int32, prod);
            int ret = 0;
            Int32.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out ret);
            return ret > 0;
        }

        /// <summary>
        /// 获取用户已生成的邀请码数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetInviteCodeCount(int userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select count(*) from aspnet_invitecodes where userId = @userId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "userId", DbType.Int32, userId);
            int ret = 0;
            Int32.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out ret);
            return ret;
        }
        /// <summary>
        /// 获取分销商的剩余邀请次数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetInvitationNum(int userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select isnull(InvitationNum,0) from aspnet_Distributors where userId = @userId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "userId", DbType.Int32, userId);
            int ret = 0;
            XTrace.WriteLine("--------获取分销商的剩余邀请次数用户ID：" + userId);
            object result = this.database.ExecuteScalar(sqlStringCommand);
            if (null == result)
            {
                ret = 0;
            }
            else
            {
                Int32.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out ret);
            }
            
            XTrace.WriteLine("--------获取分销商的剩余邀请次数用户ID：" + ret);
            return ret;
        }

        /// <summary>
        /// 减去邀请次数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UpdateInvitationNum(int userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"update aspnet_Distributors set InvitationNum = isnull(InvitationNum,0)-1 where UserId = @UserId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);

        }

        /// <summary>
        /// 获取生成的未完成的邀请码列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<InviteCode> GetInviteCodeList(int userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select i.*,m.UserName,p.ProductName from aspnet_invitecodes i
                                inner join aspnet_Members m on i.UserId=m.UserId
                                inner join Hishop_Products p on p.IsDistributorBuy=1 
                                and p.ProductId=i.ProductId
                            where i.invitestatus<3 and i.UserId = @UserId
                            ");
            builder.Append(" order by ts desc");

            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);

            var dt = this.database.ExecuteDataSet(sqlStringCommand).Tables[0];

            List<InviteCode> lstInviteCodes = new List<InviteCode>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //目前的转换类型的方式在获取参数的DBNull的时候会报错，不知道系统是否存在安全转换的方法
                    lstInviteCodes.Add(new InviteCode()
                    {
                        Code = dr["Code"].ToString(),
                        InviteId = Convert.ToInt32(dr["InviteId"]),
                        InviteStatus = Convert.ToInt32(dr["InviteStatus"]),
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"] == DBNull.Value ? "" : dr["ProductName"].ToString(),
                        TimeStamp = Convert.ToDateTime(dr["Ts"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        UserName = dr["UserName"] == DBNull.Value ? "" : dr["UserName"].ToString(),
                        InviteUserId = dr["InviteUserId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["InviteUserId"]),
                        IsUse = dr["IsUse"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsUse"]),
                    });
                }
            }

            return lstInviteCodes;
        }

        /// <summary>
        /// 根据邀请码编码获取邀请码实体
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public InviteCode GetInviteCodeByCode(string code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select top 1 i.*,m.UserName,p.ProductName from aspnet_invitecodes i
                                inner join aspnet_Members m on i.UserId=m.UserId
                                inner join Hishop_Products p on p.IsDistributorBuy=1 
                                and p.ProductId=i.ProductId
                            where i.invitestatus<3 and i.Code = @Code
                            ");
            builder.Append(" order by ts desc");

            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "Code", DbType.String, code);
            var dt = this.database.ExecuteDataSet(sqlStringCommand).Tables[0];

            InviteCode inviteCode = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    return new InviteCode()
                    {
                        Code = dr["Code"].ToString(),
                        InviteId = Convert.ToInt32(dr["InviteId"]),
                        InviteStatus = Convert.ToInt32(dr["InviteStatus"]),
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"] == DBNull.Value ? "" : dr["ProductName"].ToString(),
                        TimeStamp = Convert.ToDateTime(dr["Ts"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        UserName = dr["UserName"] == DBNull.Value ? "" : dr["UserName"].ToString(),
                        InviteUserId = dr["InviteUserId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["InviteUserId"]),
                        IsUse = dr["IsUse"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsUse"]),
                    };
                }
            }

            return inviteCode;
        }
        public InviteCode GetInviteCodeById(int inviteId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select i.*,m.UserName,p.ProductName,m2.RealName AS InviteRealName,m2.CellPhone AS InvitePhone, p.PTTypeId, p.StoreGiftPoint from aspnet_invitecodes i
                                inner join aspnet_Members m on i.UserId=m.UserId
                                inner join Hishop_Products p on p.IsDistributorBuy=1 
                                inner join aspnet_Members m2 on m2.UserId = i.InviteUserId
                                and p.ProductId=i.ProductId
                            where i.invitestatus<3 and i.InviteId = @InviteId
                            ");
            builder.Append(" order by ts desc");

            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "InviteId", DbType.Int32, inviteId);
            var dt = this.database.ExecuteDataSet(sqlStringCommand).Tables[0];

            InviteCode inviteCode = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    return new InviteCode()
                    {
                        Code = dr["Code"] == DBNull.Value ? "" : dr["Code"].ToString(),
                        InviteId = dr["InviteId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["InviteId"]),
                        InviteStatus = dr["InviteStatus"] == DBNull.Value ? 0 : Convert.ToInt32(dr["InviteStatus"]),
                        ProductId = dr["ProductId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"] == DBNull.Value ? "" : dr["ProductName"].ToString(),
                        TimeStamp = Convert.ToDateTime(dr["Ts"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        UserName = dr["UserName"] == DBNull.Value ? "" : dr["UserName"].ToString(),
                        InviteUserId = dr["InviteUserId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["InviteUserId"]),
                        IsUse = dr["IsUse"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsUse"]),
                        InviteRealName = dr["InviteRealName"] == DBNull.Value ? "" : dr["InviteRealName"].ToString(),
                        InvitePhone = dr["InvitePhone"] == DBNull.Value ? "" : dr["InvitePhone"].ToString(),
                        PTTypeId = dr["PTTypeId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["PTTypeId"]),
                        StoreGiftPoint = dr["StoreGiftPoint"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["StoreGiftPoint"])
                    };
                }
            }

            return inviteCode;
        }

        /// <summary>
        /// 获取用户已完成的邀请列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<InviteCode> GetInviteComplatedList(int userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select i.*,m.UserName,p.ProductName,m2.RealName InviteRealName,m2.CellPhone InvitePhone from aspnet_invitecodes i
                            inner join aspnet_Members m on i.UserId=m.UserId
                            inner join Hishop_Products p on p.IsDistributorBuy=1 
                            inner join aspnet_Members m2 on m2.UserId = i.InviteUserId
                            and p.ProductId=i.ProductId
                            where i.invitestatus=3 and isuse =1 and i.UserId = @UserId");
            builder.Append(" order by ts desc");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            var dt = this.database.ExecuteDataSet(sqlStringCommand).Tables[0];

            List<InviteCode> lstInviteCodes = new List<InviteCode>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //目前的转换类型的方式在获取参数的DBNull的时候会报错，不知道系统是否存在安全转换的方法
                    lstInviteCodes.Add(new InviteCode()
                    {
                        Code = dr["Code"].ToString(),
                        InviteId = Convert.ToInt32(dr["InviteId"]),
                        InviteStatus = Convert.ToInt32(dr["InviteStatus"]),
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"] == DBNull.Value ? "" : dr["ProductName"].ToString(),
                        TimeStamp = Convert.ToDateTime(dr["Ts"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        UserName = dr["UserName"] == DBNull.Value ? "" : dr["UserName"].ToString(),
                        InviteUserId = dr["InviteUserId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["InviteUserId"]),
                        IsUse = dr["IsUse"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsUse"]),
                        InviteRealName = dr["InviteRealName"] == DBNull.Value ? "" : dr["InviteRealName"].ToString(),
                        InvitePhone = dr["InvitePhone"] == DBNull.Value ? "" : dr["InvitePhone"].ToString(),
                    });
                }
            }

            return lstInviteCodes;

        }

        /// <summary>
        /// 获取分销商购买产品
        /// </summary>
        /// <returns></returns>
        public DataTable GetDistributorProduct()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select * from Hishop_Products where IsDistributorBuy = 1 AND PTTypeId IN ( 0, 1 ) ");
            builder.Append(" order by addeddate desc");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
        }

        /// <summary>
        /// 获取已完成的订单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public DataTable GetOrderComplatedByInviteIdProductId(int userId, int productId)
        {

            return null;
        }


        /// <summary>
        /// 锁定邀请码待激活状态
        /// </summary>
        /// <param name="invitecode"></param>
        /// <returns></returns>
        public bool LockInviteCode(string invitecode, int uesrid, DateTime ts, int inviteId)
        {
            //update 锁定该条记录
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("update aspnet_invitecodes set isuse = 1,ts=@ts,inviteUserId=@InviteUserId where Code=@code and isuse=0 and InviteId = @InviteId");
            this.database.AddInParameter(sqlStringCommand, "Code", DbType.String, invitecode);
            this.database.AddInParameter(sqlStringCommand, "ts", DbType.Date, ts);
            this.database.AddInParameter(sqlStringCommand, "InviteId", DbType.Int32, inviteId);
            this.database.AddInParameter(sqlStringCommand, "InviteUserId", DbType.Int32, uesrid);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

       /// <summary>
        /// 更新邀请码状态
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        public bool UpdateInviteCode(InviteCode model)
        {
            //update 锁定该条记录
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("update aspnet_invitecodes set InviteStatus = @InviteStatus,Ts=@Ts where InviteId=@InviteId");
            this.database.AddInParameter(sqlStringCommand, "InviteId", DbType.Int32, model.InviteId);
            this.database.AddInParameter(sqlStringCommand, "InviteStatus", DbType.Int32, model.InviteStatus);
            this.database.AddInParameter(sqlStringCommand, "Ts", DbType.DateTime, model.TimeStamp);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }


        /// <summary>
        /// 创建邀请码记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddInviteRecord(InviteRecord model, bool isRefUpdate = false)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("insert into aspnet_inviterecord values(@OpendId,@UserId,@InviteId,@InviteStatus,@Ts)");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, model.UserId);
            this.database.AddInParameter(sqlStringCommand, "OpendId", DbType.String, model.OpenId);
            this.database.AddInParameter(sqlStringCommand, "InviteId", DbType.Int32, model.InviteId);
            this.database.AddInParameter(sqlStringCommand, "InviteStatus", DbType.Int32, model.InviteStatus);
            this.database.AddInParameter(sqlStringCommand, "ts", DbType.DateTime, model.TimeStamp);
            try
            {
                bool ret = (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
                if (ret && isRefUpdate)
                {
                    //更新邀请码状态
                    UpdateInviteCode(new InviteCode()
                    {
                        InviteId = model.InviteId,
                        InviteStatus = model.InviteStatus,
                        TimeStamp = model.TimeStamp
                    });
                }
                return ret;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 判断用户是否已被邀请成功
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckIsInvite(int userId)
        {
            StringBuilder builder = new StringBuilder();
            //只判断已成功的用户，是否需要判断是新用户，这里需求不确定
            //目前情况只要用户没有注册完成则可以占有多个邀请，直至有一个已经完成
            builder.Append(@"select count(*) from aspnet_invitecodes where inviteuserid=@InviteUserId and InviteStatus=3");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "InviteUserId", DbType.Int32, userId);
            int ret = 0;
            Int32.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out ret);
            return ret > 0;
        }


        /// <summary>
        /// 验证用户是否已经购买了受邀请的产品
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool CheckInviteProductFinished(int userId, int productId)
        {
            StringBuilder builder = new StringBuilder();
            //OrderStatus=5表示已完成订单,2表示已付款
            builder.Append(@"select count(*) from Hishop_Orders o left join Hishop_OrderItems oi on o.orderid=oi.orderid where OrderStatus>=2 and o.UserId=@UserId and oi.ProductId=@ProductId  ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            this.database.AddInParameter(sqlStringCommand, "ProductId", DbType.Int32, productId);
            int ret = 0;
            Int32.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out ret);
            return ret > 0;
        }

        /// <summary>
        /// 根据邀请ID获取邀请码状态
        /// </summary>
        /// <param name="inviteid"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public InviteRecord GetInviteRecordByInviteId(int inviteid, string openId)
        {
            InviteRecord inviteRecord = null;

            StringBuilder builder = new StringBuilder();
            builder.Append(@"select top 1 * from aspnet_inviterecord where InviteId = @InviteId and OpenId=@OpenId ");
            builder.Append(" order by InviteStatus desc");

            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "InviteId", DbType.Int32, inviteid);
            this.database.AddInParameter(sqlStringCommand, "OpenId", DbType.String, openId);
            var dt = this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                return new InviteRecord()
                {
                    RecordId = Convert.ToInt32(dr["RecordId"]),
                    OpenId = dr["RecordId"].ToString(),
                    TimeStamp = Convert.ToDateTime(dr["Ts"]),
                    UserId = Convert.ToInt32(dr["UserId"]),
                    InviteId = Convert.ToInt32(dr["InviteId"])
                };
            }

            return inviteRecord;
        }

        /// <summary>
        /// 增加邀请码限额申请记录
        /// </summary>
        /// <returns></returns>
        public bool AddInviteApplyRecord(InviteApply model)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("insert aspnet_inviteapply values(@UserId,@ApplyNum,@ApplyTime,0,null,@Ts)");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, model.UserId);
            this.database.AddInParameter(sqlStringCommand, "ApplyNum", DbType.Int32, model.ApplyNum);
            this.database.AddInParameter(sqlStringCommand, "ApplyTime", DbType.DateTime, model.ApplyTime);
            this.database.AddInParameter(sqlStringCommand, "Ts", DbType.DateTime, model.TimeStamp);
            try
            {
                return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检测是否存在正在申请的记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckIsInviteApplyRecord(int userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select count(*) from aspnet_inviteapply where IsAudit=0 and UserId=@UserId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            int ret = 0;
            Int32.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out ret);
            return ret > 0;
        }

        /// <summary>
        /// 修改审核状态
        /// </summary>
        /// <returns></returns>
        public bool UpdateInviteApplyAudit(InviteApply model)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("update aspnet_inviteapply set IsAudit=@IsAudit,Auditor=@Auditor,Ts=@Ts where ApplyId=@ApplyId ");
            this.database.AddInParameter(sqlStringCommand, "ApplyId", DbType.Int32, model.ApplyId);
            this.database.AddInParameter(sqlStringCommand, "Auditor", DbType.Int32, model.AuditUserId);
            this.database.AddInParameter(sqlStringCommand, "IsAudit", DbType.Int32, model.IsAudit);
            this.database.AddInParameter(sqlStringCommand, "Ts", DbType.DateTime, model.TimeStamp);
            try
            {
                return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
            }
            catch
            {
                return false;
            }
        }

        public InviteApply GetInviteApplyRecordById(int applyid)
        {
            InviteApply inviteApply = null;

            StringBuilder builder = new StringBuilder();
            builder.Append(@"select ia.*,m.RealName,m.CellPhone,u.UserName from aspnet_inviteapply ia
                            inner join aspnet_Members m on ia.userid = m.UserId
                            left join aspnet_Managers u on ia.Auditor=u.UserId
                            where applyId=@ApplyId
                        ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ApplyId", DbType.Int32, applyid);
            
            var dt = this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                return new InviteApply()
                {
                    ApplyId = Convert.ToInt32(dr["ApplyId"]),
                    ApplyNum = Convert.ToInt32(dr["ApplyNum"]),
                    ApplyTime = Convert.ToDateTime(dr["ApplyTime"]),
                    IsAudit = Convert.ToInt32(dr["IsAudit"]),
                    UserId = Convert.ToInt32(dr["UserId"]),
                    UserName = dr["RealName"] == DBNull.Value ? "" : Convert.ToString(dr["RealName"]),
                    TimeStamp = Convert.ToDateTime(dr["Ts"]),
                    AuditUserId = dr["Auditor"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Auditor"]),
                    AuditUserIdName = dr["UserName"] == DBNull.Value ? "" : Convert.ToString(dr["UserName"]),
                };
            }

            return inviteApply;
        }


        public List<InviteApply> GetInviteApplyRecord(ref int total, string realname, string cellphone, int auditstatus,
            string sortBy, SortAction sortOrder, int pageIndex, int pageSize)
        {
            total = 0;
            List<InviteApply> lstApplys = new List<InviteApply>();
            string sql = @" (select ia.*,m.RealName,m.CellPhone,u.UserName from aspnet_inviteapply ia
                            inner join aspnet_Members m on ia.userid = m.UserId
                            left join aspnet_Managers u on ia.Auditor=u.UserId) p ";
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(realname))
            {
                where += " and realname like '%" + realname + "%' ";
            }
            if (!string.IsNullOrEmpty(cellphone))
            {
                where += " and cellphone = '" + cellphone + "' ";
            }
            if (auditstatus > -1)
            {
                where += " and isaudit = '" + auditstatus + "' ";
            }

            var ret = DataHelper.PagingByRownumber(pageIndex, pageSize, sortBy, sortOrder, true,
                 sql, "applyid", where, "*");
            if (ret != null)
            {
                total = ret.TotalRecords;
                if (ret.Data != null)
                {
                    var dt = ret.Data as DataTable;
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            lstApplys.Add(new InviteApply()
                            {
                                ApplyId = Convert.ToInt32(dr["ApplyId"]),
                                ApplyNum = Convert.ToInt32(dr["ApplyNum"]),
                                ApplyTime = Convert.ToDateTime(dr["ApplyTime"]),
                                IsAudit = Convert.ToInt32(dr["IsAudit"]),
                                UserId = Convert.ToInt32(dr["UserId"]),
                                UserName = dr["RealName"] == DBNull.Value ? "" : Convert.ToString(dr["RealName"]),
                                TimeStamp = Convert.ToDateTime(dr["Ts"]),
                                AuditUserId = dr["Auditor"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Auditor"]),
                                AuditUserIdName = dr["UserName"] == DBNull.Value ? "" : Convert.ToString(dr["UserName"]),
                            });
                        }
                    }
                }

            }
            return lstApplys;

        }

        /// <summary>
        /// 增加次数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool UpdateInvitationNum(int userId, int num)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("update aspnet_Distributors set InvitationNum=isnull(InvitationNum,0)+@InvitationNum where userId = @userId");
            this.database.AddInParameter(sqlStringCommand, "userId", DbType.Int32, userId);
            this.database.AddInParameter(sqlStringCommand, "InvitationNum", DbType.Int32, num);
            try
            {
                return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
            }
            catch
            {
                return false;
            }
        }

        public DistributorTree GetDistributorList()
        {
            DistributorTree model = new DistributorTree();
            model.UserId = 0;
            model.ParentId = -1;
            model.UserName = "分销商结构图";

            StringBuilder builder = new StringBuilder();
            builder.Append(@"select d.UserId,m.UserHead,m.UserName,StoreName,d.ReferralUserId, CASE WHEN d.IsTempStore = 1 THEN '钻石会员' ELSE dg.Name END AS GradeName from aspnet_Distributors d
                            inner join aspnet_Members m on d.UserId=m.UserId left outer join aspnet_DistributorGrade as dg on d.DistributorGradeId = dg.GradeId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());

            var dt = this.database.ExecuteDataSet(sqlStringCommand).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                model.Childs = GetChildDistributor(dt, 0);
            }



            return model;
        }

        private List<DistributorTree> GetChildDistributor(DataTable data, int parentId)
        {
            List<DistributorTree> lst = null;
            var parent = data.Select("ReferralUserId=" + parentId, "UserId");
            if (parent != null && parent.Length > 0)
            {
                lst = new List<DistributorTree>();

                for (int i = 0; i < parent.Length; i++)
                {
                    lst.Add(new DistributorTree()
                    {
                        UserId = Convert.ToInt32(parent[i]["UserId"]),
                        UserName = parent[i]["UserName"] == DBNull.Value ? "" : parent[i]["UserName"].ToString(),
                        UserHead = parent[i]["UserHead"] == DBNull.Value ? "" : parent[i]["UserHead"].ToString(),
                        ParentId = Convert.ToInt32(parent[i]["ReferralUserId"]),
                        StoreName = parent[i]["StoreName"] == DBNull.Value ? "" : parent[i]["StoreName"].ToString(),
                        GradeName = parent[i]["GradeName"] == DBNull.Value ? "" : parent[i]["GradeName"].ToString(),
                        Childs = GetChildDistributor(data, Convert.ToInt32(parent[i]["UserId"]))
                    });
                }
            }
            return lst;
        }


    }
}

