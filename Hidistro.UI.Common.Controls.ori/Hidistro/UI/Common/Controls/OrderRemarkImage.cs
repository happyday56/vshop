namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using Hidistro.Entities.Orders;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class OrderRemarkImage : Literal
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH = "<img border=\"0\" src=\"{0}\"  />";
        private string ABOZyE4bfGcbd5Z8N5SO7b2;

        protected string GetImageSrc(object managerMark)
        {
            string str = Globals.ApplicationPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBBAGQAbQBpAG4ALwBpAG0AYQBnAGUAcwAvAA==");
            switch (((OrderMark) managerMark))
            {
                case OrderMark.Draw:
                    return (str + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBjAG8AbgBhAGYALgBnAGkAZgA="));

                case OrderMark.ExclamationMark:
                    return (str + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBjAG8AbgBiAC4AZwBpAGYA"));

                case OrderMark.Red:
                    return (str + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBjAG8AbgBjAC4AZwBpAGYA"));

                case OrderMark.Green:
                    return (str + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBjAG8AbgBhAC4AZwBpAGYA"));

                case OrderMark.Yellow:
                    return (str + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBjAG8AbgBhAGQALgBnAGkAZgA="));

                case OrderMark.Gray:
                    return (str + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("aQBjAG8AbgBhAGUALgBnAGkAZgA="));
            }
            return string.Format(this.AAZ0JeEma(2x58C7QQ)d9L4MH, Globals.ApplicationPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBBAGQAbQBpAG4ALwBpAG0AYQBnAGUAcwAvAHgAaQAuAGcAaQBmAA=="));
        }

        protected override void OnDataBinding(EventArgs e)
        {
            object managerMark = DataBinder.Eval(this.Page.GetDataItem(), this.DataField);
            if ((managerMark != null) && (managerMark != DBNull.Value))
            {
                base.Text = string.Format(this.AAZ0JeEma(2x58C7QQ)d9L4MH, this.GetImageSrc(managerMark));
            }
            else
            {
                base.Text = string.Format(this.AAZ0JeEma(2x58C7QQ)d9L4MH, Globals.ApplicationPath + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LwBBAGQAbQBpAG4ALwBpAG0AYQBnAGUAcwAvAHgAaQAuAGcAaQBmAA=="));
            }
            base.OnDataBinding(e);
        }

        public string DataField
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

