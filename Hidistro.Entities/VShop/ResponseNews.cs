namespace Hidistro.Entities.VShop
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ResponseNews : AbstractResponseMessage
    {
        public ResponseNews()
        {
            base.MsgType = "news";
        }

        public int ArticleCount
        {
            get
            {
                return ((this.MessageInfo == null) ? 0 : this.MessageInfo.Count);
            }
        }

        public IList<Hidistro.Entities.VShop.MessageInfo> MessageInfo { get; set; }
    }
}

