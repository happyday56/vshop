namespace Hidistro.SaleSystem.Vshop
{
    using Hidistro.Core.Entities;
    using Hidistro.Entities.Store;
    using Hidistro.Entities.VShop;
    using Hidistro.SqlDal.Comments;
    using Hidistro.SqlDal.Commodities;
    using Hidistro.SqlDal.Store;
    using Hidistro.SqlDal.VShop;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public static class VshopBrowser
    {
        public static int AddPrizeRecord(PrizeRecordInfo model)
        {
            return new PrizeRecordDao().AddPrizeRecord(model);
        }

        public static DbQueryResult FriendExtensionList(FriendExtensionQuery Query)
        {
            return new FriendExtensionDao().FriendExtensionList(Query);
        }

        public static ActivityInfo GetActivity(int activityId)
        {
            return new ActivityDao().GetActivity(activityId);
        }

        public static IList<BannerInfo> GetAllBanners()
        {
            return new BannerDao().GetAllBanners();
        }

        public static IList<NavigateInfo> GetAllNavigate()
        {
            return new BannerDao().GetAllNavigate();
        }

        public static int GetCountBySignUp(int activityId)
        {
            return new PrizeRecordDao().GetCountBySignUp(activityId);
        }

        public static DataTable GetHomeProducts()
        {
            return new HomeProductDao().GetHomeProducts();
        }

        public static LotteryActivityInfo GetLotteryActivity(int activityid)
        {
            LotteryActivityInfo lotteryActivityInfo = new LotteryActivityDao().GetLotteryActivityInfo(activityid);
            if (lotteryActivityInfo != null)
            {
                lotteryActivityInfo.PrizeSettingList = JsonConvert.DeserializeObject<List<PrizeSetting>>(lotteryActivityInfo.PrizeSetting);
            }
            return lotteryActivityInfo;
        }

        public static LotteryTicketInfo GetLotteryTicket(int activityid)
        {
            LotteryTicketInfo lotteryTicket = new LotteryActivityDao().GetLotteryTicket(activityid);
            if (lotteryTicket != null)
            {
                lotteryTicket.PrizeSettingList = JsonConvert.DeserializeObject<List<PrizeSetting>>(lotteryTicket.PrizeSetting);
            }
            return lotteryTicket;
        }

        public static MessageInfo GetMessage(int messageId)
        {
            return new ReplyDao().GetMessage(messageId);
        }

        public static List<PrizeRecordInfo> GetPrizeList(PrizeQuery page)
        {
            return new PrizeRecordDao().GetPrizeList(page);
        }

        public static TopicInfo GetTopic(int topicId)
        {
            return new TopicDao().GetTopic(topicId);
        }

        public static DataTable GetTopics()
        {
            return new HomeTopicDao().GetHomeTopics();
        }

        public static int GetUserPrizeCount(int activityid)
        {
            return new PrizeRecordDao().GetUserPrizeCount(activityid);
        }

        public static PrizeRecordInfo GetUserPrizeRecord(int activityid)
        {
            return new PrizeRecordDao().GetUserPrizeRecord(activityid);
        }

        public static DataTable GetVote(int voteId, out string voteName, out int checkNum, out int voteNum)
        {
            return new VoteDao().LoadVote(voteId, out voteName, out checkNum, out voteNum);
        }

        public static bool HasSignUp(int activityId, int userId)
        {
            return new PrizeRecordDao().HasSignUp(activityId, userId);
        }

        public static bool IsVote(int voteId)
        {
            return new VoteDao().IsVote(voteId);
        }

        public static bool OpenTicket(int ticketId)
        {
            LotteryTicketInfo lotteryTicket = GetLotteryTicket(ticketId);
            if (new PrizeRecordDao().OpenTicket(ticketId, lotteryTicket.PrizeSettingList))
            {
                lotteryTicket.IsOpened = true;
                return new LotteryActivityDao().UpdateLotteryTicket(lotteryTicket);
            }
            return false;
        }

        public static string SaveActivitySignUp(ActivitySignUpInfo info)
        {
            return new ActivitySignUpDao().SaveActivitySignUp(info);
        }

        public static bool UpdatePrizeRecord(PrizeRecordInfo model)
        {
            return new PrizeRecordDao().UpdatePrizeRecord(model);
        }

        public static bool UpdatePrizeRecord(int activityId, int userId, string realName, string cellPhone)
        {
            PrizeRecordDao dao = new PrizeRecordDao();
            PrizeRecordInfo userPrizeRecord = dao.GetUserPrizeRecord(activityId);
            if (userPrizeRecord == null)
            {
                return false;
            }
            userPrizeRecord.UserID = userId;
            userPrizeRecord.RealName = realName;
            userPrizeRecord.CellPhone = cellPhone;
            return dao.UpdatePrizeRecord(userPrizeRecord);
        }

        public static bool Vote(int voteId, string itemIds)
        {
            return new VoteDao().Vote(voteId, itemIds);
        }

        public static bool AttendActivity(int activityId, int userId)
        {
            return new ActivityDao().AttendActivity(activityId, userId);
        }

        public static DataTable GetActivityList(int userId)
        {
            return new ActivityDao().GetActivityList(userId);
        }

        public static IList<ActivitiesInfo> GetAllActivities()
        {
            return new ActivitiesDao().GetAllActivities();
        }
        
    }
}

