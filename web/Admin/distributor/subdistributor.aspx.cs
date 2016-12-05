using Hidistro.Entities.VShop;
using Hidistro.SaleSystem.Vshop;
using Hidistro.UI.ControlPanel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_distributor_subdistributor : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        databind();
    }

    DistributorTree distributor = null;
    protected void databind()
    {
        StringBuilder str = new StringBuilder();
        distributor = InviteBrowser.GetDistributorList();
        if (distributor != null)
        {
            str.AppendFormat("<ul id=\"org\" style=\"display: none\"><li>{0}", distributor.UserName);
            if (distributor.Childs != null && distributor.Childs.Count > 0)
            {
                GetTreeData(ref str, distributor.Childs);
            }
            str.Append("</li></ul>");
        }
        this.ltOrganization.Text = str.ToString();

        List<object> obj = new List<object>();
        GetZTreeData(ref obj, distributor.Childs);
        this.ltOrganization.Text += "\r\n<script>var setting = {data: {simpleData: {enable: true}},callback:{onExpand:function(){$(\"[treenode_ico]\").css({\"backgroundSize\":\"100%\",\"borderRadius\":\"50%\"})}}}; var zNodes = " + LitJson.JsonMapper.ToJson(obj) + "; $(function(){$.fn.zTree.init($(\"#treeDemo\"), setting, zNodes);$(\"[treenode_ico]\").css({\"backgroundSize\":\"100%\",\"borderRadius\":\"50%\"})}); </script>";
    }

    private void GetZTreeData(ref List<object> obj, List<DistributorTree> childs)
    {
        if (childs != null && childs.Count > 0)
        {

            foreach (var item in childs)
            {
                //{ id:11, pId:1, name:"叶子节点1", icon:"../../../css/zTreeStyle/img/diy/2.png"},
                obj.Add(new
                {
                    id = item.UserId,
                    pId = item.ParentId,
                    name = item.UserName + "(" + item.StoreName + ")->" + item.GradeName,
                    icon = item.UserHead
                });

                GetZTreeData(ref obj, item.Childs);

            }

        }
    }

    private void GetTreeData(ref StringBuilder str, List<DistributorTree> childs)
    {

        if (childs != null && childs.Count > 0)
        {
            str.Append("<ul>");
            foreach (var item in childs)
            {
                str.AppendFormat("<li><dl><dt><img src='{1}' style='width:30px;height:30px;border: 1px solid white;border-radius: 50%;'/></dt><dd>{0}({2})</dd></dl>", item.UserName, item.UserHead, item.StoreName);
                GetTreeData(ref str, item.Childs);
                str.Append("</li>");
            }
            str.Append("</ul>");
        }
    }

}