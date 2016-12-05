#region 注释
/*using Hidistro.UI.ControlPanel.Utility;
using LitJson;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
namespace Hidistro.UI.Web.Admin
{
	public class FileManagerJson : AdminPage
	{
		public class DateTimeSorter : System.Collections.IComparer
		{
			private bool ascend;
			private int type;
			public DateTimeSorter(int sortType, bool isAscend)
			{
				this.ascend = isAscend;
				this.type = sortType;
			}
			public int Compare(object x, object y)
			{
				int result;
				if (x == null && y == null)
				{
					result = 0;
				}
				else
				{
					if (x == null)
					{
						if (!this.ascend)
						{
							result = 1;
						}
						else
						{
							result = -1;
						}
					}
					else
					{
						if (y == null)
						{
							if (!this.ascend)
							{
								result = -1;
							}
							else
							{
								result = 1;
							}
						}
						else
						{
							System.IO.FileInfo info = new System.IO.FileInfo(x.ToString());
							System.IO.FileInfo info2 = new System.IO.FileInfo(y.ToString());
							if (this.type == 0)
							{
								if (!this.ascend)
								{
									result = info2.CreationTime.CompareTo(info.CreationTime);
								}
								else
								{
									result = info.CreationTime.CompareTo(info2.CreationTime);
								}
							}
							else
							{
								if (!this.ascend)
								{
									result = info2.LastWriteTime.CompareTo(info.LastWriteTime);
								}
								else
								{
									result = info.LastWriteTime.CompareTo(info2.LastWriteTime);
								}
							}
						}
					}
				}
				return result;
			}
		}
		public class NameSorter : System.Collections.IComparer
		{
			private bool ascend;
			public NameSorter(bool isAscend)
			{
				this.ascend = isAscend;
			}
			public int Compare(object x, object y)
			{
				int result;
				if (x == null && y == null)
				{
					result = 0;
				}
				else
				{
					if (x == null)
					{
						if (!this.ascend)
						{
							result = 1;
						}
						else
						{
							result = -1;
						}
					}
					else
					{
						if (y == null)
						{
							if (!this.ascend)
							{
								result = -1;
							}
							else
							{
								result = 1;
							}
						}
						else
						{
							System.IO.FileInfo info = new System.IO.FileInfo(x.ToString());
							System.IO.FileInfo info2 = new System.IO.FileInfo(y.ToString());
							if (!this.ascend)
							{
								result = info2.FullName.CompareTo(info.FullName);
							}
							else
							{
								result = info.FullName.CompareTo(info2.FullName);
							}
						}
					}
				}
				return result;
			}
		}
		public class SizeSorter : System.Collections.IComparer
		{
			private bool ascend;
			public SizeSorter(bool isAscend)
			{
				this.ascend = isAscend;
			}
			public int Compare(object x, object y)
			{
				int result;
				if (x == null && y == null)
				{
					result = 0;
				}
				else
				{
					if (x == null)
					{
						if (!this.ascend)
						{
							result = 1;
						}
						else
						{
							result = -1;
						}
					}
					else
					{
						if (y == null)
						{
							if (!this.ascend)
							{
								result = -1;
							}
							else
							{
								result = 1;
							}
						}
						else
						{
							System.IO.FileInfo info = new System.IO.FileInfo(x.ToString());
							System.IO.FileInfo info2 = new System.IO.FileInfo(y.ToString());
							if (!this.ascend)
							{
								result = info2.Length.CompareTo(info.Length);
							}
							else
							{
								result = info.Length.CompareTo(info2.Length);
							}
						}
					}
				}
				return result;
			}
		}
		public void FillTableForDb(string cid, string order, System.Collections.Hashtable table)
		{
			Database database = DatabaseFactory.CreateDatabase();
			System.Collections.Generic.List<System.Collections.Hashtable> list = new System.Collections.Generic.List<System.Collections.Hashtable>();
			table["category_list"] = list;
			System.Data.Common.DbCommand sqlStringCommand = dataGetSqlStringCommand("select CategoryId,CategoryName from Hishop_PhotoCategories order by DisplaySequence");
			using (System.Data.IDataReader reader = dataExecuteReader(sqlStringCommand))
			{
				while (reader.Read())
				{
					System.Collections.Hashtable item = new System.Collections.Hashtable();
					item["cId"] = reader["CategoryId"];
					item["cName"] = reader["CategoryName"];
					list.Add(item);
				}
			}
			System.Collections.Generic.List<System.Collections.Hashtable> list2 = new System.Collections.Generic.List<System.Collections.Hashtable>();
			table["file_list"] = list2;
			if (cid.Trim() == "-1")
			{
				sqlStringCommand.CommandText = string.Format("select * from Hishop_PhotoGallery order by {1}", cid, order);
			}
			else
			{
				sqlStringCommand.CommandText = string.Format("select * from Hishop_PhotoGallery where CategoryId={0} order by {1}", cid, order);
			}
			using (System.Data.IDataReader reader2 = dataExecuteReader(sqlStringCommand))
			{
				while (reader2.Read())
				{
					System.Collections.Hashtable hashtable2 = new System.Collections.Hashtable();
					hashtable2["cid"] = reader2["CategoryId"];
					hashtable2["name"] = reader2["PhotoName"];
					hashtable2["path"] = reader2["PhotoPath"];
					hashtable2["filesize"] = reader2["FileSize"];
					hashtable2["addedtime"] = reader2["UploadTime"];
					hashtable2["updatetime"] = reader2["LastUpdateTime"];
					string str = reader2["PhotoPath"].ToString().Trim();
					hashtable2["filetype"] = str.Substring(str.LastIndexOf('.'));
					list2.Add(hashtable2);
				}
			}
			table["total_count"] = list2.Count;
			table["current_cateogry"] = int.Parse(cid);
		}
		public void FillTableForPath(string path, string url, string order, System.Collections.Hashtable table)
		{
			string str = Server.MapPath(path);
			if (!System.IO.Directory.Exists(str))
			{
				Response.Write("此目录不存在！");
				Response.End();
			}
			string[] files = System.IO.Directory.GetFiles(str);
			switch (order)
			{
			case "uploadtime":
				System.Array.Sort(files, new FileManagerJson.DateTimeSorter(0, true));
				goto IL_18F;
			case "uploadtime desc":
				System.Array.Sort(files, new FileManagerJson.DateTimeSorter(0, false));
				goto IL_18F;
			case "lastupdatetime":
				System.Array.Sort(files, new FileManagerJson.DateTimeSorter(1, true));
				goto IL_18F;
			case "lastupdatetime desc":
				System.Array.Sort(files, new FileManagerJson.DateTimeSorter(1, false));
				goto IL_18F;
			case "photoname":
				System.Array.Sort(files, new FileManagerJson.NameSorter(true));
				goto IL_18F;
			case "photoname desc":
				System.Array.Sort(files, new FileManagerJson.NameSorter(false));
				goto IL_18F;
			case "filesize":
				System.Array.Sort(files, new FileManagerJson.SizeSorter(true));
				goto IL_18F;
			case "filesize desc":
				System.Array.Sort(files, new FileManagerJson.SizeSorter(false));
				goto IL_18F;
			}
			System.Array.Sort(files, new FileManagerJson.NameSorter(true));
			IL_18F:
			table["total_count"] = files.Length;
			System.Collections.Generic.List<System.Collections.Hashtable> list = new System.Collections.Generic.List<System.Collections.Hashtable>();
			table["file_list"] = list;
			for (int i = 0; i < files.Length; i++)
			{
				System.IO.FileInfo info = new System.IO.FileInfo(files[i]);
				System.Collections.Hashtable item = new System.Collections.Hashtable();
				item["cid"] = "-1";
				item["name"] = info.Name;
				item["path"] = url + info.Name;
				item["filesize"] = info.Length;
				item["addedtime"] = info.CreationTime;
				item["updatetime"] = info.LastWriteTime;
				item["filetype"] = info.Extension.Substring(1);
				list.Add(item);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			System.Collections.Hashtable table = new System.Collections.Hashtable();
			string str = Request.QueryString["order"];
			str = (string.IsNullOrEmpty(str) ? "uploadtime" : str.ToLower());
			string cid = Request.QueryString["cid"];
			if (cid == null)
			{
				cid = "-1";
			}
			this.FillTableForDb(cid, str, table);
			string str2 = Request.Url.ToString();
			str2 = str2.Substring(0, str2.IndexOf("/", 7)) + Request.ApplicationPath;
			if (str2.EndsWith("/"))
			{
				str2 = str2.Substring(0, str2.Length - 1);
			}
			table["domain"] = str2;
			Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
			Response.Write(JsonMapper.ToJson(table));
			Response.End();
		}
	}
}*/

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Hidistro.UI.ControlPanel.Utility;
using LitJson;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Hidistro.UI.Web.Admin
{
    using Hidistro.ControlPanel.Store;

    public class FileManagerJson : AdminPage
    {
        private string dir = "image";

        public void FillTableForDb(string cid, string order, Hashtable table)
        {
            int result = 0;

            int.TryParse(cid, out result);

            Database database = DatabaseFactory.CreateDatabase();

            table["moveup_dir_path"] = "";
            table["current_dir_path"] = "";
            table["current_url"] = "";

            //new List<Hashtable>();

            List<Hashtable> list = new List<Hashtable>();

            table["file_list"] = list;


            if (result > 0)
            {
                Hashtable item = new Hashtable();

                item["is_dir"] = true;
                item["has_file"] = true;
                item["filesize"] = 0;
                item["is_photo"] = false;
                item["filetype"] = "";

                item["filename"] = "上级目录";

                item["path"] = "-1";

                item["datetime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                list.Add(item);
            }

            if (result <= 0)
            {
                string str2 = "select CategoryId,CategoryName from Hishop_PhotoCategories order by DisplaySequence";

                DbCommand command = database.GetSqlStringCommand(str2);

                using (IDataReader reader2 = database.ExecuteReader(command))
                {
                    while (reader2.Read())
                    {
                        decimal fileSize = 0M;
                        int categoryFile = this.GetCategoryFile((int)reader2["CategoryId"], out fileSize);
                        Hashtable hashtable3 = new Hashtable();
                        hashtable3["is_dir"] = true;
                        hashtable3["has_file"] = categoryFile > 0;
                        hashtable3["filesize"] = fileSize;
                        hashtable3["is_photo"] = false;
                        hashtable3["filetype"] = "";
                        hashtable3["filename"] = reader2["CategoryName"];
                        hashtable3["cid"] = reader2["CategoryId"];
                        hashtable3["path"] = reader2["CategoryName"];
                        hashtable3["datetime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        list.Add(hashtable3);
                    }
                }
            }

            string query = string.Format("select * from Hishop_PhotoGallery where CategoryId=0 order by {0}", order);
            if (result > 0)
            {
                query = string.Format("select * from Hishop_PhotoGallery where CategoryId={0} order by {1}", result, order);
            }
            DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
            using (IDataReader reader = database.ExecuteReader(sqlStringCommand))
            {
                while (reader.Read())
                {
                    Hashtable hashtable2 = new Hashtable();
                    hashtable2["cid"] = reader["CategoryId"];
                    hashtable2["name"] = reader["PhotoName"];
                    hashtable2["path"] = reader["PhotoPath"];
                    hashtable2["filename"] = reader["PhotoName"];
                    hashtable2["filesize"] = reader["FileSize"];
                    hashtable2["addedtime"] = reader["UploadTime"];
                    hashtable2["updatetime"] = reader["LastUpdateTime"];
                    hashtable2["datetime"] = (reader["LastUpdateTime"] == DBNull.Value) ? "" : ((DateTime)reader["LastUpdateTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    string str3 = reader["PhotoPath"].ToString().Trim();
                    hashtable2["filetype"] = str3.Substring(str3.LastIndexOf('.'));
                    list.Add(hashtable2);
                }
            }


            table["total_count"] = list.Count;

            table["current_cateogry"] = result;

        }

        public void FillTableForPath(string dirPath, string url, string order, Hashtable table)
        {
            string path;
            string current_url;
            string current_dir_path;
            string moveup_dir_path;

            string str = Request.QueryString["path"];
            str = string.IsNullOrEmpty(str) ? "" : str;
            if (Regex.IsMatch(str, @"\.\."))
            {
                Response.Write("Access is not allowed.");
                Response.End();
            }
            if ((str != "") && !str.EndsWith("/"))
            {
                str = str + "/";
            }
            if (str == "")
            {
                path = dirPath;
                current_url = url;
                current_dir_path = "";
                moveup_dir_path = "";
            }
            else
            {
                path = dirPath + str;
                current_url = url + str;
                current_dir_path = str;
                moveup_dir_path = Regex.Replace(current_dir_path, @"(.*?)[^\/]+\/$", "$1");
            }
            path = Server.MapPath(path);
            if (!Directory.Exists(path))
            {
                Response.Write("此目录不存在！" + path);
                Response.End();
            }
            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            switch (order)
            {
                case "uploadtime":
                    Array.Sort(files, new DateTimeSorter(0, true));
                    break;

                case "uploadtime desc":
                    Array.Sort(files, new DateTimeSorter(0, false));
                    break;

                case "lastupdatetime":
                    Array.Sort(files, new DateTimeSorter(1, true));
                    break;

                case "lastupdatetime desc":
                    Array.Sort(files, new DateTimeSorter(1, false));
                    break;

                case "photoname":
                    Array.Sort(files, new NameSorter(true));
                    break;

                case "photoname desc":
                    Array.Sort(files, new NameSorter(false));
                    break;

                case "filesize":
                    Array.Sort(files, new SizeSorter(true));
                    break;

                case "filesize desc":
                    Array.Sort(files, new SizeSorter(false));
                    break;

                default:
                    Array.Sort(files, new NameSorter(true));
                    break;
            }
            table["moveup_dir_path"] = moveup_dir_path;
            table["current_dir_path"] = current_dir_path;
            table["current_url"] = current_url;
            table["total_count"] = directories.Length + files.Length;
            List<Hashtable> list = new List<Hashtable>();
            table["file_list"] = list;
            if (str != "")
            {
                Hashtable item = new Hashtable();
                item["is_dir"] = true;
                item["has_file"] = true;
                item["filesize"] = 0;
                item["is_photo"] = false;
                item["filetype"] = "";
                item["filename"] = "";
                item["path"] = "";
                item["datetime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                item["filename"] = "上级目录";
                list.Add(item);
            }
            for (int i = 0; i < directories.Length; i++)
            {
                DirectoryInfo info = new DirectoryInfo(directories[i]);
                Hashtable hashtable = new Hashtable();
                hashtable["is_dir"] = true;
                hashtable["has_file"] = info.GetFileSystemInfos().Length > 0;
                hashtable["filesize"] = 0;
                hashtable["is_photo"] = false;
                hashtable["filetype"] = "";
                hashtable["filename"] = info.Name;
                hashtable["path"] = info.Name;
                hashtable["datetime"] = info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                list.Add(hashtable);
            }
            for (int j = 0; j < files.Length; j++)
            {
                FileInfo info2 = new FileInfo(files[j]);
                Hashtable hashtable3 = new Hashtable();
                hashtable3["cid"] = "-1";
                hashtable3["name"] = info2.Name;
                hashtable3["path"] = url + str + info2.Name;
                hashtable3["filename"] = info2.Name;
                hashtable3["filesize"] = info2.Length;
                hashtable3["addedtime"] = info2.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                hashtable3["updatetime"] = info2.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                hashtable3["datetime"] = info2.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                hashtable3["filetype"] = info2.Extension.Substring(1);
                list.Add(hashtable3);
            }
        }

        public int GetCategoryFile(int iCategryId, out decimal fileSize)
        {
            int num = 0;
            fileSize = 0M;
            Database database = DatabaseFactory.CreateDatabase();
            string query = string.Format("select Count(PhotoId) as FileCount,isnull(Sum(FileSize),0) as AllFileSize from Hishop_PhotoGallery", new object[0]);
            if (iCategryId > 0)
            {
                query = string.Format("select Count(PhotoId) as FileCount,isnull(Sum(FileSize),0) as AllFileSize from Hishop_PhotoGallery where CategoryId={0} ", iCategryId.ToString());
            }
            DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
            using (IDataReader reader = database.ExecuteReader(sqlStringCommand))
            {
                if (reader.Read())
                {
                    num = (int)reader["FileCount"];
                    fileSize = (int)reader["AllFileSize"];
                }
            }
            return num;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ManagerHelper.GetCurrentManager() == null)
            {
                Response.Write("没有权限！");
                Response.End();
            }
            else
            {
                Hashtable table = new Hashtable();

                this.dir = Request["dir"];

                if (string.IsNullOrEmpty(this.dir))
                {
                    this.dir = "image";
                }
                string str4 = "false";
                if (Request.QueryString["isAdvPositions"] != null)
                {
                    str4 = Request.QueryString["isAdvPositions"].ToString().ToLower().Trim();
                }
                string dirPath = "~/Storage/master/gallery/";
                string url = "/Storage/master/gallery/";
                string str = this.dir;
                if (str != null)
                {
                    if (str == "image")
                    {
                        dirPath = "~/Storage/master/gallery/";
                        url = "/Storage/master/gallery/";
                    }
                    else if (str == "file")
                    {
                        dirPath = "~/Storage/master/accessory/";
                        url = "/Storage/master/accessory/";
                    }
                    else if (!(str == "flash"))
                    {
                        if (str == "media")
                        {
                            dirPath = "~/Storage/master/media/";
                            url = "/Storage/master/media/";
                        }
                    }
                    else
                    {
                        dirPath = "~/Storage/master/flash/";
                        url = "/Storage/master/flash/";
                    }
                }
                string str6 = Request.QueryString["order"];
                str6 = string.IsNullOrEmpty(str6) ? "uploadtime" : str6.ToLower();
                string cid = Request.QueryString["path"];
                switch (cid)
                {
                    case null:
                    case "":
                        cid = "-1";
                        break;
                }
                if (str4 == "false")
                {
                    this.FillTableForDb(cid, str6, table);
                }
                else
                {
                    this.FillTableForPath(dirPath, url, str6, table);
                }
                string str7 = Request.Url.ToString();
                str7 = str7.Substring(0, str7.IndexOf("/", 7)) + Request.ApplicationPath;
                if (str7.EndsWith("/"))
                {
                    str7 = str7.Substring(0, str7.Length - 1);
                }
                table["domain"] = str7;
                Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
                Response.Write(JsonMapper.ToJson(table));
                Response.End();
            }
        }


        public class DateTimeSorter : System.Collections.IComparer
        {
            private bool ascend;
            private int type;
            public DateTimeSorter(int sortType, bool isAscend)
            {
                this.ascend = isAscend;
                this.type = sortType;
            }
            public int Compare(object x, object y)
            {
                int result;
                if (x == null && y == null)
                {
                    result = 0;
                }
                else
                {
                    if (x == null)
                    {
                        if (!this.ascend)
                        {
                            result = 1;
                        }
                        else
                        {
                            result = -1;
                        }
                    }
                    else
                    {
                        if (y == null)
                        {
                            if (!this.ascend)
                            {
                                result = -1;
                            }
                            else
                            {
                                result = 1;
                            }
                        }
                        else
                        {
                            System.IO.FileInfo info = new System.IO.FileInfo(x.ToString());
                            System.IO.FileInfo info2 = new System.IO.FileInfo(y.ToString());
                            if (this.type == 0)
                            {
                                if (!this.ascend)
                                {
                                    result = info2.CreationTime.CompareTo(info.CreationTime);
                                }
                                else
                                {
                                    result = info.CreationTime.CompareTo(info2.CreationTime);
                                }
                            }
                            else
                            {
                                if (!this.ascend)
                                {
                                    result = info2.LastWriteTime.CompareTo(info.LastWriteTime);
                                }
                                else
                                {
                                    result = info.LastWriteTime.CompareTo(info2.LastWriteTime);
                                }
                            }
                        }
                    }
                }
                return result;
            }
        }
        public class NameSorter : System.Collections.IComparer
        {
            private bool ascend;
            public NameSorter(bool isAscend)
            {
                this.ascend = isAscend;
            }
            public int Compare(object x, object y)
            {
                int result;
                if (x == null && y == null)
                {
                    result = 0;
                }
                else
                {
                    if (x == null)
                    {
                        if (!this.ascend)
                        {
                            result = 1;
                        }
                        else
                        {
                            result = -1;
                        }
                    }
                    else
                    {
                        if (y == null)
                        {
                            if (!this.ascend)
                            {
                                result = -1;
                            }
                            else
                            {
                                result = 1;
                            }
                        }
                        else
                        {
                            System.IO.FileInfo info = new System.IO.FileInfo(x.ToString());
                            System.IO.FileInfo info2 = new System.IO.FileInfo(y.ToString());
                            if (!this.ascend)
                            {
                                result = info2.FullName.CompareTo(info.FullName);
                            }
                            else
                            {
                                result = info.FullName.CompareTo(info2.FullName);
                            }
                        }
                    }
                }
                return result;
            }
        }
        public class SizeSorter : System.Collections.IComparer
        {
            private bool ascend;
            public SizeSorter(bool isAscend)
            {
                this.ascend = isAscend;
            }
            public int Compare(object x, object y)
            {
                int result;
                if (x == null && y == null)
                {
                    result = 0;
                }
                else
                {
                    if (x == null)
                    {
                        if (!this.ascend)
                        {
                            result = 1;
                        }
                        else
                        {
                            result = -1;
                        }
                    }
                    else
                    {
                        if (y == null)
                        {
                            if (!this.ascend)
                            {
                                result = -1;
                            }
                            else
                            {
                                result = 1;
                            }
                        }
                        else
                        {
                            System.IO.FileInfo info = new System.IO.FileInfo(x.ToString());
                            System.IO.FileInfo info2 = new System.IO.FileInfo(y.ToString());
                            if (!this.ascend)
                            {
                                result = info2.Length.CompareTo(info.Length);
                            }
                            else
                            {
                                result = info.Length.CompareTo(info2.Length);
                            }
                        }
                    }
                }
                return result;
            }
        }

    }
}

