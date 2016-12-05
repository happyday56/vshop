using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Weixin.MP.Api;
using Hishop.Weixin.MP.Domain;
using Hishop.Weixin.MP.Domain.Menu;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class ManageMenu : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSubmit;
		protected Grid grdMenu;
		private void BindData()
		{
			this.grdMenu.DataSource = VShopHelper.GetMenus();
			this.grdMenu.DataBind();
		}
		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			System.Collections.Generic.IList<MenuInfo> initMenus = VShopHelper.GetInitMenus();
			Hishop.Weixin.MP.Domain.Menu.Menu menu = new Hishop.Weixin.MP.Domain.Menu.Menu();
			foreach (MenuInfo info in initMenus)
			{
				if (info.Chilren == null || info.Chilren.Count == 0)
				{
					menu.menu.button.Add(this.BuildMenu(info));
				}
				else
				{
					SubMenu item = new SubMenu
					{
						name = info.Name
					};
					foreach (MenuInfo info2 in info.Chilren)
					{
						item.sub_button.Add(this.BuildMenu(info2));
					}
					menu.menu.button.Add(item);
				}
			}
			string json = JsonConvert.SerializeObject(menu.menu);
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
			if (string.IsNullOrEmpty(masterSettings.WeixinAppId) || string.IsNullOrEmpty(masterSettings.WeixinAppSecret))
			{
				base.Response.Write("<script>alert('您的服务号配置存在问题，请您先检查配置！');location.href='AppConfig.aspx'</script>");
			}
			else
			{
				if (MenuApi.CreateMenus(JsonConvert.DeserializeObject<Token>(TokenApi.GetToken(masterSettings.WeixinAppId, masterSettings.WeixinAppSecret)).access_token, json).Contains("ok"))
				{
					this.ShowMsg("成功的把自定义菜单保存到了微信", true);
				}
				else
				{
					this.ShowMsg("操作失败!服务号配置信息错误或没有微信自定义菜单权限", false);
				}
			}
		}
		private SingleButton BuildMenu(MenuInfo menu)
		{
			SingleButton result;
			switch (menu.BindType)
			{
			case BindType.Key:
				result = new SingleClickButton
				{
					name = menu.Name,
					key = menu.MenuId.ToString()
				};
				break;
			case BindType.Topic:
			case BindType.HomePage:
			case BindType.ProductCategory:
			case BindType.ShoppingCar:
			case BindType.OrderCenter:
			case BindType.MemberCard:
			case BindType.Url:
				result = new SingleViewButton
				{
					name = menu.Name,
					url = menu.Url
				};
				break;
			default:
				result = new SingleClickButton
				{
					name = menu.Name,
					key = "None"
				};
				break;
			}
			return result;
		}
		private void grdMenu_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			int rowIndex = ((System.Web.UI.WebControls.GridViewRow)((System.Web.UI.Control)e.CommandSource).NamingContainer).RowIndex;
			int menuId = (int)this.grdMenu.DataKeys[rowIndex].Value;
			if (e.CommandName == "Fall")
			{
				VShopHelper.SwapMenuSequence(menuId, false);
			}
			else
			{
				if (e.CommandName == "Rise")
				{
					VShopHelper.SwapMenuSequence(menuId, true);
				}
				else
				{
					if (e.CommandName == "DeleteMenu")
					{
						if (VShopHelper.DeleteMenu(menuId))
						{
							this.ShowMsg("成功删除了指定的分类", true);
						}
						else
						{
							this.ShowMsg("分类删除失败，未知错误", false);
						}
					}
				}
			}
			this.BindData();
		}
		private void grdMenu_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
		{
			if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
			{
				int num = (int)System.Web.UI.DataBinder.Eval(e.Row.DataItem, "ParentMenuId");
				string str = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Name").ToString();
				if (num == 0)
				{
					str = "<b>" + str + "</b>";
				}
				if (num > 0)
				{
					str = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + str;
				}
				System.Web.UI.WebControls.Literal literal = e.Row.FindControl("lblCategoryName") as System.Web.UI.WebControls.Literal;
				literal.Text = str;
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.grdMenu.RowCommand += new System.Web.UI.WebControls.GridViewCommandEventHandler(this.grdMenu_RowCommand);
			this.grdMenu.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(this.grdMenu_RowDataBound);
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			if (!this.Page.IsPostBack)
			{
				this.BindData();
			}
		}
		protected string RenderInfo(object menuIdObj)
		{
			ReplyInfo reply = ReplyHelper.GetReply((int)menuIdObj);
			string result;
			if (reply != null)
			{
				result = reply.Keys;
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
