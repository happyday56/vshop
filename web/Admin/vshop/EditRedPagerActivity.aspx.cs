
using Hidistro.Entities.VShop;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hidistro.UI.Web.Admin.vshop
{
    public partial class EditRedPagerActivity : AdminPage
    {
        protected string ReUrl = "redpageractivitylist.aspx";

        protected string htmlEditName = "修改";

        private int m_redpaperactivityid;


        private void btnSubmit_Click(object obj, EventArgs eventArg)
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                this.ShowMsg("名称不能为空", false);
                return;
            }
            decimal num = new decimal(0);
            int num1 = 0;
            decimal num2 = new decimal(0);
            int num3 = 0;
            decimal num4 = new decimal(0);
            RedPagerActivityInfo redPagerActivityInfo = new RedPagerActivityInfo();
            if (this.m_redpaperactivityid > 0)
            {
                redPagerActivityInfo = RedPagerActivityBrower.GetRedPagerActivityInfo(this.m_redpaperactivityid);
            }
            if (redPagerActivityInfo == null)
            {
                this.ShowMsg("代金券活动不存在!", false);
                return;
            }
            redPagerActivityInfo.Name = this.txtName.Text.Trim();
            redPagerActivityInfo.CategoryId = int.Parse(this.ddlCategoryId.SelectedValue);
            decimal.TryParse(this.txtMinOrderAmount.Text.Trim(), out num);
            int.TryParse(this.txtMaxGetTimes.Text.Trim(), out num1);
            decimal.TryParse(this.txtItemAmountLimit.Text.Trim(), out num2);
            int.TryParse(this.txtExpiryDays.Text.Trim(), out num3);
            decimal.TryParse(this.txtOrderAmountCanUse.Text.Trim(), out num4);
            redPagerActivityInfo.MinOrderAmount = num;
            redPagerActivityInfo.MaxGetTimes = num1;
            redPagerActivityInfo.ItemAmountLimit = num2;
            redPagerActivityInfo.ExpiryDays = num3;
            redPagerActivityInfo.OrderAmountCanUse = num4;
            if (RedPagerActivityBrower.IsExistsMinOrderAmount(redPagerActivityInfo.RedPagerActivityId, redPagerActivityInfo.MinOrderAmount))
            {
                this.ShowMsg("已存在相同金额的代金券活动", false);
                return;
            }
            if (this.m_redpaperactivityid <= 0)
            {
                if (!RedPagerActivityBrower.CreateRedPagerActivity(redPagerActivityInfo))
                {
                    this.ShowMsg("代金券活动新增失败", false);
                    return;
                }
                this.ShowMsgAndReUrl("代金券活动新增成功", true, this.ReUrl);
                return;
            }
            if (!RedPagerActivityBrower.UpdateRedPagerActivity(redPagerActivityInfo))
            {
                this.ShowMsg("代金券活动修改失败", false);
                return;
            }
            if (base.Request.QueryString["reurl"] != null)
            {
                this.ReUrl = base.Request.QueryString["reurl"].ToString();
            }
            this.ShowMsgAndReUrl("代金券活动修改成功", true, this.ReUrl);
        }

        private void BindCategories()
        {
            DataTable allCategories = CategoryBrowser.GetAllCategories();
            this.ddlCategoryId.DataTextField = "Name";
            this.ddlCategoryId.DataValueField = "CategoryId";
            this.ddlCategoryId.DataSource = allCategories;
            this.ddlCategoryId.DataBind();
            ListItem listItem = new ListItem("--全部--", "0");
            this.ddlCategoryId.Items.Insert(0, listItem);
        }

        private void LoadRedPagerActivityInfo()
        {
            RedPagerActivityInfo redPagerActivityInfo = RedPagerActivityBrower.GetRedPagerActivityInfo(this.m_redpaperactivityid);
            if (redPagerActivityInfo == null)
            {
                this.ShowMsg("代金券活动不存在！", false);
                return;
            }
            this.txtName.Text = redPagerActivityInfo.Name;
            int num = 0;
            while (true)
            {
                if (num >= this.ddlCategoryId.Items.Count)
                {
                    break;
                }
                else if (this.ddlCategoryId.Items[num].Value == redPagerActivityInfo.CategoryId.ToString())
                {
                    this.ddlCategoryId.Items[num].Selected = true;
                    break;
                }
                else
                {
                    num++;
                }
            }
            TextBox str = this.txtMinOrderAmount;
            decimal minOrderAmount = redPagerActivityInfo.MinOrderAmount;
            str.Text = minOrderAmount.ToString("F2");
            this.txtMaxGetTimes.Text = redPagerActivityInfo.MaxGetTimes.ToString();
            TextBox textBox = this.txtItemAmountLimit;
            decimal itemAmountLimit = redPagerActivityInfo.ItemAmountLimit;
            textBox.Text = itemAmountLimit.ToString("F2");
            TextBox str1 = this.txtOrderAmountCanUse;
            decimal orderAmountCanUse = redPagerActivityInfo.OrderAmountCanUse;
            str1.Text = orderAmountCanUse.ToString("F2");
            this.txtExpiryDays.Text = redPagerActivityInfo.ExpiryDays.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            bool flag = int.TryParse(this.Page.Request.QueryString["redpaperactivityid"], out this.m_redpaperactivityid);
            if (!base.IsPostBack)
            {
                this.BindCategories();
                if (flag)
                {
                    this.LoadRedPagerActivityInfo();
                    return;
                }
                this.htmlEditName = "新增";
                this.btnSubmit.Text = "新 增";
            }
        }
    }
}