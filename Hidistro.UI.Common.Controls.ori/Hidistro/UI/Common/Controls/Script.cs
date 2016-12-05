namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Script : Literal
    {
        private const string AAZ0JeEma(2x58C7QQ)d9L4MH = "<script src=\"{0}\" type=\"text/javascript\"></script>";
        private string ABOZyE4bfGcbd5Z8N5SO7b2;

        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.Src))
            {
                writer.Write(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAGMAcgBpAHAAdAAgAHMAcgBjAD0AIgB7ADAAfQAiACAAdAB5AHAAZQA9ACIAdABlAHgAdAAvAGoAYQB2AGEAcwBjAHIAaQBwAHQAIgA+ADwALwBzAGMAcgBpAHAAdAA+AA=="), this.Src);
            }
        }

        public virtual string Src
        {
            get
            {
                if (string.IsNullOrEmpty(this.ABOZyE4bfGcbd5Z8N5SO7b2))
                {
                    return null;
                }
                if (this.ABOZyE4bfGcbd5Z8N5SO7b2.StartsWith(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=")))
                {
                    return (Globals.ApplicationPath + this.ABOZyE4bfGcbd5Z8N5SO7b2);
                }
                return (Globals.ApplicationPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=") + this.ABOZyE4bfGcbd5Z8N5SO7b2);
            }
            set
            {
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = value;
            }
        }
    }
}

