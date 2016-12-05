using ASPNET.WebControls;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Members;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.ControlPanel.Utility;
using kindeditor.Net;
using System;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.distributor
{
    public class InviteApplyList : AdminPage
    {
        protected System.Web.UI.WebControls.Button btnSearchButton;
        protected System.Web.UI.WebControls.TextBox txtCellPhone;
        protected System.Web.UI.WebControls.TextBox txtRealName;
        protected System.Web.UI.WebControls.DropDownList DrStatus;
        protected Pager pager;
        protected System.Web.UI.WebControls.Repeater rptApplylist;

        private string realname;
        private string cellphone;
        private string status;

        private void BindData()
        {
            int total = 0;
            int audit = -1;
            int.TryParse(status, out audit);
            this.rptApplylist.DataSource = InviteBrowser.GetInviteApplyRecord(ref total, realname, cellphone, audit, "ts", Core.Enums.SortAction.Desc, this.pager.PageIndex, this.pager.PageSize);
            this.rptApplylist.DataBind();
            this.pager.TotalRecords = total;

        }
        private void btnSearchButton_Click(object sender, System.EventArgs e)
        {
            this.ReBind(true);
        }
        private void LoadParameters()
        {
           

            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["RealName"]))
                {
                    this.realname = base.Server.UrlDecode(this.Page.Request.QueryString["RealName"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["CellPhone"]))
                {
                    this.cellphone = base.Server.UrlDecode(this.Page.Request.QueryString["CellPhone"]);
                }
                if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Status"]))
                {
                    this.status = base.Server.UrlDecode(this.Page.Request.QueryString["Status"]);
                }
                else
                {
                    this.status = "-1";
                }

                this.DrStatus.SelectedValue = this.status;
                this.txtCellPhone.Text = this.cellphone;
                this.txtRealName.Text = this.realname;
            }
            else
            {
                this.status = this.DrStatus.SelectedValue;
                this.cellphone = this.txtCellPhone.Text;
                this.realname = this.txtRealName.Text;
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["IsAudit"]))
            {
                SetAudit();
            }
            this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
            this.LoadParameters();
            if (!base.IsPostBack)
            {
                this.BindData();
            }
        }
        private void ReBind(bool isSearch)
        {
            NameValueCollection queryStrings = new NameValueCollection();

            queryStrings.Add("CellPhone", this.txtCellPhone.Text);
            queryStrings.Add("RealName", this.txtRealName.Text);
            queryStrings.Add("Status", this.DrStatus.SelectedValue);
            queryStrings.Add("pageSize", this.pager.PageSize.ToString(System.Globalization.CultureInfo.InvariantCulture));
            if (!isSearch)
            {
                queryStrings.Add("pageIndex", this.pager.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            base.ReloadPage(queryStrings);
        }

        private void SetAudit()
        {
            int currentUserId = Globals.GetCurrentManagerUserId();

            int applyId = 0;
            int.TryParse(Request.QueryString["ApplyId"], out applyId);
            int isaudit = 0;
            int.TryParse(Request.QueryString["IsAudit"], out isaudit);
            if (applyId > 0)
            {
                var ret = InviteBrowser.UpdateInviteApplyAudit(new Entities.VShop.InviteApply()
                  {
                      ApplyId = applyId,
                      IsAudit = isaudit != 1 ? 2 : 1,
                      AuditUserId = currentUserId
                  });
                if (ret)
                {
                    if (isaudit == 1)
                    {
                        var applyModel = InviteBrowser.GetInviteApplyRecordById(applyId);
                        if (applyModel != null)
                        {
                            InviteBrowser.UpdateInvitationNum(applyModel.UserId, applyModel.ApplyNum);
                        }
                    }
                }
            }
            ReBind(true);

        }
    }
}
