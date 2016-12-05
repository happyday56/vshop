<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageVThemes.aspx.cs" Inherits="Hidistro.UI.Web.Admin.ManageVThemes" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
    <div class="dataarea mainwidth databody">
    <div class="title"> 
      <em><img src="../images/01.gif" width="32" height="32" /></em>
      <h1>您正在使用<span style="color:red; font-size:18px;margin:0px 10px;" id="spanThemeName"><asp:Literal ID="litThemeName" runat="server"></asp:Literal></span>模板</h1>
		<span>微信商城的页面风格，好比实体店面的装修，您可以从以下列表中选择您喜欢的风格</span>
    </div>
    <div class="blank12 clearfix"></div>
	<div class="datafrom">
      <div class="Template">
        <h1>可供您选择的模板（总数为：<asp:Literal ID="lblThemeCount" runat="server"  />）</h1>
          <asp:DataList runat="server" ID="dtManageThemes" RepeatColumns="6" DataKeyField="ThemeName"  RepeatDirection="Horizontal">                                   
                <ItemTemplate>
                  <ul>
                      <li ThemeName='<%# Eval("ThemeName")%>' class="">
                        <span><img src='<%#  Eval("ThemeImgUrl") %>' /></span>
                        <em>
                          <strong><%# Eval("Name") %> (<%# Eval("ThemeName")%>)</strong>
						    <div>
							  <p class="tenter"><asp:LinkButton ID="btnManageThemesOK" runat="server" CommandName="btnUse"  Text="应用"/></p>
							<%--  <p><a href="SetVThemes.aspx?themeName=<%# Eval("ThemeName")%>">配置</a></p>--%>
							</div>           
                        </em>
                      </li>                                                                                                           
                 </ul>
                </ItemTemplate>
            </asp:DataList>   
	   </div>

	</div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
    <script type="text/javascript" >
        $(document).ready(function () {
            $.each($(".Template li"), function () {
                if ($(this).attr("ThemeName") == $("#spanThemeName").html()) {
                    
                    $(this).attr({ "class": "themes_hover" });
                    $(this).find("p.tenter").html("已应用");
                }
            });
        });
</script>
</asp:Content>

