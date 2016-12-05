<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master"
    CodeBehind="DistributorIncomeList.aspx.cs" Inherits="Hidistro.UI.Web.Admin.distributor.DistributorIncomeList" %>

<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
        <div class="title">
            <em>
                <img src="../images/04.gif" width="32" height="32" /></em>
            <h1>分销商佣金收益汇总列表</h1>
            <span>显示分销商佣金收益信息。</span>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div class="datalist">
            <div class="searcharea clearfix br_search">
                <ul>
                    <li><span>店铺名：</span> <span>
                        <asp:TextBox ID="txtStoreName" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li><span>手机号码：</span> <span>
                        <asp:TextBox ID="txtCellPhone" CssClass="forminput" runat="server" /></span>
                    </li>
                    <li>
                        <asp:Button ID="btnSearchButton" runat="server" class="searchbutton" Text="搜索" />
                    </li>
                    <li>
                        <asp:LinkButton ID="btnCreateReport" runat="server" Text="导出提现佣金" />
                    </li>
                </ul>
            </div>
            
            <table>
                <thead>
                    <tr class="table_title">
                        <td style="text-align:left;">店铺名称
                        </td>
                        <td style="text-align:left;">店主名称
                        </td>
                        <td style="text-align:left;">真实姓名
                        </td>
                        <td style="text-align:left;">店主ID
                        </td>
                        <td style="text-align:left;">手机号码
                        </td>
                        <td style="text-align:right;">累计收益
                        </td>
                        <td style="text-align:right;">不可提现
                        </td>
                        <td style="text-align:right;">可提现
                        </td>
                        <td style="text-align:right;">可提现未提现</td>
                        <td style="text-align:right;">已提现待发放</td>
                        <td style="text-align:right;">已发放收益</td>
                    </tr>
                </thead>
                <asp:Repeater ID="reDistIncome" runat="server">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td style="text-align:left;"><%# Eval("StoreName")%>&nbsp;</td>
                                <td style="text-align:left;"><%# Eval("UserName")%>&nbsp;</td>
                                <td style="text-align:left;"><%# Eval("RealName")%>&nbsp;</td>
                                <td style="text-align:left;"><%# Eval("UserId")%>&nbsp;</td>
                                <td style="text-align:left;"><%# Eval("CellPhone")%>&nbsp;</td>
                                <td style="text-align:right;"><%# Eval("LJSY", "{0:F2}")%>&nbsp;</td>
                                <td style="text-align:right;"><%# Eval("BKTX", "{0:F2}")%>&nbsp;</td>
                                <td style="text-align:right;"><%# Eval("KTX", "{0:F2}")%>&nbsp;</td>
                                <td style="text-align:right;"><%# Eval("KTXWTX", "{0:F2}")%></td>
                                <td style="text-align:right;"><%# Eval("YTXDFF", "{0:F2}")%></td>
                                <td style="text-align:right;"><%# Eval("YFFSY", "{0:F2}")%></td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="blank12 clearfix">
            </div>
        </div>
        <!--数据列表底部功能区域-->
        <div class="bottomPageNumber clearfix">
            <div class="pageNumber">
                <div class="pagination" style="width: auto">
                    <UI:Pager runat="server" ShowTotalPages="true" ID="pager" />
                </div>
            </div>
        </div>
    </div>
    <div style="color:red;">
        &nbsp;&nbsp;&nbsp;&nbsp;注解：各列由各明细表按设定规则汇总所得。<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;财务校验数据汇总准确性规则：<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;累计收益＝不可提现+可提现<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;可提现＝可提现未提现+已提现待发放
    </div>
    <script type="text/javascript">
        //验证
        function validatorForm() {

            return true;
        }
        $(function () {

        });

    </script>
</asp:Content>
