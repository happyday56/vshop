using System;
using System.Collections.Generic;
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
using Hidistro.UI.Common.Controls;

namespace Hidistro.UI.Web.Admin.vshop
{
    public partial class EditActivities : AdminPage
    {
        int m_ActivitiesId;



        private void AAbiuZJB(int num)
        {
            //IList<ActivitiesInfo> activitiesInfo = VShopHelper.GetActivitiesInfo(num.ToString());
            ActivitiesInfo activitiesInfo = VShopHelper.GetActivitiesInfoDatail(num.ToString());
            if (null != activitiesInfo)
            {
                this.txtName.Text = activitiesInfo.ActivitiesName;
                this.txtDescription.Text = activitiesInfo.ActivitiesDescription;
                WebCalendar str = this.txtEndDate;
                DateTime endTIme = activitiesInfo.EndTIme;
                str.Text = endTIme.ToString("yyyy-MM-dd");
                WebCalendar webCalendar = this.txtStartDate;
                DateTime startTime = activitiesInfo.StartTime;
                webCalendar.Text = startTime.ToString("yyyy-MM-dd");
                TextBox textBox = this.txtMeetMoney;
                decimal meetMoney = activitiesInfo.MeetMoney;
                textBox.Text = meetMoney.ToString("0.00");
                TextBox str1 = this.txtReductionMoney;
                decimal reductionMoney = activitiesInfo.ReductionMoney;
                str1.Text = reductionMoney.ToString("0.00");
                this.dropCategories.SelectedValue = new int?(activitiesInfo.ActivitiesType);
                this.imgCover.ImageUrl = activitiesInfo.CoverImg;
                this.btnCoverDelete.Visible = !string.IsNullOrEmpty(this.imgCover.ImageUrl);
                if (activitiesInfo.IsDisplayHome == 1)
                {
                    this.radOnHome.Checked = true;
                }
                else
                {
                    this.radUnHome.Checked = true;
                }
                foreach (ListItem item in this.chlistProductCategories.Items)
                {
                    if (activitiesInfo.ProductCategories.Contains(int.Parse(item.Value)))
                    {
                        item.Selected = true;
                    }
                }

            }
        }

        private void btnCoverDelete_Click(object sender, System.EventArgs e)
        {
            this.imgCover.ImageUrl = string.Empty;
            this.btnCoverDelete.Visible = !string.IsNullOrEmpty(this.imgCover.ImageUrl);
            this.imgCover.Visible = !string.IsNullOrEmpty(this.imgCover.ImageUrl);
            
        }


        private void btnEditActivity_Click(object obj, EventArgs eventArg)
        {
            decimal meetMoney = new decimal(0);
            decimal reductionMoney = new decimal(0);
            int actTypeId = -1;

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
            if (this.txtReductionMoney.Text.Trim() == "")
            {
                this.ShowMsg("减免金额请输入整数！", false);
                return;
            }
            if (!decimal.TryParse(this.txtReductionMoney.Text.Trim(), out reductionMoney))
            {
                this.ShowMsg("减免金额请输入整数", false);
                return;
            }
            if (this.txtMeetMoney.Text.Trim() == "")
            {
                this.ShowMsg("满足金额请输入整数！", false);
                return;
            }
            if (!decimal.TryParse(this.txtMeetMoney.Text.Trim(), out meetMoney))
            {
                this.ShowMsg("满足金额请输入整数", false);
                return;
            }
            if (decimal.Parse(this.txtReductionMoney.Text.Trim()) >= decimal.Parse(this.txtMeetMoney.Text.Trim()))
            {
                this.ShowMsg("减免金额不能大于等于满足金额！", false);
                return;
            }

            ActivitiesInfo activities = VShopHelper.GetActivitiesInfoDatail(this.m_ActivitiesId.ToString());

            //IList<ActivitiesInfo> activitiesList = VShopHelper.GetActivitiesInfo(this.m_ActivitiesId.ToString());
            if (null != activities)
            {
                actTypeId = activities.ActivitiesType;
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
                activitiesInfo.Type = 1;
                activitiesInfo.ActivitiesType = 0;
            }
            else
            {
                activitiesInfo.Type = 1;
                int? selectedValue = this.dropCategories.SelectedValue;
                activitiesInfo.ActivitiesType = int.Parse(selectedValue.ToString());
            }
            activitiesInfo.ActivitiesId = this.m_ActivitiesId;
            DataTable dataTable = new DataTable();
            //dataTable = (activitiesInfo.Type != 1 ? VShopHelper.GetType(1) : VShopHelper.GetType(0));
            dataTable = VShopHelper.GetActivitiesType(activitiesInfo.ActivitiesType, actTypeId, meetMoney, reductionMoney);
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
            if (!string.IsNullOrEmpty(cover))
            {
                activitiesInfo.CoverImg = cover;
            }
            else
            {
                if (string.IsNullOrEmpty(this.imgCover.ImageUrl))
                {
                    activitiesInfo.CoverImg = "";
                }
                else
                {
                    activitiesInfo.CoverImg = this.imgCover.ImageUrl;
                }
            }

            IList<int> list = new List<int>();
            foreach (ListItem item in this.chlistProductCategories.Items)
            {
                if (item.Selected)
                {
                    list.Add(int.Parse(item.Value));
                }
            }
            activitiesInfo.ProductCategories = list;

            if (VShopHelper.UpdateActivities(activitiesInfo))
            {
                this.ShowMsg("修改成功", true);
                return;
            }
            this.ShowMsg("修改失败", false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_ActivitiesId = base.GetUrlIntParam("activitiesid");
            this.btnEditActivity.Click += new EventHandler(this.btnEditActivity_Click);
            this.btnCoverDelete.Click += new EventHandler(this.btnCoverDelete_Click);
            if (!this.Page.IsPostBack)
            {
                if (this.m_ActivitiesId == 0)
                {
                    this.Page.Response.Redirect("ActivitiesList.aspx");
                    return;
                }
                this.dropCategories.IsTopCategory = true;
                this.dropCategories.IsUnclassified = false;
                this.dropCategories.DataBind();
                this.chlistProductCategories.DataBind();
                this.AAbiuZJB(this.m_ActivitiesId);
            }
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