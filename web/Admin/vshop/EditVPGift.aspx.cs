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
    public partial class EditVPGift : AdminPage
    {
        int m_VPGiftId;



        private void AAbiuZJB(int num)
        {
            VPGiftInfo giftInfo = VShopHelper.GetVPGiftInfoDatail(num);
            if (null != giftInfo)
            {
                this.txtName.Text = giftInfo.VPGiftName;
                WebCalendar str = this.txtEndDate;
                DateTime endTIme = giftInfo.EndDate;
                str.Text = endTIme.ToString("yyyy-MM-dd");
                WebCalendar webCalendar = this.txtStartDate;
                DateTime startTime = giftInfo.StartDate;
                webCalendar.Text = startTime.ToString("yyyy-MM-dd");
                
                if (giftInfo.VPGiftType == 1)
                {
                    this.radVPGiftType1.Checked = true;
                }
                else
                {
                    this.radVPGiftType2.Checked = true;
                }
                if (giftInfo.VPGiftCategory == 1)
                {
                    this.radVPGiftCategory1.Checked = true;
                }
                else
                {
                    this.radVPGiftCategory2.Checked = true;
                }

            }
        }

        private void btnCoverDelete_Click(object sender, System.EventArgs e)
        {
            
        }


        private void btnEditActivity_Click(object obj, EventArgs eventArg)
        {
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

            VPGiftInfo vpgiftdetail = VShopHelper.GetVPGiftInfoDatail(this.m_VPGiftId);

            if (null != vpgiftdetail)
            {
                actTypeId = vpgiftdetail.VPGiftType;
            }

            VPGiftInfo giftInfo = new VPGiftInfo()
            {
                VPGiftName = this.txtName.Text.Trim(),
                StartDate = this.txtStartDate.SelectedDate.Value,
                EndDate = this.txtEndDate.SelectedDate.Value
            };
            if (this.radVPGiftType1.Checked)
            {
                giftInfo.VPGiftType = 1;
            }
            if (this.radVPGiftType2.Checked)
            {
                giftInfo.VPGiftType = 2;
            }
            if (this.radVPGiftCategory1.Checked)
            {
                giftInfo.VPGiftCategory = 1;
            }
            if (this.radVPGiftCategory2.Checked)
            {
                giftInfo.VPGiftCategory = 2;
            }
            giftInfo.VPGiftId = this.m_VPGiftId;


            DataTable dataTable = new DataTable();
            dataTable = VShopHelper.GetVPGiftType(giftInfo.VPGiftType, actTypeId);
            if (dataTable.Rows.Count > 0)
            {
                this.ShowMsg("存在同一时间段内同一赠送类型的活动！", false);
                return;
            }

            if (VShopHelper.UpdateVPGift(giftInfo))
            {
                this.ShowMsg("修改成功", true);
                this.Page.Response.Redirect("VPGiftList.aspx");
            }
            this.ShowMsg("修改失败", false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.m_VPGiftId = base.GetUrlIntParam("VPGiftId");
            this.btnEditActivity.Click += new EventHandler(this.btnEditActivity_Click);
            if (!this.Page.IsPostBack)
            {
                if (this.m_VPGiftId == 0)
                {
                    this.Page.Response.Redirect("VPGiftList.aspx");
                    return;
                }
                this.AAbiuZJB(this.m_VPGiftId);
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