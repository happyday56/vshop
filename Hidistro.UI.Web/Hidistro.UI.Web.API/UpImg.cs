using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using Hidistro.Core;


namespace Hidistro.UI.Web.API
{
    /// <summary>
    /// 图片上传处理程序
    /// </summary>
    public class UpImg : IHttpHandler
    {

        #region 字段
        string action;
        string uploaderId;
        string uploadType;
        #endregion

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="context"></param>
        void DoDelete(HttpContext context)
        {
            string originalPath = context.Request.MapPath(Globals.ApplicationPath + context.Request.Form[uploaderId + "_uploadedImageUrl"]);
            string thumbnailUrl40 = originalPath.Replace(@"\images\", @"\thumbs40\40_");
            string thumbnailUrl60 = originalPath.Replace(@"\images\", @"\thumbs60\60_");
            string thumbnailUrl100 = originalPath.Replace(@"\images\", @"\thumbs100\100_");
            string thumbnailUrl160 = originalPath.Replace(@"\images\", @"\thumbs160\160_");
            string thumbnailUrl180 = originalPath.Replace(@"\images\", @"\thumbs180\180_");
            string thumbnailUrl220 = originalPath.Replace(@"\images\", @"\thumbs220\220_");
            string thumbnailUrl226 = originalPath.Replace(@"\images\", @"\thumbs226\226_");
            string thumbnailUrl310 = originalPath.Replace(@"\images\", @"\thumbs310\310_");
            string thumbnailUrl410 = originalPath.Replace(@"\images\", @"\thumbs410\410_");
            if (File.Exists(originalPath))
            {
                File.Delete(originalPath);
            }
            if (File.Exists(thumbnailUrl40))
            {
                File.Delete(thumbnailUrl40);
            }
            if (File.Exists(thumbnailUrl60))
            {
                File.Delete(thumbnailUrl60);
            }
            if (File.Exists(thumbnailUrl100))
            {
                File.Delete(thumbnailUrl100);
            }
            if (File.Exists(thumbnailUrl160))
            {
                File.Delete(thumbnailUrl160);
            }
            if (File.Exists(thumbnailUrl180))
            {
                File.Delete(thumbnailUrl180);
            }
            if (File.Exists(thumbnailUrl220))
            {
                File.Delete(thumbnailUrl220);
            }
            if (File.Exists(thumbnailUrl226))
            {
                File.Delete(thumbnailUrl226);
            }
            if (File.Exists(thumbnailUrl310))
            {
                File.Delete(thumbnailUrl310);
            }
            if (File.Exists(thumbnailUrl410))
            {
                File.Delete(thumbnailUrl410);
            }
            context.Response.Write("<script type=\"text/javascript\">window.parent.DeleteCallback('" + uploaderId + "');</script>");
        }

        void swfobject_Upload(HttpContext context)
        {
            try
            {
                string uploadPath = Globals.GetStoragePath() + "/" + uploadType;

                string newFilename = Guid.NewGuid().ToString("N") + ".jpg";

                string originalSavePath = uploadPath + "/images/" + newFilename;

                CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "images")));
                CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs40")));
                CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs60")));
                CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs100")));
                CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs160")));
                CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs180")));
                CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs220")));
                CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs226")));
                CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs310")));
                CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs410")));

                string thumbnail40SavePath = uploadPath + "/thumbs40/40_" + newFilename;
                string thumbnail60SavePath = uploadPath + "/thumbs60/60_" + newFilename;
                string thumbnail100SavePath = uploadPath + "/thumbs100/100_" + newFilename;
                string thumbnail160SavePath = uploadPath + "/thumbs160/160_" + newFilename;
                string thumbnail180SavePath = uploadPath + "/thumbs180/180_" + newFilename;
                string thumbnail220SavePath = uploadPath + "/thumbs220/220_" + newFilename;
                string thumbnail226SavePath = uploadPath + "/thumbs226/226_" + newFilename;
                string thumbnail310SavePath = uploadPath + "/thumbs310/310_" + newFilename;
                string thumbnail410SavePath = uploadPath + "/thumbs410/410_" + newFilename;

                // file.SaveAs(context.Request.MapPath(Globals.ApplicationPath + originalSavePath));

                // System.Drawing.Image image = System.Drawing.Image.FromStream(context.Request.InputStream);
                // image.Save(context.Request.MapPath(originalSavePath));


                #region 读取图片数据

                //读取流，并转为byte
                Stream stream = context.Request.InputStream;

                byte[] buffer = new byte[stream.Length];

                stream.Read(buffer, 0, buffer.Length);

                //分隔符
                byte[] splitStr = System.Text.Encoding.UTF8.GetBytes("--------------------");

                //读取分隔符位置
                IList<int> points = new List<int>();
                for (int i = 0; i < buffer.Length - splitStr.Length; i++)
                {
                    stream.Position = i;
                    byte[] tempSplitStr = new byte[splitStr.Length];
                    stream.Read(tempSplitStr, 0, splitStr.Length);
                    if (tempSplitStr.SequenceEqual(splitStr))
                        points.Add(i);
                }
                points.Add(buffer.Length);

                //按分隔符的位置，取出段数据（图片），并存为图片
                int curPosition = 0;
                //foreach (var point in points)
                //{
                //    using (FileStream fs = new FileStream("c:\\" + point.GetHashCode() + ".jpg", FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                //    {
                //        fs.Write(buffer, curPosition, point - curPosition);
                //        curPosition += (splitStr.Length + point);
                //    }
                //}

                if (points.Count > 0)
                {
                    //缩略图
                    using (FileStream fs = new FileStream(context.Request.MapPath(Globals.ApplicationPath + thumbnail226SavePath), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    {
                        fs.Write(buffer, curPosition, points[0] - curPosition);
                        curPosition += (splitStr.Length + points[0]);
                    }
                }

                if (points.Count > 1)
                {
                    //缩略图
                    using (FileStream fs = new FileStream(context.Request.MapPath(Globals.ApplicationPath + originalSavePath), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    {
                        fs.Write(buffer, curPosition, points[1] - curPosition);
                        curPosition += (splitStr.Length + points[1]);
                    }
                }

                #endregion

                string originalFullPath = context.Request.MapPath(Globals.ApplicationPath + originalSavePath);
                ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail40SavePath), 40, 40);
                ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail60SavePath), 60, 60);
                ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail100SavePath), 100, 100);
                ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail160SavePath), 160, 160);
                ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail180SavePath), 180, 180);
                ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail220SavePath), 220, 220);

                //image.Save(context.Request.MapPath(thumbnail226SavePath));
                //ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail226SavePath), 226, 340);

                ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail310SavePath), 310, 310);
                ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail410SavePath), 410, 410);

                string[] parameters = new string[] { "'" + uploadType + "'", "'" + uploaderId + "'", "'" + originalSavePath + "'", "'" + thumbnail40SavePath + "'", "'" + thumbnail60SavePath + "'", "'" + thumbnail100SavePath + "'", "'" + thumbnail160SavePath + "'", "'" + thumbnail180SavePath + "'", "'" + thumbnail220SavePath + "'", "'" + thumbnail226SavePath + "'", "'" + thumbnail310SavePath + "'", "'" + thumbnail410SavePath + "'" };
                //context.Response.Write("<script type=\"text/javascript\">UploadCallback(" + string.Join(",", parameters) + ");</script>");

                StringBuilder json = new StringBuilder();

                json.Append("{");
                json.AppendFormat("\"uploadType\":\"{0}\",", uploadType);
                json.AppendFormat("\"uploaderId\":\"{0}\",", uploaderId);
                json.AppendFormat("\"originalSavePath\":\"{0}\",", originalSavePath);
                json.AppendFormat("\"thumbnail40SavePath\":\"{0}\",", thumbnail40SavePath);
                json.AppendFormat("\"thumbnail60SavePath\":\"{0}\",", thumbnail60SavePath);
                json.AppendFormat("\"thumbnail100SavePath\":\"{0}\",", thumbnail100SavePath);
                json.AppendFormat("\"thumbnail160SavePath\":\"{0}\",", thumbnail160SavePath);
                json.AppendFormat("\"thumbnail180SavePath\":\"{0}\",", thumbnail180SavePath);
                json.AppendFormat("\"thumbnail220SavePath\":\"{0}\",", thumbnail220SavePath);
                json.AppendFormat("\"thumbnail226SavePath\":\"{0}\",", thumbnail226SavePath);
                json.AppendFormat("\"thumbnail310SavePath\":\"{0}\",", thumbnail310SavePath);
                json.AppendFormat("\"thumbnail410SavePath\":\"{0}\"", thumbnail410SavePath);
                json.Append("}");

                context.Response.Write("{\"status\":\"1\",\"callbackdata\":" + json.ToString() + "}");


            }
            catch// (Exception ex)
            {
                context.Response.Write("{\"status\":\"-2\"}");
                // WriteBackError(context, "应用程序出错");
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="context"></param>
        void DoUpload(HttpContext context)
        {

            if (context.Request.Files.Count == 0)
            {
                WriteBackError(context, "没有检测到任何文件");
            }
            else
            {
                HttpPostedFile file = context.Request.Files[0];
                for (int index = 1; (file.ContentLength == 0) && (index < context.Request.Files.Count); index++)
                {
                    file = context.Request.Files[index];
                }
                if (file.ContentLength == 0)
                {
                    WriteBackError(context, "当前文件没有任何内容");
                }
                else if (!file.ContentType.ToLower().StartsWith("image/") || !Regex.IsMatch(Path.GetExtension(file.FileName.ToLower()), @"\.(jpg|gif|png|bmp|jpeg)$", RegexOptions.Compiled))
                {
                    WriteBackError(context, "文件类型错误，请选择有效的图片文件");
                }
                else
                {
                    UploadImage(context, file);
                }
            }



        }


        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dir"></param>
        void CreateDir(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="context"></param>
        /// <param name="file"></param>
        void UploadImage(HttpContext context, HttpPostedFile file)
        {
            string uploadPath = Globals.GetStoragePath() + "/" + uploadType;

            string newFilename = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

            string originalSavePath = uploadPath + "/images/" + newFilename;

            CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "images")));
            CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs40")));
            CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs60")));
            CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs100")));
            CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs160")));
            CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs180")));
            CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs220")));
            CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs226")));
            CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs310")));
            CreateDir(context.Request.MapPath(Path.Combine(uploadPath, "thumbs410")));

            string thumbnail40SavePath = uploadPath + "/thumbs40/40_" + newFilename;
            string thumbnail60SavePath = uploadPath + "/thumbs60/60_" + newFilename;
            string thumbnail100SavePath = uploadPath + "/thumbs100/100_" + newFilename;
            string thumbnail160SavePath = uploadPath + "/thumbs160/160_" + newFilename;
            string thumbnail180SavePath = uploadPath + "/thumbs180/180_" + newFilename;
            string thumbnail220SavePath = uploadPath + "/thumbs220/220_" + newFilename;
            string thumbnail226SavePath = uploadPath + "/thumbs226/226_" + newFilename;
            string thumbnail310SavePath = uploadPath + "/thumbs310/310_" + newFilename;
            string thumbnail410SavePath = uploadPath + "/thumbs410/410_" + newFilename;

            file.SaveAs(context.Request.MapPath(Globals.ApplicationPath + originalSavePath));
            string originalFullPath = context.Request.MapPath(Globals.ApplicationPath + originalSavePath);

            ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail40SavePath), 40, 40);
            ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail60SavePath), 60, 60);
            ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail100SavePath), 100, 100);
            ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail160SavePath), 160, 160);
            ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail180SavePath), 180, 180);
            ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail220SavePath), 220, 220);
            ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail226SavePath), 226, 340);
            ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail310SavePath), 310, 310);
            ResourcesHelper.CreateThumbnail(originalFullPath, context.Request.MapPath(Globals.ApplicationPath + thumbnail410SavePath), 410, 410);
            string[] parameters = new string[] { "'" + uploadType + "'", "'" + uploaderId + "'", "'" + originalSavePath + "'", "'" + thumbnail40SavePath + "'", "'" + thumbnail60SavePath + "'", "'" + thumbnail100SavePath + "'", "'" + thumbnail160SavePath + "'", "'" + thumbnail180SavePath + "'", "'" + thumbnail220SavePath + "'", "'" + thumbnail226SavePath + "'", "'" + thumbnail310SavePath + "'", "'" + thumbnail410SavePath + "'" };
            context.Response.Write("<script type=\"text/javascript\">window.parent.UploadCallback(" + string.Join(",", parameters) + ");</script>");
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="context"></param>
        /// <param name="error"></param>
        void WriteBackError(HttpContext context, string error)
        {
            string[] parameters = new string[] { "'" + uploadType + "'", "'" + uploaderId + "'", "'" + error + "'" };
            context.Response.Write("<script type=\"text/javascript\">window.parent.ErrorCallback(" + string.Join(",", parameters) + ");</script>");
        }

        #region IHttpHandler实现

        public void ProcessRequest(HttpContext context)
        {
            action = context.Request["action"];

            uploaderId = context.Request["uploaderId"] == null ? "uploader1" : context.Request["uploaderId"];

            uploadType = (context.Request["uploadType"] == null ? "product" : context.Request["uploadType"]);

            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.ClearContent();
            context.Response.Expires = -1;

            try
            {

                switch (action.ToLowerInvariant())
                {
                    case "upload":
                        {
                            DoUpload(context);
                            break;
                        }
                    case "remove":
                        {
                            DoDelete(context);
                            break;
                        }
                    case "swfobject":
                        {
                            swfobject_Upload(context);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                WriteBackError(context, e.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion

    }

}