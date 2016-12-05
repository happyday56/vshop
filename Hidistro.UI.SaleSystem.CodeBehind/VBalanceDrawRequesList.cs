namespace Hidistro.UI.SaleSystem.CodeBehind
{
    using Hidistro.Core.Entities;
    using Hidistro.Core.Enums;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.SaleSystem.Vshop;
    using Hidistro.UI.Common.Controls;
    using System;
    using System.Web.UI;

    [ParseChildren(true)]
    public class VBalanceDrawRequesList : VWeiXinOAuthTemplatedWebControl
    {
        private VshopTemplatedRepeater vshopbalancedraw;

        protected override void AttachChildControls()
        {
            this.vshopbalancedraw = (VshopTemplatedRepeater) this.FindControl("vshopbalancedraw");
            string userId = "";
            MemberInfo member = MemberProcessor.GetCurrentMember();

            if (null != member)
            {
                userId = member.UserId.ToString();
            }
            else
            {
                userId = "0";
            }

            BalanceDrawRequestQuery query = new BalanceDrawRequestQuery {
                PageIndex = 1,
                PageSize = 0x186a0,
                //UserId = DistributorsBrower.GetCurrentDistributors().UserId.ToString(),
                UserId = userId,
                SortBy = "SerialID",
                RequestTime = "",
                RequestStartTime = "",
                RequestEndTime = "",
                CheckTime = "",
                IsCheck = "",
                SortOrder = SortAction.Desc
            };
            DbQueryResult balanceDrawRequest = DistributorsBrower.GetBalanceDrawRequest(query);
            if (balanceDrawRequest.TotalRecords > 0)
            {
                this.vshopbalancedraw.DataSource = balanceDrawRequest.Data;
                this.vshopbalancedraw.DataBind();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (this.SkinName == null)
            {
                this.SkinName = "Skin-BalanceDrawRequesList.html";
            }
            base.OnInit(e);
        }
    }
}

