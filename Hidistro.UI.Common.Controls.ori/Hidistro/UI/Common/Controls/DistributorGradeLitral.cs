namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core.Enums;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class DistributorGradeLitral : Literal
    {
        protected override void Render(HtmlTextWriter writer)
        {
            switch (((DistributorGrade) this.GradeId))
            {
                case DistributorGrade.OneDistributor:
                    base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("AE6nfg==");
                    break;

                case DistributorGrade.TowDistributor:
                    base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("jE6nfg==");
                    break;

                case DistributorGrade.ThreeDistributor:
                    base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("CU6nfg==");
                    break;

                default:
                    base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("gmbgZQ==");
                    break;
            }
            base.Render(writer);
        }

        public object GradeId
        {
            get
            {
                return this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RwByAGEAZABlAEkAZAA=")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("RwByAGEAZABlAEkAZAA=")] = value;
            }
        }
    }
}

