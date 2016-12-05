namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class FormatedMoneyLabel : Label
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH = "-";

        protected override void Render(HtmlTextWriter writer)
        {
            if ((this.Money != null) && (this.Money != DBNull.Value))
            {
                base.Text = Globals.FormatMoney((decimal) this.Money);
            }
            if (string.IsNullOrEmpty(base.Text))
            {
                base.Text = this.NullToDisplay;
            }
            base.Render(writer);
        }

        public object Money
        {
            get
            {
                if (this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TQBvAG4AZQB5AA==")] == null)
                {
                    return null;
                }
                return this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TQBvAG4AZQB5AA==")];
            }
            set
            {
                if ((value == null) || (value == DBNull.Value))
                {
                    this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TQBvAG4AZQB5AA==")] = null;
                }
                else
                {
                    this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("TQBvAG4AZQB5AA==")] = value;
                }
            }
        }

        public string NullToDisplay
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

