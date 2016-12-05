using System;
using System.Runtime.CompilerServices;
using Hishop.Weixin.MP;
using Hishop.Weixin.MP.Request;

namespace Hishop.Weixin.MP.Request.Event
{

    /// <summary>
    /// 定位请求
    /// </summary>
    public class LocationEventRequest : EventRequest
    {


        public override RequestEventType Event
        {

            get
            {
                return RequestEventType.Location;
            }
            set { }

        }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public float Precision { get; set; }


    }


}


