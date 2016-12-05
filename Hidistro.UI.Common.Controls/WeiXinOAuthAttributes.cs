using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.UI.Common.Controls
{
    /// <summary>
    /// 微信登录
    /// </summary>
    public class WeiXinOAuthAttribute : Attribute
    {

        public WeiXinOAuthAttribute(WeiXinOAuthPage page)
        {
            m_WeiXinOAuthPage = page;
        }

        WeiXinOAuthPage m_WeiXinOAuthPage;
        public WeiXinOAuthPage WeiXinOAuthPage
        {
            get { return m_WeiXinOAuthPage; }
            set { m_WeiXinOAuthPage = value; }
        }

    }

}
