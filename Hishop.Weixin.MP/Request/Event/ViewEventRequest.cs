namespace Hishop.Weixin.MP.Request.Event
{
    using Hishop.Weixin.MP;
    using Hishop.Weixin.MP.Request;
    using System;
    using System.Runtime.CompilerServices;

    public class ViewEventRequest : EventRequest
    {
        public override RequestEventType Event
        {
            get
            {
                return RequestEventType.VIEW;
            }
            set
            {
            }
        }

        public string EventKey { get; set; }

        //public string Encrypt { get; set; }
    }
}

