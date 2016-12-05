using Hidistro.ControlPanel.Members;
using Hidistro.UI.Common.Controls;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin
{
	public class branchAddDistributors : AdminPage
	{
		protected System.Web.UI.WebControls.Button batchCreate;
		protected System.Web.UI.WebControls.Button btnExport;
		private static System.Collections.Generic.IList<string> exportdistirbutors;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton radioaccount;
		protected System.Web.UI.HtmlControls.HtmlInputRadioButton radionumber;
		protected Hidistro.UI.Common.Controls.Style Style1;
		protected System.Web.UI.HtmlControls.HtmlTextArea txtdistributornames;
		protected System.Web.UI.HtmlControls.HtmlInputText txtnumber;
		protected System.Web.UI.HtmlControls.HtmlInputText txtslsdistributors;
		private void batchCreate_Click(object sender, System.EventArgs e)
		{
			try
			{
				string distributorname = this.txtslsdistributors.Value;
				int referruserId = MemberHelper.IsExiteDistributorNames(distributorname);
				if (string.IsNullOrEmpty(this.txtslsdistributors.Value))
				{
					this.ShowMsg("输入的推荐分销商不能为空！", false);
				}
				else
				{
					if (!string.IsNullOrEmpty(distributorname) && referruserId <= 0)
					{
						this.ShowMsg("输入的推荐分销商不存在！", false);
					}
					else
					{
						if (this.radionumber.Checked)
						{
							if (string.IsNullOrEmpty(this.txtnumber.Value.Trim()))
							{
								this.ShowMsg("请输入要生成的账号数量", false);
							}
							else
							{
								int result = 0;
								int.TryParse(this.txtnumber.Value, out result);
								if (result <= 0 || result > 999)
								{
									this.ShowMsg("数值必须在1~999之间的正整数", false);
								}
								else
								{
									branchAddDistributors.exportdistirbutors = MemberHelper.BatchCreateMembers(this.CreateDistributros(result), referruserId, "1");
									this.ShowMsg("批量制作成功", true);
									if (branchAddDistributors.exportdistirbutors != null && branchAddDistributors.exportdistirbutors.Count > 0)
									{
										this.btnExport.Visible = true;
										this.btnExport.Text = "导出分销商";
									}
								}
							}
						}
						else
						{
							string str2 = this.txtdistributornames.Value;
							System.Collections.Generic.IList<string> distributornames = new System.Collections.Generic.List<string>();
							if (string.IsNullOrEmpty(str2))
							{
								this.ShowMsg("请输入要制作的账号", false);
							}
							else
							{
								bool flag = false;
								string[] array = str2.Split(new string[]
								{
									"\r\n"
								}, System.StringSplitOptions.None);
								for (int i = 0; i < array.Length; i++)
								{
									string str3 = array[i];
									if (string.IsNullOrEmpty(str3) || str3.Length < 2 || str3.Length > 10)
									{
										flag = true;
										break;
									}
									distributornames.Add(str3);
								}
								if (flag)
								{
									this.ShowMsg("每个账号长度在2~10个字符", false);
								}
								else
								{
									branchAddDistributors.exportdistirbutors = MemberHelper.BatchCreateMembers(distributornames, referruserId, "2");
									if (branchAddDistributors.exportdistirbutors != null && branchAddDistributors.exportdistirbutors.Count > 0)
									{
										this.btnExport.Visible = true;
										this.btnExport.Text = "导出失败分销商";
										this.ShowMsg("生成成功，分销商账号已存在！", true);
									}
									else
									{
										this.btnExport.Visible = false;
										this.ShowMsg("生成成功！", true);
									}
								}
							}
						}
					}
				}
			}
			catch (System.Exception)
			{
				throw;
			}
		}
		private void BindDistributors()
		{
		}
		private void btnExport_Click(object sender, System.EventArgs e)
		{
			this.ResponseTxt();
		}
		private System.Collections.Generic.IList<string> CreateDistributros(int len)
		{
			System.Collections.Generic.IList<string> list = new System.Collections.Generic.List<string>();
			System.Random random = new System.Random(System.Environment.TickCount);
			for (int i = 0; i < len; i++)
			{
				list.Add(random.Next(11111111, 99999999).ToString());
			}
			return list;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request["action"]) && base.Request["action"] == "SearchKey")
			{
				string allDistributorsName = string.Empty;
				if (!string.IsNullOrEmpty(base.Request["keyword"]))
				{
					allDistributorsName = MemberHelper.GetAllDistributorsName(base.Request["keyword"]);
				}
				base.Response.ContentType = "application/json";
				base.Response.Write("{\"data\":[" + allDistributorsName + "]}");
				base.Response.End();
			}
			if (!base.IsPostBack)
			{
				branchAddDistributors.exportdistirbutors = null;
				this.BindDistributors();
				if (this.radionumber.Checked)
				{
					this.txtnumber.Attributes.Remove("disabled");
					this.txtdistributornames.Attributes["disabled"] = "disabled";
				}
				else
				{
					this.txtdistributornames.Attributes.Remove("disabled");
					this.txtnumber.Attributes["disabled"] = "disabled";
				}
			}
			this.batchCreate.Click += new System.EventHandler(this.batchCreate_Click);
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
		}
		private void ResponseTxt()
		{
			this.Page.Response.Clear();
			this.Page.Response.Buffer = true;
            this.Page.Response.Charset = "UTF-8";
			this.Page.Response.AppendHeader("Content-Disposition", "attachment;filename=tempdistributors.txt");
            this.Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
			base.Response.ContentType = "text/plain";
			this.EnableViewState = false;
			System.Globalization.CultureInfo formatProvider = new System.Globalization.CultureInfo("ZH-CN", true);
			new System.IO.StringWriter(formatProvider);
			this.Page.Response.Write(string.Join("\r\n", branchAddDistributors.exportdistirbutors));
			this.Page.Response.End();
		}
	}
}
