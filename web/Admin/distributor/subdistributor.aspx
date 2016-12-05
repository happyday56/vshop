<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeFile="subdistributor.aspx.cs" Inherits="Admin_distributor_subdistributor" %>


<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="http://apps.bdimg.com/libs/jqueryui/1.10.4/jquery-ui.min.js"></script>
    <link href="../css/jquery.jOrgChart.css" rel="stylesheet" />
    <link href="../css/custom.css" rel="stylesheet" />
    <link href="../css/prettify.css" rel="stylesheet" />
    <script type="text/javascript"  src="../js/prettify.js"></script>
    <script type="text/javascript"  src="../js/jquery.jOrgChart.js"></script>
    <link href="../js/zTree-v3.5/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <script src="../js/zTree-v3.5/js/jquery.ztree.core-3.5.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">

    <div class="dataarea mainwidth databody" style="background-color: #A9CCCE;overflow:scroll;">
        <div class="title">
            <em>
                <img src="../images/04.gif" width="32" height="32" /></em>
            <h1>分销商结构图</h1>
            <span></span>
        </div>
        <!--搜索-->
        <!--数据列表区域-->
        <div>
            <ul id="treeDemo" class="ztree"></ul>
        </div>
        
        <div>
            <asp:Literal runat="server" ID="ltOrganization" />
            <div id="chart" class="orgChart"></div>
        </div>
      <%--  <ul id="org1" style="display: none">
            <li>一级分销3商

                    <ul>
                        <li>二级分销商小弟1
                            <ul>
                                <li>三级分销商小弟11</li>
                                <li>三级分销商小弟12</li>
                                <li>三级分销商小弟13</li>
                            </ul>
                        </li>
                        <li>二级分销商小弟2
                             <ul>
                                 <li>三级分销商小弟21</li>
                                 <li>三级分销商小弟22</li>
                                 <li>三级分销商小弟23</li>
                             </ul>
                        </li>
                        <li>二级分销商小弟3
                             <ul>
                                 <li>三级分销商小弟31</li>
                                 <li>三级分销商小弟32</li>
                                 <li>三级分销商小弟33</li>
                             </ul>
                        </li>
                    </ul>
            </li>
            <li>一级2</li>
        </ul>--%>

        <script>
            jQuery(document).ready(function () {
                $("#org").jOrgChart({
                    chartElement: '#chart',
                    dragAndDrop: true
                });
            });
        </script>

    </div>
</asp:Content>
