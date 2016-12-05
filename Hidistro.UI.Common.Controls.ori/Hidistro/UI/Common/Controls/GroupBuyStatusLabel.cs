namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Entities.Promotions;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class GroupBuyStatusLabel : Label
    {
        private object AAZ0JeEma(2x58C7QQ)d9L4MH;

        protected override void Render(HtmlTextWriter writer)
        {
            switch (((GroupBuyStatus) this.GroupBuyStatusCode))
            {
                case GroupBuyStatus.UnderWay:
                    if (DateTime.Compare(Convert.ToDateTime(this.GroupBuyStartTime), DateTime.Now) > 0)
                    {
                        base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("2I8qZwBfy1k=");
                        break;
                    }
                    base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("Y2soV9uPTIgtTg==");
                    break;

                case GroupBuyStatus.EndUntreated:
                    base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("035fZypnBFkGdA==");
                    break;

                case GroupBuyStatus.Success:
                    base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("EGKfUtN+X2c=");
                    break;

                case GroupBuyStatus.Failed:
                    base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MVkljdN+X2c=");
                    break;

                case GroupBuyStatus.FailedWaitForReFund:
                    base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MVkljYVfAJA+aw==");
                    break;

                default:
                    base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("LQA=");
                    break;
            }
            base.Render(writer);
        }

        public object GroupBuyStartTime
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

        public object GroupBuyStatusCode
        {
            get
            {
                return this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RwByAG8AdQBwAEIAdQB5AFMAdABhAHQAdQBzAEMAbwBkAGUA")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RwByAG8AdQBwAEIAdQB5AFMAdABhAHQAdQBzAEMAbwBkAGUA")] = value;
            }
        }
    }
}

