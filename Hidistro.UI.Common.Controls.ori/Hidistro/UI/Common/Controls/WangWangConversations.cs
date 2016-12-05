namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using Hidistro.Core;
    using System;
    using System.Web.UI;

    public class WangWangConversations : Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.WangWangAccounts))
            {
                writer.WriteLine(string.Format(AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("PABhACAAdABhAHIAZwBlAHQAPQAiAF8AYgBsAGEAbgBrACIAIABoAHIAZQBmAD0AIgBoAHQAdABwADoALwAvAGEAbQBvAHMAMQAuAHQAYQBvAGIAYQBvAC4AYwBvAG0ALwBtAHMAZwAuAHcAdwA/AHYAPQAyACYAdQBpAGQAPQB7ADAAfQAmAHMAPQAxACIAIAA+ADwAaQBtAGcAIABiAG8AcgBkAGUAcgA9ACIAMAAiACAAcwByAGMAPQAiAGgAdAB0AHAAOgAvAC8AYQBtAG8AcwAxAC4AdABhAG8AYgBhAG8ALgBjAG8AbQAvAG8AbgBsAGkAbgBlAC4AdwB3AD8AdgA9ADIAJgB1AGkAZAA9AHsAMAB9ACYAcwA9ADEAIgAgAGEAbAB0AD0AIgC5cPtR2Y/Mkdl+EWLRU4htb2AiACAALwA+ADwALwBhAD4A"), this.WangWangAccounts));
            }
        }

        public string WangWangAccounts
        {
            get
            {
                if (this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dwBhAG4AZwBXAGEAbgBnAEEAYwBjAG8AdQBuAHQAcwA=")] == null)
                {
                    return null;
                }
                return (string) this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dwBhAG4AZwBXAGEAbgBnAEEAYwBjAG8AdQBuAHQAcwA=")];
            }
            set
            {
                this.ViewState[AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("dwBhAG4AZwBXAGEAbgBnAEEAYwBjAG8AdQBuAHQAcwA=")] = Globals.UrlEncode(value);
            }
        }
    }
}

