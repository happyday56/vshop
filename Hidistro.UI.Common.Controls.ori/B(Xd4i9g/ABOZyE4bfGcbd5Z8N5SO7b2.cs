namespace B(Xd4i9g
{
    using A0CvdP16Is;
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;

    internal class ABOZyE4bfGcbd5Z8N5SO7b2 : HtmlTextWriter
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH;

        internal ABOZyE4bfGcbd5Z8N5SO7b2(TextWriter writer1) : base(writer1)
        {
            this.AAZ0JeEma(2x58C7QQ)d9L4MH = HttpContext.Current.Request.RawUrl;
        }

        public override void WriteAttribute(string text1, string text2, bool flag1)
        {
            if ((this.AAZ0JeEma(2x58C7QQ)d9L4MH != null) && (string.Compare(text1, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("YQBjAHQAaQBvAG4A"), true) == 0))
            {
                text2 = this.AAZ0JeEma(2x58C7QQ)d9L4MH;
            }
            base.WriteAttribute(text1, text2, flag1);
        }
    }
}

