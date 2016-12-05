namespace Hidistro.Entities.VShop
{
    using System;
    using System.Runtime.CompilerServices;

    public class InviteApply
    {
        public InviteApply()
        {
            this.TimeStamp = DateTime.Now;
            this.ApplyTime = this.TimeStamp;
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int ApplyId { get; set; }
        /// <summary>
        /// 申请用户
        /// </summary>
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string Phone { get; set; }
        /// <summary>
        /// 申请限额
        /// </summary>
        public int ApplyNum { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int IsAudit { get; set; }
        public string IsAuditText
        {
            get
            {
                switch (IsAudit)
                {
                    case 0:
                        return "待审核";
                    case 1:
                        return "通过";
                    case 2:
                        return "拒绝";
                }
                return "";
            }
        }

        /// <summary>
        /// 审核用户ID
        /// </summary>
        public int AuditUserId { get; set; }
        /// <summary>
        /// 审核用户名
        /// </summary>
        public string AuditUserIdName { get; set; }

    }
}
