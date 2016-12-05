using ASPNET.WebControls;
using Hidistro.ControlPanel.Members;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Configuration;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.Members;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Plugins;
using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
namespace Hidistro.UI.Web.Admin
{
	[PrivilegeCheck(Privilege.ClientNew)]
	public class MemberMarket : AdminPage
	{
		private bool? approved;
		protected System.Web.UI.WebControls.Button btnExport;
		protected System.Web.UI.WebControls.Button btnSearchButton;
		protected System.Web.UI.WebControls.Button btnSendEmail;
		protected System.Web.UI.WebControls.Button btnSendMessage;
		protected ExportFieldsCheckBoxList exportFieldsCheckBoxList;
		protected ExportFormatRadioButtonList exportFormatRadioButtonList;
		protected Grid grdMemberList;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdenableemail;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdenablemsg;
		protected PageSize hrefPageSize;
		protected System.Web.UI.WebControls.Literal litsmscount;
		protected System.Web.UI.WebControls.Literal litType;
		protected ImageLinkButton lkbDelectCheck;
		protected ImageLinkButton lkbDelectCheck1;
		protected Pager pager;
		protected Pager pager1;
		private int? rankId;
		protected MemberGradeDropDownList rankList;
		private string realName;
		private string searchKey;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Span1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Span2;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Span4;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Span5;
		protected System.Web.UI.HtmlControls.HtmlTextArea txtemailcontent;
		protected System.Web.UI.HtmlControls.HtmlTextArea txtmsgcontent;
		protected System.Web.UI.WebControls.TextBox txtRealName;
		protected System.Web.UI.WebControls.TextBox txtSearchText;
		protected void BindClientList()
		{
			MemberQuery query = new MemberQuery
			{
				Username = this.searchKey,
				Realname = this.realName,
				GradeId = this.rankId,
				PageIndex = this.pager.PageIndex,
				IsApproved = this.approved,
				SortBy = this.grdMemberList.SortOrderBy,
				PageSize = this.pager.PageSize
			};
			if (this.grdMemberList.SortOrder.ToLower() == "desc")
			{
				query.SortOrder = SortAction.Desc;
			}
			DbQueryResult members = MemberHelper.GetMembers(this.SetClient(query));
			this.grdMemberList.DataSource = members.Data;
			this.grdMemberList.DataBind();
			this.pager1.TotalRecords = (this.pager.TotalRecords = members.TotalRecords);
		}
		private void btnSearchButton_Click(object sender, System.EventArgs e)
		{
			this.ReBind(true);
		}
		private void btnSendEmail_Click(object sender, System.EventArgs e)
		{
			SiteSettings siteSetting = this.GetSiteSetting();
			string str = siteSetting.EmailSender.ToLower();
			if (string.IsNullOrEmpty(str))
			{
				this.ShowMsg("请先选择发送方式", false);
			}
			else
			{
				ConfigData data = null;
				if (siteSetting.EmailEnabled)
				{
					data = new ConfigData(HiCryptographer.Decrypt(siteSetting.EmailSettings));
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
						foreach (string str2 in data.ErrorMsgs)
						{
							msg += Formatter.FormatErrorMessage(str2);
						}
						this.ShowMsg(msg, false);
					}
					else
					{
						string str3 = this.txtemailcontent.Value.Trim();
						if (string.IsNullOrEmpty(str3))
						{
							this.ShowMsg("请先填写发送的内容信息", false);
						}
						else
						{
							string str4 = null;
							foreach (System.Web.UI.WebControls.GridViewRow row in this.grdMemberList.Rows)
							{
								System.Web.UI.WebControls.CheckBox box = (System.Web.UI.WebControls.CheckBox)row.FindControl("checkboxCol");
								if (box.Checked)
								{
									string str5 = ((System.Web.UI.DataBoundLiteralControl)row.Controls[3].Controls[0]).Text.Trim().Replace("<div></div>", "");
									if (!string.IsNullOrEmpty(str5) && Regex.IsMatch(str5, "([a-zA-Z\\.0-9_-])+@([a-zA-Z0-9_-])+((\\.[a-zA-Z0-9_-]{2,4}){1,2})"))
									{
										str4 = str4 + str5 + ",";
									}
								}
							}
							if (str4 == null)
							{
								this.ShowMsg("请先选择要发送的会员或检测邮箱格式是否正确", false);
							}
							else
							{
								str4 = str4.Substring(0, str4.Length - 1);
								string[] strArray;
								if (str4.Contains(","))
								{
									strArray = str4.Split(new char[]
									{
										','
									});
								}
								else
								{
									strArray = new string[]
									{
										str4
									};
								}
								MailMessage mail = new MailMessage
								{
									IsBodyHtml = true,
									Priority = MailPriority.High,
									SubjectEncoding = System.Text.Encoding.UTF8,
									BodyEncoding = System.Text.Encoding.UTF8,
									Body = str3,
									Subject = "来自" + siteSetting.SiteName
								};
								string[] array = strArray;
								for (int i = 0; i < array.Length; i++)
								{
									string str6 = array[i];
									mail.To.Add(str6);
								}
								EmailSender sender2 = EmailSender.CreateInstance(str, data.SettingsXml);
								try
								{
									if (sender2.Send(mail, System.Text.Encoding.GetEncoding(HiConfiguration.GetConfig().EmailEncoding)))
									{
										this.ShowMsg("发送邮件成功", true);
									}
									else
									{
										this.ShowMsg("发送邮件失败", false);
									}
								}
								catch (System.Exception exception)
								{
									this.ShowMsg(exception.Message, false);
								}
								this.txtemailcontent.Value = "输入发送内容……";
							}
						}
					}
				}
			}
		}
		private void btnSendMessage_Click(object sender, System.EventArgs e)
		{
			SiteSettings siteSetting = this.GetSiteSetting();
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
                            // Modify 2015-09-26
							//int num = System.Convert.ToInt32(this.litsmscount.Text);
                            int num = 99999;
							string str5 = null;
							foreach (System.Web.UI.WebControls.GridViewRow row in this.grdMemberList.Rows)
							{
								System.Web.UI.WebControls.CheckBox box = (System.Web.UI.WebControls.CheckBox)row.FindControl("checkboxCol");
								if (box.Checked)
								{
									string str6 = ((System.Web.UI.DataBoundLiteralControl)row.Controls[2].Controls[0]).Text.Trim().Replace("<div></div>", "");
									if (!string.IsNullOrEmpty(str6) && Regex.IsMatch(str6, "^(13|14|15|18)\\d{9}$"))
									{
										str5 = str5 + str6 + ",";
									}
								}
							}
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
								if (num < phoneNumbers.Length)
								{
									this.ShowMsg("发送失败，您的剩余短信条数不足", false);
								}
								else
								{
									string str7;
									bool success = SMSSender.CreateInstance(sMSSender, data.SettingsXml).Send(phoneNumbers, str4, out str7);
									this.ShowMsg(str7, success);
									this.txtmsgcontent.Value = "输入发送内容……";
									this.litsmscount.Text = (num - phoneNumbers.Length).ToString();
								}
							}
						}
					}
				}
			}
		}
		protected int GetAmount(SiteSettings settings)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(settings.SMSSettings))
			{
				string xml = HiCryptographer.Decrypt(settings.SMSSettings);
				XmlDocument document = new XmlDocument();
				document.LoadXml(xml);
				string innerText = document.SelectSingleNode("xml/Appkey").InnerText;
				string postData = "method=getAmount&Appkey=" + innerText;
				string s = this.PostData("http://sms.kuaidiantong.cn/getAmount.aspx", postData);
				int num2;
				if (int.TryParse(s, out num2))
				{
					num = System.Convert.ToInt32(s);
				}
			}
			return num;
		}
		private SiteSettings GetSiteSetting()
		{
			return SettingsManager.GetMasterSettings(false);
		}
		private void grdMemberList_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			ManagerHelper.CheckPrivilege(Privilege.DeleteMember);
			int userId = (int)this.grdMemberList.DataKeys[e.RowIndex].Value;
			if (!MemberHelper.Delete(userId))
			{
				this.ShowMsg("未知错误", false);
			}
			else
			{
				this.BindClientList();
				this.ShowMsg("成功删除了选择的会员", true);
			}
		}
		private bool IsMembers(string name)
		{
			string pattern = "[\\u4e00-\\u9fa5a-zA-Z]+[\\u4e00-\\u9fa5_a-zA-Z0-9]*";
			Regex regex = new Regex(pattern);
			return regex.IsMatch(name) && name.Length >= 2 && name.Length <= 20;
		}
		private void lkbDelectCheck_Click(object sender, System.EventArgs e)
		{
			ManagerHelper.CheckPrivilege(Privilege.DeleteMember);
			int num = 0;
			foreach (System.Web.UI.WebControls.GridViewRow row in this.grdMemberList.Rows)
			{
				System.Web.UI.WebControls.CheckBox box = (System.Web.UI.WebControls.CheckBox)row.FindControl("checkboxCol");
				if (box.Checked && MemberHelper.Delete(System.Convert.ToInt32(this.grdMemberList.DataKeys[row.RowIndex].Value)))
				{
					num++;
				}
			}
			if (num == 0)
			{
				this.ShowMsg("请先选择要删除的会员账号", false);
			}
			else
			{
				this.BindClientList();
				this.ShowMsg("成功删除了选择的会员", true);
			}
		}
		private void LoadParameters()
		{
			if (!this.Page.IsPostBack)
			{
				int result = 0;
				if (int.TryParse(this.Page.Request.QueryString["rankId"], out result))
				{
					this.rankId = new int?(result);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["searchKey"]))
				{
					this.searchKey = base.Server.UrlDecode(this.Page.Request.QueryString["searchKey"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["realName"]))
				{
					this.realName = base.Server.UrlDecode(this.Page.Request.QueryString["realName"]);
				}
				if (!string.IsNullOrEmpty(this.Page.Request.QueryString["Approved"]))
				{
					this.approved = new bool?(System.Convert.ToBoolean(this.Page.Request.QueryString["Approved"]));
				}
				this.rankList.SelectedValue = this.rankId;
				this.txtSearchText.Text = this.searchKey;
				this.txtRealName.Text = this.realName;
			}
			else
			{
				this.rankId = this.rankList.SelectedValue;
				this.searchKey = this.txtSearchText.Text;
				this.realName = this.txtRealName.Text.Trim();
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.LoadParameters();
			if (!base.IsPostBack)
			{
				this.BindClientList();
				this.rankList.DataBind();
				this.rankList.SelectedValue = this.rankId;
				SiteSettings siteSetting = this.GetSiteSetting();
				if (siteSetting.SMSEnabled)
				{
					//this.litsmscount.Text = this.GetAmount(siteSetting).ToString();
					this.hdenablemsg.Value = "1";
				}
				if (siteSetting.EmailEnabled)
				{
					this.hdenableemail.Value = "1";
				}
				if (!string.IsNullOrEmpty(base.Request.QueryString["type"]))
				{
					string str = base.Request.QueryString["type"];
					if (str != null)
					{
						if (str == "sleep")
						{
							this.litType.Text = "休眠客户";
							goto IL_12A;
						}
						if (str == "activy")
						{
							this.litType.Text = "活跃客户";
							goto IL_13E;
						}
					}
					this.litType.Text = "新客户";
				}
				IL_12A:;
			}
			IL_13E:
			CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);
			this.btnSearchButton.Click += new System.EventHandler(this.btnSearchButton_Click);
			this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
			this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
			this.lkbDelectCheck.Click += new System.EventHandler(this.lkbDelectCheck_Click);
			this.lkbDelectCheck1.Click += new System.EventHandler(this.lkbDelectCheck_Click);
			this.grdMemberList.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(this.grdMemberList_RowDeleting);
		}
		public string PostData(string url, string postData)
		{
			string str = string.Empty;
			string result;
			try
			{
				Uri requestUri = new Uri(url);
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
				byte[] bytes = System.Text.Encoding.UTF8.GetBytes(postData);
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = (long)bytes.Length;
				using (System.IO.Stream stream = request.GetRequestStream())
				{
					stream.Write(bytes, 0, bytes.Length);
				}
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					using (System.IO.Stream stream2 = response.GetResponseStream())
					{
						System.Text.Encoding encoding = System.Text.Encoding.UTF8;
						System.IO.Stream stream3 = stream2;
						if (response.ContentEncoding.ToLower() == "gzip")
						{
							stream3 = new GZipStream(stream2, CompressionMode.Decompress);
						}
						else
						{
							if (response.ContentEncoding.ToLower() == "deflate")
							{
								stream3 = new DeflateStream(stream2, CompressionMode.Decompress);
							}
						}
						using (System.IO.StreamReader reader = new System.IO.StreamReader(stream3, encoding))
						{
							result = reader.ReadToEnd();
							return result;
						}
					}
				}
			}
			catch (System.Exception exception)
			{
				str = string.Format("获取信息错误：{0}", exception.Message);
			}
			result = str;
			return result;
		}
		private void ReBind(bool isSearch)
		{
			NameValueCollection queryStrings = new NameValueCollection();
			if (this.rankList.SelectedValue.HasValue)
			{
				queryStrings.Add("rankId", this.rankList.SelectedValue.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
			}
			queryStrings.Add("searchKey", this.txtSearchText.Text);
			queryStrings.Add("realName", this.txtRealName.Text);
			queryStrings.Add("pageSize", this.pager.PageSize.ToString(System.Globalization.CultureInfo.InvariantCulture));
			queryStrings.Add("type", this.Page.Request.QueryString["type"]);
			if (!isSearch)
			{
				queryStrings.Add("pageIndex", this.pager.PageIndex.ToString(System.Globalization.CultureInfo.InvariantCulture));
			}
			base.ReloadPage(queryStrings);
		}
		protected MemberQuery SetClient(MemberQuery query)
		{
			MemberQuery result;
			if (!string.IsNullOrEmpty(base.Request.QueryString["type"]))
			{
				System.Collections.Generic.Dictionary<int, MemberClientSet> memberClientSet = MemberHelper.GetMemberClientSet();
				int[] numArray = new int[memberClientSet.Count];
				memberClientSet.Keys.CopyTo(numArray, 0);
				if (memberClientSet.Count <= 0)
				{
					result = query;
					return result;
				}
				MemberClientSet set = new MemberClientSet();
				string str = base.Request.QueryString["type"];
				query.ClientType = str;
				string str2 = str;
				if (str2 != null)
				{
					if (str2 == "new")
					{
						set = memberClientSet[numArray[0]];
						this.litType.Text = "新客户";
						query.StartTime = set.StartTime;
						query.EndTime = set.EndTime;
						if (set.LastDay > 0)
						{
							query.StartTime = new System.DateTime?(System.DateTime.Now.AddDays((double)(-(double)set.LastDay)));
							query.EndTime = new System.DateTime?(System.DateTime.Now);
						}
						goto IL_20F;
					}
					if (str2 == "activy")
					{
						set = memberClientSet[numArray[1]];
						this.litType.Text = "活跃客户";
						if (set.ClientValue > 0m)
						{
							query.StartTime = new System.DateTime?(System.DateTime.Now.AddDays((double)(-(double)set.LastDay)));
							query.EndTime = new System.DateTime?(System.DateTime.Now);
							query.CharSymbol = set.ClientChar;
							if (set.ClientTypeId == 6)
							{
								query.OrderNumber = new int?((int)set.ClientValue);
								result = query;
								return result;
							}
							query.OrderMoney = new decimal?(set.ClientValue);
						}
						result = query;
						return result;
					}
				}
				set = memberClientSet[numArray[2]];
				this.litType.Text = "睡眠客户";
				query.StartTime = new System.DateTime?(System.DateTime.Now.AddDays((double)(-(double)set.LastDay)));
				query.EndTime = new System.DateTime?(System.DateTime.Now);
				result = query;
				return result;
			}
			IL_20F:
			result = query;
			return result;
		}
	}
}
