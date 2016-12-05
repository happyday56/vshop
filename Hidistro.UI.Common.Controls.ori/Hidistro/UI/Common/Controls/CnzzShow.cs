namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using System;
    using System.Web.UI;

    public class CnzzShow : LiteralControl
    {
        protected override void OnLoad(EventArgs e)
        {
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
            if ((masterSettings.EnabledCnzz && !string.IsNullOrEmpty(masterSettings.CnzzPassword)) && !string.IsNullOrEmpty(masterSettings.CnzzUsername))
            {
                base.Text = AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABzAGMAcgBpAHAAdAAgAHMAcgBjAD0AJwBoAHQAdABwADoALwAvAHAAdwAuAGMAbgB6AHoALgBjAG8AbQAvAGMALgBwAGgAcAA/AGkAZAA9AA==") + masterSettings.CnzzUsername + AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("JgBsAD0AMgAnACAAbABhAG4AZwB1AGEAZwBlAD0AJwBKAGEAdgBhAFMAYwByAGkAcAB0ACcAIABjAGgAYQByAHMAZQB0AD0AJwBnAGIAMgAzADEAMgAnAD4APAAvAHMAYwByAGkAcAB0AD4A");
            }
            base.OnLoad(e);
        }
    }
}

