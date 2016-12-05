namespace Hidistro.ControlPanel.Store
{
    using Hidistro.Core;
    using Hidistro.Entities.Store;
    using Hidistro.Entities.VShop;
    using Hidistro.SqlDal;
    using Hidistro.SqlDal.Store;
    using Hidistro.SqlDal.VShop;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Web;
    using System.Xml;

    public static class StoreHelper
    {
        public static string BackupData()
        {
            return new BackupRestoreDao().BackupData(HttpContext.Current.Request.MapPath(Globals.ApplicationPath + "/Storage/data/Backup/"));
        }

        public static int CreateVote(VoteInfo vote)
        {
            int num = 0;
            VoteDao dao = new VoteDao();
            long num2 = dao.CreateVote(vote);
            if (num2 > 0L)
            {
                ReplyInfo reply = new TextReplyInfo {
                    Keys = vote.Keys,
                    MatchType = MatchType.Equal,
                    ReplyType = ReplyType.Vote,
                    ActivityId = Convert.ToInt32(num2)
                };
                new ReplyDao().SaveReply(reply);
                num = 1;
                if (vote.VoteItems == null)
                {
                    return num;
                }
                foreach (VoteItemInfo info2 in vote.VoteItems)
                {
                    info2.VoteId = num2;
                    info2.ItemCount = 0;
                    num += dao.CreateVoteItem(info2, null);
                }
            }
            return num;
        }

        public static bool DeleteBackupFile(string backupName)
        {
            string filename = HttpContext.Current.Request.MapPath(Globals.ApplicationPath + "/config/BackupFiles.config");
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(filename);
                foreach (XmlNode node in document.SelectSingleNode("root").ChildNodes)
                {
                    XmlElement element = (XmlElement) node;
                    if (element.GetAttribute("BackupName") == backupName)
                    {
                        document.SelectSingleNode("root").RemoveChild(node);
                    }
                }
                document.Save(filename);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void DeleteImage(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                try
                {
                    string path = HttpContext.Current.Request.MapPath(Globals.ApplicationPath + imageUrl);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch
                {
                }
            }
        }

        public static int DeleteVote(long voteId)
        {
            return new VoteDao().DeleteVote(voteId);
        }

        public static DataTable GetBackupFiles()
        {
            DataTable table = new DataTable();
            table.Columns.Add("BackupName", typeof(string));
            table.Columns.Add("Version", typeof(string));
            table.Columns.Add("FileSize", typeof(string));
            table.Columns.Add("BackupTime", typeof(string));
            string filename = HttpContext.Current.Request.MapPath(Globals.ApplicationPath + "/config/BackupFiles.config");
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            foreach (XmlNode node in document.SelectSingleNode("root").ChildNodes)
            {
                XmlElement element = (XmlElement) node;
                DataRow row = table.NewRow();
                row["BackupName"] = element.GetAttribute("BackupName");
                row["Version"] = element.GetAttribute("Version");
                row["FileSize"] = element.GetAttribute("FileSize");
                row["BackupTime"] = element.GetAttribute("BackupTime");
                table.Rows.Add(row);
            }
            return table;
        }

        public static VoteInfo GetVoteById(long voteId)
        {
            return new VoteDao().GetVoteById(voteId);
        }

        public static int GetVoteCounts(long voteId)
        {
            return new VoteDao().GetVoteCounts(voteId);
        }

        public static IList<VoteItemInfo> GetVoteItems(long voteId)
        {
            return new VoteDao().GetVoteItems(voteId);
        }

        public static IList<VoteInfo> GetVoteList()
        {
            return new VoteDao().GetVoteList();
        }

        public static DataSet GetVotes()
        {
            return new VoteDao().GetVotes();
        }

        public static bool InserBackInfo(string fileName, string version, long fileSize)
        {
            string filename = HttpContext.Current.Request.MapPath(Globals.ApplicationPath + "/config/BackupFiles.config");
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(filename);
                XmlNode node = document.SelectSingleNode("root");
                XmlElement newChild = document.CreateElement("backupfile");
                newChild.SetAttribute("BackupName", fileName);
                newChild.SetAttribute("Version", version.ToString());
                newChild.SetAttribute("FileSize", fileSize.ToString());
                newChild.SetAttribute("BackupTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                node.AppendChild(newChild);
                document.Save(filename);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool RestoreData(string bakFullName)
        {
            BackupRestoreDao dao = new BackupRestoreDao();
            bool flag = dao.RestoreData(bakFullName);
            dao.Restor();
            return flag;
        }

        public static int SetVoteIsBackup(long voteId)
        {
            return new VoteDao().SetVoteIsBackup(voteId);
        }

        public static bool UpdateVote(VoteInfo vote)
        {
            bool flag;
            VoteDao dao = new VoteDao();
            using (DbConnection connection = DatabaseFactory.CreateDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction dbTran = connection.BeginTransaction();
                try
                {
                    if (!dao.UpdateVote(vote, dbTran))
                    {
                        dbTran.Rollback();
                        return false;
                    }
                    if (!dao.DeleteVoteItem(vote.VoteId, dbTran))
                    {
                        dbTran.Rollback();
                        return false;
                    }
                    int num = 0;
                    if (vote.VoteItems != null)
                    {
                        foreach (VoteItemInfo info in vote.VoteItems)
                        {
                            info.VoteId = vote.VoteId;
                            info.ItemCount = 0;
                            num += dao.CreateVoteItem(info, dbTran);
                        }
                        if (num < vote.VoteItems.Count)
                        {
                            dbTran.Rollback();
                            return false;
                        }
                    }
                    dbTran.Commit();
                    flag = true;
                }
                catch
                {
                    dbTran.Rollback();
                    flag = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return flag;
        }

        public static string UploadLinkImage(HttpPostedFile postedFile)
        {
            if (!ResourcesHelper.CheckPostedFile(postedFile, "image"))
            {
                return string.Empty;
            }
            string str = Globals.GetStoragePath() + "/link/" + ResourcesHelper.GenerateFilename(Path.GetExtension(postedFile.FileName));
            postedFile.SaveAs(HttpContext.Current.Request.MapPath(Globals.ApplicationPath + str));
            return str;
        }

        public static string UploadLogo(HttpPostedFile postedFile)
        {
            if (!ResourcesHelper.CheckPostedFile(postedFile, "image"))
            {
                return string.Empty;
            }
            string str = Globals.GetStoragePath() + ResourcesHelper.GenerateFilename(Path.GetExtension(postedFile.FileName));
            postedFile.SaveAs(HttpContext.Current.Request.MapPath(Globals.ApplicationPath + str));
            return str;
        }

        public static string UploadVoteImage(HttpPostedFile postedFile)
        {
            if (!ResourcesHelper.CheckPostedFile(postedFile, "image"))
            {
                return string.Empty;
            }
            string str = Globals.GetStoragePath() + "/vote/" + ResourcesHelper.GenerateFilename(Path.GetExtension(postedFile.FileName));
            postedFile.SaveAs(HttpContext.Current.Request.MapPath(Globals.ApplicationPath + str));
            return str;
        }
    }
}

