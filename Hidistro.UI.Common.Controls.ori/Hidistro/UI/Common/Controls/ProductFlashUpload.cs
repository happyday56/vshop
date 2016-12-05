namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ProductFlashUpload : WebControl
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH = "product";
        private string ABOZyE4bfGcbd5Z8N5SO7b2 = string.Empty;
        private string AC2pFbvHCa1v4C1DIZ8SngolueQb = string.Empty;
        private int AD)2Z(NKy = 5;

        private string AAZ0JeEma(2x58C7QQ)d9L4MH(string text1)
        {
            string str = string.Empty;
            if (text1.Length > 10)
            {
                str = A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABkAGkAdgAgAGMAbABhAHMAcwA9ACIAdQBwAGwAbwBhAGQAaQBtAGEAZwBlAHMAIgA+ADwAZABpAHYAIABjAGwAYQBzAHMAPQAiAHAAcgBlAHYAaQBlAHcAIgA+ADwAZABpAHYAIABjAGwAYQBzAHMAPQAiAGQAaQB2AG8AcABlAHIAYQB0AG8AcgAiAD4APABhACAAaAByAGUAZgA9ACIAagBhAHYAYQBzAGMAcgBpAHAAdAA6ADsAIgAgAGMAbABhAHMAcwA9ACIAbABlAGYAdABtAG8AdgBlACIAIAB0AGkAdABsAGUAPQAiAOZd+3kiAD4AJgBsAHQAOwA8AC8AYQA+ADwAYQAgAGgAcgBlAGYAPQAiAGoAYQB2AGEAcwBjAHIAaQBwAHQAOgA7ACIAIABjAGwAYQBzAHMAPQAiAHIAaQBnAGgAdABtAG8AdgBlACIAIAB0AGkAdABsAGUAPQAiAPNT+3kiAD4AJgBnAHQAOwA8AC8AYQA+ADwAYQAgAGgAcgBlAGYAPQAiAGoAYQB2AGEAcwBjAHIAaQBwAHQAOgA7ACIAIABjAGwAYQBzAHMAPQAiAHAAaABvAHQAbwBkAGUAbAAiACAAdABpAHQAbABlAD0AIgAgUmSWIgA+AFgAPAAvAGEAPgA8AC8AZABpAHYAPgA8AGkAbQBnACAAcwB0AHkAbABlAD0AIgB3AGkAZAB0AGgAOgAgADgANQBwAHgAOwAgAGgAZQBpAGcAaAB0ADoAIAA4ADUAcAB4ADsAIgAgAHMAcgBjAD0AIgA=") + text1 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IgA+ADwALwBkAGkAdgA+ADwAZABpAHYAIABjAGwAYQBzAHMAPQAiAGEAYwB0AGkAbwBuAEIAbwB4ACIAPgA8AGEAIABoAHIAZQBmAD0AIgBqAGEAdgBhAHMAYwByAGkAcAB0ADoAOwAiACAAYwBsAGEAcwBzAD0AIgBhAGMAdABpAG8AbgBzACIAPgC+izpO2J6kizwALwBhAD4APAAvAGQAaQB2AD4APAAvAGQAaQB2AD4A");
            }
            return str;
        }

        private string ABOZyE4bfGcbd5Z8N5SO7b2(string text1)
        {
            string[] strArray = this.AD)2Z(NKy(text1).Trim().Trim(new char[] { ',' }).Split(new char[] { ',' });
            StringBuilder builder = new StringBuilder();
            foreach (string str in strArray)
            {
                builder.Append(this.AC2pFbvHCa1v4C1DIZ8SngolueQb(str.Trim()));
            }
            string str2 = builder.ToString().Trim(new char[] { ',' });
            this.AE)AZJK0MajbU(this.OldValue, str2);
            return str2;
        }

        private string AC2pFbvHCa1v4C1DIZ8SngolueQb(string text1)
        {
            string str = text1;
            if (str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGUAbQBwAC8A")) && (str.Length > 10))
            {
                string[] strArray = text1.Split(new char[] { '.' });
                if (strArray.Length <= 1)
                {
                    return "";
                }
                string str2 = strArray[strArray.Length - 1].Trim().ToLower();
                string str3 = Globals.GetStoragePath() + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=") + this.AAZ0JeEma(2x58C7QQ)d9L4MH;
                string str4 = Guid.NewGuid().ToString(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TgA="), CultureInfo.InvariantCulture) + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgA=") + str2;
                string path = str;
                string[] strArray2 = text1.Split(new char[] { '/' });
                if (path.StartsWith(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAB0AHQAcAA6AC8ALwA=")))
                {
                    path = path.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAB0AHQAcAA6AC8ALwA=") + strArray2[2], "");
                }
                path = this.Context.Server.MapPath(path);
                string str6 = str3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBpAG0AYQBnAGUAcwAvAA==") + str4;
                File.Copy(path, this.Context.Server.MapPath(str6), true);
                string sourceFilename = this.Context.Request.MapPath(Globals.ApplicationPath + str6);
                string str8 = str3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwA0ADAALwA0ADAAXwA=") + str4;
                string str9 = str3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwA2ADAALwA2ADAAXwA=") + str4;
                string str10 = str3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwAxADAAMAAvADEAMAAwAF8A") + str4;
                string str11 = str3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwAxADYAMAAvADEANgAwAF8A") + str4;
                string str12 = str3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwAxADgAMAAvADEAOAAwAF8A") + str4;
                string str13 = str3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwAyADIAMAAvADIAMgAwAF8A") + str4;
                string str14 = str3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwAzADEAMAAvADMAMQAwAF8A") + str4;
                string str15 = str3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB0AGgAdQBtAGIAcwA0ADEAMAAvADQAMQAwAF8A") + str4;
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
                str = A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LAA=") + str;
            }
            return str;
        }

        private string AD)2Z(NKy(string text1)
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
                if (str2.StartsWith(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAB0AHQAcAA6AC8ALwA=")) && !str2.StartsWith(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAB0AHQAcAA6AC8ALwBpAG0AYQBnAGUAcwAuAG4AZQB0AC4AOQAyAGgAaQBkAGMALgBjAG8AbQA=")))
                {
                    string[] strArray2 = str2.Split(new char[] { '/' });
                    builder.Append(str2.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAB0AHQAcAA6AC8ALwA=") + strArray2[2], "").Trim() + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LAA="));
                }
                else
                {
                    builder.Append(str2 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LAA="));
                }
            }
            return builder.ToString().Trim(new char[] { ',' });
        }

        private void AE)AZJK0MajbU(string text1, string text2)
        {
            if (!string.IsNullOrEmpty(text1))
            {
                string[] strArray = text1.Split(new char[] { ',' });
                string str = A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LAA=") + text2 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LAA=");
                foreach (string str2 in strArray)
                {
                    if (!str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LAA=") + str2 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LAA=")) && str2.StartsWith(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBTAHQAbwByAGEAZwBlAC8A")))
                    {
                        this.AFCkJKk(this.Context.Server.MapPath(str2));
                    }
                }
            }
        }

        private void AFCkJKk(string text1)
        {
            if (((this.AAZ0JeEma(2x58C7QQ)d9L4MH == A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cAByAG8AZAB1AGMAdAA=")) || this.AAZ0JeEma(2x58C7QQ)d9L4MH.Equals(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZwBpAGYAdAA="))) && this.AIYbPEZFPIB(c7C(text1))
            {
                string path = text1.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XABpAG0AYQBnAGUAcwBcAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XAB0AGgAdQBtAGIAcwA0ADAAXAA0ADAAXwA="));
                string str2 = text1.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XABpAG0AYQBnAGUAcwBcAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XAB0AGgAdQBtAGIAcwA2ADAAXAA2ADAAXwA="));
                string str3 = text1.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XABpAG0AYQBnAGUAcwBcAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XAB0AGgAdQBtAGIAcwAxADAAMABcADEAMAAwAF8A"));
                string str4 = text1.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XABpAG0AYQBnAGUAcwBcAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XAB0AGgAdQBtAGIAcwAxADYAMABcADEANgAwAF8A"));
                string str5 = text1.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XABpAG0AYQBnAGUAcwBcAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XAB0AGgAdQBtAGIAcwAxADgAMABcADEAOAAwAF8A"));
                string str6 = text1.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XABpAG0AYQBnAGUAcwBcAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XAB0AGgAdQBtAGIAcwAyADIAMABcADIAMgAwAF8A"));
                string str7 = text1.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XABpAG0AYQBnAGUAcwBcAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XAB0AGgAdQBtAGIAcwAzADEAMABcADMAMQAwAF8A"));
                string str8 = text1.Replace(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XABpAG0AYQBnAGUAcwBcAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XAB0AGgAdQBtAGIAcwA0ADEAMABcADQAMQAwAF8A"));
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

        private bool AGeIcrrVo5pC(snrJL(string text1)
        {
            if (!this.AHxCaphXxC5uvu(text1))
            {
                return false;
            }
            if (text1.ToLower().IndexOf(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aAB0AHQAcAA6AC8ALwA=")) < 0)
            {
                return File.Exists(this.Page.Request.MapPath(Globals.ApplicationPath + text1));
            }
            return true;
        }

        private bool AHxCaphXxC5uvu(string text1)
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

        private bool AIYbPEZFPIB(c7C(string text1)
        {
            if (!string.IsNullOrEmpty(text1))
            {
                string str = text1.ToUpper();
                if (((str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBKAFAARwA=")) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBHAEkARgA="))) || ((str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBQAE4ARwA=")) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBCAE0AUAA="))) || str.Contains(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LgBKAFAARQBHAA==")))) && (text1.Contains(this.Context.Server.MapPath(Globals.GetStoragePath() + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=") + this.AAZ0JeEma(2x58C7QQ)d9L4MH)) || text1.Contains(this.Context.Server.MapPath(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwB1AHQAaQBsAGkAdAB5AC8AcABpAGMAcwAvAG4AbwBuAGUALgBnAGkAZgA=")))))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            WebControl child = new WebControl(HtmlTextWriterTag.Input);
            child.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), this.ID.ToString() + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBoAGQAUABoAG8AdABvAEwAaQBzAHQATwByAGkAZwBpAG4AYQBsAA=="));
            child.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bgBhAG0AZQA="), this.ID.ToString() + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBoAGQAUABoAG8AdABvAEwAaQBzAHQATwByAGkAZwBpAG4AYQBsAA=="));
            child.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dAB5AHAAZQA="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aABpAGQAZABlAG4A"));
            child.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAGwAdQBlAA=="), this.ABOZyE4bfGcbd5Z8N5SO7b2);
            WebControl control2 = new WebControl(HtmlTextWriterTag.Input);
            control2.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBoAGQAUABoAG8AdABvAEwAaQBzAHQA"));
            control2.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bgBhAG0AZQA="), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBoAGQAUABoAG8AdABvAEwAaQBzAHQA"));
            control2.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dAB5AHAAZQA="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aABpAGQAZABlAG4A"));
            control2.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dgBhAGwAdQBlAA=="), this.ABOZyE4bfGcbd5Z8N5SO7b2);
            WebControl control3 = new WebControl(HtmlTextWriterTag.Div);
            control3.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBkAGkAdgBGAGkAbABlAFAAcgBvAGcAcgBlAHMAcwBDAG8AbgB0AGEAaQBuAGUAcgA="));
            control3.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwB0AHkAbABlAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aABlAGkAZwBoAHQAOgAgADcANQBwAHgAOwBkAGkAcwBwAGwAYQB5ADoAbgBvAG4AZQA7AA=="));
            Literal literal = new Literal();
            StringBuilder builder = new StringBuilder();
            string[] strArray = this.ABOZyE4bfGcbd5Z8N5SO7b2.Split(new char[] { ',' });
            foreach (string str in strArray)
            {
                builder.Append(this.AAZ0JeEma(2x58C7QQ)d9L4MH(str));
            }
            literal.Text = builder.ToString();
            Literal literal2 = new Literal {
                Text = A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABkAGkAdgAgAGkAZAA9ACIA") + this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBkAGkAdgBJAG0AZwBMAGkAcwB0ACIAPgA8AGQAaQB2ACAAYwBsAGEAcwBzAD0AIgBwAGkAYwBmAGkAcgBzAHQAIgA+ADwALwBkAGkAdgA+AA==") + builder.ToString() + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PAAvAGQAaQB2AD4A")
            };
            WebControl control4 = new WebControl(HtmlTextWriterTag.Div);
            control4.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBkAA=="), this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBkAGkAdgBGAGwAYQBzAGgAVQBwAGwAbwBhAGQASABvAGwAZABlAHIA"));
            control4.Attributes.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("cwB0AHkAbABlAA=="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dwBpAGQAdABoADoAIAA5ADEAcAB4ADsAIABtAGEAcgBnAGkAbgA6ACAAMABwAHgAIAAxADAAcAB4ADsAZgBsAG8AYQB0ADoAIABsAGUAZgB0ADsA"));
            this.Controls.Add(child);
            this.Controls.Add(control2);
            this.Controls.Add(control3);
            this.Controls.Add(literal2);
            this.Controls.Add(control4);
            int length = strArray.Length;
            if (string.IsNullOrEmpty(this.ABOZyE4bfGcbd5Z8N5SO7b2))
            {
                length = 0;
            }
            else if (this.AD)2Z(NKy <= length)
            {
                control4.Style.Add(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("ZABpAHMAcABsAGEAeQA="), A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("bgBvAG4AZQA="));
            }
            Literal literal3 = new Literal {
                Text = string.Concat(new object[] { 
                    A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAGMAcgBpAHAAdAAgAHQAeQBwAGUAPQAiAHQAZQB4AHQALwBqAGEAdgBhAHMAYwByAGkAcAB0ACIAPgB2AGEAcgAgAG8AYgBqAA=="), this.ID, A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBoAGQAUABoAG8AdABvAEwAaQBzAHQAIAA9ACAAbgBlAHcAIABGAGwAYQBzAGgAVQBwAGwAbwBhAGQATwBiAGoAZQBjAHQAKAAiAA=="), this.ID, A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBoAGQAUABoAG8AdABvAEwAaQBzAHQAIgAsACAAIgA="), this.ID, A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBkAGkAdgBJAG0AZwBMAGkAcwB0ACIALAAgACIA"), this.ID, A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBkAGkAdgBGAGwAYQBzAGgAVQBwAGwAbwBhAGQASABvAGwAZABlAHIAIgAsACAAIgBwAGkAYwBmAGkAcgBzAHQAIgAsACAAIgA="), this.ID, A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBkAGkAdgBGAGkAbABlAFAAcgBvAGcAcgBlAHMAcwBDAG8AbgB0AGEAaQBuAGUAcgAiACwAIAA="), this.MaxNum, A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LAA="), length, A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("KQA7AG8AYgBqAA=="), this.ID, 
                    A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBoAGQAUABoAG8AdABvAEwAaQBzAHQALgB1AHAAZgBpAGwAZQBiAHUAdAB0AG8AbgBsAG8AYQBkACgAKQA7AG8AYgBqAA=="), this.ID, A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBoAGQAUABoAG8AdABvAEwAaQBzAHQALgBHAGUAdABQAGgAbwB0AG8AVgBhAGwAdQBlACgAKQA7ADwALwBzAGMAcgBpAHAAdAA+AA==")
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
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH;
            }
            set
            {
                this.AAZ0JeEma(2x58C7QQ)d9L4MH = value;
            }
        }

        public int MaxNum
        {
            get
            {
                return this.AD)2Z(NKy;
            }
            set
            {
                this.AD)2Z(NKy = value;
            }
        }

        [Browsable(false)]
        public string OldValue
        {
            get
            {
                return this.Context.Request.Form[this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBoAGQAUABoAG8AdABvAEwAaQBzAHQATwByAGkAZwBpAG4AYQBsAA==")];
            }
            set
            {
                this.AC2pFbvHCa1v4C1DIZ8SngolueQb = value;
            }
        }

        public string Value
        {
            get
            {
                return this.ABOZyE4bfGcbd5Z8N5SO7b2(this.Context.Request.Form[this.ID + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("XwBoAGQAUABoAG8AdABvAEwAaQBzAHQA")]);
            }
            set
            {
                string str = this.AD)2Z(NKy(value);
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = str;
                this.AC2pFbvHCa1v4C1DIZ8SngolueQb = str;
            }
        }
    }
}

