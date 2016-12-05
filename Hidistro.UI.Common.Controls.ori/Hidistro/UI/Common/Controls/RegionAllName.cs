namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Entities;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class RegionAllName : Literal
    {
        protected override void Render(HtmlTextWriter writer)
        {
            int currentRegionId = int.Parse(this.RegionId);
            string fullRegion = string.Empty;
            if (currentRegionId > 0)
            {
                fullRegion = RegionHelper.GetFullRegion(currentRegionId, AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IAAgAA=="));
            }
            base.Text = fullRegion;
            base.Render(writer);
        }

        public string RegionId
        {
            get
            {
                if (this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UgBlAGcAaQBvAG4ASQBkAA==")] == null)
                {
                    return null;
                }
                return (string) this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UgBlAGcAaQBvAG4ASQBkAA==")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("UgBlAGcAaQBvAG4ASQBkAA==")] = value;
            }
        }
    }
}

