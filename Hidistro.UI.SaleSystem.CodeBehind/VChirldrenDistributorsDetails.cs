namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Members;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Data;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using NewLife.Log;

    [ParseChildren(true)]
    public class VChirldrenDistributorsDetails : VWeiXinOAuthTemplatedWebControl
    {
        private Panel twodistributor;
        private VshopTemplatedRepeater rpDistributorDetails;

        private Literal litTeamTotal;
        private Literal litTeamNumber;
        
        protected override void AttachChildControls()
        {
            PageTitle.AddSiteNameTitle("我的团队");
            this.twodistributor = (Panel)this.FindControl("twodistributor");
            this.litTeamTotal = (Literal)this.FindControl("litTeamTotal");
            this.litTeamNumber = (Literal)this.FindControl("litTeamNumber");
            this.rpDistributorDetails = (VshopTemplatedRepeater)this.FindControl("rpDistributorDetails");

            int userId = 0;
            int.TryParse(this.Page.Request.QueryString["uid"], out userId);
            string searchGradeId = this.Page.Request.QueryString["gradeId"];
                        
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

            DistributorsInfo currentDistributors = DistributorsBrower.GetCurrentDistributors(userId);
            
            if (null != currentDistributors)
            {
                //XTrace.WriteLine("Current Distributor: " + currentDistributors.UserId + " --- DistriGradeId: " + currentDistributors.DistriGradeId + " --- DefaultPartnerGradeId: " + masterSettings.DefaultPartnerGradeId);
                if (!currentDistributors.DistriGradeId.ToString().Equals(masterSettings.DefaultPartnerGradeId))
                {
                    searchGradeId = "";
                }

                // 获取下级店铺信息
                DataTable data = DistributorsBrower.GetDistributorAllBalanceByUserId(userId, masterSettings.DefaultPartnerGradeId, masterSettings.DefaultTutorGradeId, masterSettings.DefaultStoreGradeId, searchGradeId);

                if (null != data && data.Rows.Count > 0)
                {
                    PageTitle.AddSiteNameTitle(data.Rows[0]["UserName"].ToString() + "（" + data.Rows[0]["DisplayGradeName"].ToString() + "）的团队");
                    // 团队成员
                    this.litTeamNumber.Text = data.Rows[0]["DistriCnt"].ToString();
                    // 团队总收入
                    if (string.IsNullOrEmpty(data.Rows[0]["SumAllBalance"].ToString()))
                    {
                        this.litTeamTotal.Text = "0.00";
                    }
                    else
                    {
                        this.litTeamTotal.Text = decimal.Parse(data.Rows[0]["SumAllBalance"].ToString() ).ToString("F2");
                    }

                }
                else
                {
                    // 团队成员
                    this.litTeamNumber.Text = "0";
                    // 团队总收入
                    this.litTeamTotal.Text = "0.00";
                }

                DataTable detailData = DistributorsBrower.GetDistributorDetailBalanceByUserId(userId, masterSettings.DefaultPartnerGradeId, masterSettings.DefaultTutorGradeId, masterSettings.DefaultStoreGradeId, searchGradeId);

                if (null != detailData)
                {
                    this.rpDistributorDetails.DataSource = detailData;
                    this.rpDistributorDetails.DataBind();
                }
            }
           
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-VChirldrenDistributorsDetails.html";
            }
            base.OnInit(e);
        }
    }
}

