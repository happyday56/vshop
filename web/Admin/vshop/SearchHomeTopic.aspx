
<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SearchHomeTopic.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.SearchHomeTopic" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<%@ Import Namespace="Hidistro.Core" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHolder" Runat="Server">
<!--选项卡-->
	<!--选项卡-->

	<div class="dataarea mainwidth databody">
			  <div class="title">
  <em><img src="../images/01.gif" width="32" height="32" /></em>
  <h1>首页专题添加</h1>
        </div>
		<!--搜索-->

			<!--结束-->
		
		
		<!--数据列表区域-->
	  <div class="datalist">
	  		<div class="searcharea clearfix">
			    
		    </div>
		<!--结束-->
             <div class="functionHandleArea clearfix">
			    <!--分页功能-->
			        <div class="pageHandleArea">
				        <ul>
					        <li class="paginalNum"><span>每页显示数量：</span><UI:PageSize runat="server" ID="hrefPageSize" /></li>
				        </ul>
			        </div>
			        <div class="pageNumber">
				        <div class="pagination">
                            <UI:Pager runat="server" ShowTotalPages="false" ID="pager" />
                        </div>
                    </div>

                    <div class="blank8 clearfix"></div>
			        <div class="batchHandleArea">
				        <ul>
					        <li class="batchHandleButton">
					            <span class="signicon"></span>
					            <span class="allSelect"><a href="javascript:void(0)" onclick="SelectAll()">全选</a></span>
					            <span class="reverseSelect"><a href="javascript:void(0)" onclick="ReverseSelect()">反选</a></span>
                                <span class="submit_btnxiajia"><Hi:ImageLinkButton ID="btnAdd" runat="server" Text="添加" /></span>
                                           
                            </li>
				        </ul>
			        </div>
			  </div>
	   <UI:Grid ID="grdTopics" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="TopicId" GridLines="None" Width="100%" HeaderStyle-CssClass="table_title">
              <Columns>
                  <asp:TemplateField ItemStyle-Width="10%" HeaderText="选择" HeaderStyle-CssClass="td_right td_left">
                        <itemtemplate>
                            <input name="CheckBoxGroup" type="checkbox" value='<%#Eval("TopicId") %>' />
                        </itemtemplate>
                  </asp:TemplateField>   
                    <asp:TemplateField HeaderText="专题图片" HeaderStyle-CssClass="td_right td_left" HeaderStyle-Width="30%">
                        <ItemTemplate>
		                    <img class="Img100_30" src='<%# Eval("IconUrl") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="专题标题"  ItemStyle-Width="30%" HeaderStyle-CssClass="td_right td_left">
                            <itemtemplate>
                             <%#Eval("title")%>
                          </itemtemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="关键字"  ItemStyle-Width="10%" HeaderStyle-CssClass="td_right td_left">
                            <itemtemplate>
                             <%#Eval("Keys")%>
                          </itemtemplate>
                    </asp:TemplateField>
              </Columns>
              <EmptyDataTemplate>暂时没有任何可供选择的专题，请到微配置-微信专题下添加专题！</EmptyDataTemplate>
            </UI:Grid>
   
      <div class="blank5 clearfix"></div>
	  </div>
	  <!--数据列表底部功能区域-->
	  <div class="bottomPageNumber clearfix">
	  <div class="pageNumber">
					<div class="pagination">
            <UI:Pager runat="server" ShowTotalPages="true" ID="pager1" />
            </div>
			</div>
		</div>
</div>


	<div class="databottom"></div>
<div class="bottomarea testArea">
  <!--顶部logo区域-->
</div>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" Runat="Server">
   
</asp:Content>