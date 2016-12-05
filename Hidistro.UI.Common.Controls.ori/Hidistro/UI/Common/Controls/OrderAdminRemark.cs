namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class OrderAdminRemark : Label
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH = "adminremark";

        protected override void OnDataBinding(EventArgs e)
        {
            object obj2 = DataBinder.Eval(this.Page.GetDataItem(), this.DataField);
            if (((obj2 != null) && (obj2 != DBNull.Value)) && !string.IsNullOrEmpty(obj2.ToString()))
            {
                base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABpAG0AZwAgAHMAcgBjAD0AIgAuAC4ALwBpAG0AYQBnAGUAcwAvAHgAaQAuAGcAaQBmACIAIAAvAD4A");
                base.ToolTip = obj2.ToString();
            }
            else
            {
                base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LQA=");
            }
            base.OnDataBinding(e);
        }

        public string DataField
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
    }
}

