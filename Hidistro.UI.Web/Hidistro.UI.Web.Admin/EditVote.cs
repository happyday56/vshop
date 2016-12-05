//using ASPNET.WebControls;
//using Hidistro.ControlPanel.Store;
//using Hidistro.Core;
//using Hidistro.Entities.Store;
//using Hidistro.UI.Common.Controls;
//using Hidistro.UI.ControlPanel.Utility;
//using Hishop.Components.Validation;
//using System;
//using System.Collections.Generic;
//using System.Web.UI.WebControls;
//namespace Hidistro.UI.Web.Admin
//{
//    [PrivilegeCheck(Privilege.Votes)]
//    public class EditVote : AdminPage
//    {
//        protected System.Web.UI.WebControls.Button btnEditVote;
//        protected WebCalendar calendarEndDate;
//        protected WebCalendar calendarStartDate;
//        protected System.Web.UI.WebControls.FileUpload fileUpload;
//        //protected HiImage imgPic;
//        protected System.Web.UI.WebControls.TextBox txtAddVoteName;
//        protected System.Web.UI.WebControls.TextBox txtKeys;
//        protected System.Web.UI.WebControls.TextBox txtMaxCheck;
//        protected System.Web.UI.WebControls.TextBox txtValues;
//        protected UpImg uploader1;
//        private long voteId;
//        private void btnEditVote_Click(object sender, System.EventArgs e)
//        {
//            if (StoreHelper.GetVoteCounts(this.voteId) > 0)
//            {
//                this.ShowMsg("投票已经开始，不能再对投票选项进行任何操作", false);
//            }
//            else
//            {
//                VoteInfo voteById = StoreHelper.GetVoteById(this.voteId);
//                if (voteById.Keys != this.txtKeys.Text.Trim() && ReplyHelper.HasReplyKey(this.txtKeys.Text.Trim()))
//                {
//                    this.ShowMsg("关键字重复!", false);
//                }
//                else
//                {
//                    if (this.fileUpload.HasFile)
//                    {
//                        try
//                        {
//                            ResourcesHelper.DeleteImage(voteById.ImageUrl);
//                            this.imgPic.ImageUrl = (voteById.ImageUrl = StoreHelper.UploadVoteImage(this.fileUpload.PostedFile));
//                        }
//                        catch
//                        {
//                            this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
//                            return;
//                        }
//                    }
//                    voteById.VoteName = Globals.HtmlEncode(this.txtAddVoteName.Text.Trim());
//                    voteById.Keys = this.txtKeys.Text.Trim();
//                    int result = 1;
//                    if (int.TryParse(this.txtMaxCheck.Text.Trim(), out result))
//                    {
//                        voteById.MaxCheck = result;
//                    }
//                    voteById.StartDate = this.calendarStartDate.SelectedDate.Value;
//                    voteById.EndDate = this.calendarEndDate.SelectedDate.Value;
//                    if (!string.IsNullOrEmpty(this.txtValues.Text.Trim()))
//                    {
//                        System.Collections.Generic.IList<VoteItemInfo> list = new System.Collections.Generic.List<VoteItemInfo>();
//                        string[] strArray = this.txtValues.Text.Trim().Replace("\r\n", "\n").Replace("\n", "*").Split(new char[]
//                        {
//                            '*'
//                        });
//                        for (int i = 0; i < strArray.Length; i++)
//                        {
//                            VoteItemInfo item = new VoteItemInfo();
//                            if (strArray[i].Length > 60)
//                            {
//                                this.ShowMsg("投票选项长度限制在60个字符以内", false);
//                                return;
//                            }
//                            item.VoteItemName = Globals.HtmlEncode(strArray[i]);
//                            list.Add(item);
//                        }
//                        voteById.VoteItems = list;
//                        if (this.ValidationVote(voteById))
//                        {
//                            if (StoreHelper.UpdateVote(voteById))
//                            {
//                                this.ShowMsg("修改投票成功", true);
//                            }
//                            else
//                            {
//                                this.ShowMsg("修改投票失败", false);
//                            }
//                        }
//                    }
//                    else
//                    {
//                        this.ShowMsg("投票选项不能为空", false);
//                    }
//                }
//            }
//        }
//        protected void Page_Load(object sender, System.EventArgs e)
//        {
//            if (!long.TryParse(this.Page.Request.QueryString["VoteId"], out this.voteId))
//            {
//                base.GotoResourceNotFound();
//            }
//            else
//            {
//                this.btnEditVote.Click += new System.EventHandler(this.btnEditVote_Click);
//                if (!this.Page.IsPostBack)
//                {
//                    VoteInfo voteById = StoreHelper.GetVoteById(this.voteId);
//                    System.Collections.Generic.IList<VoteItemInfo> voteItems = StoreHelper.GetVoteItems(this.voteId);
//                    if (voteById == null)
//                    {
//                        base.GotoResourceNotFound();
//                    }
//                    else
//                    {
//                        this.txtAddVoteName.Text = Globals.HtmlDecode(voteById.VoteName);
//                        this.txtKeys.Text = voteById.Keys;
//                        this.txtMaxCheck.Text = voteById.MaxCheck.ToString();
//                        this.calendarStartDate.SelectedDate = new System.DateTime?(voteById.StartDate);
//                        this.calendarEndDate.SelectedDate = new System.DateTime?(voteById.EndDate);
//                        //this.imgPic.ImageUrl = voteById.ImageUrl;
//                        string imageUrl = voteById.ImageUrl;
//                        if (imageUrl != "")
//                        {
//                            this.uploader1.UploadedImageUrl = imageUrl;
//                        }
//                        string str = "";
//                        foreach (VoteItemInfo info2 in voteItems)
//                        {
//                            str = str + Globals.HtmlDecode(info2.VoteItemName) + "\r\n";
//                        }
//                        this.txtValues.Text = str;
//                    }
//                }
//            }
//        }
//        private bool ValidationVote(VoteInfo vote)
//        {
//            ValidationResults results = Validation.Validate<VoteInfo>(vote, new string[]
//            {
//                "ValVote"
//            });
//            string msg = string.Empty;
//            if (!results.IsValid)
//            {
//                foreach (ValidationResult result in (System.Collections.Generic.IEnumerable<ValidationResult>)results)
//                {
//                    msg += Formatter.FormatErrorMessage(result.Message);
//                }
//                this.ShowMsg(msg, false);
//            }
//            return results.IsValid;
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Components.Validation;

namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.Votes)]
	public class EditVote : AdminPage
	{
		private long voteId;

		protected string ReUrl = "votes.aspx";

		protected TextBox txtAddVoteName;

		protected TextBox txtMaxCheck;

		protected WebCalendar calendarStartDate;

		protected WebCalendar calendarEndDate;

		protected TextBox txtValues;

		protected TextBox txtKeys;

		protected UpImg uploader1;

		protected Button btnEditVote;

			private void btnEditVote_Click(object obj, EventArgs eventArg)
		{
			if (StoreHelper.GetVoteCounts(this.voteId) > 0)
			{
                this.ShowMsg("投票已经开始，不能再对投票选项进行任何操作", false);
				return;
			}
			VoteInfo voteById = StoreHelper.GetVoteById(this.voteId);
			if (voteById.Keys != this.txtKeys.Text.Trim() && ReplyHelper.HasReplyKey(this.txtKeys.Text.Trim()))
			{
                this.ShowMsg("关键字重复!", false);
				return;
			}
			if (this.calendarStartDate.SelectedDate.Value.CompareTo(this.calendarEndDate.SelectedDate.Value) >= 0)
			{
                this.ShowMsg("开始时间不能晚于结束时间！", false);
				return;
			}
			string uploadedImageUrl = this.uploader1.UploadedImageUrl;
			if (string.IsNullOrEmpty(uploadedImageUrl))
			{
                this.ShowMsg("请上传封面图片", false);
				return;
			}
			voteById.ImageUrl = uploadedImageUrl;
			voteById.VoteName = Globals.HtmlEncode(this.txtAddVoteName.Text.Trim());
			voteById.Keys = this.txtKeys.Text.Trim();
			int num = 1;
			if (int.TryParse(this.txtMaxCheck.Text.Trim(), out num))
			{
				voteById.MaxCheck = num;
			}
			voteById.StartDate = this.calendarStartDate.SelectedDate.Value;
			voteById.EndDate = this.calendarEndDate.SelectedDate.Value;
			IList<VoteItemInfo> voteItemInfos = null;
			if (string.IsNullOrEmpty(this.txtValues.Text.Trim()))
			{
                this.ShowMsg("投票选项不能为空", false);
				return;
			}

            System.Collections.Generic.IList<VoteItemInfo> list = new System.Collections.Generic.List<VoteItemInfo>();
            

			voteItemInfos = new List<VoteItemInfo>();
            //string str = this.txtValues.Text.Trim().Replace("DQAKAA==", "CgA=");
            //string[] strArrays = str.Replace("CgA=", "KgA=").Split(new char[] { '*' });
            string[] strArrays = this.txtValues.Text.Trim().Replace("\r\n", "\n").Replace("\n", "*").Split(new char[]
                                    {
                                        '*'
                                    });
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				VoteItemInfo voteItemInfo = new VoteItemInfo();
				if (strArrays[i].Length > 60)
				{
                    this.ShowMsg("投票选项长度限制在60个字符以内", false);
					return;
				}
				voteItemInfo.VoteItemName = Globals.HtmlEncode(strArrays[i]);
				voteItemInfos.Add(voteItemInfo);
			}
			voteById.VoteItems = voteItemInfos;
            if (!this.ValidationVote(voteById))
			{
				return;
			}
			if (!StoreHelper.UpdateVote(voteById))
			{
                this.ShowMsg("修改投票失败", false);
				return;
			}
            if (base.Request.QueryString["reurl"] != null)
			{
                this.ReUrl = base.Request.QueryString["reurl"].ToString();
			}
            this.ShowMsgAndReUrl("修改投票成功", true, this.ReUrl);
		}

        //private bool ABdcZUiWNfsGNbV(oArVIo9LHFJa(VoteInfo voteInfo)
        //{
        //    string[] strArrays = new string[] { "VgBhAGwAVgBvAHQAZQA=" };
        //    ValidationResults validationResults = Validation.Validate<VoteInfo>(voteInfo, strArrays);
        //    string empty = string.Empty;
        //    if (!validationResults.IsValid)
        //    {
        //        foreach (ValidationResult validationResult in (IEnumerable<ValidationResult>)validationResults)
        //        {
        //            empty = string.Concat(empty, Formatter.FormatErrorMessage(validationResult.Message));
        //        }
        //        this.ShowMsg(empty, false);
        //    }
        //    return validationResults.IsValid;
        //}

        private bool ValidationVote(VoteInfo vote)
        {
            ValidationResults results = Validation.Validate<VoteInfo>(vote, new string[]
            {
                "ValVote"
            });
            string msg = string.Empty;
            if (!results.IsValid)
            {
                foreach (ValidationResult result in (System.Collections.Generic.IEnumerable<ValidationResult>)results)
                {
                    msg += Formatter.FormatErrorMessage(result.Message);
                }
                this.ShowMsg(msg, false);
            }
            return results.IsValid;
        }
    

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!long.TryParse(this.Page.Request.QueryString["VoteId"], out this.voteId))
			{
				base.GotoResourceNotFound();
				return;
			}
			this.btnEditVote.Click += new EventHandler(this.btnEditVote_Click);
			if (!this.Page.IsPostBack)
			{
				VoteInfo voteById = StoreHelper.GetVoteById(this.voteId);
				IList<VoteItemInfo> voteItems = StoreHelper.GetVoteItems(this.voteId);
				if (voteById == null)
				{
					base.GotoResourceNotFound();
					return;
				}
				this.txtAddVoteName.Text = Globals.HtmlDecode(voteById.VoteName);
				this.txtKeys.Text = voteById.Keys;
				this.txtMaxCheck.Text = voteById.MaxCheck.ToString();
				this.calendarStartDate.SelectedDate = new DateTime?(voteById.StartDate);
				this.calendarEndDate.SelectedDate = new DateTime?(voteById.EndDate);
				string imageUrl = voteById.ImageUrl;
				if (imageUrl != "")
				{
					this.uploader1.UploadedImageUrl = imageUrl;
				}
				string str = "";
				foreach (VoteItemInfo voteItem in voteItems)
				{
					str = string.Concat(str, Globals.HtmlDecode(voteItem.VoteItemName), "\r\n");
				}
				this.txtValues.Text = str;
			}
		}
	}
}