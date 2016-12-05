using Hidistro.ControlPanel.Store;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class EditMenu : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnAddMenu;
		protected System.Web.UI.WebControls.DropDownList ddlType;
		protected System.Web.UI.WebControls.DropDownList ddlValue;
		protected System.Web.UI.WebControls.Literal lblParent;
		protected System.Web.UI.HtmlControls.HtmlGenericControl liBind;
		protected System.Web.UI.HtmlControls.HtmlGenericControl liParent;
		protected System.Web.UI.HtmlControls.HtmlGenericControl liUrl;
		protected System.Web.UI.HtmlControls.HtmlGenericControl liValue;
		protected System.Web.UI.WebControls.TextBox txtMenuName;
		protected System.Web.UI.WebControls.TextBox txtUrl;
		private void btnAddMenu_Click(object sender, System.EventArgs e)
		{
			MenuInfo menu = VShopHelper.GetMenu(base.GetUrlIntParam("MenuId"));
			menu.Name = this.txtMenuName.Text;
			menu.Type = "click";
			if (menu.ParentMenuId == 0)
			{
				menu.Type = "view";
			}
			else
			{
				if (string.IsNullOrEmpty(this.ddlType.SelectedValue) || this.ddlType.SelectedValue == "0")
				{
					this.ShowMsg("二级菜单必须绑定一个对象", false);
					return;
				}
			}
			menu.Bind = System.Convert.ToInt32(this.ddlType.SelectedValue);
			BindType bindType = menu.BindType;
			switch (bindType)
			{
			case BindType.Key:
				menu.ReplyId = System.Convert.ToInt32(this.ddlValue.SelectedValue);
				break;
			case BindType.Topic:
				menu.Content = this.ddlValue.SelectedValue;
				break;
			default:
				if (bindType == BindType.Url)
				{
					menu.Content = this.txtUrl.Text.Trim();
				}
				break;
			}
			if (VShopHelper.UpdateMenu(menu))
			{
				base.Response.Redirect("ManageMenu.aspx");
			}
			else
			{
				this.ShowMsg("添加失败", false);
			}
		}
		protected void ddlType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindType type = (BindType)System.Convert.ToInt32(this.ddlType.SelectedValue);
			BindType bindType = type;
			switch (bindType)
			{
			case BindType.Key:
			case BindType.Topic:
				this.liUrl.Visible = false;
				this.liValue.Visible = true;
				break;
			default:
				if (bindType != BindType.Url)
				{
					this.liUrl.Visible = false;
					this.liValue.Visible = false;
				}
				else
				{
					this.liUrl.Visible = true;
					this.liValue.Visible = false;
				}
				break;
			}
			switch (type)
			{
			case BindType.Key:
				this.ddlValue.DataSource = 
					from a in ReplyHelper.GetAllReply()
					where !string.IsNullOrWhiteSpace(a.Keys)
					select a;
				this.ddlValue.DataTextField = "Keys";
				this.ddlValue.DataValueField = "Id";
				this.ddlValue.DataBind();
				break;
			case BindType.Topic:
				this.ddlValue.DataSource = VShopHelper.Gettopics();
				this.ddlValue.DataTextField = "Title";
				this.ddlValue.DataValueField = "TopicId";
				this.ddlValue.DataBind();
				break;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnAddMenu.Click += new System.EventHandler(this.btnAddMenu_Click);
			if (!this.Page.IsPostBack)
			{
				this.liValue.Visible = false;
				this.liUrl.Visible = false;
				int urlIntParam = base.GetUrlIntParam("MenuId");
				MenuInfo menu = VShopHelper.GetMenu(urlIntParam);
				this.txtMenuName.Text = menu.Name;
				if (menu.ParentMenuId == 0)
				{
					if (VShopHelper.GetMenusByParentId(urlIntParam).Count > 0)
					{
						this.liBind.Visible = false;
					}
					this.liParent.Visible = false;
				}
				else
				{
					this.lblParent.Text = VShopHelper.GetMenu(menu.ParentMenuId).Name;
				}
				this.ddlType.SelectedValue = System.Convert.ToString((int)menu.BindType);
				BindType bindType = menu.BindType;
				switch (bindType)
				{
				case BindType.Key:
				case BindType.Topic:
					this.liUrl.Visible = false;
					this.liValue.Visible = true;
					break;
				default:
					if (bindType != BindType.Url)
					{
						this.liUrl.Visible = false;
						this.liValue.Visible = false;
					}
					else
					{
						this.liUrl.Visible = true;
						this.liValue.Visible = false;
					}
					break;
				}
				bindType = menu.BindType;
				switch (bindType)
				{
				case BindType.Key:
					this.ddlValue.DataSource = 
						from a in ReplyHelper.GetAllReply()
						where !string.IsNullOrWhiteSpace(a.Keys)
						select a;
					this.ddlValue.DataTextField = "Keys";
					this.ddlValue.DataValueField = "Id";
					this.ddlValue.DataBind();
					this.ddlValue.SelectedValue = menu.ReplyId.ToString();
					break;
				case BindType.Topic:
					this.ddlValue.DataSource = VShopHelper.Gettopics();
					this.ddlValue.DataTextField = "Title";
					this.ddlValue.DataValueField = "TopicId";
					this.ddlValue.DataBind();
					this.ddlValue.SelectedValue = menu.Content;
					break;
				default:
					if (bindType == BindType.Url)
					{
						this.txtUrl.Text = menu.Content;
					}
					break;
				}
			}
		}
	}
}
