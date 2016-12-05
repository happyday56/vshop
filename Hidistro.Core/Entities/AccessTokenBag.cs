using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.Core.Entities
{
    public class AccessTokenBag
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public DateTime AccessTokenExpireTime { get; set; }

        public string AccessToken { get; set; }

    }
}
