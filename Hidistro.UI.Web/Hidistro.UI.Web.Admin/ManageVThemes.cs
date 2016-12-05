using Hidistro.Core;
using Hidistro.Core.Configuration;
using Hidistro.Core.Entities;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
namespace Hidistro.UI.Web.Admin
{
	public class ManageVThemes : AdminPage
	{
		protected System.Web.UI.WebControls.DataList dtManageThemes;
		protected System.Web.UI.WebControls.Literal lblThemeCount;
		protected System.Web.UI.WebControls.Literal litThemeName;
		private void BindData(SiteSettings siteSettings)
		{
			System.Collections.Generic.IList<ManageThemeInfo> list = this.LoadThemes(siteSettings.VTheme);
			this.dtManageThemes.DataSource = list;
			this.dtManageThemes.DataBind();
			this.lblThemeCount.Text = list.Count.ToString();
		}
		private void dtManageThemes_ItemCommand(object sender, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
			{
				string name = this.dtManageThemes.DataKeys[e.Item.ItemIndex].ToString();
				if (e.CommandName == "btnUse")
				{
					this.UserThems(name);
					this.ShowMsg("成功修改了店铺模板", true);
				}
			}
		}
		protected System.Collections.Generic.IList<ManageThemeInfo> LoadThemes(string currentThemeName)
		{
			XmlDocument document = new XmlDocument();
			System.Collections.Generic.IList<ManageThemeInfo> list = new System.Collections.Generic.List<ManageThemeInfo>();
			string path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + HiConfiguration.GetConfig().FilesPath + "\\Templates\\vshop";
			string[] strArray = System.IO.Directory.Exists(path) ? System.IO.Directory.GetDirectories(path) : null;
			string[] array = strArray;
			for (int i = 0; i < array.Length; i++)
			{
				string str3 = array[i];
				System.IO.DirectoryInfo info2 = new System.IO.DirectoryInfo(str3);
				string str4 = info2.Name.ToLower(System.Globalization.CultureInfo.InvariantCulture);
				if (str4.Length > 0 && !str4.StartsWith("_"))
				{
					System.IO.FileInfo[] files = info2.GetFiles("template.xml");
					for (int j = 0; j < files.Length; j++)
					{
						System.IO.FileInfo info3 = files[j];
						ManageThemeInfo item = new ManageThemeInfo();
						System.IO.FileStream inStream = info3.OpenRead();
						document.Load(inStream);
						inStream.Close();
						item.Name = document.SelectSingleNode("root/Name").InnerText;
						item.ThemeImgUrl = string.Concat(new string[]
						{
							Globals.ApplicationPath,
							"/Templates/vshop/",
							str4,
							"/",
							document.SelectSingleNode("root/ImageUrl").InnerText
						});
						item.ThemeName = str4;
						if (string.Compare(item.ThemeName, currentThemeName) == 0)
						{
							this.litThemeName.Text = item.ThemeName;
						}
						list.Add(item);
					}
				}
			}
			return list;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
			this.litThemeName.Text = masterSettings.VTheme;
			this.dtManageThemes.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtManageThemes_ItemCommand);
			if (!this.Page.IsPostBack)
			{
				this.BindData(masterSettings);
			}
		}
		protected void UserThems(string name)
		{
			SiteSettings masterSettings = SettingsManager.GetMasterSettings(true);
			masterSettings.VTheme = name;
			SettingsManager.Save(masterSettings);
			HiCache.Remove("TemplateFileCache");
			this.BindData(masterSettings);
		}
	}
}
