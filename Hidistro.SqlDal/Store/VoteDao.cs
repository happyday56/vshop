namespace Hidistro.SqlDal.Store
{
    using Hidistro.Core;
    using Hidistro.Entities;
    using Hidistro.Entities.Store;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;

    public class VoteDao
    {
        private Database database = DatabaseFactory.CreateDatabase();

        public long CreateVote(VoteInfo vote)
        {
            DbCommand storedProcCommand = this.database.GetStoredProcCommand("cp_Votes_Create");
            if (vote.StartDate > vote.EndDate)
            {
                DateTime startDate = vote.StartDate;
                vote.StartDate = vote.EndDate;
                vote.EndDate = startDate;
            }
            this.database.AddInParameter(storedProcCommand, "VoteName", DbType.String, vote.VoteName);
            this.database.AddInParameter(storedProcCommand, "IsBackup", DbType.Boolean, vote.IsBackup);
            this.database.AddInParameter(storedProcCommand, "MaxCheck", DbType.Int32, vote.MaxCheck);
            this.database.AddInParameter(storedProcCommand, "ImageUrl", DbType.String, vote.ImageUrl);
            this.database.AddInParameter(storedProcCommand, "StartDate", DbType.DateTime, vote.StartDate);
            this.database.AddInParameter(storedProcCommand, "EndDate", DbType.DateTime, vote.EndDate);
            this.database.AddInParameter(storedProcCommand, "Keys", DbType.String, vote.Keys);
            this.database.AddOutParameter(storedProcCommand, "VoteId", DbType.Int64, 8);
            long parameterValue = 0L;
            if (this.database.ExecuteNonQuery(storedProcCommand) > 0)
            {
                parameterValue = (long) this.database.GetParameterValue(storedProcCommand, "VoteId");
            }
            return parameterValue;
        }

        public int CreateVoteItem(VoteItemInfo voteItem, DbTransaction dbTran)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("INSERT INTO Hishop_VoteItems(VoteId, VoteItemName, ItemCount) Values(@VoteId, @VoteItemName, @ItemCount)");
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, voteItem.VoteId);
            this.database.AddInParameter(sqlStringCommand, "VoteItemName", DbType.String, voteItem.VoteItemName);
            this.database.AddInParameter(sqlStringCommand, "ItemCount", DbType.Int32, voteItem.ItemCount);
            if (dbTran == null)
            {
                return this.database.ExecuteNonQuery(sqlStringCommand);
            }
            return this.database.ExecuteNonQuery(sqlStringCommand, dbTran);
        }

        public int DeleteVote(long voteId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_Votes WHERE VoteId = @VoteId; DELETE FROM vshop_Reply WHERE ActivityId = @VoteId AND [ReplyType] = @ReplyType");
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, voteId);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x80);
            return this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool DeleteVoteItem(long voteId, DbTransaction dbTran)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("DELETE FROM Hishop_VoteItems WHERE VoteId = @VoteId");
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, voteId);
            return (this.database.ExecuteNonQuery(sqlStringCommand, dbTran) >= 0);
        }

        public VoteInfo GetVoteById(long voteId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT *, (SELECT Keys FROM vshop_Reply WHERE [ReplyType] = @ReplyType AND ActivityId = v.VoteId) AS Keys, (SELECT ISNULL(SUM(ItemCount),0) FROM Hishop_VoteItems WHERE VoteId = v.VoteId) AS VoteCounts FROM Hishop_Votes v WHERE VoteId = @VoteId");
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, voteId);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x80);
            VoteInfo info = null;
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    info = DataMapper.PopulateVote(reader);
                }
            }
            return info;
        }

        public int GetVoteCounts(long voteId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT ISNULL(SUM(ItemCount),0) FROM Hishop_VoteItems WHERE VoteId = @VoteId");
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, voteId);
            return (int) this.database.ExecuteScalar(sqlStringCommand);
        }

        public IList<VoteItemInfo> GetVoteItems(long voteId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_VoteItems WHERE VoteId = @VoteId");
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, voteId);
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<VoteItemInfo>(reader);
            }
        }

        public IList<VoteInfo> GetVoteList()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT * FROM Hishop_Votes ");
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                return ReaderConvert.ReaderToList<VoteInfo>(reader);
            }
        }

        public DataSet GetVotes()
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT *, (SELECT Keys FROM vshop_Reply WHERE [ReplyType] = @ReplyType AND ActivityId = v.VoteId) AS Keys, (SELECT ISNULL(SUM(ItemCount),0) FROM Hishop_VoteItems WHERE VoteId = v.VoteId) AS VoteCounts FROM Hishop_Votes v");
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x80);
            return this.database.ExecuteDataSet(sqlStringCommand);
        }

        public bool IsVote(int voteId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT COUNT(*) FROM Hishop_VoteRecord WHERE VoteId = @VoteId AND UserId = @UserId");
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int64, Globals.GetCurrentMemberUserId());
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, voteId);
            return (((int) this.database.ExecuteScalar(sqlStringCommand)) > 0);
        }

        public DataTable LoadVote(int voteId, out string voteName, out int checkNum, out int voteNum)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("SELECT VoteName, MaxCheck, (SELECT SUM(ItemCount) FROM Hishop_VoteItems WHERE VoteId = @VoteId) AS VoteNum FROM Hishop_Votes WHERE VoteId = @VoteId; SELECT * FROM Hishop_VoteItems WHERE VoteId = @VoteId");
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, voteId);
            voteName = string.Empty;
            checkNum = 1;
            voteNum = 0;
            using (IDataReader reader = this.database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    voteName = (string) reader["VoteName"];
                    checkNum = (int) reader["MaxCheck"];
                    voteNum = (int) reader["VoteNum"];
                }
                reader.NextResult();
                return DataHelper.ConverDataReaderToDataTable(reader);
            }
        }

        public int SetVoteIsBackup(long voteId)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("Update Hishop_Votes Set IsBackup = (~IsBackup) Where VoteId =@VoteId");
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, voteId);
            return this.database.ExecuteNonQuery(sqlStringCommand);
        }

        public bool UpdateVote(VoteInfo vote, DbTransaction dbTran)
        {
            if (vote.StartDate > vote.EndDate)
            {
                DateTime startDate = vote.StartDate;
                vote.StartDate = vote.EndDate;
                vote.EndDate = startDate;
            }
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("UPDATE Hishop_Votes SET VoteName = @VoteName, MaxCheck = @MaxCheck, ImageUrl=@ImageUrl, StartDate=@StartDate, EndDate=@EndDate WHERE VoteId = @VoteId; UPDATE vshop_Reply SET Keys = @Keys WHERE ActivityId = @VoteId AND [ReplyType] = @ReplyType");
            this.database.AddInParameter(sqlStringCommand, "VoteName", DbType.String, vote.VoteName);
            this.database.AddInParameter(sqlStringCommand, "MaxCheck", DbType.Int32, vote.MaxCheck);
            this.database.AddInParameter(sqlStringCommand, "ImageUrl", DbType.String, vote.ImageUrl);
            this.database.AddInParameter(sqlStringCommand, "StartDate", DbType.DateTime, vote.StartDate);
            this.database.AddInParameter(sqlStringCommand, "EndDate", DbType.DateTime, vote.EndDate);
            this.database.AddInParameter(sqlStringCommand, "Keys", DbType.String, vote.Keys);
            this.database.AddInParameter(sqlStringCommand, "ReplyType", DbType.Int32, 0x80);
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, vote.VoteId);
            return (this.database.ExecuteNonQuery(sqlStringCommand, dbTran) > 0);
        }

        public bool Vote(int voteId, string itemIds)
        {
            DbCommand sqlStringCommand = this.database.GetSqlStringCommand("IF EXISTS (SELECT 1 FROM Hishop_Votes WHERE VoteId=@VoteId AND (GETDATE() < StartDate OR GETDATE() > EndDate) ) return;INSERT INTO Hishop_VoteRecord (UserId, VoteId) VALUES (@UserId, @VoteId);" + string.Format(" UPDATE Hishop_VoteItems SET ItemCount = ItemCount + 1 WHERE VoteId = @VoteId AND VoteItemId IN ({0})", itemIds));
            this.database.AddInParameter(sqlStringCommand, "UserId", DbType.Int64, Globals.GetCurrentMemberUserId());
            this.database.AddInParameter(sqlStringCommand, "VoteId", DbType.Int64, voteId);
            return (this.database.ExecuteNonQuery(sqlStringCommand) > 0);
        }
    }
}

