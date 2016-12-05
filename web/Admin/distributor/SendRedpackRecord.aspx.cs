using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPNET.WebControls;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Core.Enums;
using Hidistro.Entities.VShop;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.ControlPanel.Utility;
using Hishop.Weixin.Pay;
using NewLife.Log;

namespace Hidistro.UI.Web.Admin.distributor
{
    public partial class SendRedpackRecord : AdminPage
    {
        private int m_BalanceDrawRequestID;

        protected string htmlOperatorName = "操作";

        protected string LocalUrl = string.Empty;

        private void AAbiuZJB(bool flag)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            string str = "pageSize";
            int pageSize = this.pager.PageSize;
            nameValueCollection.Add(str, pageSize.ToString(CultureInfo.InvariantCulture));
            if (!flag)
            {
                string str1 = "pageIndex";
                int pageIndex = this.pager.PageIndex;
                nameValueCollection.Add(str1, pageIndex.ToString(CultureInfo.InvariantCulture));
            }
            base.ReloadPage(nameValueCollection);
        }

        private void LoadSendRedpackRecord()
        {
            SendRedpackRecordQuery sendRedpackRecordQuery = new SendRedpackRecordQuery()
            {
                BalanceDrawRequestID = this.m_BalanceDrawRequestID,
                SortBy = "ID",
                SortOrder = SortAction.Asc
            };
            Globals.EntityCoding(sendRedpackRecordQuery, true);
            sendRedpackRecordQuery.PageIndex = this.pager.PageIndex;
            sendRedpackRecordQuery.PageSize = this.pager.PageSize;
            DbQueryResult sendRedpackRecordRequest = DistributorsBrower.GetSendRedpackRecordRequest(sendRedpackRecordQuery);
            this.rptList.DataSource = sendRedpackRecordRequest.Data;
            this.rptList.DataBind();
            this.pager.TotalRecords = sendRedpackRecordRequest.TotalRecords;
        }

        protected void btnSendAllRedPack_Click(object sender, EventArgs e)
        {
            string empty;
            DataTable notSendRedpackRecord = DistributorsBrower.GetNotSendRedpackRecord(this.m_BalanceDrawRequestID);
            string str = string.Empty;
            int num = 0;
            if (notSendRedpackRecord.Rows.Count <= 0)
            {
                str = "微信红包都已发送完成！";
            }
            else
            {
                for (int i = 0; i < notSendRedpackRecord.Rows.Count; i++)
                {
                    str = this.SendRedPack(notSendRedpackRecord.Rows[i]["OpenID"].ToString(), "", notSendRedpackRecord.Rows[i]["Wishing"].ToString(), notSendRedpackRecord.Rows[i]["ActName"].ToString(), "分销商发红包提现", int.Parse(notSendRedpackRecord.Rows[i]["Amount"].ToString()), int.Parse(notSendRedpackRecord.Rows[i]["ID"].ToString()));
                    if (str != "1")
                    {
                        if (str == "1")
                        {
                            this.AAbiuZJB(false);
                            return;
                        }
                        empty = string.Empty;
                        if (num <= 0)
                        {
                            empty = string.Concat("发送失败，原因是：", str);
                            this.ShowMsgAndReUrl(empty, false, base.Server.UrlDecode(this.LocalUrl));
                            return;
                        }
                        empty = string.Format("成功发送{0}个红包，其余发送失败，原因是：{1}", num, str);
                        this.ShowMsgAndReUrl(empty, false, base.Server.UrlDecode(this.LocalUrl));
                        return;
                    }
                    num++;
                    DistributorsBrower.SetRedpackRecordIsUsed(this.m_BalanceDrawRequestID, true);
                }
            }
            if (str == "1")
            {
                this.AAbiuZJB(false);
                return;
            }
            empty = string.Empty;
            if (num <= 0)
            {
                empty = string.Concat("发送失败，原因是：", str);
                this.ShowMsgAndReUrl(empty, false, base.Server.UrlDecode(this.LocalUrl));
                return;
            }
            empty = string.Format("成功发送{0}个红包，其余发送失败，原因是：{1}", num, str);
            this.ShowMsgAndReUrl(empty, false, base.Server.UrlDecode(this.LocalUrl));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(this.Page.Request.QueryString["serialid"], out this.m_BalanceDrawRequestID))
            {
                base.Response.Redirect("BalanceDrawApplyList.aspx");
                base.Response.End();
            }
            else
            {
                if (DistributorsBrower.GetBalanceDrawRequestIsCheck(this.m_BalanceDrawRequestID))
                {
                    this.htmlOperatorName = "状态";
                    this.btnSendAllRedPack.Visible = false;
                }
                this.LocalUrl = base.Server.UrlEncode(base.Request.Url.ToString());
                if (!base.IsPostBack)
                {
                    this.LoadSendRedpackRecord();
                    return;
                }
            }
        }



        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int num = 0;
            int.TryParse(e.CommandArgument.ToString(), out num);
            if (num > 0)
            {
                string commandName = e.CommandName;
                string str = commandName;
                if (commandName != null)
                {
                    if (str != "send")
                    {
                        return;
                    }
                    SendRedpackRecordInfo sendRedpackRecordByID = DistributorsBrower.GetSendRedpackRecordByID(num);
                    if (sendRedpackRecordByID != null)
                    {
                        string str1 = this.SendRedPack(sendRedpackRecordByID.OpenID, "", sendRedpackRecordByID.Wishing, sendRedpackRecordByID.ActName, "分销商发红包提现", sendRedpackRecordByID.Amount, num);
                        if (str1 == "1")
                        {
                            DistributorsBrower.SetRedpackRecordIsUsed(num, true);
                            int amount = sendRedpackRecordByID.Amount;
                            decimal num1 = decimal.Parse(amount.ToString()) / new decimal(100);
                            VShopHelper.UpdateBalanceDistributors(sendRedpackRecordByID.UserID, num1);
                            this.AAbiuZJB(false);
                            return;
                        }
                        this.ShowMsg(string.Concat("发送失败，原因是：", str1), false);
                    }
                }
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton linkButton = (LinkButton)e.Item.FindControl("lbtnSend");
                if (((DataRowView)e.Item.DataItem).Row["IsSend"].ToString() == "False")
                {
                    if (this.btnSendAllRedPack.Visible)
                    {
                        linkButton.OnClientClick = "return confirm('点击发送后，会直接以红包的形式发送到客户的微信账户中\n请确认是否发送？')";
                        return;
                    }
                    linkButton.Enabled = false;
                    linkButton.Text = "已线下完成";
                    return;
                }
                linkButton.Enabled = false;
                linkButton.Text = "已发送";
                linkButton.Style.Add("color", "red");
            }
        }

        public string SendRedPack(string re_openid, string sub_mch_id, string wishing, string act_name, string remark, int amount, int sendredpackrecordid)
        {
            string empty = string.Empty;
            try
            {
                SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                //XTrace.WriteLine("OpenId=" + re_openid);
                if (!masterSettings.EnableWeiXinRequest)
                {
                    empty = "管理员后台未开启微信付款！";
                }
                else
                {
                    DateTime now = DateTime.Now;
                    DateTime dateTime = DateTime.Parse(string.Concat(now.ToString("yyyy-MM-dd"), " 00:00:01"));
                    DateTime dateTime1 = DateTime.Parse(string.Concat(now.ToString("yyyy-MM-dd"), " 08:00:00"));
                    if (now > dateTime && now < dateTime1)
                    {
                        empty = "北京时间0：00-8：00不触发红包赠送";
                    }
                    else if (string.IsNullOrEmpty(masterSettings.WeixinAppId) || string.IsNullOrEmpty(masterSettings.WeixinPartnerID) || string.IsNullOrEmpty(masterSettings.WeixinPartnerKey) || string.IsNullOrEmpty(masterSettings.WeixinCertPath) || string.IsNullOrEmpty(masterSettings.WeixinCertPassword))
                    {
                        empty = "系统微信发红包配置接口未配置好";
                    }
                    else if (!string.IsNullOrEmpty(re_openid))
                    {
                        string siteName = masterSettings.SiteName;
                        string str = masterSettings.SiteName;
                        RedPackClient redPackClient = new RedPackClient();
                        //XTrace.WriteLine("ClientIP=" + Globals.IPAddress);
                        empty = redPackClient.SendRedpack(masterSettings.WeixinAppId, masterSettings.WeixinPartnerID, sub_mch_id, siteName, str, re_openid, wishing, Globals.IPAddress, act_name, remark, amount, masterSettings.WeixinPartnerKey, masterSettings.WeixinCertPath, masterSettings.WeixinCertPassword, sendredpackrecordid);
                    }
                    else
                    {
                        empty = "用户未绑定微信";
                    }
                }
            }
            catch (Exception ex)
            {
                empty = ex.Message;
                //XTrace.WriteLine(ex.ToString());
            }
            //XTrace.WriteLine(empty);
            return empty;
        }
    }
}