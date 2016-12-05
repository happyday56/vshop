using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Members;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Plugins;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
	public class DistributorList : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnSearchButton;
		private string CellPhone = "";
        protected DistributorGradeDropDownList DrGrade;
		protected System.Web.UI.WebControls.DropDownList DrStatus;
		private string Grade = "0";
		private string MicroSignal = "";
		protected Pager pager;
		private string RealName = "";
		protected System.Web.UI.WebControls.Repeater reDistributor;
		private string Status = "0";
		private string StoreName = "";
		protected System.Web.UI.WebControls.TextBox txtCellPhone;
		protected System.Web.UI.WebControls.TextBox txtMicroSignal;
		protected System.Web.UI.WebControls.TextBox txtRealName;
		protected System.Web.UI.WebControls.TextBox txtStoreName;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hdenablemsg;
        protected System.Web.UI.WebControls.Button btnSendMessage;
        protected System.Web.UI.HtmlControls.HtmlTextArea txtmsgcontent;
        protected DropDownList DrDeadlineStatus;
        private int DeadlineStatus = 0;

		private void BindData()
		{
			DistributorsQuery entity = new DistributorsQuery
			{
				GradeId = int.Parse(this.Grade),
				StoreName = this.StoreName,
				CellPhone = this.CellPhone,
				RealName = this.RealName,
				MicroSignal = this.MicroSignal,
				ReferralStatus = int.Parse(this.Status),
                DeadlineStatus = this.DeadlineStatus,
				PageIndex = this.pager.PageIndex,
				PageSize = this.pager.PageSize,
				SortOrder = SortAction.Desc,
				SortBy = "userid"
			};
			Globals.EntityCoding(entity, true);
			DbQueryResult distributors = VShopHelper.GetDistributors(entity);
			this.reDistributor.DataSource = distributors.Data;
			this.reDistributor.DataBind();
			this.pager.TotalRecords = distributors.TotalRecords;
		}
		private void btnSearchButton_Click(object sender, System.EventArgs e)
		{
			this.ReBind(true);
		}
		private void LoadParameters()
		{
			if (!this.Page.IsPostBack)
			{
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["StoreName"]))
				{
					this.StoreName = base.Server.UrlDecode(this.Page.Request.QueryString["StoreName"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Grade"]))
				{
					this.Grade = base.Server.UrlDecode(this.Page.Request.QueryString["Grade"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Status"]))
				{
					this.Status = base.Server.UrlDecode(this.Page.Request.QueryString["Status"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["RealName"]))
				{
					this.RealName = base.Server.UrlDecode(this.Page.Request.QueryString["RealName"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CellPhone"]))
				{
					this.CellPhone = base.Server.UrlDecode(this.Page.Request.QueryString["CellPhone"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["MicroSignal"]))
				{
					this.MicroSignal = base.Server.UrlDecode(this.Page.Request.QueryString["MicroSignal"]);
				}
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["DeadlineStatus"]))
                {
                    this.DeadlineStatus = int.Parse(base.Server.UrlDecode(this.Page.Request.QueryString["DeadlineStatus"]));
                }
				this.DrStatus.SelectedValue = this.Status;
				this.txtStoreName.Text = this.StoreName;
                this.DrGrade.DataBind();
				this.DrGrade.SelectedValue = int.Parse( this.Grade );
				this.txtCellPhone.Text = this.CellPhone;
				this.txtMicroSignal.Text = this.MicroSignal;
				this.txtRealName.Text = this.RealName;
                this.DrDeadlineStatus.SelectedValue = this.DeadlineStatus.ToString();
			}
			else
			{
				this.Status = this.DrStatus.SelectedValue;
				this.StoreName = this.txtStoreName.Text;
				this.Grade = this.DrGrade.SelectedValue.ToString();
				this.CellPhone = this.txtCellPhone.Text;
				this.RealName = this.txtRealName.Text;
				this.MicroSignal = this.txtMicroSignal.Text;
                this.DeadlineStatus = int.Parse(this.DrDeadlineStatus.SelectedValue);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
			this.LoadParameters();
            SiteSettings siteSetting = SettingsManager.GetMasterSettings(false);

			if (!base.IsPostBack)
			{
                if (siteSetting.SMSEnabled)
                {
                    this.hdenablemsg.Value = "1";
                }

				this.BindData();
			}
		}
		private void ReBind(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			queryStrings.Add("Grade", this.DrGrade.Text);
			queryStrings.Add("StoreName", this.txtStoreName.Text);
			queryStrings.Add("CellPhone", this.txtCellPhone.Text);
			queryStrings.Add("RealName", this.txtRealName.Text);
			queryStrings.Add("MicroSignal", this.txtMicroSignal.Text);
			queryStrings.Add("Status", this.DrStatus.SelectedValue);
            queryStrings.Add("DeadlineStatus", this.DrDeadlineStatus.SelectedValue);
			queryStrings.Add("pageSize", this.pager.PageSize.ToString(System.Globalization.CultureInfo.InvariantCulture));
			if (!isSearch)
			{
				queryStrings.Add("pageIndex", this.pager.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
			}
			base.ReloadPage(queryStrings);
		}

        private void btnSendMessage_Click(object sender, System.EventArgs e)
        {
            SiteSettings siteSetting = SettingsManager.GetMasterSettings(false);
            string sMSSender = siteSetting.SMSSender;
            if (string.IsNullOrEmpty(sMSSender))
            {
                this.ShowMsg("请先选择发送方式", false);
            }
            else
            {
                ConfigData data = null;
                if (siteSetting.SMSEnabled)
                {
                    data = new ConfigData(HiCryptographer.Decrypt(siteSetting.SMSSettings));
                }
                if (data == null)
                {
                    this.ShowMsg("请先选择发送方式并填写配置信息", false);
                }
                else
                {
                    if (!data.IsValid)
                    {
                        string msg = "";
                        foreach (string str3 in data.ErrorMsgs)
                        {
                            msg += Formatter.FormatErrorMessage(str3);
                        }
                        this.ShowMsg(msg, false);
                    }
                    else
                    {
                        string str4 = this.txtmsgcontent.Value.Trim();
                        if (string.IsNullOrEmpty(str4))
                        {
                            this.ShowMsg("请先填写发送的内容信息", false);
                        }
                        else
                        {
                            //int num = System.Convert.ToInt32(this.litsmscount.Text);
                            string str5 = null;
                            foreach (RepeaterItem item in this.reDistributor.Items)
                            {
                                HtmlInputCheckBox box = (HtmlInputCheckBox)item.FindControl("CheckBoxGroup");
                                if (box.Checked)
                                {
                                    Literal litCellPhone = (Literal)item.FindControl("litCellPhone");
                                    string str6 = litCellPhone.Text;
                                    if (!string.IsNullOrEmpty(str6))
                                    {
                                        str6 = str6.Replace("&nbsp;", "");
                                        str6 = str6.Trim();
                                        if (Regex.IsMatch(str6, "^1[34578]\\d{9}$"))
                                        {
                                            str5 = str5 + str6 + ",";
                                        }
                                    }
                                }
                            }
                            //foreach (System.Web.UI.WebControls.GridViewRow row in this.grdMemberList.Rows)
                            //{
                            //    System.Web.UI.WebControls.CheckBox box = (System.Web.UI.WebControls.CheckBox)row.FindControl("checkboxCol");
                            //    if (box.Checked)
                            //    {
                            //        string str6 = ((System.Web.UI.DataBoundLiteralControl)row.Controls[4].Controls[0]).Text.Trim().Replace("<div></div>", "");
                            //        if (!string.IsNullOrEmpty(str6))
                            //        {
                            //            str6 = str6.Replace("&nbsp;", "");
                            //            str6 = str6.Trim();
                            //            if (Regex.IsMatch(str6, "^1[34578]\\d{9}$"))
                            //            {
                            //                str5 = str5 + str6 + ",";
                            //            }
                            //        }
                            //    }
                            //}
                            if (str5 == null)
                            {
                                this.ShowMsg("请先选择要发送的会员或检测所选手机号格式是否正确", false);
                            }
                            else
                            {
                                str5 = str5.Substring(0, str5.Length - 1);
                                string[] phoneNumbers;
                                if (str5.Contains(","))
                                {
                                    phoneNumbers = str5.Split(new char[]
									{
										','
									});
                                }
                                else
                                {
                                    phoneNumbers = new string[]
									{
										str5
									};
                                }
                                //if (num < phoneNumbers.Length)
                                //{
                                //    this.ShowMsg("发送失败，您的剩余短信条数不足", false);
                                //}
                                //else
                                //{
                                string str7;
                                bool success = SMSSender.CreateInstance(sMSSender, data.SettingsXml).Send(phoneNumbers, str4, out str7);
                                this.ShowMsg(str7, success);
                                this.txtmsgcontent.Value = "输入发送内容……";
                                //this.litsmscount.Text = (num - phoneNumbers.Length).ToString();
                                //}
                            }
                        }
                    }
                }
            }
        }


        protected void reDistributor_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string str2;
            int result = 0;
            int.TryParse(e.CommandArgument.ToString(), out result);
            if ((result > 0) && ((str2 = e.CommandName) != null))
            {
                if (str2 == "Frozen")
                {
                    VShopHelper.UpdateDistributorReferralStatusById(0, result);
                    this.ShowMsg("处理成功！", false);
                    this.ReBind(true);
                }
                else if (str2 == "OneYear")
                {
                    VShopHelper.UpdateDistributorDeadlineTimeById(1, result);
                    this.ShowMsg("处理成功！", false);
                    this.ReBind(true);
                }
                else if (str2 == "TwoYear")
                {
                    VShopHelper.UpdateDistributorDeadlineTimeById(2, result);
                    this.ShowMsg("处理成功！", false);
                    this.ReBind(true);
                }
            }
        }

        protected void reDistributor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                ImageLinkButton button = (ImageLinkButton)e.Item.FindControl("btnFrozen");
                if (((DataRowView)e.Item.DataItem).Row["ReferralStatus"].ToString() == "2")
                {
                    button.Text = "启用";
                    button.DeleteMsg = "确定要启用分销商?";
                }
                else
                {
                    button.Text = "冻结";
                    button.DeleteMsg = "确定要冻结分销商?";
                }
            }
        }
    }
}
