using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hidistro.Core;

namespace Hidistro.UI.Common.Controls
{
    public class ProductFlashUpload : WebControl
    {
        private string m_FlashUploadType = "product";
        private string m_Value = string.Empty;
        private string m_OldValue = string.Empty;
        private int m_MaxNum = 5;

        private string GetContainer(string text1)
        {
            string str = string.Empty;
            if (text1.Length > 10)
            {
                str = "<div class=\"uploadimages\"><div class=\"preview\"><div class=\"divoperator\"><a href=\"javascript:;\" class=\"leftmove\" title=\"杻y\">&lt;</a><a href=\"javascript:;\" class=\"rightmove\" title=\"Sy\">&gt;</a><a href=\"javascript:;\" class=\"photodel\" title=\" Rd\">X</a></div><img style=\"width: 85px; height: 85px;\" src=\"" + text1 + "\"></div><div class=\"actionBox\"><a href=\"javascript:;\" class=\"actions\">设为默认</a></div></div>";
            }
            return str;
        }

        private string ABOZyE4bfGcbd5Z8N5SO7b2(string text1)
        {
            string[] strArray = this.MyTest(text1).Trim().Trim(new char[] { ',' }).Split(new char[] { ',' });
            StringBuilder builder = new StringBuilder();
            foreach (string str in strArray)
            {
                builder.Append(this.AC2pFbvHCa1v4C1DIZ8SngolueQb(str.Trim()));
            }
            string str2 = builder.ToString().Trim(new char[] { ',' });
            this.MyTestA(this.OldValue, str2);
            return str2;
        }

        private string AC2pFbvHCa1v4C1DIZ8SngolueQb(string text1)
        {
            string str = text1;
            if (str.Contains("/temp/") && (str.Length > 10))
            {
                string[] strArray = text1.Split(new char[] { '.' });
                if (strArray.Length <= 1)
                {
                    return "";
                }
                string str2 = strArray[strArray.Length - 1].Trim().ToLower();
                string str3 = Globals.GetStoragePath() + "/" + this.m_FlashUploadType;
                string str4 = Guid.NewGuid().ToString("N") + "." + str2;
                string path = str;
                string[] strArray2 = text1.Split(new char[] { '/' });
                if (path.StartsWith("http://"))
                {
                    path = path.Replace("http://" + strArray2[2], "");
                }
                path = this.Context.Server.MapPath(path);
                string str6 = str3 + "/images/" + str4;
                File.Copy(path, this.Context.Server.MapPath(str6), true);
                string sourceFilename = this.Context.Request.MapPath(Globals.ApplicationPath + str6);
                string str8 = str3 + "/thumbs40/40_" + str4;
                string str9 = str3 + "/thumbs60/60_" + str4;
                string str10 = str3 + "/thumbs100/100_" + str4;
                string str11 = str3 + "/thumbs160/160_" + str4;
                string str12 = str3 + "/thumbs180/180_" + str4;
                string str13 = str3 + "/thumbs220/220_" + str4;
                string str14 = str3 + "/thumbs310/310_" + str4;
                string str15 = str3 + "/thumbs410/410_" + str4;
                str = str6;
                ResourcesHelper.CreateThumbnail(sourceFilename, this.Context.Request.MapPath(Globals.ApplicationPath + str8), 40, 40);
                ResourcesHelper.CreateThumbnail(sourceFilename, this.Context.Request.MapPath(Globals.ApplicationPath + str9), 60, 60);
                ResourcesHelper.CreateThumbnail(sourceFilename, this.Context.Request.MapPath(Globals.ApplicationPath + str10), 100, 100);
                ResourcesHelper.CreateThumbnail(sourceFilename, this.Context.Request.MapPath(Globals.ApplicationPath + str11), 160, 160);
                ResourcesHelper.CreateThumbnail(sourceFilename, this.Context.Request.MapPath(Globals.ApplicationPath + str12), 180, 180);
                ResourcesHelper.CreateThumbnail(sourceFilename, this.Context.Request.MapPath(Globals.ApplicationPath + str13), 220, 220);
                ResourcesHelper.CreateThumbnail(sourceFilename, this.Context.Request.MapPath(Globals.ApplicationPath + str14), 310, 310);
                ResourcesHelper.CreateThumbnail(sourceFilename, this.Context.Request.MapPath(Globals.ApplicationPath + str15), 410, 410);
            }
            if (str.Length > 10)
            {
                str = "," + str;
            }
            return str;
        }

        private string MyTest(string text1)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(text1))
            {
                return str;
            }
            string[] strArray = text1.Trim().Trim(new char[] { ',' }).Split(new char[] { ',' });
            StringBuilder builder = new StringBuilder();
            foreach (string str2 in strArray)
            {
                if (str2.StartsWith("http://") && !str2.StartsWith("http://images.net.92hidc.com"))
                {
                    string[] strArray2 = str2.Split(new char[] { '/' });
                    builder.Append(str2.Replace("http://" + strArray2[2], "").Trim() + ",");
                }
                else
                {
                    builder.Append(str2 + ",");
                }
            }
            return builder.ToString().Trim(new char[] { ',' });
        }

        private void MyTestA(string text1, string text2)
        {
            if (!string.IsNullOrEmpty(text1))
            {
                string[] strArray = text1.Split(new char[] { ',' });
                string str = "," + text2 + ",";
                foreach (string str2 in strArray)
                {
                    if (!str.Contains("," + str2 + ",") && str2.StartsWith("/Storage/"))
                    {
                        this.DoDelete(this.Context.Server.MapPath(str2));
                    }
                }
            }
        }

        private void DoDelete(string text1)
        {
            if (((this.m_FlashUploadType == "product") || this.m_FlashUploadType.Equals("gift")) && this.CheckUploadFileType(text1))
            {
                string path = text1.Replace(@"\images\", @"\thumbs40\40_");
                string str2 = text1.Replace(@"\images\", @"\thumbs60\60_");
                string str3 = text1.Replace(@"\images\", @"\thumbs100\100_");
                // string str4 = text1.Replace(@"\images\", @"\thumbs100\100_");
                string str4 = text1.Replace(@"\images\", @"\thumbs160\160_");
                string str5 = text1.Replace(@"\images\", @"\thumbs180\180_");
                string str6 = path.Replace(@"\images\", @"\thumbs220\220_");
                string str7 = text1.Replace(@"\images\", @"\thumbs310\310_");
                string str8 = text1.Replace(@"\images\", @"\thumbs410\410_");
                if (File.Exists(text1))
                {
                    File.Delete(text1);
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                if (File.Exists(str2))
                {
                    File.Delete(str2);
                }
                if (File.Exists(str3))
                {
                    File.Delete(str3);
                }
                if (File.Exists(str4))
                {
                    File.Delete(str4);
                }
                if (File.Exists(str5))
                {
                    File.Delete(str5);
                }
                if (File.Exists(str6))
                {
                    File.Delete(str6);
                }
                if (File.Exists(str7))
                {
                    File.Delete(str7);
                }
                if (File.Exists(str8))
                {
                    File.Delete(str8);
                }
            }
        }

        private bool CheckFileExists(string text1)
        {
            if (!this.CheckUploadFileType(text1))
            {
                return false;
            }
            if (text1.ToLower().IndexOf("http://") < 0)
            {
                return File.Exists(this.Page.Request.MapPath(Globals.ApplicationPath + text1));
            }
            return true;
        }

        //private bool AGeIcrrVo5pC(snrJL(string text1)
        //{
        //    if (!this.AHxCaphXxC5uvu(text1))
        //    {
        //        return false;
        //    }
        //    if (text1.ToLower().IndexOf("http://") < 0)
        //    {
        //        return File.Exists(this.Page.Request.MapPath(Globals.ApplicationPath + text1));
        //    }
        //    return true;
        //}

        //private bool AHxCaphXxC5uvu(string text1)
        //{
        //    if (!string.IsNullOrEmpty(text1))
        //    {
        //        string str = text1.ToUpper();
        //        if ((str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBKAFAARwA=")) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBHAEkARgA="))) || ((str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBQAE4ARwA=")) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBCAE0AUAA="))) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBKAFAARQBHAA=="))))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        private bool CheckUploadFileType(string text1)
        {
            if (!string.IsNullOrEmpty(text1))
            {
                string str = text1.ToUpper();
                if (str.Contains(".JPG") || str.Contains(".GIF") || str.Contains(".PNG") || str.Contains(".BMP") || str.Contains(".JPEG"))
                {
                    return true;
                }
            }
            return false;
        }

        //private bool CheckUploadFileType(string text1)
        //{
        //    if (!string.IsNullOrEmpty(text1))
        //    {
        //        string str = text1.ToUpper();
        //        if (((str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBKAFAARwA=")) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBHAEkARgA="))) || ((str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBQAE4ARwA=")) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBCAE0AUAA="))) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBKAFAARQBHAA==")))) && (text1.Contains(this.Context.Server.MapPath(Globals.GetStoragePath() + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=") + this.m_FlashUploadType)) || text1.Contains(this.Context.Server.MapPath(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB1AHQAaQBsAGkAdAB5AC8AcABpAGMAcwAvAG4AbwBuAGUALgBnAGkAZgA=")))))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            WebControl child = new WebControl(HtmlTextWriterTag.Input);
            child.Attributes.Add("id", this.ID.ToString() + "_hdPhotoListOriginal");
            child.Attributes.Add("name", this.ID.ToString() + "_hdPhotoListOriginal");
            child.Attributes.Add("type", "hidden");
            child.Attributes.Add("value", this.m_Value);
            WebControl control2 = new WebControl(HtmlTextWriterTag.Input);
            control2.Attributes.Add("id", this.ID + "_hdPhotoList");
            control2.Attributes.Add("name", this.ID + "_hdPhotoList");
            control2.Attributes.Add("type", "hidden");
            control2.Attributes.Add("value", this.m_Value);
            WebControl control3 = new WebControl(HtmlTextWriterTag.Div);
            control3.Attributes.Add("id", this.ID + "_divFileProgressContainer");
            control3.Attributes.Add("style", "height: 75px;display:none;");
            Literal literal = new Literal();
            StringBuilder builder = new StringBuilder();
            string[] strArray = this.m_Value.Split(new char[] { ',' });
            foreach (string str in strArray)
            {
                builder.Append(this.GetContainer(str));
            }
            literal.Text = builder.ToString();
            Literal literal2 = new Literal
            {
                Text = "<div id=\"" + this.ID + "_divImgList\"><div class=\"picfirst\"></div>" + builder.ToString() + "</div>"
            };
            WebControl control4 = new WebControl(HtmlTextWriterTag.Div);
            control4.Attributes.Add("id", this.ID + "_divFlashUploadHolder");
            control4.Attributes.Add("style", "width: 91px; margin: 0px 10px;float: left;");
            this.Controls.Add(child);
            this.Controls.Add(control2);
            this.Controls.Add(control3);
            this.Controls.Add(literal2);
            this.Controls.Add(control4);
            int length = strArray.Length;
            if (string.IsNullOrEmpty(this.m_Value))
            {
                length = 0;
            }
            else if (this.m_MaxNum <= length)
            {
                control4.Style.Add("display", "none");
            }
            Literal literal3 = new Literal
            {
                Text = string.Concat(new object[] { 
                    "<script type=\"text/javascript\">var obj", this.ID, "_hdPhotoList = new FlashUploadObject(\"", this.ID, "_hdPhotoList\", \"", this.ID, "_divImgList\", \"", this.ID, "_divFlashUploadHolder\", \"", "picfirst\", \"", this.ID,"_divFileProgressContainer\", ", this.MaxNum, ",", length, ");obj", this.ID, 
                    "_hdPhotoList.upfilebuttonload();obj", this.ID, "_hdPhotoList.GetPhotoValue();</script>"
                 })
            };
            this.Controls.Add(literal3);
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            foreach (Control control in this.Controls)
            {
                control.RenderControl(writer);
                writer.WriteLine();
            }
            writer.WriteLine();
        }

        public override ControlCollection Controls
        {
            get
            {
                this.EnsureChildControls();
                return base.Controls;
            }
        }

        public string FlashUploadType
        {
            get
            {
                return this.m_FlashUploadType;
            }
            set
            {
                this.m_FlashUploadType = value;
            }
        }

        public int MaxNum
        {
            get
            {
                return this.m_MaxNum;
            }
            set
            {
                this.m_MaxNum = value;
            }
        }

        [Browsable(false)]
        public string OldValue
        {
            get
            {
                return this.Context.Request.Form[this.ID + "_hdPhotoListOriginal"];
            }
            set
            {
                this.m_OldValue = value;
            }
        }

        public string Value
        {
            get
            {
                return this.ABOZyE4bfGcbd5Z8N5SO7b2(this.Context.Request.Form[this.ID + "_hdPhotoList"]);
            }
            set
            {
                string str = this.MyTest(value);
                this.m_Value = str;
                this.m_OldValue = str;
            }
        }
    }
}

