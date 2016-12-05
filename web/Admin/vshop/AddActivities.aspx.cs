using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using kindeditor.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Hidistro.UI.Web.Admin.vshop
{
	public partial class AddActivities : AdminPage
	{
		private void AAbiuZJB(object obj, EventArgs eventArg)
		{
            int meetMoney = 0;
            int reductionMoney = 0;

			if (!this.txtStartDate.SelectedDate.HasValue)
			{
                this.ShowMsg("请选择开始日期！", false);
				return;
			}
			if (!this.txtEndDate.SelectedDate.HasValue)
			{
                this.ShowMsg("请选择结束日期！", false);
				return;
			}
			if (this.txtStartDate.SelectedDate.Value.CompareTo(this.txtEndDate.SelectedDate.Value) > 0)
			{
                this.ShowMsg("开始日期不能晚于结束日期！", false);
				return;
			}
            if (this.txtReductionMoney.Text.Trim() == "" || !int.TryParse(this.txtReductionMoney.Text.Trim(), out reductionMoney))
			{
                this.ShowMsg("减免金额请输入整数！", false);
				return;
			}
            if (this.txtMeetMoney.Text.Trim() == "" || !int.TryParse(this.txtMeetMoney.Text.Trim(), out meetMoney))
			{
                this.ShowMsg("满足金额请输入整数！", false);
				return;
			}
			if (int.Parse(this.txtReductionMoney.Text.Trim()) >= int.Parse(this.txtMeetMoney.Text.Trim()))
			{
                this.ShowMsg("减免金额不能大于等于满足金额！", false);
				return;
			}
			ActivitiesInfo activitiesInfo = new ActivitiesInfo()
			{
				ActivitiesName = this.txtName.Text.Trim(),
				ActivitiesDescription = this.txtDescription.Text.Trim(),
				StartTime = this.txtStartDate.SelectedDate.Value,
				EndTIme = this.txtEndDate.SelectedDate.Value,
                MeetMoney = meetMoney,
				ReductionMoney = reductionMoney
			};
            if (this.radOnHome.Checked)
            {
                activitiesInfo.IsDisplayHome = 1;
            }
            if (this.radUnHome.Checked)
            {
                activitiesInfo.IsDisplayHome = 0;
            }

			if (this.dropCategories.SelectedValue.ToString() == "0" || this.dropCategories.SelectedValue.ToString() == "")
			{
				activitiesInfo.ActivitiesType = 0;
				activitiesInfo.Type = 1;
			}
			else
			{
				int? selectedValue = this.dropCategories.SelectedValue;
				activitiesInfo.ActivitiesType = int.Parse(selectedValue.ToString());
				activitiesInfo.Type = 1;
			}
            
			DataTable dataTable = new DataTable();
			//dataTable = (activitiesInfo.Type != 1 ? VShopHelper.GetType(1) : VShopHelper.GetType(0));
            dataTable = VShopHelper.GetActivitiesType(activitiesInfo.ActivitiesType, -1, meetMoney, reductionMoney);
			if (dataTable.Rows.Count > 0)
			{
                this.ShowMsg("存在相同类目同一时间段内的其它活动！", false);
				return;
			}
            //if (this.fileUploader.PostedFile.FileName != "")
            //{
            //    if (!this.IsAllowableFileType(this.fileUploader.PostedFile.FileName))
            //    {
            //        this.ShowMsg("请上传正确的文件", false);
            //        return;
            //    }
            //    DateTime now = DateTime.Now;
            //    string str = string.Concat(now.ToString("yyyyMMddhhmmss"), Path.GetFileName(this.fileUploader.PostedFile.FileName));
            //    this.fileUploader.PostedFile.SaveAs(Path.Combine(this.Page.Request.MapPath("~/Storage/Article"), str));
            //    activitiesInfo.CoverImg = Path.Combine(this.Page.Request.MapPath("~/Storage/Article"), str);
            //}

            string cover = string.Empty;
            if (this.fileUploader.HasFile)
            {
                try
                {
                    cover = VShopHelper.UploadActivitiesImage(this.fileUploader.PostedFile);
                }
                catch
                {
                }
            }
            activitiesInfo.CoverImg = cover;

            IList<int> list = new List<int>();
            foreach (ListItem item in this.chlistProductCategories.Items)
            {
                if (item.Selected)
                {
                    list.Add(int.Parse(item.Value));
                }
            }

            activitiesInfo.ProductCategories = list;

			if (VShopHelper.AddActivities(activitiesInfo) > 0)
			{
                base.Response.Redirect("ActivitiesList.aspx");
				return;
			}
            this.ShowMsg("添加失败", false);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.dropCategories.IsTopCategory = true;
				this.dropCategories.IsUnclassified = false;
				this.dropCategories.DataBind();
                this.chlistProductCategories.DataBind();
			}
			this.btnAddActivity.Click += new EventHandler(this.AAbiuZJB);
		}

        protected bool IsAllowableFileType(string FileName)
        {
            if (".jpg".IndexOf(Path.GetExtension(FileName).ToLower()) != -1)
            {
                return true;
            }
            else if (".png".IndexOf(Path.GetExtension(FileName).ToLower()) != -1)
            {
                return true;
            }
            else if (".gif".IndexOf(Path.GetExtension(FileName).ToLower()) != -1)
            {
                return true;
            }
            return false;
        }
	}
}