using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
    public class EditNavigate : AdminPage
    {
        protected System.Web.UI.WebControls.Button btnEditBanner;
        protected System.Web.UI.WebControls.DropDownList ddlSubType;
        protected System.Web.UI.WebControls.DropDownList ddlThridType;
        protected System.Web.UI.WebControls.DropDownList ddlType;
        protected System.Web.UI.HtmlControls.HtmlInputHidden fmSrc;
        protected string iconpath = string.Empty;
        private int id;
        protected System.Web.UI.HtmlControls.HtmlGenericControl liParent;
        protected System.Web.UI.HtmlControls.HtmlImage littlepic;
        protected System.Web.UI.HtmlControls.HtmlInputHidden locationUrl;
        protected System.Web.UI.HtmlControls.HtmlGenericControl navigateDesc;
        protected System.Web.UI.WebControls.Repeater RpIcon;
        protected System.Web.UI.WebControls.TextBox Tburl;
        protected string tplpath = Globals.GetVshopSkinPath(null) + "/images/deskicon/";
        protected System.Web.UI.WebControls.TextBox txtNavigateDesc;
        protected void BindIcons()
        {
            string str2;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(base.Server.MapPath("/Utility/icomoon") + "/icomoon.font"))
            {
                str2 = reader.ReadToEnd();
            }
            this.RpIcon.DataSource = str2.Split(new char[]
			{
				','
			});
            this.RpIcon.DataBind();
        }
        protected void btnEditBanner_Click(object sender, System.EventArgs e)
        {
            TplCfgInfo tplCfgById = VShopHelper.GetTplCfgById(this.id);
            tplCfgById.IsDisable = false;
            tplCfgById.ImageUrl = this.fmSrc.Value;
            tplCfgById.ShortDesc = this.txtNavigateDesc.Text;
            tplCfgById.LocationType = (LocationType)System.Enum.Parse(typeof(LocationType), this.ddlType.SelectedValue);
            tplCfgById.Url = this.locationUrl.Value;
            if (VShopHelper.UpdateTplCfg(tplCfgById))
            {
                this.CloseWindow();
            }
            else
            {
                this.ShowMsg("修改失败！", false);
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (int.TryParse(base.Request.QueryString["Id"], out this.id))
            {
                if (!this.Page.IsPostBack)
                {
                    this.ddlType.BindEnum<LocationType>("VipCard");
                    this.BindIcons();
                    this.Restore();
                }
            }
            else
            {
                base.Response.Redirect("ManageNavigate.aspx");
            }
        }
        protected void Restore()
        {
            TplCfgInfo tplCfgById = VShopHelper.GetTplCfgById(this.id);
            this.txtNavigateDesc.Text = tplCfgById.ShortDesc;
            this.ddlType.SelectedValue = tplCfgById.LocationType.ToString();
            if (!tplCfgById.ImageUrl.ToLower().Contains("storage/master/navigate"))
            {
                this.iconpath = tplCfgById.ImageUrl;
            }
            this.littlepic.Src = tplCfgById.ImageUrl;
            this.fmSrc.Value = tplCfgById.ImageUrl;
            switch (tplCfgById.LocationType)
            {
                //case LocationType.Topic:
                //    this.ddlSubType.Attributes.CssStyle.Remove("display");
                //    this.ddlSubType.DataSource = VShopHelper.Gettopics();
                //    this.ddlSubType.DataTextField = "Title";
                //    this.ddlSubType.DataValueField = "TopicId";
                //    this.ddlSubType.DataBind();
                //    this.ddlSubType.SelectedValue = tplCfgById.Url;
                //    break;
                case LocationType.Vote:
                    this.ddlSubType.Attributes.CssStyle.Remove("display");
                    this.ddlSubType.DataSource = StoreHelper.GetVoteList();
                    this.ddlSubType.DataTextField = "VoteName";
                    this.ddlSubType.DataValueField = "VoteId";
                    this.ddlSubType.DataBind();
                    this.ddlSubType.SelectedValue = tplCfgById.Url;
                    break;
                case LocationType.Activity:
                    {
                        this.ddlSubType.Attributes.CssStyle.Remove("display");
                        this.ddlSubType.BindEnum<LotteryActivityType>("");
                        this.ddlSubType.SelectedValue = tplCfgById.Url.Split(new char[]
				{
					','
				})[0];
                        this.ddlThridType.Attributes.CssStyle.Remove("display");
                        LotteryActivityType type = (LotteryActivityType)System.Enum.Parse(typeof(LotteryActivityType), tplCfgById.Url.Split(new char[]
				{
					','
				})[0]);
                        if (type != LotteryActivityType.SignUp)
                        {
                            this.ddlThridType.DataSource = VShopHelper.GetLotteryActivityByType(type);
                        }
                        else
                        {
                            this.ddlThridType.DataSource =
                                from item in VShopHelper.GetAllActivity()
                                select new
                                {
                                    ActivityId = item.ActivityId,
                                    ActivityName = item.Name
                                };
                        }
                        this.ddlThridType.DataTextField = "ActivityName";
                        this.ddlThridType.DataValueField = "Activityid";
                        this.ddlThridType.DataBind();
                        this.ddlThridType.SelectedValue = tplCfgById.Url.Split(new char[]
				{
					','
				})[1];
                        break;
                    }
                case LocationType.Link:
                    this.Tburl.Text = tplCfgById.Url;
                    this.Tburl.Attributes.CssStyle.Remove("display");
                    break;
                case LocationType.Phone:
                    this.Tburl.Text = tplCfgById.Url;
                    this.Tburl.Attributes.CssStyle.Remove("display");
                    break;
                case LocationType.Address:
                    this.Tburl.Attributes.CssStyle.Remove("display");
                    this.navigateDesc.Attributes.CssStyle.Remove("display");
                    this.Tburl.Text = tplCfgById.Url;
                    break;
            }
        }
    }
}
