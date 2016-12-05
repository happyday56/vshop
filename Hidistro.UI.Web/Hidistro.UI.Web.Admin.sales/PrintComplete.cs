using Hidistro.ControlPanel.Sales;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
namespace Hidistro.UI.Web.Admin.sales
{
	public class PrintComplete : AdminPage
	{
		protected string script;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string startNumber = base.Request["mailNo"];
			string[] orderIds = base.Request["orderIds"].Split(new char[]
			{
				','
			});
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			string[] array = orderIds;
			for (int i = 0; i < array.Length; i++)
			{
				string str2 = array[i];
				list.Add("'" + str2 + "'");
			}
			OrderHelper.SetOrderExpressComputerpe(string.Join(",", list.ToArray()), base.Request["templateName"], base.Request["templateName"]);
			OrderHelper.SetOrderShipNumber(orderIds, startNumber, base.Request["templateName"]);
			OrderHelper.SetOrderPrinted(orderIds);
		}
	}
}
