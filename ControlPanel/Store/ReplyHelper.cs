namespace Hidistro.ControlPanel.Store
{
    using Hidistro.Entities.VShop;
    using Hidistro.SqlDal.VShop;
    using System;
    using System.Collections.Generic;

    public class ReplyHelper
    {
        public static void DeleteNewsMsg(int id)
        {
            new ReplyDao().DeleteNewsMsg(id);
        }

        public static bool DeleteReply(int id)
        {
            return new ReplyDao().DeleteReply(id);
        }

        public static IList<ReplyInfo> GetAllReply()
        {
            return new ReplyDao().GetAllReply();
        }

        public static ReplyInfo GetMismatchReply()
        {
            IList<ReplyInfo> replies = new ReplyDao().GetReplies(ReplyType.NoMatch);
            if ((replies != null) && (replies.Count > 0))
            {
                return replies[0];
            }
            return null;
        }

        public static IList<ReplyInfo> GetReplies(ReplyType type)
        {
            return new ReplyDao().GetReplies(type);
        }

        public static ReplyInfo GetReply(int id)
        {
            return new ReplyDao().GetReply(id);
        }

        public static ReplyInfo GetSubscribeReply()
        {
            IList<ReplyInfo> replies = new ReplyDao().GetReplies(ReplyType.Subscribe);
            if ((replies != null) && (replies.Count > 0))
            {
                return replies[0];
            }
            return null;
        }

        public static bool HasReplyKey(string key)
        {
            return new ReplyDao().HasReplyKey(key);
        }

        public static bool SaveReply(ReplyInfo reply)
        {
            reply.LastEditDate = DateTime.Now;
            reply.LastEditor = ManagerHelper.GetCurrentManager().UserName;
            return new ReplyDao().SaveReply(reply);
        }

        public static bool UpdateReply(ReplyInfo reply)
        {
            reply.LastEditDate = DateTime.Now;
            reply.LastEditor = ManagerHelper.GetCurrentManager().UserName;
            return new ReplyDao().UpdateReply(reply);
        }

        public static bool UpdateReplyRelease(int id)
        {
            return new ReplyDao().UpdateReplyRelease(id);
        }
    }
}

