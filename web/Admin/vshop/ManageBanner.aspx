<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageBanner.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.ManageBanner" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Entities.VShop"%> 
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentHolder" runat="server">
<div class="dataarea mainwidth databody">
  <div class="title">
  <em><img src="../images/03.gif" width="32" height="32" /></em>

  <h1>轮播图配置</h1>
  <span>如果您选择了首页带有轮播图的模板，您需要在此配置好相关参数，最多只能是4张。</span></div>  
		<!-- 添加按钮-->
   <div class="btn">
       <a class="submit_jia" href="javascript:DialogFrame('Vshop/AddBanner.aspx','添加轮播',null,null)">添加轮播 </a>
 </div>
   
<!--结束-->
		<!--数据列表区域-->
  <div class="datalist">
  <UI:Grid ID="grdBanner" DataKeyNames="Id" runat="server" ShowHeader="true" 
          AutoGenerateColumns="false" HeaderStyle-CssClass="table_title" GridLines="None" 
          Width="100%" onrowcommand="grdBanner_RowCommand">
                    <Columns> 
                    <asp:TemplateField HeaderText="图片" HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <span class="Name"> <img src='<%#((BannerInfo)Container.DataItem).ImageUrl==""?"/utility/pics/none.gif":((BannerInfo)Container.DataItem).ImageUrl%>'  width="90px" height="50px;"/></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="描述"  HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <span class="Name"><%#((BannerInfo)Container.DataItem).ShortDesc%></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="跳转至"  HeaderStyle-CssClass="td_right td_left">
                        <ItemTemplate>
                            <span class="Name">(<%#((BannerInfo)Container.DataItem).LocationType.ToShowText()%>) 
<%#((BannerInfo)Container.DataItem).LocationType == LocationType.Phone ? ((BannerInfo)Container.DataItem).Url : ((BannerInfo)Container.DataItem).LoctionUrl%>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <UI:SortImageColumn HeaderText="排序" ItemStyle-Width="60px" ReadOnly="true" HeaderStyle-CssClass="td_right td_left"/>
                     <asp:TemplateField HeaderText="操作" HeaderStyle-CssClass="td_left td_right_fff">
                         <ItemTemplate>
                            <span class="submit_bianji"> <a  href="javascript:DialogFrame('<%# "Vshop/EditBanner.aspx?Id="+ ((BannerInfo)Container.DataItem).Id %>','编辑轮播图',null,null)">编辑</a> </span>
                            <span class="submit_shanchu"><Hi:ImageLinkButton ID="ImageLinkButton1" runat="server" IsShow="true" Text="删除" CommandName="DeleteBanner"  DeleteMsg="确定要删除此轮播图吗？" /></span>
                          </span>
                         </ItemTemplate>
                     </asp:TemplateField>                     
                    </Columns>
                </UI:Grid>
  </div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="validateHolder" runat="server">
</asp:Content>

