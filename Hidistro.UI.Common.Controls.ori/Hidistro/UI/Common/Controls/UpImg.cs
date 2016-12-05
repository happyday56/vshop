namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class UpImg : WebControl
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH = string.Empty;
        private bool ABOZyE4bfGcbd5Z8N5SO7b2 = true;
        private string AC2pFbvHCa1v4C1DIZ8SngolueQb;
        private string AD)2Z(NKy;
        private string AE)AZJK0MajbU;
        private string AFCkJKk;
        private string AGeIcrrVo5pC(snrJL;
        private string AHxCaphXxC5uvu;
        private string AIYbPEZFPIB(c7C;
        private string AJK4pBZQRuLk5fMo8iYo;
        [CompilerGenerated]
        private Hidistro.UI.Common.Controls.UploadType AKCpNDo;

        public UpImg()
        {
            this.UploadType = Hidistro.UI.Common.Controls.UploadType.Product;
        }

        private bool AAZ0JeEma(2x58C7QQ)d9L4MH(string text1)
        {
            if (!this.ABOZyE4bfGcbd5Z8N5SO7b2(text1))
            {
                return false;
            }
            if (text1.ToLower().IndexOf(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAB0AHQAcAA6AC8ALwA=")) < 0)
            {
                return File.Exists(this.Page.Request.MapPath(Globals.ApplicationPath + text1));
            }
            return true;
        }

        private bool ABOZyE4bfGcbd5Z8N5SO7b2(string text1)
        {
            if (!string.IsNullOrEmpty(text1))
            {
                string str = text1.ToUpper();
                if ((str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBKAFAARwA=")) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBHAEkARgA="))) || ((str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBQAE4ARwA=")) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBCAE0AUAA="))) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBKAFAARQBHAA=="))))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("SABpAGQAaQBzAHQAcgBvAC4AVQBJAC4AQwBvAG0AbQBvAG4ALgBDAG8AbgB0AHIAbwBsAHMALgBJAG0AYQBnAGUAVQBwAGwAbwBhAGQAZQByAC4AaQBtAGEAZwBlAHMALgB1AHAAbABvAGEAZAAuAHAAbgBnAA=="));
            WebControl child = new WebControl(HtmlTextWriterTag.Div);
            string str2 = "";
            child.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBwAHIAZQB2AGkAZQB3AA=="));
            child.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwB0AHkAbABlAA=="), str2);
            child.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBsAGEAcwBzAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cAByAGUAdgBpAGUAdwA="));
            WebControl control2 = new WebControl(HtmlTextWriterTag.Div);
            control2.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwB1AHAAbABvAGEAZAA="));
            control2.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBsAGEAcwBzAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YQBjAHQAaQBvAG4AQgBvAHgA"));
            if ((Globals.GetCurrentManagerUserId() == 0) || this.IsUploaded)
            {
                control2.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwB0AHkAbABlAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZABpAHMAcABsAGEAeQA6AG4AbwBuAGUAOwA="));
            }
            WebControl control3 = new WebControl(HtmlTextWriterTag.A);
            control3.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAByAGUAZgA="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("agBhAHYAYQBzAGMAcgBpAHAAdAA6AHYAbwBpAGQAKAAwACkAOwA="));
            control3.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwB0AHkAbABlAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YgBhAGMAawBnAHIAbwB1AG4AZAAtAGkAbQBhAGcAZQA6ACAAdQByAGwAKAA=") + webResourceUrl + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("KQA7AA=="));
            control3.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBsAGEAcwBzAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZgBpAGwAZQBzAA=="));
            control3.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBjAG8AbgB0AGUAbgB0AA=="));
            control2.Controls.Add(control3);
            WebControl control4 = new WebControl(HtmlTextWriterTag.Div);
            WebControl control5 = new WebControl(HtmlTextWriterTag.A);
            control4.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBkAGUAbABlAHQAZQA="));
            control4.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBsAGEAcwBzAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YQBjAHQAaQBvAG4AQgBvAHgA"));
            if ((Globals.GetCurrentManagerUserId() == 0) || !this.IsUploaded)
            {
                control4.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwB0AHkAbABlAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZABpAHMAcABsAGEAeQA6AG4AbwBuAGUAOwA="));
            }
            control5.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAByAGUAZgA="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("agBhAHYAYQBzAGMAcgBpAHAAdAA6AEQAZQBsAGUAdABlAEkAbQBhAGcAZQAoACcA") + this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JwAsACcA") + this.UploadType.ToString().ToLower() + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JwAsAA==") + (this.ABOZyE4bfGcbd5Z8N5SO7b2 ? A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MQA=") : A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA=")) + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("KQA7AA=="));
            control5.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwB0AHkAbABlAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YgBhAGMAawBnAHIAbwB1AG4AZAAtAGkAbQBhAGcAZQA6ACAAdQByAGwAKAA=") + webResourceUrl + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("KQA7AA=="));
            control5.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YwBsAGEAcwBzAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YQBjAHQAaQBvAG4AcwA="));
            control4.Controls.Add(control5);
            this.Controls.Add(child);
            this.Controls.Add(control2);
            this.Controls.Add(control4);
            if (this.Page.Header.FindControl(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dQBwAGwAbwBhAGQAZQByAFMAdAB5AGwAZQA=")) == null)
            {
                WebControl control6 = new WebControl(HtmlTextWriterTag.Link);
                control6.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cgBlAGwA"), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwB0AHkAbABlAHMAaABlAGUAdAA="));
                control6.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAByAGUAZgA="), this.Page.ClientScript.GetWebResourceUrl(base.GetType(), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("SABpAGQAaQBzAHQAcgBvAC4AVQBJAC4AQwBvAG0AbQBvAG4ALgBDAG8AbgB0AHIAbwBsAHMALgBJAG0AYQBnAGUAVQBwAGwAbwBhAGQAZQByAC4AYwBzAHMALgBzAHQAeQBsAGUALgBjAHMAcwA=")));
                control6.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dAB5AHAAZQA="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dABlAHgAdAAvAGMAcwBzAA=="));
                control6.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bQBlAGQAaQBhAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwBjAHIAZQBlAG4A"));
                control6.ID = A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dQBwAGwAbwBhAGQAZQByAFMAdAB5AGwAZQA=");
                this.Page.Header.Controls.Add(control6);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.UploadedImageUrl = this.Context.Request.Form[this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwB1AHAAbABvAGEAZABlAGQASQBtAGEAZwBlAFUAcgBsAA==")];
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            foreach (Control control in this.Controls)
            {
                control.RenderControl(writer);
                writer.WriteLine();
            }
            if (!this.Page.ClientScript.IsStartupScriptRegistered(base.GetType(), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VQBwAGwAbwBhAGQAUwBjAHIAaQBwAHQA")))
            {
                string script = string.Format(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAGMAcgBpAHAAdAAgAHMAcgBjAD0AIgB7ADAAfQAiACAAdAB5AHAAZQA9ACIAdABlAHgAdAAvAGoAYQB2AGEAcwBjAHIAaQBwAHQAIgA+ADwALwBzAGMAcgBpAHAAdAA+AA=="), this.Page.ClientScript.GetWebResourceUrl(base.GetType(), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("SABpAGQAaQBzAHQAcgBvAC4AVQBJAC4AQwBvAG0AbQBvAG4ALgBDAG8AbgB0AHIAbwBsAHMALgBJAG0AYQBnAGUAVQBwAGwAbwBhAGQAZQByAC4AcwBjAHIAaQBwAHQALgB1AHAAaQBtAGcALgBqAHMA")));
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VQBwAGwAbwBhAGQAUwBjAHIAaQBwAHQA"), script, false);
            }
            if (!this.Page.ClientScript.IsStartupScriptRegistered(base.GetType(), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBJAG4AaQB0AFMAYwByAGkAcAB0AA==")))
            {
                string str2 = A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JAAoAGQAbwBjAHUAbQBlAG4AdAApAC4AcgBlAGEAZAB5ACgAZgB1AG4AYwB0AGkAbwBuACgAKQAgAHsAIABJAG4AaQB0AFUAcABsAG8AYQBkAGUAcgAoACIA") + this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgAsACAAIgA=") + this.UploadType.ToString().ToLower() + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgAsAA==") + (this.ABOZyE4bfGcbd5Z8N5SO7b2 ? A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MQA=") : A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA=")) + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("KQA7AA==");
                if (this.IsUploaded)
                {
                    string str3 = str2;
                    str2 = str3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VQBwAGQAYQB0AGUAUAByAGUAdgBpAGUAdwAoACcA") + this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JwAsACAAJwA=") + this.AAZ0JeEma(2x58C7QQ)d9L4MH + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JwApADsA");
                }
                str2 = str2 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("fQApADsA") + Environment.NewLine;
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBJAG4AaQB0AFMAYwByAGkAcAB0AA=="), str2, true);
            }
            writer.WriteLine();
            writer.AddAttribute(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwB1AHAAbABvAGEAZABlAGQASQBtAGEAZwBlAFUAcgBsAA=="));
            writer.AddAttribute(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bgBhAG0AZQA="), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwB1AHAAbABvAGEAZABlAGQASQBtAGEAZwBlAFUAcgBsAA=="));
            writer.AddAttribute(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAGwAdQBlAA=="), this.UploadedImageUrl);
            writer.AddAttribute(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dAB5AHAAZQA="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aABpAGQAZABlAG4A"));
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        public override ControlCollection Controls
        {
            get
            {
                this.EnsureChildControls();
                return base.Controls;
            }
        }

        public bool IsNeedThumbnail
        {
            get
            {
                return this.ABOZyE4bfGcbd5Z8N5SO7b2;
            }
            set
            {
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = value;
            }
        }

        public bool IsUploaded
        {
            get
            {
                return !string.IsNullOrEmpty(this.UploadedImageUrl);
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl100
        {
            get
            {
                return this.AE)AZJK0MajbU;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl160
        {
            get
            {
                return this.AFCkJKk;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl180
        {
            get
            {
                return this.AGeIcrrVo5pC(snrJL;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl220
        {
            get
            {
                return this.AHxCaphXxC5uvu;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl310
        {
            get
            {
                return this.AIYbPEZFPIB(c7C;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl40
        {
            get
            {
                return this.AC2pFbvHCa1v4C1DIZ8SngolueQb;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl410
        {
            get
            {
                return this.AJK4pBZQRuLk5fMo8iYo;
            }
        }

        [Browsable(false)]
        public string ThumbnailUrl60
        {
            get
            {
                return this.AD)2Z(NKy;
            }
        }

        [Browsable(false)]
        public string UploadedImageUrl
        {
            get
            {
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH;
            }
            set
            {
                if (this.AAZ0JeEma(2x58C7QQ)d9L4MH(value))
                {
                    this.AAZ0JeEma(2x58C7QQ)d9L4MH = value;
                    if (this.ABOZyE4bfGcbd5Z8N5SO7b2)
                    {
                        this.AC2pFbvHCa1v4C1DIZ8SngolueQb = value.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwA0ADAALwA0ADAAXwA="));
                        this.AD)2Z(NKy = value.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwA2ADAALwA2ADAAXwA="));
                        this.AE)AZJK0MajbU = value.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwAxADAAMAAvADEAMAAwAF8A"));
                        this.AFCkJKk = value.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwAxADYAMAAvADEANgAwAF8A"));
                        this.AGeIcrrVo5pC(snrJL = value.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwAxADgAMAAvADEAOAAwAF8A"));
                        this.AHxCaphXxC5uvu = value.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwAyADIAMAAvADIAMgAwAF8A"));
                        this.AIYbPEZFPIB(c7C = value.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwAzADEAMAAvADMAMQAwAF8A"));
                        this.AJK4pBZQRuLk5fMo8iYo = value.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwA0ADEAMAAvADQAMQAwAF8A"));
                    }
                }
            }
        }

        public Hidistro.UI.Common.Controls.UploadType UploadType
        {
            [CompilerGenerated]
            get
            {
                return this.AKCpNDo;
            }
            [CompilerGenerated]
            set
            {
                this.AKCpNDo = value;
            }
        }
    }
}

