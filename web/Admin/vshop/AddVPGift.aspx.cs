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
	public partial class AddVPGift : AdminPage
	{
		private void AAbiuZJB(object obj, EventArgs eventArg)
		{
			if (!this.txtStartDate.SelectedDate.HasValue)
			{
                this.ShowMsg("��ѡ��ʼ���ڣ�", false);
				return;
			}
			if (!this.txtEndDate.SelectedDate.HasValue)
			{
                this.ShowMsg("��ѡ��������ڣ�", false);
				return;
			}
			if (this.txtStartDate.SelectedDate.Value.CompareTo(this.txtEndDate.SelectedDate.Value) > 0)
			{
                this.ShowMsg("��ʼ���ڲ������ڽ������ڣ�", false);
				return;
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
			            
			DataTable dataTable = new DataTable();
            dataTable = VShopHelper.GetVPGiftType(giftInfo.VPGiftType, -1);
			if (dataTable.Rows.Count > 0)
			{
                this.ShowMsg("����ͬһʱ�����ͬһ�������͵Ļ��", false);
				return;
			}

            if (VShopHelper.AddVPGift(giftInfo) > 0)
			{
                base.Response.Redirect("VPGiftList.aspx");
				return;
			}
            this.ShowMsg("���ʧ��", false);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
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