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
//    public class AddVote : AdminPage
//    {
//        protected System.Web.UI.WebControls.Button btnAddVote;
//        protected WebCalendar calendarEndDate;
//        protected WebCalendar calendarStartDate;
//        protected System.Web.UI.WebControls.CheckBox checkIsBackup;
//        protected System.Web.UI.WebControls.FileUpload fileUpload;
//        protected System.Web.UI.WebControls.TextBox txtAddVoteName;
//        protected System.Web.UI.WebControls.TextBox txtKeys;
//        protected System.Web.UI.WebControls.TextBox txtMaxCheck;
//        protected System.Web.UI.WebControls.TextBox txtValues;
//        private void btnAddVote_Click(object sender, System.EventArgs e)
//        {
//            if (ReplyHelper.HasReplyKey(this.txtKeys.Text.Trim()))
//            {
//                this.ShowMsg("关键字重复!", false);
//            }
//            else
//            {
//                string str = string.Empty;
//                if (!this.fileUpload.HasFile)
//                {
//                    this.ShowMsg("请上传一张封面图", false);
//                }
//                else
//                {
//                    try
//                    {
//                        str = StoreHelper.UploadVoteImage(this.fileUpload.PostedFile);
//                    }
//                    catch
//                    {
//                        this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
//                        return;
//                    }
//                    if (this.calendarStartDate.SelectedDate.HasValue)
//                    {
//                        if (!this.calendarEndDate.SelectedDate.HasValue)
//                        {
//                            this.ShowMsg("请选择活动结束时间", false);
//                        }
//                        else
//                        {
//                            VoteInfo vote = new VoteInfo
//                            {
//                                VoteName = Globals.HtmlEncode(this.txtAddVoteName.Text.Trim()),
//                                Keys = this.txtKeys.Text.Trim(),
//                                IsBackup = this.checkIsBackup.Checked
//                            };
//                            int result = 1;
//                            if (int.TryParse(this.txtMaxCheck.Text.Trim(), out result))
//                            {
//                                vote.MaxCheck = result;
//                            }
//                            vote.ImageUrl = str;
//                            vote.StartDate = this.calendarStartDate.SelectedDate.Value;
//                            vote.EndDate = this.calendarEndDate.SelectedDate.Value;
//                            if (!string.IsNullOrEmpty(this.txtValues.Text.Trim()))
//                            {
//                                System.Collections.Generic.IList<VoteItemInfo> list = new System.Collections.Generic.List<VoteItemInfo>();
//                                string[] strArray = this.txtValues.Text.Trim().Replace("\r\n", "\n").Replace("\n", "*").Split(new char[]
//                                {
//                                    '*'
//                                });
//                                for (int i = 0; i < strArray.Length; i++)
//                                {
//                                    VoteItemInfo item = new VoteItemInfo();
//                                    if (strArray[i].Length > 60)
//                                    {
//                                        this.ShowMsg("投票选项长度限制在60个字符以内", false);
//                                        return;
//                                    }
//                                    item.VoteItemName = Globals.HtmlEncode(strArray[i]);
//                                    list.Add(item);
//                                }
//                                vote.VoteItems = list;
//                                if (this.ValidationVote(vote))
//                                {
//                                    if (StoreHelper.CreateVote(vote) > 0)
//                                    {
//                                        this.ShowMsg("成功的添加了一个投票", true);
//                                        this.txtAddVoteName.Text = string.Empty;
//                                        this.checkIsBackup.Checked = false;
//                                        this.txtMaxCheck.Text = string.Empty;
//                                        this.txtValues.Text = string.Empty;
//                                    }
//                                    else
//                                    {
//                                        this.ShowMsg("添加投票失败", false);
//                                    }
//                                }
//                            }
//                            else
//                            {
//                                this.ShowMsg("投票选项不能为空", false);
//                            }
//                        }
//                    }
//                    else
//                    {
//                        this.ShowMsg("请选择活动开始时间", false);
//                    }
//                }
//            }
//        }
//        protected void Page_Load(object sender, System.EventArgs e)
//        {
//            this.btnAddVote.Click += new System.EventHandler(this.btnAddVote_Click);
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
    public class AddVote : AdminPage
    {
        protected string ReUrl = "votes.aspx";

        protected TextBox txtAddVoteName;

        protected CheckBox checkIsBackup;

        protected TextBox txtMaxCheck;

        protected WebCalendar calendarStartDate;

        protected WebCalendar calendarEndDate;

        protected TextBox txtValues;

        protected TextBox txtKeys;

        protected UpImg uploader1;

        protected Button btnAddVote;


        private void btnAddVote_Click(object obj, EventArgs eventArg)
        {
            if (ReplyHelper.HasReplyKey(this.txtKeys.Text.Trim()))
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
            if (!this.calendarStartDate.SelectedDate.HasValue)
            {
                this.ShowMsg("请选择活动开始时间", false);
                return;
            }
            if (!this.calendarEndDate.SelectedDate.HasValue)
            {
                this.ShowMsg("请选择活动结束时间", false);
                return;
            }
            VoteInfo voteInfo = new VoteInfo()
            {
                VoteName = Globals.HtmlEncode(this.txtAddVoteName.Text.Trim()),
                Keys = this.txtKeys.Text.Trim(),
                IsBackup = this.checkIsBackup.Checked
            };
            int num = 1;
            if (int.TryParse(this.txtMaxCheck.Text.Trim(), out num))
            {
                voteInfo.MaxCheck = num;
            }
            voteInfo.ImageUrl = uploadedImageUrl;
            voteInfo.StartDate = this.calendarStartDate.SelectedDate.Value;
            voteInfo.EndDate = this.calendarEndDate.SelectedDate.Value;
            IList<VoteItemInfo> voteItemInfos = null;
            if (string.IsNullOrEmpty(this.txtValues.Text.Trim()))
            {
                this.ShowMsg("投票选项不能为空", false);
                return;
            }
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
            voteInfo.VoteItems = voteItemInfos;
            if (!this.ValidationVote(voteInfo))
            {
                return;
            }
            if (StoreHelper.CreateVote(voteInfo) <= 0)
            {
                this.ShowMsg("添加投票失败", false);
                return;
            }
            if (base.Request.QueryString["reurl"] != null)
            {
                this.ReUrl = base.Request.QueryString["reurl"].ToString();
            }
            this.ShowMsgAndReUrl("成功的添加了一个投票", true, this.ReUrl);
        }

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
            this.btnAddVote.Click += new EventHandler(this.btnAddVote_Click);
        }
    }
}