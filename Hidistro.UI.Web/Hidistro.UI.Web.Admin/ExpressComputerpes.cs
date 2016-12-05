using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.ExpressComputerpes)]
	public class ExpressComputerpes : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnCreateValue;
		protected System.Web.UI.WebControls.Button btnSearchButton;
		private string companyname = "";
		protected Grid grdExpresscomputors;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdcomputers;
		private string kuaidi100Code = "";
		//private string path = "";
		private string taobaoCode = "";
		protected System.Web.UI.WebControls.TextBox txtAddCmpName;
		protected System.Web.UI.WebControls.TextBox txtAddKuaidi100Code;
		protected System.Web.UI.WebControls.TextBox txtAddTaobaoCode;
		protected System.Web.UI.WebControls.TextBox txtcompany;
		protected System.Web.UI.WebControls.TextBox txtKuaidi100Code;
		protected System.Web.UI.WebControls.TextBox txtTaobaoCode;
		private void BindQuery()
		{
			NameValueCollection queryStrings = new NameValueCollection();
			if (!string.IsNullOrEmpty(this.txtcompany.Text.Trim()))
			{
				queryStrings.Add("cname", Globals.UrlEncode(this.txtcompany.Text.Trim()));
			}
			if (!string.IsNullOrEmpty(this.txtKuaidi100Code.Text.Trim()))
			{
				queryStrings.Add("kuaidi100Code", Globals.UrlEncode(this.txtKuaidi100Code.Text.Trim()));
			}
			if (!string.IsNullOrEmpty(this.txtTaobaoCode.Text.Trim()))
			{
				queryStrings.Add("taobaoCode", Globals.UrlEncode(this.txtTaobaoCode.Text.Trim()));
			}
			base.ReloadPage(queryStrings);
		}
		protected void btn_Click(object sender, System.EventArgs e)
		{
			this.ShowMsg("取消成功", true);
		}
		private void btnCreateValue_Click(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtAddCmpName.Text.Trim()))
			{
				this.ShowMsg("物流名称不允许为空！", false);
			}
			else
			{
				if (string.IsNullOrEmpty(this.txtAddKuaidi100Code.Text.Trim()))
				{
					this.ShowMsg("快递100Code不允许为空！", false);
				}
				else
				{
					if (string.IsNullOrEmpty(this.txtAddTaobaoCode.Text.Trim()))
					{
						this.ShowMsg("淘宝Code不允许为空！", false);
					}
					else
					{
						if (!string.IsNullOrEmpty(this.hdcomputers.Value.Trim()))
						{
							ExpressHelper.UpdateExpress(Globals.HtmlEncode(this.hdcomputers.Value.Trim()), Globals.HtmlEncode(this.txtAddCmpName.Text.Trim()), Globals.HtmlEncode(this.txtAddKuaidi100Code.Text.Trim()), Globals.HtmlEncode(this.txtAddTaobaoCode.Text.Trim()));
							this.ShowMsg("修改物流公司信息成功！", true);
						}
						else
						{
							if (ExpressHelper.IsExitExpress(this.txtAddCmpName.Text.Trim()))
							{
								this.ShowMsg("此物流公司已存在，请重新输入！", false);
								return;
							}
							ExpressHelper.AddExpress(Globals.HtmlEncode(this.txtAddCmpName.Text.Trim()), Globals.HtmlEncode(this.txtAddKuaidi100Code.Text.Trim()), Globals.HtmlEncode(this.txtAddTaobaoCode.Text.Trim()));
							this.ShowMsg("添加物流公司信息成功！", true);
						}
						this.LoadDataSource();
						this.txtAddCmpName.Text = "";
						this.txtAddKuaidi100Code.Text = "";
						this.txtAddTaobaoCode.Text = "";
						this.hdcomputers.Value = "";
					}
				}
			}
		}
		private void btnSearchButton_Click(object sender, System.EventArgs e)
		{
			this.BindQuery();
		}
		private void grdExpresscomputors_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			string name = (string)this.grdExpresscomputors.DataKeys[e.RowIndex].Value;
			ExpressHelper.DeleteExpress(name);
			this.ShowMsg("删除物流公司" + name + "成功", true);
			this.LoadDataSource();
		}
		private void LoadDataSource()
		{
			System.Data.DataTable expressTable = ExpressHelper.GetExpressTable();
			if (!string.IsNullOrEmpty(this.companyname))
			{
				expressTable.DefaultView.RowFilter = "Name like '%" + this.companyname + "%'";
			}
			if (!string.IsNullOrEmpty(this.kuaidi100Code))
			{
				expressTable.DefaultView.RowFilter = "Kuaidi100Code like '%" + this.kuaidi100Code + "%'";
			}
			if (!string.IsNullOrEmpty(this.taobaoCode))
			{
				expressTable.DefaultView.RowFilter = "TaobaoCode like '%" + this.taobaoCode + "%'";
			}
			this.grdExpresscomputors.DataSource = expressTable.DefaultView;
			this.grdExpresscomputors.DataBind();
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			base.OnInitComplete(e);
			this.btnCreateValue.Click += new System.EventHandler(this.btnCreateValue_Click);
			this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
			this.grdExpresscomputors.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(this.grdExpresscomputors_RowDeleting);
			if (!base.IsPostBack)
			{
				this.SearchQuery();
				this.LoadDataSource();
			}
		}
		private void SearchQuery()
		{
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["cname"]))
			{
				this.companyname = Globals.UrlDecode(DataHelper.CleanSearchString(this.Page.Request.QueryString["cname"]));
			}
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["kuaidi100Code"]))
			{
				this.kuaidi100Code = Globals.UrlDecode(DataHelper.CleanSearchString(this.Page.Request.QueryString["kuaidi100Code"]));
			}
			if (!string.IsNullOrEmpty(this.Page.Request.QueryString["taobaoCode"]))
			{
				this.taobaoCode = Globals.UrlDecode(DataHelper.CleanSearchString(this.Page.Request.QueryString["taobaoCode"]));
			}
			this.txtcompany.Text = this.companyname;
			this.txtKuaidi100Code.Text = this.kuaidi100Code;
			this.txtTaobaoCode.Text = this.taobaoCode;
		}
	}
}
