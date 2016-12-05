using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Entities.VShop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace Hidistro.UI.Web.Admin.vshop
{
	public class Vshop_LotteryActivity : AdminPage
	{
		protected System.Web.UI.WebControls.Button btnAddActivity;
		protected WebCalendar calendarEndDate;
		protected WebCalendar calendarStartDate;
		protected System.Web.UI.WebControls.CheckBox ChkOpen;
		protected System.Web.UI.WebControls.FileUpload fileUpload;
		protected System.Web.UI.WebControls.Literal Litdesc;
		protected System.Web.UI.WebControls.Literal LitTitle;
		protected System.Web.UI.WebControls.TextBox txtActiveName;
		protected System.Web.UI.WebControls.TextBox txtdesc;
		protected System.Web.UI.WebControls.TextBox txtKeyword;
		protected System.Web.UI.WebControls.TextBox txtMaxNum;
		protected System.Web.UI.WebControls.TextBox txtPrize1;
		protected System.Web.UI.WebControls.TextBox txtPrize1Num;
		protected System.Web.UI.WebControls.TextBox txtPrize2;
		protected System.Web.UI.WebControls.TextBox txtPrize2Num;
		protected System.Web.UI.WebControls.TextBox txtPrize3;
		protected System.Web.UI.WebControls.TextBox txtPrize3Num;
		protected System.Web.UI.WebControls.TextBox txtPrize4;
		protected System.Web.UI.WebControls.TextBox txtPrize4Num;
		protected System.Web.UI.WebControls.TextBox txtPrize5;
		protected System.Web.UI.WebControls.TextBox txtPrize5Num;
		protected System.Web.UI.WebControls.TextBox txtPrize6;
		protected System.Web.UI.WebControls.TextBox txtPrize6Num;
		protected System.Web.UI.WebControls.TextBox txtProbability1;
		protected System.Web.UI.WebControls.TextBox txtProbability2;
		protected System.Web.UI.WebControls.TextBox txtProbability3;
		protected System.Web.UI.WebControls.TextBox txtProbability4;
		protected System.Web.UI.WebControls.TextBox txtProbability5;
		protected System.Web.UI.WebControls.TextBox txtProbability6;
		private int type;
		protected void btnAddActivity_Click(object sender, System.EventArgs e)
		{
			if (ReplyHelper.HasReplyKey(this.txtKeyword.Text.Trim()))
			{
				this.ShowMsg("关键字重复!", false);
			}
			else
			{
				string str = string.Empty;
				if (this.fileUpload.HasFile)
				{
					try
					{
						str = VShopHelper.UploadTopicImage(this.fileUpload.PostedFile);
						if (!this.calendarStartDate.SelectedDate.HasValue)
						{
							this.ShowMsg("请选择活动开始时间", false);
						}
						else
						{
							if (!this.calendarEndDate.SelectedDate.HasValue)
							{
								this.ShowMsg("请选择活动结束时间", false);
							}
							else
							{
								int num;
								if (!int.TryParse(this.txtMaxNum.Text, out num) || num.ToString() != this.txtMaxNum.Text)
								{
									this.ShowMsg("可抽奖次数必须是整数", false);
								}
								else
								{
									LotteryActivityInfo info = new LotteryActivityInfo
									{
										ActivityName = this.txtActiveName.Text,
										ActivityKey = this.txtKeyword.Text,
										ActivityDesc = this.txtdesc.Text,
										ActivityPic = str,
										ActivityType = this.type,
										StartTime = this.calendarStartDate.SelectedDate.Value,
										EndTime = this.calendarEndDate.SelectedDate.Value,
										MaxNum = System.Convert.ToInt32(this.txtMaxNum.Text)
									};
									System.Collections.Generic.List<PrizeSetting> list = new System.Collections.Generic.List<PrizeSetting>();
									int num2;
									if (int.TryParse(this.txtPrize1Num.Text, out num2) && int.TryParse(this.txtPrize2Num.Text, out num2) && int.TryParse(this.txtPrize3Num.Text, out num2))
									{
										decimal num3 = System.Convert.ToDecimal(this.txtProbability1.Text);
										decimal num4 = System.Convert.ToDecimal(this.txtProbability2.Text);
										decimal num5 = System.Convert.ToDecimal(this.txtProbability3.Text);
										System.Collections.Generic.List<PrizeSetting> collection = new System.Collections.Generic.List<PrizeSetting>();
										PrizeSetting item = new PrizeSetting
										{
											PrizeName = this.txtPrize1.Text,
											PrizeNum = System.Convert.ToInt32(this.txtPrize1Num.Text),
											PrizeLevel = "一等奖",
											Probability = num3
										};
										collection.Add(item);
										PrizeSetting setting2 = new PrizeSetting
										{
											PrizeName = this.txtPrize2.Text,
											PrizeNum = System.Convert.ToInt32(this.txtPrize2Num.Text),
											PrizeLevel = "二等奖",
											Probability = num4
										};
										collection.Add(setting2);
										PrizeSetting setting3 = new PrizeSetting
										{
											PrizeName = this.txtPrize3.Text,
											PrizeNum = System.Convert.ToInt32(this.txtPrize3Num.Text),
											PrizeLevel = "三等奖",
											Probability = num5
										};
										collection.Add(setting3);
										list.AddRange(collection);
										if (this.ChkOpen.Checked)
										{
											if (string.IsNullOrEmpty(this.txtPrize4.Text) || string.IsNullOrEmpty(this.txtPrize5.Text) || string.IsNullOrEmpty(this.txtPrize6.Text))
											{
												this.ShowMsg("开启四五六名必须填写", false);
												return;
											}
											if (!int.TryParse(this.txtPrize4Num.Text, out num2) || !int.TryParse(this.txtPrize5Num.Text, out num2) || !int.TryParse(this.txtPrize6Num.Text, out num2))
											{
												this.ShowMsg("奖品数量必须为数字！", false);
												return;
											}
											decimal num6 = System.Convert.ToDecimal(this.txtProbability4.Text);
											decimal num7 = System.Convert.ToDecimal(this.txtProbability5.Text);
											decimal num8 = System.Convert.ToDecimal(this.txtProbability6.Text);
											System.Collections.Generic.List<PrizeSetting> list2 = new System.Collections.Generic.List<PrizeSetting>();
											PrizeSetting setting4 = new PrizeSetting
											{
												PrizeName = this.txtPrize4.Text,
												PrizeNum = System.Convert.ToInt32(this.txtPrize4Num.Text),
												PrizeLevel = "四等奖",
												Probability = num6
											};
											list2.Add(setting4);
											PrizeSetting setting5 = new PrizeSetting
											{
												PrizeName = this.txtPrize5.Text,
												PrizeNum = System.Convert.ToInt32(this.txtPrize5Num.Text),
												PrizeLevel = "五等奖",
												Probability = num7
											};
											list2.Add(setting5);
											PrizeSetting setting6 = new PrizeSetting
											{
												PrizeName = this.txtPrize6.Text,
												PrizeNum = System.Convert.ToInt32(this.txtPrize6Num.Text),
												PrizeLevel = "六等奖",
												Probability = num8
											};
											list2.Add(setting6);
											list.AddRange(list2);
										}
										info.PrizeSettingList = list;
										int num9 = VShopHelper.InsertLotteryActivity(info);
										if (num9 > 0)
										{
											ReplyInfo reply = new TextReplyInfo
											{
												Keys = info.ActivityKey,
												MatchType = MatchType.Equal,
												ActivityId = num9
											};
											string str2 = ((LotteryActivityType)info.ActivityType).ToString();
											object obj2 = System.Enum.Parse(typeof(ReplyType), str2);
											reply.ReplyType = (ReplyType)obj2;
											ReplyHelper.SaveReply(reply);
											base.Response.Redirect("ManageLotteryActivity.aspx?type=" + this.type, true);
											this.ShowMsg("添加成功！", true);
										}
									}
									else
									{
										this.ShowMsg("奖品数量必须为数字！", false);
									}
								}
							}
						}
						return;
					}
					catch
					{
						this.ShowMsg("图片上传失败，您选择的不是图片类型的文件，或者网站的虚拟目录没有写入文件的权限", false);
						return;
					}
				}
				this.ShowMsg("您没有选择上传的图片文件！", false);
			}
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (int.TryParse(base.Request.QueryString["type"], out this.type))
			{
				switch (this.type)
				{
				case 1:
					this.LitTitle.Text = "大转盘活动";
					this.Litdesc.Text = "大转盘活动";
					break;
				case 2:
					this.LitTitle.Text = "刮刮卡活动";
					this.Litdesc.Text = "刮刮卡活动";
					break;
				case 3:
					this.LitTitle.Text = "砸金蛋活动";
					this.Litdesc.Text = "砸金蛋活动";
					break;
				}
			}
			else
			{
				this.ShowMsg("参数错误", false);
			}
		}
	}
}
