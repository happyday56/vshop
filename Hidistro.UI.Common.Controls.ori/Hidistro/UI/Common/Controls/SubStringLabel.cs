namespace Hidistro.UI.Common.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class SubStringLabel : Literal
    {
        private int AAZ0JeEma(2x58C7QQ)d9L4MH;
        private string ABOZyE4bfGcbd5Z8N5SO7b2 = "...";
        private string AC2pFbvHCa1v4C1DIZ8SngolueQb;

        protected override void OnDataBinding(EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Field))
            {
                object obj2 = DataBinder.Eval(this.Page.GetDataItem(), this.Field);
                if ((obj2 != null) && (obj2 != DBNull.Value))
                {
                    base.Text = (string) obj2;
                }
            }
            base.OnDataBinding(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if ((this.StrLength > 0) && (this.StrLength < base.Text.Length))
            {
                base.Text = base.Text.Substring(0, this.StrLength) + this.StrReplace;
            }
            base.Render(writer);
        }

        public string Field
        {
            get
            {
                return this.AC2pFbvHCa1v4C1DIZ8SngolueQb;
            }
            set
            {
                this.AC2pFbvHCa1v4C1DIZ8SngolueQb = value;
            }
        }

        public int StrLength
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

        public string StrReplace
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
    }
}

