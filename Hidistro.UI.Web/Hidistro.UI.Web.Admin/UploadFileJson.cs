using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Web;

using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.UI.ControlPanel.Utility;
using LitJson;

using Microsoft.Practices.EnterpriseLibrary.Data;
namespace Hidistro.UI.Web.Admin
{
    public class UploadFileJson : AdminPage
    {

        string savePath;
        string saveUrl;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (ManagerHelper.GetCurrentManager() == null)
            {
                this.showError("您没有权限执行此操作！");
            }
            else
            {


                this.savePath = "~/Storage/master/gallery/";

                this.saveUrl = "/Storage/master/gallery/";

                int cid = 0;

                if (Request.Form["fileCategory"] != null)
                {
                    int.TryParse(base.Request.Form["fileCategory"], out cid);
                }

                string name = string.Empty;

                if (Request.Form["imgTitle"] != null)
                {
                    name = base.Request.Form["imgTitle"];
                }


                System.Web.HttpPostedFile postedFile = Request.Files["imgFile"];

                if (postedFile == null)
                {
                    this.showError("请先选择文件！");
                }
                else
                {

                    if (!ResourcesHelper.CheckPostedFile(postedFile))
                    {
                        this.showError("不能上传空文件，且必须是有效的图片文件！");
                    }
                    else
                    {

                        string path = Server.MapPath(this.savePath);

                        if (!System.IO.Directory.Exists(path))
                        {
                            this.showError("上传目录不存在。");
                        }
                        else
                        {

                            path += string.Format("{0}/", System.DateTime.Now.ToString("yyyyMM"));

                            this.saveUrl += string.Format("{0}/", System.DateTime.Now.ToString("yyyyMM"));

                            if (!System.IO.Directory.Exists(path))
                            {
                                System.IO.Directory.CreateDirectory(path);
                            }

                            string fileName = postedFile.FileName;

                            if (name.Length == 0)
                            {
                                name = fileName;
                            }

                            ///取出文件扩展名
                            string fileExt = System.IO.Path.GetExtension(fileName).ToLower();

                            //生成新的文件名
                            string newFileName = System.DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo) + fileExt;

                            //文件保存真实路径
                            string filename = path + newFileName;

                            //网站相对路径
                            string relativePath = this.saveUrl + newFileName;

                            bool err = false;

                            try
                            {

                                postedFile.SaveAs(filename);

                                Database database = DatabaseFactory.CreateDatabase();
                                System.Data.Common.DbCommand sqlStringCommand = database.GetSqlStringCommand("insert into Hishop_PhotoGallery(CategoryId,PhotoName,PhotoPath,FileSize,UploadTime,LastUpdateTime)values(@cid,@name,@path,@size,@time,@time1)");
                                database.AddInParameter(sqlStringCommand, "cid", System.Data.DbType.Int32, cid);
                                database.AddInParameter(sqlStringCommand, "name", System.Data.DbType.String, name);
                                database.AddInParameter(sqlStringCommand, "path", System.Data.DbType.String, relativePath);
                                database.AddInParameter(sqlStringCommand, "size", System.Data.DbType.Int32, postedFile.ContentLength);
                                database.AddInParameter(sqlStringCommand, "time", System.Data.DbType.DateTime, System.DateTime.Now);
                                database.AddInParameter(sqlStringCommand, "time1", System.Data.DbType.DateTime, System.DateTime.Now);
                                database.ExecuteNonQuery(sqlStringCommand);

   
                            }
                            catch
                            {
                                err = true;
                                this.showError("保存文件出错！");
                            }
                            finally { }

                            if (!err)
                            {
                                System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
                                hashtable["error"] = 0;
                                hashtable["url"] = Globals.ApplicationPath + relativePath;
                                Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                                Response.Write(JsonMapper.ToJson(hashtable));
                                Response.End();
                            }

                        }

                    }

                }

            }

        }


        private void showError(string message)
        {

            System.Collections.Hashtable hashtable = new System.Collections.Hashtable();

            hashtable["error"] = 1;

            hashtable["message"] = message;

            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");

            Response.Write(JsonMapper.ToJson(hashtable));

            Response.End();

        }

    }


}
