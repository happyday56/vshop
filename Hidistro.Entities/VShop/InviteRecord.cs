namespace Hidistro.Entities.VShop
{
    using System;
    using System.Runtime.CompilerServices;

    public class InviteRecord
    {
        public InviteRecord()
        {
            this.TimeStamp = DateTime.Now;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// openid
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 真实名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邀请码ID
        /// </summary>
        public int InviteId { get; set; }

        /// <summary>
        /// 邀请码
        /// </summary>
        public string InviteCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int InviteStatus { get; set; }

        public string InviteStatusText
        {
            get
            {
                switch (InviteStatus)
                {
                    case 0:
                        return "未使用";
                    case 1:
                        return "待激活";
                    case 2:
                        return "未完成";
                    case 3:
                        return "已完成";
                }
                return "";
            }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime TimeStamp { get; set; }


    }
}
