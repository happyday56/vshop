<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="HomeTopicSetting.aspx.cs" Inherits="Hidistro.UI.Web.Admin.vshop.HomeTopicSetting" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.Common.Controls" Assembly="Hidistro.UI.Common.Controls" %>
<%@ Register TagPrefix="Hi" Namespace="Hidistro.UI.ControlPanel.Utility" Assembly="Hidistro.UI.ControlPanel.Utility" %>

<%@ Register TagPrefix="Kindeditor" Namespace="kindeditor.Net" Assembly="kindeditor.Net" %>
<%@ Register TagPrefix="UI" Namespace="ASPNET.WebControls" Assembly="ASPNET.WebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="validateHolder" runat="server">
<script type="text/javascript">
    function ShowAddDiv()
    {
       DialogFrame("./vshop/SearchHomeTopic.aspx", "添加首页专题", 975, null);
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentHolder" runat="server">
<input type="hidden" id="hdtopic" runat="server" />
<div class="dataarea mainwidth databody">
	  <div class="title"> <em><img src="../images/03.gif" width="32" height="32" /></em>
	    <h1>首页专题 </h1>
      <span>如果您选择了首页带有专题的模板，您需要在此配置好相关参数。</span>
     </div>       
	  <!--结束-->
    <div class="btn">
           <div style="float:right; margin-right:15px;" class="delete"><Hi:ImageLinkButton ID="btnDeleteAll"  runat="server" Text="清空" IsShow="true" DeleteMsg="确定要清空首页专题吗？" /></div>
       <a class="submit_jia" href="javascript:void(0)" onclick="ShowAddDiv()">添加首页专题</a>

       <asp:LinkButton ID="btnFinish" runat="server" Text="保存排序" CssClass="base_blue_btn" 
                onclick="btnFinish_Click"></asp:LinkButton>
    </div>
	  <!--数据列表区域-->
  <div class="datalist">
	    <UI:Grid ID="grdHomeTopics" runat="server" ShowHeader="true" AutoGenerateColumns="false" DataKeyNames="TopicId" HeaderStyle-CssClass="table_title" GridLines="None" Width="100%">
            <Columns> 
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
                     <asp:TemplateField HeaderText="排序"  ItemStyle-Width="10%" HeaderStyle-CssClass="td_right td_left">
                            <itemtemplate>
                           <asp:TextBox ID="txtSequence" runat="server" Text='<%# Eval("DisplaySequence") %>' Width="60px" />
                          </itemtemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="操作" HeaderStyle-CssClass="td_left td_right_fff" HeaderStyle-Width="10%">
                        <ItemStyle />
                        <itemtemplate> 
			                <span class="submit_shanchu"><Hi:ImageLinkButton  runat="server" ID="Delete" Text="删除" IsShow="true" CommandName="Delete" ></Hi:ImageLinkButton></span>
                        </itemtemplate>
                    </asp:TemplateField>
            </Columns>
        </UI:Grid>
    </div>
  </div>
</asp:Content>


