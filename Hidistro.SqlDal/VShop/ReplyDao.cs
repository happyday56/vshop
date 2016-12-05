namespace Hidistro.SqlDal.VShop
{
    using Hidistro.Entities;
    using Hidistro.Entities.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ReplyDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public void DeleteNewsMsg(int id)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(new StringBuilder(" delete from vshop_Message where MsgID=@MsgID").ToString());
            this.database.AddInParameter(sqlStringCommand, "MsgID", DbType.Int32, id);
            this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool DeleteReply(int id)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE vshop_Reply WHERE ReplyId = @ReplyId;DELETE vshop_Message WHERE ReplyId = @ReplyId");
            this.database.AddInParameter(sqlStringCommand, "ReplyId", DbType.Int32, id);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        public IList<ReplyInfo> GetAllReply()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ReplyId,Keys,MatchType,ReplyType,MessageType,IsDisable,LastEditDate,LastEditor,Content,Type,ActivityId");
            builder.Append(" FROM vshop_Reply order by Replyid desc ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            List<ReplyInfo> list = new List<ReplyInfo>();
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    object obj2;
                    ReplyInfo info = this.ReaderBind(reader);
                    switch (info.MessageType)
                    {
                        case MessageType.Text:
                        {
                            TextReplyInfo info3 = info as TextReplyInfo;
                            obj2 = reader["Content"];
                            if ((obj2 != null) && (obj2 != DBNull.Value))
                            {
                                info3.Text = obj2.ToString();
                            }
                            list.Add(info3);
                            continue;
                        }
                        case MessageType.News:
                        case MessageType.List:
                        {
                            NewsReplyInfo info2 = info as NewsReplyInfo;
                            info2.NewsMsg = this.GetNewsReplyInfo(info2.Id);
                            list.Add(info2);
                            continue;
                        }
                    }
                    TextReplyInfo item = info as TextReplyInfo;
                    obj2 = reader["Content"];
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        item.Text = obj2.ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public MessageInfo GetMessage(int messageId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Vshop_Message WHERE MsgID =@MsgID");
            this.database.AddInParameter(sqlStringCommand, "MsgID", DbType.Int32, messageId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToModel<MessageInfo>(reader);
            }
        }

        public IList<NewsMsgInfo> GetNewsReplyInfo(int replyid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ReplyId,MsgID,Title,ImageUrl,Url,Description,Content from vshop_Message ");
            builder.Append(" where ReplyId=@ReplyId ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ReplyId", DbType.Int32, replyid);
            List<NewsMsgInfo> list = new List<NewsMsgInfo>();
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBindNewsRelpy(reader));
                }
            }
            return list;
        }

        public IList<ReplyInfo> GetReplies(ReplyType type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ReplyId,Keys,MatchType,ReplyType,MessageType,IsDisable,LastEditDate,LastEditor,Content,Type,ActivityId ");
            builder.Append(" FROM vshop_Reply ");
            builder.Append(" where ReplyType & @ReplyType = @ReplyType and IsDisable=0");
            builder.Append(" order by replyid desc");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, (int) type);
            List<ReplyInfo> list = new List<ReplyInfo>();
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    TextReplyInfo info3;
                    object obj2;
                    ReplyInfo info = this.ReaderBind(reader);
                    switch (info.MessageType)
                    {
                        case MessageType.Text:
                        {
                            info3 = info as TextReplyInfo;
                            obj2 = reader["Content"];
                            if ((obj2 != null) && (obj2 != DBNull.Value))
                            {
                                info3.Text = obj2.ToString();
                            }
                            list.Add(info3);
                            continue;
                        }
                        case MessageType.News:
                        case MessageType.List:
                        {
                            NewsReplyInfo item = info as NewsReplyInfo;
                            item.NewsMsg = this.GetNewsReplyInfo(item.Id);
                            list.Add(item);
                            continue;
                        }
                    }
                    info3 = info as TextReplyInfo;
                    obj2 = reader["Content"];
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        info3.Text = obj2.ToString();
                    }
                    list.Add(info3);
                }
            }
            return list;
        }

        public ReplyInfo GetReply(int id)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM vshop_Reply WHERE ReplyId = @ReplyId");
            this.database.AddInParameter(sqlStringCommand, "ReplyId", DbType.Int32, id);
            ReplyInfo info = null;
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = this.ReaderBind(reader);
                    switch (info.MessageType)
                    {
                        case MessageType.Text:
                        {
                            TextReplyInfo info3 = info as TextReplyInfo;
                            object obj2 = reader["Content"];
                            if ((obj2 != null) && (obj2 != DBNull.Value))
                            {
                                info3.Text = obj2.ToString();
                            }
                            return info3;
                        }
                        case MessageType.News:
                        case MessageType.List:
                        {
                            NewsReplyInfo info2 = info as NewsReplyInfo;
                            info2.NewsMsg = this.GetNewsReplyInfo(info2.Id);
                            return info2;
                        }
                    }
                }
                return info;
            }
        }

        public bool HasReplyKey(string key)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT COUNT(*) FROM vshop_Reply WHERE Keys = @Keys");
            this.database.AddInParameter(sqlStringCommand, "Keys", DbType.String, key);
            return (Convert.ToInt32(this.database.ExecuteScalar(sqlStringCommand)) > 0);
        }

        public ReplyInfo ReaderBind(IDataReader dataReader)
        {
            ReplyInfo info = null;
            object obj2 = dataReader["MessageType"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                if (((MessageType) obj2) == MessageType.Text)
                {
                    info = new TextReplyInfo();
                }
                else
                {
                    info = new NewsReplyInfo();
                }
            }
            obj2 = dataReader["ReplyId"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Id = (int) obj2;
            }
            info.Keys = dataReader["Keys"].ToString();
            obj2 = dataReader["MatchType"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.MatchType = (MatchType) obj2;
            }
            obj2 = dataReader["ReplyType"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.ReplyType = (ReplyType) obj2;
            }
            obj2 = dataReader["MessageType"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.MessageType = (MessageType) obj2;
            }
            obj2 = dataReader["IsDisable"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.IsDisable = (bool) obj2;
            }
            obj2 = dataReader["LastEditDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.LastEditDate = (DateTime) obj2;
            }
            info.LastEditor = dataReader["LastEditor"].ToString();
            obj2 = dataReader["ActivityId"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.ActivityId = (int) obj2;
            }
            return info;
        }

        private NewsMsgInfo ReaderBindNewsRelpy(IDataReader dataReader)
        {
            NewsMsgInfo info = new NewsMsgInfo();
            object obj2 = dataReader["MsgID"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Id = (int) obj2;
            }
            obj2 = dataReader["Title"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Title = dataReader["Title"].ToString();
            }
            obj2 = dataReader["ImageUrl"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.PicUrl = dataReader["ImageUrl"].ToString();
            }
            obj2 = dataReader["Url"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Url = dataReader["Url"].ToString();
            }
            obj2 = dataReader["Description"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Description = dataReader["Description"].ToString();
            }
            obj2 = dataReader["Content"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                info.Content = dataReader["Content"].ToString();
            }
            return info;
        }

        private bool SaveNewsReply(NewsReplyInfo model)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into vshop_Reply(");
            builder.Append("Keys,MatchType,ReplyType,MessageType,IsDisable,LastEditDate,LastEditor,Content,Type)");
            builder.Append(" values (");
            builder.Append("@Keys,@MatchType,@ReplyType,@MessageType,@IsDisable,@LastEditDate,@LastEditor,@Content,@Type)");
            builder.Append(";select @@IDENTITY");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "Keys", DbType.String, model.Keys);
            this.database.AddInParameter(sqlStringCommand, "MatchType", DbType.Int32, (int) model.MatchType);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, (int) model.ReplyType);
            this.database.AddInParameter(sqlStringCommand, "MessageType", DbType.Int32, (int) model.MessageType);
            this.database.AddInParameter(sqlStringCommand, "IsDisable", DbType.Boolean, model.IsDisable);
            this.database.AddInParameter(sqlStringCommand, "LastEditDate", DbType.DateTime, model.LastEditDate);
            this.database.AddInParameter(sqlStringCommand, "LastEditor", DbType.String, model.LastEditor);
            this.database.AddInParameter(sqlStringCommand, "Content", DbType.String, "");
            this.database.AddInParameter(sqlStringCommand, "Type", DbType.Int32, 2);
            if (int.TryParse(this.database.ExecuteScalar(sqlStringCommand).ToString(), out num))
            {
                foreach (NewsMsgInfo info in model.NewsMsg)
                {
                    builder = new StringBuilder();
                    builder.Append("insert into vshop_Message(");
                    builder.Append("ReplyId,Title,ImageUrl,Url,Description,Content)");
                    builder.Append(" values (");
                    builder.Append("@ReplyId,@Title,@ImageUrl,@Url,@Description,@Content)");
                    sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
                    this.database.AddInParameter(sqlStringCommand, "ReplyId", DbType.Int32, num);
                    this.database.AddInParameter(sqlStringCommand, "Title", DbType.String, info.Title);
                    this.database.AddInParameter(sqlStringCommand, "ImageUrl", DbType.String, info.PicUrl);
                    this.database.AddInParameter(sqlStringCommand, "Url", DbType.String, info.Url);
                    this.database.AddInParameter(sqlStringCommand, "Description", DbType.String, info.Description);
                    this.database.AddInParameter(sqlStringCommand, "Content", DbType.String, info.Content);
                    this.database.ExecuteNonQuery(sqlStringCommand);
                }
            }
            return true;
        }

        public bool SaveReply(ReplyInfo reply)
        {
            bool flag = false;
            switch (reply.MessageType)
            {
                case MessageType.Text:
                    return this.SaveTextReply(reply as TextReplyInfo);

                case MessageType.News:
                case MessageType.List:
                    return this.SaveNewsReply(reply as NewsReplyInfo);

                case (MessageType.News | MessageType.Text):
                    return flag;
            }
            return flag;
        }

        private bool SaveTextReply(TextReplyInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into vshop_Reply(");
            builder.Append("Keys,MatchType,ReplyType,MessageType,IsDisable,LastEditDate,LastEditor,Content,Type,ActivityId)");
            builder.Append(" values (");
            builder.Append("@Keys,@MatchType,@ReplyType,@MessageType,@IsDisable,@LastEditDate,@LastEditor,@Content,@Type,@ActivityId)");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "Keys", DbType.String, model.Keys);
            this.database.AddInParameter(sqlStringCommand, "MatchType", DbType.Int32, (int) model.MatchType);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, (int) model.ReplyType);
            this.database.AddInParameter(sqlStringCommand, "MessageType", DbType.Int32, (int) model.MessageType);
            this.database.AddInParameter(sqlStringCommand, "IsDisable", DbType.Boolean, model.IsDisable);
            this.database.AddInParameter(sqlStringCommand, "LastEditDate", DbType.DateTime, model.LastEditDate);
            this.database.AddInParameter(sqlStringCommand, "LastEditor", DbType.String, model.LastEditor);
            this.database.AddInParameter(sqlStringCommand, "Content", DbType.String, model.Text);
            this.database.AddInParameter(sqlStringCommand, "Type", DbType.Int32, 1);
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, model.ActivityId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        private bool UpdateNewsReply(NewsReplyInfo reply)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update vshop_Reply set ");
            builder.Append("Keys=@Keys,");
            builder.Append("MatchType=@MatchType,");
            builder.Append("ReplyType=@ReplyType,");
            builder.Append("MessageType=@MessageType,");
            builder.Append("IsDisable=@IsDisable,");
            builder.Append("LastEditDate=@LastEditDate,");
            builder.Append("LastEditor=@LastEditor,");
            builder.Append("Content=@Content,");
            builder.Append("Type=@Type");
            builder.Append(" where ReplyId=@ReplyId;delete from vshop_Message ");
            builder.Append(" where ReplyId=@ReplyId ");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "Keys", DbType.String, reply.Keys);
            this.database.AddInParameter(sqlStringCommand, "MatchType", DbType.Int32, (int) reply.MatchType);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, (int) reply.ReplyType);
            this.database.AddInParameter(sqlStringCommand, "MessageType", DbType.Int32, (int) reply.MessageType);
            this.database.AddInParameter(sqlStringCommand, "IsDisable", DbType.Boolean, reply.IsDisable);
            this.database.AddInParameter(sqlStringCommand, "LastEditDate", DbType.DateTime, reply.LastEditDate);
            this.database.AddInParameter(sqlStringCommand, "LastEditor", DbType.String, reply.LastEditor);
            this.database.AddInParameter(sqlStringCommand, "Content", DbType.String, "");
            this.database.AddInParameter(sqlStringCommand, "Type", DbType.Int32, 2);
            this.database.AddInParameter(sqlStringCommand, "ReplyId", DbType.Int32, reply.Id);
            this.database.ExecuteNonQuery(sqlStringCommand);
            foreach (NewsMsgInfo info in reply.NewsMsg)
            {
                builder = new StringBuilder();
                builder.Append("insert into vshop_Message(");
                builder.Append("ReplyId,Title,ImageUrl,Url,Description,Content)");
                builder.Append(" values (");
                builder.Append("@ReplyId,@Title,@ImageUrl,@Url,@Description,@Content)");
                sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
                this.database.AddInParameter(sqlStringCommand, "ReplyId", DbType.Int32, reply.Id);
                this.database.AddInParameter(sqlStringCommand, "Title", DbType.String, info.Title);
                this.database.AddInParameter(sqlStringCommand, "ImageUrl", DbType.String, info.PicUrl);
                this.database.AddInParameter(sqlStringCommand, "Url", DbType.String, info.Url);
                this.database.AddInParameter(sqlStringCommand, "Description", DbType.String, info.Description);
                this.database.AddInParameter(sqlStringCommand, "Content", DbType.String, info.Content);
                this.database.ExecuteNonQuery(sqlStringCommand);
            }
            return true;
        }

        public bool UpdateReply(ReplyInfo reply)
        {
            switch (reply.MessageType)
            {
                case MessageType.Text:
                    return this.UpdateTextReply(reply as TextReplyInfo);

                case MessageType.News:
                case MessageType.List:
                    return this.UpdateNewsReply(reply as NewsReplyInfo);
            }
            return this.UpdateTextReply(reply as TextReplyInfo);
        }

        public bool UpdateReplyRelease(int id)
        {
            ReplyInfo reply = this.GetReply(id);
            StringBuilder builder = new StringBuilder();
            if (reply.IsDisable)
            {
                if ((reply.ReplyType & ReplyType.NoMatch) == ReplyType.NoMatch)
                {
                    builder.AppendFormat("update  vshop_Reply set IsDisable = 1 where ReplyType&{0}>0;", 4);
                }
                if ((reply.ReplyType & ReplyType.Subscribe) == ReplyType.Subscribe)
                {
                    builder.AppendFormat("update  vshop_Reply set IsDisable = 1 where ReplyType&{0}>0;", 1);
                }
            }
            builder.Append("update vshop_Reply set ");
            builder.Append("IsDisable=~IsDisable");
            builder.Append(" where ReplyId=@ReplyId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "ReplyId", DbType.Int32, id);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }

        private bool UpdateTextReply(TextReplyInfo reply)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update vshop_Reply set ");
            builder.Append("Keys=@Keys,");
            builder.Append("MatchType=@MatchType,");
            builder.Append("ReplyType=@ReplyType,");
            builder.Append("MessageType=@MessageType,");
            builder.Append("IsDisable=@IsDisable,");
            builder.Append("LastEditDate=@LastEditDate,");
            builder.Append("LastEditor=@LastEditor,");
            builder.Append("Content=@Content,");
            builder.Append("Type=@Type,");
            builder.Append("ActivityId=@ActivityId");
            builder.Append(" where ReplyId=@ReplyId");
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand(builder.ToString());
            this.database.AddInParameter(sqlStringCommand, "Keys", DbType.String, reply.Keys);
            this.database.AddInParameter(sqlStringCommand, "MatchType", DbType.Int32, (int) reply.MatchType);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, (int) reply.ReplyType);
            this.database.AddInParameter(sqlStringCommand, "MessageType", DbType.Int32, (int) reply.MessageType);
            this.database.AddInParameter(sqlStringCommand, "IsDisable", DbType.Boolean, reply.IsDisable);
            this.database.AddInParameter(sqlStringCommand, "LastEditDate", DbType.DateTime, reply.LastEditDate);
            this.database.AddInParameter(sqlStringCommand, "LastEditor", DbType.String, reply.LastEditor);
            this.database.AddInParameter(sqlStringCommand, "Content", DbType.String, reply.Text);
            this.database.AddInParameter(sqlStringCommand, "Type", DbType.Int32, 2);
            this.database.AddInParameter(sqlStringCommand, "ActivityId", DbType.Int32, reply.ActivityId);
            this.database.AddInParameter(sqlStringCommand, "ReplyId", DbType.Int32, reply.Id);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

