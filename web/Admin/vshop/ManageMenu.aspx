<%@ Page Language="C#" AutoEventWireup="true" Inherits="Hidistro.UI.Web.Admin.ManageMenu" CodeBehind="~/Admin/vshop/ManageMenu.aspx.cs" MasterPageFile="~/Admin/Admin.Master" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server"> 
<div class="dataarea mainwidth databody">
  <div class="title">
  <em><img src="../images/03.gif" width="32" height="32" /></em>

  <h1>自定义菜单配置</h1>
  <span>自定义菜单能够帮助公众号丰富界面，让用户更好更快地理解公众号的功能。</span></div>  
	<div class="btn">	
   <span style="margin-right:13px;">目前自定义菜单最多包括3个一级菜单，每个一级菜单最多包含5个二级菜单。一级菜单最多4个汉字，二级菜单最多7个汉字，多出来的部分将会以“...”代替。请注意，创建自定义菜单后，由于微信客户端缓存，需要24小时微信客户端才会展现出来。建议测试时可以尝试取消关注公众账号后再次关注，则可以看到创建后的效果。</span></btn>
   </div>
   
<!--结束-->
		<!--数据列表区域-->
  <div class="datalist">
		 <!-- 添加按钮-->
  <div style="padding:10px 0px;"><a href="AddMenu.aspx" class="submit_jia">添加主菜单</a></div>
  <UI:Grid ID="grdMenu" DataKeyNames="MenuId" runat="server" ShowHeader="true" AutoGenerateColumns="false" HeaderStyle-CssClass="table_title" GridLines="None" Width="100%">
                    <Columns> 
                    <asp:TemplateField HeaderText="菜单名称" HeaderStyle-Width="20%" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <span class="Name"><asp:Literal ID="lblCategoryName" runat="server" /></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="绑定关键字" HeaderStyle-Width="40%" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <%--<span class="Name"><%# RenderInfo(Eval("ReplyId"))%></span>--%>
                            <span class="Name" <%# Eval("Url").ToString() == "" ? "style='display:none'":"" %> ><%# Eval("BindTypeName") %>&nbsp;
                                 <%# Eval("Url").ToString().StartsWith("http") ? Eval("Url") : ""%>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <UI:SortImageColumn HeaderText="排序" ItemStyle-Width="60px" ReadOnly="true" HeaderStyle-CssClass="td_right td_left"/>
                     <asp:TemplateField HeaderText="操作" HeaderStyle-CssClass="td_left td_right_fff">
                         <ItemTemplate>
                            <span class="submit_bianji"><asp:HyperLink ID="lkEdit" runat="server" Text="编辑" NavigateUrl='<%# "EditMenu.aspx?MenuID="+Eval("MenuID")%>'></asp:HyperLink> </span>
                            <span class="submit_shanchu"><Hi:ImageLinkButton runat="server" IsShow="true" Text="删除" CommandName="DeleteMenu"  DeleteMsg="删除分类会级联删除其所有子分类，确定要删除选择的分类吗？" /></span>
                            <span class="submit_bianji"><asp:HyperLink ID="HyperLink1" Visible='<%#Eval("BindType").ToString() == "None" %>' runat="server" Text="添加子菜单" NavigateUrl='<%# "AddMenu.aspx?pid="+Eval("MenuID")%>'></asp:HyperLink> </span>
                         </ItemTemplate>
                     </asp:TemplateField>                     
                    </Columns>
                </UI:Grid>
	<div style="margin-top:10px;"><asp:Button ID="btnSubmit" runat="server"  Text="保存到微信"  CssClass="submit_DAqueding" /></div>
  
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">

</asp:Content>

