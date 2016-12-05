using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
namespace Hidistro.UI.Web.Admin
{
	[AdministerCheck(true)]
	public class SetVThemes : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.HtmlControls.HtmlGenericControl customerbg;
		protected System.Web.UI.HtmlControls.HtmlAnchor delpic;
		protected System.Web.UI.HtmlControls.HtmlInputHidden fmSrc;
		protected HiImage imgPic;
		protected System.Web.UI.WebControls.Literal litThemeName;
		protected System.Web.UI.HtmlControls.HtmlImage littlepic;
		private string path;
		protected System.Web.UI.WebControls.RadioButton RadCustom;
		protected System.Web.UI.WebControls.RadioButton RadDefault;
		protected System.Web.UI.WebControls.TextBox txtMarketPrice;
		protected System.Web.UI.WebControls.TextBox txtNavigate;
		protected System.Web.UI.WebControls.TextBox txtPhone;
		protected System.Web.UI.WebControls.TextBox txtSalePrice;
		protected System.Web.UI.WebControls.TextBox txtTopicProductMaxNum;
		protected System.Web.UI.HtmlControls.HtmlGenericControl txtTopicProductMaxNumTip;
		private string vTheme;
		protected void btnOK_Click(object sender, System.EventArgs e)
		{
			XmlDocument xmlNode = this.GetXmlNode();
			if (this.RadDefault.Checked || string.IsNullOrWhiteSpace(this.fmSrc.Value))
			{
				xmlNode.SelectSingleNode("root/DefaultBg").InnerText = Globals.GetVshopSkinPath(this.vTheme) + "/images/ad/imgDefaultBg.jpg";
			}
			else
			{
				xmlNode.SelectSingleNode("root/DefaultBg").InnerText = this.fmSrc.Value;
			}
			xmlNode.SelectSingleNode("root/TopicProductMaxNum").InnerText = this.txtTopicProductMaxNum.Text;
			xmlNode.SelectSingleNode("root/MarketPrice").InnerText = this.txtMarketPrice.Text;
			xmlNode.SelectSingleNode("root/SalePrice").InnerText = this.txtSalePrice.Text;
			xmlNode.SelectSingleNode("root/Phone").InnerText = this.txtPhone.Text;
			xmlNode.SelectSingleNode("root/Navigate").InnerText = this.txtNavigate.Text;
			xmlNode.Save(this.path);
			HiCache.Remove("TemplateFileCache");
			base.Response.Redirect("ManageVthemes.aspx");
		}
		private XmlDocument GetXmlNode()
		{
			XmlDocument document = new XmlDocument();
			document.Load(this.path);
			return document;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.vTheme = base.Request.QueryString["themeName"];
			if (string.IsNullOrWhiteSpace(this.vTheme))
			{
				this.vTheme = SettingsManager.GetMasterSettings(true).VTheme;
			}
			this.path = System.Web.HttpContext.Current.Request.MapPath(Globals.ApplicationPath + "/Templates/vshop/" + this.vTheme + "/template.xml");
			this.litThemeName.Text = this.vTheme;
			if (!this.Page.IsPostBack)
			{
				XmlDocument xmlNode = this.GetXmlNode();
				this.txtTopicProductMaxNum.Text = xmlNode.SelectSingleNode("root/TopicProductMaxNum").InnerText;
				this.txtMarketPrice.Text = xmlNode.SelectSingleNode("root/MarketPrice").InnerText;
				this.txtSalePrice.Text = xmlNode.SelectSingleNode("root/SalePrice").InnerText;
				this.txtPhone.Text = xmlNode.SelectSingleNode("root/Phone").InnerText;
				this.txtNavigate.Text = xmlNode.SelectSingleNode("root/Navigate").InnerText;
				this.imgPic.ImageUrl = Globals.GetVshopSkinPath(this.vTheme) + "/images/ad/imgDefaultBg.jpg";
				if (xmlNode.SelectSingleNode("root/DefaultBg").InnerText.EndsWith("imgDefaultBg.jpg"))
				{
					this.RadDefault.Checked = true;
					this.delpic.Attributes.CssStyle.Add("display", "none");
					this.customerbg.Attributes.CssStyle.Add("display", "none");
					this.littlepic.Attributes.CssStyle.Add("display", "none");
				}
				else
				{
					this.littlepic.Src = xmlNode.SelectSingleNode("root/DefaultBg").InnerText;
					this.fmSrc.Value = xmlNode.SelectSingleNode("root/DefaultBg").InnerText;
					this.RadCustom.Checked = true;
					this.customerbg.Attributes.CssStyle.Remove("display");
					this.delpic.Attributes.CssStyle.Remove("display");
					this.littlepic.Attributes.CssStyle.Remove("display");
				}
			}
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
		}
	}
}
