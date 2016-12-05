namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using System;
    using System.Text;
    using System.Web.UI;

    public class LeaveListTime : Control
    {
        private string AAZ0JeEma(2x58C7QQ)d9L4MH = string.Empty;
        private string ABOZyE4bfGcbd5Z8N5SO7b2 = "productId";
        private string AC2pFbvHCa1v4C1DIZ8SngolueQb = "EndDate";
        private string AD)2Z(NKy = "StartDate";

        public override void DataBind()
        {
            int num = 1;
            int num2 = (int) DataBinder.Eval(this.Page.GetDataItem(), this.Auto);
            DateTime time = (DateTime) DataBinder.Eval(this.Page.GetDataItem(), this.BindData);
            DateTime time2 = (DateTime) DataBinder.Eval(this.Page.GetDataItem(), this.StartData);
            if (time < DateTime.Now)
            {
                num = 0;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IAA8AHMAYwByAGkAcAB0ACAAdAB5AHAAZQA9ACIAdABlAHgAdAAvAGoAYQB2AGEAcwBjAHIAaQBwAHQAIgA+ACAA"));
            builder.AppendFormat(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IABmAHUAbgBjAHQAaQBvAG4AIABMAGkAbQBpAHQAVABpAG0AZQBCAHUAeQBUAGkAbQBlAFMAaABvAHcAXwB7ADAAfQAoACkA"), num2.ToString());
            builder.Append(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IAB7ACAA"));
            builder.AppendFormat(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IABzAGgAbwB3AFQAaQBtAGUATABpAHMAdAAoACIAewAwAH0AIgAsACIAaAB0AG0AbABzAHAAYQBuAHsAMQB9ACIALAAiAHsAMgB9ACIALAAiAHsAMwB9ACIAKQA7AA=="), new object[] { time.ToString(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("eQB5AHkAeQAtAE0ATQAtAGQAZAAgAEgASAA6AG0AbQA6AHMAcwA=")), num2.ToString(), num, time2.ToString(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("eQB5AHkAeQAtAE0ATQAtAGQAZAAgAEgASAA6AG0AbQA6AHMAcwA=")) });
            builder.AppendFormat(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IABzAGUAdABUAGkAbQBlAG8AdQB0ACgAIgBMAGkAbQBpAHQAVABpAG0AZQBCAHUAeQBUAGkAbQBlAFMAaABvAHcAXwB7ADAAfQAoACkAIgAsACAAMQAwADAAMAApADsA"), num2.ToString());
            builder.Append(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IAB9AA=="));
            builder.AppendFormat(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IABMAGkAbQBpAHQAVABpAG0AZQBCAHUAeQBUAGkAbQBlAFMAaABvAHcAXwB7ADAAfQAoACkAOwAgAA=="), num2.ToString());
            builder.Append(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IAA8AC8AcwBjAHIAaQBwAHQAPgA="));
            this.AAZ0JeEma(2x58C7QQ)d9L4MH = builder.ToString();
            base.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(this.AAZ0JeEma(2x58C7QQ)d9L4MH.ToString());
        }

        public string Auto
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

        public string BindData
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

        public string StartData
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
    }
}

