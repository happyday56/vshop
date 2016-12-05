using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Store;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.SubjectProducts)]
	public class ProductTags : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnaddtag;
		protected System.Web.UI.WebControls.Button btnupdatetag;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdtagId;
		protected System.Web.UI.WebControls.Repeater rp_prducttag;
		protected System.Web.UI.WebControls.TextBox txtaddtagname;
		protected System.Web.UI.WebControls.TextBox txttagname;
		protected void btnaddtag_Click(object sender, System.EventArgs e)
		{
			string str = Globals.HtmlEncode(this.txtaddtagname.Text.Trim());
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("标签名称不允许为空！", false);
			}
			else
			{
				if (CatalogHelper.AddTags(str) > 0)
				{
					this.ShowMsg("添加商品标签成功！", true);
					this.ProductTagsBind();
				}
				else
				{
					this.ShowMsg("添加商品标签失败，请确认是否存在重名标签名称", false);
				}
			}
		}
		protected void btnupdatetag_Click(object sender, System.EventArgs e)
		{
			string str = this.hdtagId.Value.Trim();
			string str2 = Globals.HtmlEncode(this.txttagname.Text.Trim());
			if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str2))
			{
				this.ShowMsg("请选择要修改的商品标签或输入商品标签名称", false);
			}
			else
			{
				if (System.Convert.ToInt32(str) <= 0)
				{
					this.ShowMsg("选择的商品标签有误", false);
				}
				else
				{
					if (CatalogHelper.UpdateTags(System.Convert.ToInt32(str), str2))
					{
						this.ShowMsg("修改商品标签成功", true);
						this.ProductTagsBind();
					}
					else
					{
						this.ShowMsg("修改商品标签失败，请确认输入的商品标签名称是否存在同名", false);
					}
				}
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request["isAjax"]) && base.Request["isAjax"] == "true")
			{
				string str = base.Request["Mode"].ToString();
				string str2 = "false";
				string str3;
				if ((str3 = str) != null && str3 == "Add")
				{
					string str4 = "标签名称不允许为空";
					if (!string.IsNullOrEmpty(base.Request["TagValue"].Trim()))
					{
						str4 = "添加标签名称失败，请确认标签名是否已存在";
						int num = CatalogHelper.AddTags(Globals.HtmlEncode(base.Request["TagValue"].ToString()));
						if (num > 0)
						{
							str2 = "true";
							str4 = num.ToString();
						}
					}
					base.Response.Clear();
					base.Response.ContentType = "application/json";
					base.Response.Write(string.Concat(new string[]
					{
						"{\"Status\":\"",
						str2,
						"\",\"msg\":\"",
						str4,
						"\"}"
					}));
					base.Response.End();
				}
			}
			this.btnaddtag.Click += new System.EventHandler(this.btnaddtag_Click);
			this.btnupdatetag.Click += new System.EventHandler(this.btnupdatetag_Click);
			this.rp_prducttag.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.rp_prducttag_ItemCommand);
			if (!base.IsPostBack)
			{
				this.ProductTagsBind();
			}
		}
		protected void ProductTagsBind()
		{
			this.rp_prducttag.DataSource = CatalogHelper.GetTags();
			this.rp_prducttag.DataBind();
		}
		protected void rp_prducttag_ItemCommand(object sender, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			if (e.CommandName.Equals("delete"))
			{
				string str = e.CommandArgument.ToString();
				if (!string.IsNullOrEmpty(str) && System.Convert.ToInt32(str) > 0)
				{
					if (CatalogHelper.DeleteTags(System.Convert.ToInt32(str)))
					{
						this.ShowMsg("删除商品标签成功", true);
						this.ProductTagsBind();
					}
					else
					{
						this.ShowMsg("删除商品标签失败", false);
					}
				}
			}
		}
	}
}
