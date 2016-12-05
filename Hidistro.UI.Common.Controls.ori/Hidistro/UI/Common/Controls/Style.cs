namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Style : Literal
    {
        private const string AAZ0JeEma(2x58C7QQ)d9L4MH = "<link rel=\"stylesheet\" href=\"{0}\" type=\"text/css\" media=\"{1}\" />";
        private string ABOZyE4bfGcbd5Z8N5SO7b2;
        [CompilerGenerated]
        private string AC2pFbvHCa1v4C1DIZ8SngolueQb;

        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.Href))
            {
                writer.Write(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABsAGkAbgBrACAAcgBlAGwAPQAiAHMAdAB5AGwAZQBzAGgAZQBlAHQAIgAgAGgAcgBlAGYAPQAiAHsAMAB9ACIAIAB0AHkAcABlAD0AIgB0AGUAeAB0AC8AYwBzAHMAIgAgAG0AZQBkAGkAYQA9ACIAewAxAH0AIgAgAC8APgA="), this.Href, this.Media);
            }
        }

        public virtual string Href
        {
            get
            {
                if (string.IsNullOrEmpty(this.ABOZyE4bfGcbd5Z8N5SO7b2))
                {
                    return null;
                }
                if (this.ABOZyE4bfGcbd5Z8N5SO7b2.StartsWith(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=")))
                {
                    return (Globals.ApplicationPath + this.ABOZyE4bfGcbd5Z8N5SO7b2);
                }
                return (Globals.ApplicationPath + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwA=") + this.ABOZyE4bfGcbd5Z8N5SO7b2);
            }
            set
            {
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = value;
            }
        }

        [DefaultValue("screen")]
        public string Media
        {
            [CompilerGenerated]
            get
            {
                return this.AC2pFbvHCa1v4C1DIZ8SngolueQb;
            }
            [CompilerGenerated]
            set
            {
                this.AC2pFbvHCa1v4C1DIZ8SngolueQb = value;
            }
        }
    }
}

