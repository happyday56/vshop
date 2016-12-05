namespace Hidistro.Entities.VShop
{
    using System;
    using System.Runtime.CompilerServices;

    public class InviteCode
    {
        public InviteCode()
        {
            this.TimeStamp = DateTime.Now;
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int InviteId { get; set; }

        /// <summary>
        /// 邀请码
        /// </summary>
        public string Code { get; set; }

        public int ProductId { get; set; }
        private string _productname;
        public string ProductName
        {
            get
            {
                if (string.IsNullOrEmpty(_productname) && ProductId > 0)
                {
                    //查询产品名称

                }

                return _productname;

            }
            set { _productname = value; }
        }

        public int UserId { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// 邀请状态 
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

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUse { get; set; }

        /// <summary>
        /// 受邀请的用户ID
        /// </summary>
        public int InviteUserId { get; set; }

        public string InviteRealName { get; set; }

        public string InvitePhone { get; set; }

        public int PTTypeId { get; set; }

        public decimal StoreGiftPoint { get; set; }

    }
}
