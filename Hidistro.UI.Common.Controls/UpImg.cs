using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Hidistro.Core;

namespace Hidistro.UI.Common.Controls
{
    /// <summary>
    /// 图片上传
    /// </summary>
    public class UpImg : WebControl
    {

        #region 字段定义
        UploadType _UploadType;
        string thumbnailUrl100 = string.Empty;
        string thumbnailUrl160 = string.Empty;
        string thumbnailUrl180 = string.Empty;
        string thumbnailUrl220 = string.Empty;
        string thumbnailUrl226 = string.Empty;
        string thumbnailUrl310 = string.Empty;
        string thumbnailUrl40 = string.Empty;
        string thumbnailUrl410 = string.Empty;
        string thumbnailUrl60 = string.Empty;
        string uploadedImageUrl = string.Empty;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public UpImg()
        {
            UploadType = Hidistro.UI.Common.Controls.UploadType.Product;
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        /// 
        private bool CheckFileExists(string imageUrl)
        {
            if (!this.CheckFileFormat(imageUrl))
            {
                return false;
            }
            if (imageUrl.ToLower().IndexOf("http://") < 0)
            {
                return File.Exists(this.Page.Request.MapPath(Globals.ApplicationPath + imageUrl));
            }
            return true;
        }
        //bool CheckFileExists(string imageUrl)
        //{
        //    try
        //    {
        //        return (CheckFileFormat(imageUrl) && File.Exists(Page.Request.MapPath(Globals.ApplicationPath + imageUrl)));
        //    }
        //    catch { }
        //    return false;
        //}

        /// <summary>
        /// 检查图片格式
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        bool CheckFileFormat(string imageUrl)
        {
            bool isFmtCorrect = false;

            if (!string.IsNullOrEmpty(imageUrl))
            {
                //将路径转成大写字符
                string url = imageUrl.ToUpper();

                if ((url.Contains(".JPG") || url.Contains(".GIF")) || ((url.Contains(".PNG") || url.Contains(".BMP")) || url.Contains(".JPEG")))
                {
                    isFmtCorrect = true;
                }

            }

            return isFmtCorrect;

        }

        /// <summary>
        /// 生成子控件
        /// </summary>
        protected override void CreateChildControls()
        {
            Controls.Clear();

            string webResourceUrl = Page.ClientScript.GetWebResourceUrl(base.GetType(), "Hidistro.UI.Common.Controls.ImageUploader.images.upload.png");

            WebControl child = new WebControl(HtmlTextWriterTag.Div);

            string str2 = "background:url(" + webResourceUrl + ");background-repeat:no-repeat; background-position:20px -80px";

            child.Attributes.Add("id", ID + "_preview");
            child.Attributes.Add("style", str2);
            child.Attributes.Add("class", "preview");

            WebControl control2 = new WebControl(HtmlTextWriterTag.Div);
            control2.Attributes.Add("id", ID + "_upload");
            control2.Attributes.Add("class", "actionBox");
            //if ((HiContext.Current.User.UserRole != UserRole.SiteManager) || IsUploaded)
            if (IsUploaded)
            {
                control2.Attributes.Add("style", "display:none;");
            }

            WebControl control3 = new WebControl(HtmlTextWriterTag.A);
            control3.Attributes.Add("href", "javascript:void(0);");
            control3.Attributes.Add("style", "background-image: url(" + webResourceUrl + ");");
            control3.Attributes.Add("class", "files");
            control3.Attributes.Add("id", ID + "_content");
            control2.Controls.Add(control3);

            WebControl control4 = new WebControl(HtmlTextWriterTag.Div);

            WebControl control5 = new WebControl(HtmlTextWriterTag.A);

            control4.Attributes.Add("id", ID + "_delete");
            control4.Attributes.Add("class", "actionBox");

            // if ((HiContext.Current.User.UserRole != UserRole.SiteManager) || !IsUploaded)
            if (!IsUploaded)
            {
                control4.Attributes.Add("style", "display:none;");
            }

            control5.Attributes.Add("href", "javascript:DeleteImage('" + ID + "','" + UploadType.ToString().ToLower() + "');");
            control5.Attributes.Add("style", "background-image: url(" + webResourceUrl + ");");
            control5.Attributes.Add("class", "actions");

            control4.Controls.Add(control5);
            Controls.Add(child);
            Controls.Add(control2);
            Controls.Add(control4);

            if (Page.Header.FindControl("uploaderStyle") == null)
            {
                WebControl uploaderStyleCtl = new WebControl(HtmlTextWriterTag.Link);

                uploaderStyleCtl.Attributes.Add("rel", "stylesheet");
                uploaderStyleCtl.Attributes.Add("href", Page.ClientScript.GetWebResourceUrl(base.GetType(), "Hidistro.UI.Common.Controls.ImageUploader.css.style.css"));
                uploaderStyleCtl.Attributes.Add("type", "text/css");
                uploaderStyleCtl.Attributes.Add("media", "screen");
                uploaderStyleCtl.ID = "uploaderStyle";
                Page.Header.Controls.Add(uploaderStyleCtl);

            }

        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UploadedImageUrl = Context.Request.Form[ID + "_uploadedImageUrl"];
        }

        /// <summary>
        /// 呈现控件
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderControl(HtmlTextWriter writer)
        {

            //循环生成控件
            foreach (Control control in Controls)
            {
                control.RenderControl(writer);
                writer.WriteLine();
            }



            if (!Page.ClientScript.IsStartupScriptRegistered(base.GetType(), "UploadScript"))
            {
                string script = string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", Page.ClientScript.GetWebResourceUrl(base.GetType(), "Hidistro.UI.Common.Controls.ImageUploader.script.upload.js"));
                Page.ClientScript.RegisterStartupScript(base.GetType(), "UploadScript", script, false);
            }

            if (!Page.ClientScript.IsStartupScriptRegistered(base.GetType(), ID + "_InitScript"))
            {
                StringBuilder jsBuilder = new StringBuilder();

                jsBuilder.Append("$(document).ready(function(){");
                // string str2 = "$(document).ready(function() { InitUploader(\"" + ID + "\", \"" + UploadType.ToString().ToLower() + "\");";
                jsBuilder.Append("InitUploader(\"" + ID + "\", \"" + UploadType.ToString().ToLower() + "\");");

                if (IsUploaded)
                {
                    //string str3 = str2;
                    // str2 = str3 + "UpdatePreview('" + ID + "', '" + ThumbnailUrl100 + "');";
                    jsBuilder.Append("UpdatePreview('" + ID + "', '" + ThumbnailUrl100 + "');");
                }
                //str2 = str2 + "});" + Environment.NewLine;
                jsBuilder.Append("});").AppendLine();
                Page.ClientScript.RegisterStartupScript(base.GetType(), ID + "_InitScript", jsBuilder.ToString(), true);// str2, true);
            }
            writer.WriteLine();
            writer.AddAttribute("id", ID + "_uploadedImageUrl");
            writer.AddAttribute("name", ID + "_uploadedImageUrl");
            writer.AddAttribute("value", UploadedImageUrl);
            writer.AddAttribute("type", "hidden");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        /// <summary>
        /// 控件集
        /// </summary>
        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        /// <summary>
        /// 是否已经上传
        /// </summary>
        public bool IsUploaded
        {
            get
            {
                return !string.IsNullOrEmpty(UploadedImageUrl);
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl100
        {
            get
            {
                return thumbnailUrl100;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl160
        {
            get
            {
                return thumbnailUrl160;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl180
        {
            get
            {
                return thumbnailUrl180;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl220
        {
            get
            {
                return thumbnailUrl220;
            }
        }


        [Browsable(false)]
        public string ThumbnailUrl226
        {
            get
            {
                return thumbnailUrl226;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl310
        {
            get
            {
                return thumbnailUrl310;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl40
        {
            get
            {
                return thumbnailUrl40;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl410
        {
            get
            {
                return thumbnailUrl410;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl60
        {
            get
            {
                return thumbnailUrl60;
            }
        }

        /// <summary>
        /// 上传的图片路径
        /// </summary>
        [Browsable(false)]
        public string UploadedImageUrl
        {
            get
            {
                return uploadedImageUrl;
            }
            set
            {
                if (CheckFileExists(value))
                {
                    uploadedImageUrl = value;
                    thumbnailUrl40 = value.Replace("/images/", "/thumbs40/40_");
                    thumbnailUrl60 = value.Replace("/images/", "/thumbs60/60_");
                    thumbnailUrl100 = value.Replace("/images/", "/thumbs100/100_");
                    thumbnailUrl160 = value.Replace("/images/", "/thumbs160/160_");
                    thumbnailUrl180 = value.Replace("/images/", "/thumbs180/180_");
                    thumbnailUrl220 = value.Replace("/images/", "/thumbs220/220_");
                    thumbnailUrl226 = value.Replace("/images/", "/thumbs226/226_");
                    thumbnailUrl310 = value.Replace("/images/", "/thumbs310/310_");
                    thumbnailUrl410 = value.Replace("/images/", "/thumbs410/410_");
                }
            }
        }

        /// <summary>
        /// 上传类型,上传到的目录
        /// </summary>
        public Hidistro.UI.Common.Controls.UploadType UploadType
        {

            get
            {
                return _UploadType;
            }

            set
            {
                _UploadType = value;
            }
        }
    }
}

