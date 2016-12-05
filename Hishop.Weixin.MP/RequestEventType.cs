using System;

namespace Hishop.Weixin.MP
{

    public enum RequestEventType
    {

        /// <summary>
        /// 订阅
        /// </summary>
        Subscribe,

        /// <summary>
        /// 取消订阅
        /// </summary>
        UnSubscribe,

        /// <summary>
        /// 扫描带参数的二维码
        /// </summary>
        Scan,

        /// <summary>
        /// 地理位置
        /// </summary>
        Location,

        /// <summary>
        /// 单击按钮
        /// </summary>
        Click,

        /// <summary>
        /// 链接按钮
        /// </summary>
        VIEW,

        /// <summary>
        /// 模板消息推送
        /// </summary>
        TEMPLATESENDJOBFINISH

    }


}

