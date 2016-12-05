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
    public partial class AddVPGiftDetail : AdminPage
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
            int meetMoney = 0;
            int giftMoney = 0;
            bool isCheck = false;

            VPGiftInfo giftInfo = VShopHelper.GetVPGiftInfoDatail(this.m_VPGiftId);
            if (null != giftInfo)
            {
                if (giftInfo.VPGiftCategory == 1)
                {
                    isCheck = true;
                }
            }

            if (this.txtGiftMoney.Text.Trim() == "" || !int.TryParse(this.txtGiftMoney.Text.Trim(), out giftMoney))
            {
                this.ShowMsg("赠送金额请输入整数！", false);
                return;
            }
            if (this.txtMeetMoney.Text.Trim() == "" || !int.TryParse(this.txtMeetMoney.Text.Trim(), out meetMoney))
            {
                this.ShowMsg("满足金额请输入整数！", false);
                return;
            }
            if (isCheck)
            {
                if (int.Parse(this.txtGiftMoney.Text.Trim()) >= int.Parse(this.txtMeetMoney.Text.Trim()))
                {
                    this.ShowMsg("赠送金额不能大于等于满足金额！", false);
                    return;
                }
            }            
            
            VPGiftDetailInfo giftDetailInfo = new VPGiftDetailInfo()
            {
                MeetMoney = meetMoney,
                GiftMoney = giftMoney,
                VPGiftId = this.m_VPGiftId
            };

            DataTable dataTable = new DataTable();
            dataTable = VShopHelper.GetVPGiftDetailType(giftDetailInfo.VPGiftId, meetMoney);
            if (dataTable.Rows.Count > 0)
            {
                this.ShowMsg("存在同范围内的满足金额数据，请重新输入！", false);
                return;
            }

            if (VShopHelper.AddVPGiftDetail(giftDetailInfo) > 0)
            {
                this.ShowMsg("添加成功", true);
                this.Page.Response.Redirect("VPGiftList.aspx");
            }
            this.ShowMsg("添加失败", false);
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