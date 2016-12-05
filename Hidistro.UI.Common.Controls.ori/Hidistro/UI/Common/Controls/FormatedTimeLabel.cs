namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using System;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class FormatedTimeLabel : Literal
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH;
        private string ABOZyE4bfGcbd5Z8N5SO7b2 = "-";
        private string AC2pFbvHCa1v4C1DIZ8SngolueQb = string.Empty;
        private bool AD)2Z(NKy = true;

        public override void DataBind()
        {
            if (this.DataField != null)
            {
                this.Time = DataBinder.Eval(this.Page.GetDataItem(), this.DataField);
            }
            else
            {
                base.DataBind();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (((this.Time == null) || (this.Time == DBNull.Value)) || (Convert.ToDateTime((DateTime) this.Time, CultureInfo.InvariantCulture) == DateTime.MinValue))
            {
                base.Text = this.NullToDisplay;
            }
            else
            {
                DateTime time = (DateTime) this.Time;
                if (!string.IsNullOrEmpty(this.FormatDateTime))
                {
                    base.Text = time.ToString(this.FormatDateTime, CultureInfo.InvariantCulture);
                }
                else if (this.ShopTime)
                {
                    base.Text = time.ToString(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("eQB5AHkAeQAtAE0ATQAtAGQAZAAgAEgASAA6AG0AbQA6AHMAcwA="), CultureInfo.InvariantCulture);
                }
                else
                {
                    base.Text = time.ToString(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("eQB5AHkAeQAtAE0ATQAtAGQAZAA="), CultureInfo.InvariantCulture);
                }
                base.Render(writer);
            }
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

        public string FormatDateTime
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

        public string NullToDisplay
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

        public bool ShopTime
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

        public object Time
        {
            get
            {
                if (this.ViewState[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABpAG0AZQA=")] == null)
                {
                    return null;
                }
                return this.ViewState[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABpAG0AZQA=")];
            }
            set
            {
                if ((value == null) || (value == DBNull.Value))
                {
                    this.ViewState[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABpAG0AZQA=")] = null;
                }
                else
                {
                    this.ViewState[A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABpAG0AZQA=")] = value;
                }
            }
        }
    }
}

