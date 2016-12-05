using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Enums;
using Hidistro.Entities.Members;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Data;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class DistributorStatistics : AdminPage
	{
		private int i;
		protected System.Web.UI.WebControls.Repeater reDistributor;
		private void BindData()
		{
			DistributorsQuery entity = new DistributorsQuery
			{
				GradeId = 0,
				StoreName = "",
				CellPhone = "",
				RealName = "",
				MicroSignal = "",
				ReferralStatus = 0,
				PageIndex = 1,
				PageSize = 15,
				SortOrder = SortAction.Desc,
				SortBy = "OrdersTotal"
			};
			Globals.EntityCoding(entity, true);
			System.Data.DataTable data = (System.Data.DataTable)VShopHelper.GetDistributors(entity).Data;
			this.reDistributor.DataSource = data;
			this.reDistributor.DataBind();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.reDistributor.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.reDistributor_ItemDataBound);
			if (!base.IsPostBack)
			{
				this.BindData();
			}
		}
		private void reDistributor_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
			{
				System.Web.UI.WebControls.Literal literal = (System.Web.UI.WebControls.Literal)e.Item.FindControl("litph");
				this.i++;
				if (this.i == 1)
				{
					literal.Text = "<img src=\"../images/0001.gif\"></img>";
				}
				else
				{
					if (this.i == 2)
					{
						literal.Text = "<img src=\"../images/0002.gif\"></img>";
					}
					else
					{
						if (this.i == 3)
						{
							literal.Text = "<img src=\"../images/0003.gif\"></img>";
						}
						else
						{
							literal.Text = "<span style=\"padding-left:10px;\">" + (int.Parse(literal.Text) + this.i).ToString() + "</span>";
						}
					}
				}
			}
		}
	}
}
