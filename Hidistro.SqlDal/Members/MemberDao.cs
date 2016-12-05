namespace Hidistro.SqlDal.Members
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities;
    using Hidistro.Entities.Members;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class MemberDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public bool CreateMember(MemberInfo member)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO aspnet_Members(GradeId,ReferralUserId,UserName,CreateDate,OrderNumber,Expenditure,Points,TopRegionId, RegionId,OpenId, SessionId, SessionEndTime,Password,UserHead,VirtualPoints, IsStore) VALUES(@GradeId,@ReferralUserId,@UserName,@CreateDate,0,0,0,0,0,@OpenId, @SessionId, @SessionEndTime,@Password,@UserHead,0, 0)");
            this.database.AddInParameter(sqlStringCommand, "GradeId", DbType.Int32, member.GradeId);
            this.database.AddInParameter(sqlStringCommand, "ReferralUserId", DbType.Int32, member.ReferralUserId);
            this.database.AddInParameter(sqlStringCommand, "UserName", DbType.String, member.UserName);
            this.database.AddInParameter(sqlStringCommand, "CreateDate", DbType.DateTime, member.CreateDate);
            this.database.AddInParameter(sqlStringCommand, "OpenId", DbType.String, member.OpenId);
            this.database.AddInParameter(sqlStringCommand, "SessionId", DbType.String, member.SessionId);
            this.database.AddInParameter(sqlStringCommand, "SessionEndTime", DbType.DateTime, member.SessionEndTime);
            this.database.AddInParameter(sqlStringCommand, "Password", DbType.String, member.Password);
            this.database.AddInParameter(sqlStringCommand, "UserHead", DbType.String, member.UserHead);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool Delete(int userId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM aspnet_Members WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public string GetCurrentParentUserId(int? userId)
        {
            string str = "";
            string query = "SELECT ReferralPath FROM aspnet_Distributors WHERE UserId=@UserId";
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(query);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int64, userId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                str = userId.ToString();
                if (reader["ReferralUserId"].ToString() != "0")
                {
                    str = reader["ReferralUserId"].ToString() + "|" + userId.ToString();
                }
            }
            return str;
        }

        public MemberInfo GetMember(int userId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM aspnet_Members WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<MemberInfo>(reader);
            }
        }

        public MemberInfo GetReferralMember(int userId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM aspnet_Members WHERE ReferralUserId = @ReferralUserId");
            this.database.AddInParameter(sqlStringCommand, "ReferralUserId", DbType.Int32, userId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<MemberInfo>(reader);
            }
        }

        public MemberInfo GetMember(string sessionId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM aspnet_Members WHERE SessionId = @SessionId");
            this.database.AddInParameter(sqlStringCommand, "SessionId", DbType.String, sessionId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<MemberInfo>(reader);
            }
        }

        public Dictionary<int, MemberClientSet> GetMemberClientSet()
        {
            Dictionary<int, MemberClientSet> dictionary = new Dictionary<int, MemberClientSet>();
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_MemberClientSet");
            MemberClientSet set = null;
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    set = DataMapper.PopulateMemberClientSet(reader);
                    dictionary.Add(set.ClientTypeId, set);
                }
            }
            return dictionary;
        }

        public DbQueryResult GetMembers(MemberQuery query)
        {
            object obj2;
            StringBuilder builder = new StringBuilder();
            if (query.HasVipCard.HasValue)
            {
                if (query.HasVipCard.Value)
                {
                    builder.Append("VipCardNumber is not null");
                }
                else
                {
                    builder.Append("VipCardNumber is null");
                }
            }
            if (query.GradeId.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("GradeId = {0}", query.GradeId.Value);
            }
            if (query.IsApproved.HasValue)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("IsApproved = '{0}'", query.IsApproved.Value);
            }
            if (!string.IsNullOrEmpty(query.Username))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }
                builder.AppendFormat("UserName LIKE '%{0}%'", DataHelper.CleanSearchString(query.Username));
            }
            if (!string.IsNullOrEmpty(query.Realname))
            {
                if (builder.Length > 0)
                {
                    builder.AppendFormat(" AND ", new object[0]);
                }
                builder.AppendFormat("RealName LIKE '%{0}%'", DataHelper.CleanSearchString(query.Realname));
            }
            string str = "";
            if (!string.IsNullOrEmpty(query.ClientType))
            {
                string clientType = query.ClientType;
                if (clientType == null)
                {
                    goto Label_0533;
                }
                if (!(clientType == "new"))
                {
                    if (clientType == "activy")
                    {
                        str = "SELECT UserId FROM Hishop_Orders WHERE 1=1";
                        if (query.OrderNumber.HasValue)
                        {
                            obj2 = str;
                            obj2 = string.Concat(new object[] { obj2, " AND datediff(dd,OrderDate,'", query.StartTime.Value.Date, "')<=0" });
                            obj2 = string.Concat(new object[] { obj2, " AND datediff(dd,OrderDate,'", query.EndTime.Value.Date, "')>=0" });
                            str = string.Concat(new object[] { obj2, " GROUP BY UserId HAVING COUNT(*)", query.CharSymbol, query.OrderNumber.Value });
                        }
                        if (query.OrderMoney.HasValue)
                        {
                            obj2 = str;
                            obj2 = string.Concat(new object[] { obj2, " AND datediff(dd,OrderDate,'", query.StartTime.Value.Date, "')<=0" });
                            obj2 = string.Concat(new object[] { obj2, " AND datediff(dd,OrderDate,'", query.EndTime.Value.Date, "')>=0" });
                            str = string.Concat(new object[] { obj2, " GROUP BY UserId HAVING SUM(OrderTotal)", query.CharSymbol, query.OrderMoney.Value });
                        }
                        if (builder.Length > 0)
                        {
                            builder.AppendFormat(" AND ", new object[0]);
                        }
                        builder.AppendFormat("UserId IN (" + str + ")", new object[0]);
                        goto Label_0621;
                    }
                    goto Label_0533;
                }
                str = "SElECT UserId FROM aspnet_Members WHERE 1=1";
                if (query.StartTime.HasValue)
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, " AND datediff(dd,CreateDate,'", query.StartTime.Value.Date, "')<=0" });
                }
                if (query.EndTime.HasValue)
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, " AND datediff(dd,CreateDate,'", query.EndTime.Value.Date, "')>=0" });
                }
                if (builder.Length > 0)
                {
                    builder.AppendFormat(" AND ", new object[0]);
                }
                builder.Append("UserId IN (" + str + ")");
            }
            goto Label_0621;
        Label_0533:
            str = "SELECT UserId FROM Hishop_Orders WHERE 1=1";
            obj2 = str;
            obj2 = string.Concat(new object[] { obj2, " AND datediff(dd,OrderDate,'", query.StartTime.Value.Date, "')<=0" });
            str = string.Concat(new object[] { obj2, " AND datediff(dd,OrderDate,'", query.EndTime.Value.Date, "')>=0" }) + " GROUP BY UserId";
            if (builder.Length > 0)
            {
                builder.AppendFormat(" AND ", new object[0]);
            }
            builder.AppendFormat("UserId NOT IN (" + str + ")", new object[0]);
        Label_0621:
            return DataHelper.PagingByRownumber(query.PageIndex, query.PageSize, query.SortBy, query.SortOrder, query.IsCount, "aspnet_Members m", "UserId", (builder.Length > 0) ? builder.ToString() : null, "*, (SELECT Name FROM aspnet_MemberGrades WHERE GradeId = m.GradeId) AS GradeName");
        }

        public IList<MemberInfo> GetMembersByRank(int? gradeId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM aspnet_Members");
            if (gradeId.HasValue && (gradeId.Value > 0))
            {
                sqlStringCommand.CommandText = sqlStringCommand.CommandText + string.Format(" WHERE GradeId={0}", gradeId.Value);
            }
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<MemberInfo>(reader);
            }
        }

        public DataTable GetMembersNopage(MemberQuery query, IList<string> fields)
        {
            if (fields.Count == 0)
            {
                return null;
            }
            DataTable table = null;
            string str = string.Empty;
            foreach (string str2 in fields)
            {
                str = str + str2 + ",";
            }
            str = str.Substring(0, str.Length - 1);
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT {0} FROM aspnet_Members WHERE 1=1 ", str);
            if (!string.IsNullOrEmpty(query.Username))
            {
                builder.AppendFormat(" AND UserName LIKE '%{0}%'", query.Username);
            }
            if (query.GradeId.HasValue)
            {
                builder.AppendFormat(" AND GradeId={0}", query.GradeId);
            }
            if (query.HasVipCard.HasValue)
            {
                if (query.HasVipCard.Value)
                {
                    builder.Append(" AND VipCardNumber is not null");
                }
                else
                {
                    builder.Append(" AND VipCardNumber is null");
                }
            }
            if (!string.IsNullOrEmpty(query.Realname))
            {
                builder.AppendFormat(" AND Realname LIKE '%{0}%'", query.Realname);
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                table = DataHelper.ConverDataReaderToDataTable(reader);
                reader.Close();
            }
            return table;
        }

        public IList<MemberInfo> GetMemdersByCardNumbers(string cards)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM aspnet_Members WHERE VipCardNumber IN ({0})", cards));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<MemberInfo>(reader);
            }
        }

        public IList<MemberInfo> GetMemdersByOpenIds(string openids)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(string.Format("SELECT * FROM aspnet_Members where openid IN ({0})", openids));
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<MemberInfo>(reader);
            }
        }

        public MemberInfo GetOpenIdMember(string openId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM aspnet_Members WHERE openId = @openId");
            this.database.AddInParameter(sqlStringCommand, "openId", DbType.String, openId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<MemberInfo>(reader);
            }
        }

        public MemberInfo GetusernameMember(string username)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM aspnet_Members WHERE username = @username");
            this.database.AddInParameter(sqlStringCommand, "username", DbType.String, username);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<MemberInfo>(reader);
            }
        }

        public bool InsertClientSet(Dictionary<int, MemberClientSet> clientsets)
        {
            StringBuilder builder = new StringBuilder("DELETE FROM  [Hishop_MemberClientSet];");
            foreach (KeyValuePair<int, MemberClientSet> pair in clientsets)
            {
                string str = "";
                string str2 = "";
                if (pair.Value.StartTime.HasValue)
                {
                    str = pair.Value.StartTime.Value.ToString("yyyy-MM-dd");
                }
                if (pair.Value.EndTime.HasValue)
                {
                    str2 = pair.Value.EndTime.Value.ToString("yyyy-MM-dd");
                }
                builder.AppendFormat(string.Concat(new object[] { "INSERT INTO Hishop_MemberClientSet(ClientTypeId,StartTime,EndTime,LastDay,ClientChar,ClientValue) VALUES (", pair.Key, ",'", str, "','", str2, "',", pair.Value.LastDay, ",'", pair.Value.ClientChar, "',", pair.Value.ClientValue, ");" }), new object[0]);
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool IsExitOpenId(string openId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT Count(*) FROM aspnet_Members WHERE OpenId = @OpenId and OpenId!=null");
            this.database.AddInParameter(sqlStringCommand, "OpenId", DbType.String, openId);
            return (((int) this.database.ExecuteScalar(sqlStringCommand)) > 0);
        }

        public bool SetMemberSessionId(string sessionId, DateTime sessionEndTime, string openId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Members SET SessionId = @SessionId, SessionEndTime = @SessionEndTime WHERE OpenId = @OpenId");
            this.database.AddInParameter(sqlStringCommand, "SessionId", DbType.String, sessionId);
            this.database.AddInParameter(sqlStringCommand, "SessionEndTime", DbType.DateTime, sessionEndTime);
            this.database.AddInParameter(sqlStringCommand, "OpenId", DbType.String, openId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool SetPwd(string userid, string pwd)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Members SET Password = @Password  WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "Password", DbType.String, pwd);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userid);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool Update(MemberInfo member)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Members SET GradeId = @GradeId,OpenId = @OpenId,UserName = @UserName, RealName = @RealName, TopRegionId = @TopRegionId, RegionId = @RegionId,VipCardNumber = @VipCardNumber, VipCardDate = @VipCardDate, Email = @Email, CellPhone = @CellPhone, QQ = @QQ, Address = @Address, Expenditure = @Expenditure, OrderNumber = @OrderNumber,MicroSignal=@MicroSignal,UserHead=@UserHead,ReferralUserId=@ReferralUserId, VirtualPoints = @VirtualPoints WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, member.UserId);
            this.database.AddInParameter(sqlStringCommand, "GradeId", DbType.Int32, member.GradeId);
            this.database.AddInParameter(sqlStringCommand, "OpenId", DbType.String, member.OpenId);
            this.database.AddInParameter(sqlStringCommand, "UserName", DbType.String, member.UserName);
            this.database.AddInParameter(sqlStringCommand, "RealName", DbType.String, member.RealName);
            this.database.AddInParameter(sqlStringCommand, "TopRegionId", DbType.Int32, member.TopRegionId);
            this.database.AddInParameter(sqlStringCommand, "RegionId", DbType.Int32, member.RegionId);
            this.database.AddInParameter(sqlStringCommand, "Email", DbType.String, member.Email);
            this.database.AddInParameter(sqlStringCommand, "VipCardNumber", DbType.String, member.VipCardNumber);
            this.database.AddInParameter(sqlStringCommand, "VipCardDate", DbType.DateTime, member.VipCardDate);
            this.database.AddInParameter(sqlStringCommand, "CellPhone", DbType.String, member.CellPhone);
            this.database.AddInParameter(sqlStringCommand, "QQ", DbType.String, member.QQ);
            this.database.AddInParameter(sqlStringCommand, "Address", DbType.String, member.Address);
            this.database.AddInParameter(sqlStringCommand, "Expenditure", DbType.Currency, member.Expenditure);
            this.database.AddInParameter(sqlStringCommand, "OrderNumber", DbType.Int32, member.OrderNumber);
            this.database.AddInParameter(sqlStringCommand, "MicroSignal", DbType.String, member.MicroSignal);
            this.database.AddInParameter(sqlStringCommand, "UserHead", DbType.String, member.UserHead);
            this.database.AddInParameter(sqlStringCommand, "ReferralUserId", DbType.Int32, member.ReferralUserId);
            this.database.AddInParameter(sqlStringCommand, "VirtualPoints", DbType.Decimal, member.VirtualPoints);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateOpenid(MemberInfo member)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Members SET OpenId=@OpenId,UserHead=@UserHead WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, member.UserId);
            this.database.AddInParameter(sqlStringCommand, "OpenId", DbType.String, member.OpenId);
            this.database.AddInParameter(sqlStringCommand, "UserHead", DbType.String, member.UserHead);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateUserNameAndPhone(string userid, string referralUserId, string userName, string realName, string phone)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Members SET IsStore = 1, UserName = @UserName, RealName = @RealName, CellPhone = @CellPhone, ReferralUserId = @ReferralUserId WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserName", DbType.String, userName);
            this.database.AddInParameter(sqlStringCommand, "RealName", DbType.String, realName);
            this.database.AddInParameter(sqlStringCommand, "CellPhone", DbType.String, phone);
            this.database.AddInParameter(sqlStringCommand, "ReferralUserId", DbType.String, referralUserId);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userid);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateVerifyCode(string userid, string verifyCode)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Members SET VerifyCode = @VerifyCode,VerifyCodeTime=@VerifyCodeTime WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "VerifyCodeTime", DbType.DateTime, DateTime.Now);
            this.database.AddInParameter(sqlStringCommand, "VerifyCode", DbType.String, verifyCode);
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userid);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
        /// <summary>
        /// 获取最后一条验证码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="timeoutMins">验证码过期时间(分)</param>
        /// <returns></returns>
        public string GetLastVerifyCode(int userId, int timeoutMins)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("select verifycode from aspnet_Members WHERE UserId = @UserId and DATEDIFF(mi,verifycodetime,@NowTime)<=@TimeoutMins");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            this.database.AddInParameter(sqlStringCommand, "NowTime", DbType.DateTime, DateTime.Now);
            this.database.AddInParameter(sqlStringCommand, "TimeoutMins", DbType.Int32, timeoutMins);
            var ret = this.database.ExecuteScalar(sqlStringCommand);
            return ret == null ? "" : ret.ToString();
        }

        public bool UpdateVirtualPoints(int userId, decimal points)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Members SET VirtualPoints = ISNULL(VirtualPoints, 0) - @Points WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            this.database.AddInParameter(sqlStringCommand, "Points", DbType.Decimal, points);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool AddUserVirtualPoints(int userId, decimal points)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Members SET VirtualPoints = ISNULL(VirtualPoints, 0) + @Points WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            this.database.AddInParameter(sqlStringCommand, "Points", DbType.Decimal, points);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public bool UpdateUserIsStore(int userId, int isStore)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE aspnet_Members SET IsStore = @IsStore WHERE UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int32, userId);
            this.database.AddInParameter(sqlStringCommand, "IsStore", DbType.Int32, isStore);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        
    }
}

