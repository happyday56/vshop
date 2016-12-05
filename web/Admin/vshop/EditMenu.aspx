<%@ Page Language="C#" AutoEventWireup="true" Inherits="Hidistro.UI.Web.Admin.EditMenu" CodeBehind="~/Admin/vshop/EditMenu.aspx.cs" MasterPageFile="~/Admin/Admin.Master" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>
<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHolder" runat="server">
<div class="areacolumn clearfix">
      <div class="columnright">
          <div class="title">
            <em><img src="../images/03.gif" width="32" height="32" /></em>
            <h1>修改菜单</h1>
            <span class="font">修改菜单</span>
      </div>
          <div class="formitem validator2">
            <ul>
              <li><span class="formitemtitle Pw_100">菜单名称：<em >*</em></span>
                <asp:TextBox ID="txtMenuName" runat="server" CssClass="forminput" />
                <p id="ctl00_contentHolder_txtCategoryNameTip">菜单名称不能为空，在1至7个字符之间</p>
              </li>
              <li runat="server" id="liParent"><span class="formitemtitle Pw_100">上级菜单：</span>
                <asp:Literal runat="server" ID="lblParent"></asp:Literal>
              </li>
               <li runat="server" id="liBind"> <span class="formitemtitle Pw_100">绑定对象：</span>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="productType" 
                      AutoPostBack="True" ClientIDMode="Static" 
                      onselectedindexchanged="ddlType_SelectedIndexChanged">
                    <asp:ListItem Text="不绑定" Value="0"></asp:ListItem>
                    <asp:ListItem Text="关键字" Value="1"></asp:ListItem> 
                    <%--<asp:ListItem Text="专题" Value="2"></asp:ListItem>--%>
                    <asp:ListItem Text="首页" Value="3"></asp:ListItem>
                    <asp:ListItem Text="产品分类" Value="4"></asp:ListItem>
                    <asp:ListItem Text="购物车" Value="5"></asp:ListItem>
                    <asp:ListItem Text="会员中心" Value="6"></asp:ListItem>
                    <asp:ListItem Text="链接" Value="8"></asp:ListItem>
                </asp:DropDownList>
                <p id="P2">绑定后主菜单将不能再添加下属子菜单</p>
              </li>
              <li id="liValue" runat="server"><span class="formitemtitle Pw_100">&nbsp;</span>
                <asp:DropDownList ID="ddlValue" runat="server" CssClass="productType" />
              </li>
              <li id="liUrl" runat="server"><span class="formitemtitle Pw_100">链接地址：</span>
                <asp:TextBox ID="txtUrl" runat="server" Text="http://" CssClass="forminput" Width="300px" />
              </li>
            </ul>
              <ul class="btn Pa_100 clearfix">
                <asp:Button ID="btnAddMenu" runat="server" OnClientClick="return PageIsValid();" Text="确 定"  CssClass="submit_DAqueding float" />
         </ul>
          </div>
  </div>
        
  </div>	
  
  
  
  

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" language="javascript">
        function InitValidators() {
            initValid(new InputValidator('ctl00_contentHolder_txtMenuName', 1, 7, false, null, '必填 菜单名称不能为空，在1至7个字符之间'))
        }
        $(document).ready(function () { InitValidators(); });

    </script>
</asp:Content>

