using Hidistro.Core;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;

namespace Hidistro.UI.Web.Admin.vshop
{
	public partial class ShowActivityUrl : System.Web.UI.Page
	{
		protected string htmlActivityUrl = string.Empty;


		public string GetUrl(string type, int voteId)
		{
			object[] objArray;
			string empty = string.Empty;
			string str = type;
			string str1 = str;
			if (str != null)
			{
                if (str1 == "vote")
				{
                    object[] objArray1 = new object[] { "http://", Globals.DomainName, Globals.ApplicationPath, "/Vshop/Vote.aspx?voteId=", voteId };
					empty = string.Concat(objArray1);
					return empty;
				}
				else if (str1 == "baoming")
				{
                    object[] objArray2 = new object[] { "http://", Globals.DomainName, Globals.ApplicationPath, "/Vshop/Activity.aspx?id=", voteId };
					empty = string.Concat(objArray2);
					return empty;
				}
				else
				{
					if (str1 != "choujiang")
					{
						objArray = new object[] { "http://", Globals.DomainName, Globals.ApplicationPath, "/Vshop/Vote.aspx?voteId=", voteId };
						empty = string.Concat(objArray);
						return empty;
					}
					object[] objArray3 = new object[] { "http://", Globals.DomainName, Globals.ApplicationPath, "/Vshop/Ticket.aspx?id=", voteId };
					empty = string.Concat(objArray3);
					return empty;
				}
			}
			objArray = new object[] { "http://", Globals.DomainName, Globals.ApplicationPath, "/Vshop/Vote.aspx?voteId=", voteId };
			empty = string.Concat(objArray);
			return empty;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			string item = base.Request.QueryString["url"];
			if (!string.IsNullOrEmpty(item))
			{
				this.htmlActivityUrl = item;
				return;
			}
			base.Response.Write("²ÎÊý´íÎó£¡");
			base.Response.End();
		}
	}
}