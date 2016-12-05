namespace Hidistro.SaleSystem.Vshop
{
    using System.Linq;
    using Hidistro.Core;
    using Hidistro.Core.Entities;
    using Hidistro.Core.Enums;
    using Hidistro.Entities.Members;
    using Hidistro.Entities.Orders;
    using Hidistro.Entities.VShop;
    using Hidistro.SqlDal.Members;
    using Hidistro.SqlDal.Orders;
    using Hidistro.SqlDal.VShop;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.Caching;
    using NewLife.Log;
    using Hidistro.Messages;
    using Hidistro.Entities.Sales;
    using System.Text;
    using Hidistro.Entities.Commodities;
    using Hidistro.SqlDal.Commodities;

    public class DistributorsBrower
    {
        public static bool AddBalanceDrawRequest(BalanceDrawRequestInfo balancerequestinfo, string regionAddress)
        {
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();
            DistributorsInfo currentDistributors = GetCurrentDistributors();
            if ((((currentMember != null) && !string.IsNullOrEmpty(currentMember.RealName)) && ((currentDistributors != null) && (currentDistributors.UserId > 0))) && !string.IsNullOrEmpty(currentMember.CellPhone))
            {
                //if (!(string.IsNullOrEmpty(balancerequestinfo.MerchanCade) || !(currentDistributors.RequestAccount != balancerequestinfo.MerchanCade)))
                if (!string.IsNullOrEmpty(balancerequestinfo.MerchanCade))
                {
                    //new DistributorsDao().UpdateDistributorById(balancerequestinfo.MerchanCade, currentMember.UserId);
                    new DistributorsDao().UpdateDistributorById(balancerequestinfo.MerchanCade, balancerequestinfo.BankName
                        , balancerequestinfo.BankAddress, balancerequestinfo.AccountName, currentMember.UserId, regionAddress);
                }
                if (string.IsNullOrEmpty(balancerequestinfo.AccountName))
                {
                    balancerequestinfo.AccountName = currentMember.RealName;
                }
                balancerequestinfo.UserId = currentMember.UserId;
                balancerequestinfo.UserName = currentMember.UserName;
                //if (!string.IsNullOrEmpty(currentDistributors.RequestAccount))
                //{
                //    balancerequestinfo.MerchanCade = currentDistributors.RequestAccount;
                //}
                balancerequestinfo.CellPhone = currentMember.CellPhone;
                return new DistributorsDao().AddBalanceDrawRequest(balancerequestinfo);
            }
            return false;
        }

        public static void AddDistributorProductId(List<int> productList)
        {
            int userId = GetCurrentDistributors().UserId;
            if ((userId > 0) && (productList.Count > 0))
            {
                new DistributorsDao().RemoveDistributorProducts(productList, userId);
                foreach (int num2 in productList)
                {
                    new DistributorsDao().AddDistributorProducts(num2, userId);
                }
            }
        }

        public static void RegistDistributorAddProduct(int userId)
        {
            IList<ProductInfo> productList = new ProductDao().GetAllProducts();
            new DistributorsDao().RemoveDistributorProductsByUserId(userId);

            foreach (ProductInfo pi in productList)
            {
                new DistributorsDao().AddDistributorProducts(pi.ProductId, userId);
            }
        }

        public static bool AddDistributors(DistributorsInfo distributors)
        {
            if (IsExiteDistributorsByStoreName(distributors.StoreName) > 0)
            {
                return false;
            }

            // 增加判断订单状态是否已付款
            if (!CheckOrderIsPay(distributors.UserId))
            {
                return false;
            }

            XTrace.WriteLine("-----------------------------------开店流程处理开始......-----------------------------------");
            MemberInfo currentMember = MemberProcessor.GetCurrentMember();

            SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);

            XTrace.WriteLine("传入进来的ReferralUserId:" + distributors.ParentUserId + "   : " + distributors.ReferralUserId);
            XTrace.WriteLine("当前申请开店的会员ID：" + currentMember.UserId + " 会员对应的上级ID：" + currentMember.ReferralUserId);
            int parentUserId = distributors.ReferralUserId;
            //MemberInfo parentMember = MemberProcessor.GetReferralMember(currentMember.UserId);
            //MemberInfo parentMember = MemberProcessor.GetMember(currentMember.ReferralUserId);
            MemberInfo parentMember = MemberProcessor.GetMember(distributors.ReferralUserId);
            if (null != parentMember)
            {
                XTrace.WriteLine("根据会员ID：" + currentMember.UserId + " 查找出来的上级ID：" + parentMember.UserId + " 上级ID的上级ID：" + parentMember.ReferralUserId);
                parentUserId = parentMember.UserId;
            }

            distributors.DistributorGradeId = DistributorGrade.OneDistributor;
            //distributors.ParentUserId = new int?(currentMember.UserId);
            distributors.ParentUserId = parentUserId;
            distributors.UserId = currentMember.UserId;
            XTrace.WriteLine("添加分销商时的上级分销商ID1：" + distributors.ParentUserId);

            //DistributorsInfo currentDistributors = GetCurrentDistributors();
            DistributorsInfo currentDistributors = GetDistributorInfo(parentUserId);

            if (currentDistributors != null)
            {
                if (!(string.IsNullOrEmpty(currentDistributors.ReferralPath) || currentDistributors.ReferralPath.Contains("|")))
                {
                    distributors.ReferralPath = currentDistributors.ReferralPath + "|" + currentDistributors.UserId.ToString();
                }
                else if (!(string.IsNullOrEmpty(currentDistributors.ReferralPath) || !currentDistributors.ReferralPath.Contains("|")))
                {
                    distributors.ReferralPath = currentDistributors.ReferralPath.Split(new char[] { '|' })[1] + "|" + currentDistributors.UserId.ToString();
                }
                else
                {
                    distributors.ReferralPath = currentDistributors.UserId.ToString();
                }
                //distributors.ParentUserId = new int?(currentDistributors.UserId);
                XTrace.WriteLine("添加分销商时的上级分销商ID2：" + distributors.ParentUserId);
                if (currentDistributors.DistributorGradeId == DistributorGrade.OneDistributor)
                {
                    distributors.DistributorGradeId = DistributorGrade.TowDistributor;
                }
                else if (currentDistributors.DistributorGradeId == DistributorGrade.TowDistributor)
                {
                    distributors.DistributorGradeId = DistributorGrade.ThreeDistributor;
                }
                else
                {
                    distributors.DistributorGradeId = DistributorGrade.ThreeDistributor;
                }
            }

            XTrace.WriteLine("添加分销商时的上级分销商ID3：" + distributors.ParentUserId);

            // 更新分销商的姓名和手机号码，在注册的时候 2015-10-13
            bool isUpdateUserInfoFlag = new MemberDao().UpdateUserNameAndPhone(currentMember.UserId.ToString(), distributors.ParentUserId.Value.ToString(), distributors.UserName, distributors.RealName, distributors.CellPhone);

            if (new DistributorsDao().CreateDistributor(distributors))
            {

                if (!siteSettings.SiteFlag.EqualIgnoreCase("ls"))
                {
                    // 注册成功后，添加所有的商品给这个分销商
                    DistributorsBrower.RegistDistributorAddProduct(distributors.UserId);
                }

                // 对当前用户增加虚拟币 2016-02-24再次修改
                bool pointFlag = false;

                if (distributors.IsTempStore == 1)
                {
                    pointFlag = new MemberDao().AddUserVirtualPoints(currentMember.UserId, siteSettings.TempStorePoint);
                    XTrace.WriteLine("分销商注册时的ID和默认Points(准):" + currentMember.UserId.ToString() + "---" + siteSettings.TempStorePoint.ToString() + "---" + pointFlag);
                }
                else
                {
                    pointFlag = new MemberDao().AddUserVirtualPoints(currentMember.UserId, siteSettings.DefaultVirtualPoint);
                    XTrace.WriteLine("分销商注册时的ID和默认Points(正):" + currentMember.UserId.ToString() + "---" + siteSettings.DefaultVirtualPoint.ToString() + "---" + pointFlag);
                }

                // 更新用户为店主标识
                new MemberDao().UpdateUserIsStore(currentMember.UserId, 1);

                string orderId = "";

                orderId = DistributorsBrower.GetOrderIdByUserIdAndProductId(distributors.UserId, siteSettings.DefaultProductId);

                // 更新订单状态(完成)
                DistributorsBrower.UpdateOrderStatus(orderId, 5, DateTime.Now);
                
                // 判断是否要处理收益或钻石会员订单不计算佣金
                if (siteSettings.IsProcessCommissions && distributors.IsTempStore != 1)
                {

                    #region 推荐收益                    

                    if (string.IsNullOrEmpty(orderId) || orderId.EqualIgnoreCase("推荐收入"))
                    {
                        XTrace.WriteLine("---------------------------------没有获取到订单号，暂时不处理，交由后台自动处理。---------------------------------");
                    }
                    else
                    {
                        // 判断订单上的邀请人是否跟邀请记录中的一致，不一致的情况下更新订单上的邀请人
                        
                        // 获取到了订单号才处理佣金数据
                        // 计算推荐收益
                        StringBuilder recommendSql = new StringBuilder();

                        DataTable distributorDatatable = DistributorsBrower.GetAllParentDistributorsByUserId(distributors.UserId);
                        if (null != distributorDatatable && distributorDatatable.Rows.Count > 0)
                        {
                            DistributorGradeInfo oneDistributorGrade = null;
                            DistributorsInfo currDistributor = null;
                            DistributorsInfo parentDistributor = null;
                            DistributorGradeInfo currDistributorGrade = null;
                            DistributorGradeInfo parentDistributorGrade = null;
                            DistributorsInfo twoParentDistributor = null;
                            DistributorsInfo twoTutorDistributor = null;
                            DistributorsInfo thirdTutorDistributor = null;
                            DistributorsInfo fourTutorDistributor = null;
                            // 是否存在二级合伙人
                            bool isExistTwoParent = false;
                            // 是否存在二级导师
                            bool isExistTwoTutor = false;
                            // 第一个合伙人扣减金额
                            decimal firstParentDeductionAmount = 0;

                            decimal oneRecommendedIncome = 0;

                            decimal currRecommendedIncome = 0;

                            int currUserId = 0;
                            int parentCurrUserId = 0;
                            int lvl = 0;
                            int oneParentUserId = 0;
                            int OrderFromStoreId = 0;
                            string rodNum = InviteBrowser.getRandomizer(6, false, false, true, false);
                            int storeGradeId = 0;
                            int.TryParse(siteSettings.DefaultStoreGradeId, out storeGradeId);
                            oneDistributorGrade = DistributorGradeBrower.GetDistributorGradeInfo(storeGradeId);
                            if (null != oneDistributorGrade)
                            {
                                oneRecommendedIncome = oneDistributorGrade.RecommendedIncome;
                            }

                            foreach (DataRow dr in distributorDatatable.Rows)
                            {
                                currUserId = int.Parse(dr["UserId"].ToString());
                                lvl = int.Parse(dr["LVL"].ToString());
                                parentCurrUserId = int.Parse(dr["ReferralUserId"].ToString());

                                if (lvl == 0)
                                {
                                    recommendSql.AppendLine("");
                                    recommendSql.AppendLine("---------------------------------计算推荐佣金开始------注册的分销商ID：" + currUserId + ";上级分销商ID：" + parentCurrUserId + ";---------------------------------");

                                    OrderFromStoreId = currUserId;

                                    // 标识直接上级分销商
                                    oneParentUserId = parentCurrUserId;

                                    // 获取直接上级分销商
                                    parentDistributor = DistributorsBrower.GetDistributorInfo(parentCurrUserId);
                                    if (null != parentDistributor)
                                    {
                                        // 获取直接上级分销商等级
                                        parentDistributorGrade = DistributorGradeBrower.GetDistributorGradeInfo(parentDistributor.DistriGradeId);
                                        if (null != parentDistributorGrade)
                                        {
                                            if (parentDistributorGrade.GradeId.ToString().EqualIgnoreCase(siteSettings.DefaultPartnerGradeId)
                                                || parentDistributorGrade.GradeId.ToString().EqualIgnoreCase(siteSettings.DefaultTutorGradeId))
                                            {
                                                currRecommendedIncome = oneRecommendedIncome;
                                            }
                                            else
                                            {
                                                currRecommendedIncome = parentDistributorGrade.RecommendedIncome;
                                            }

                                            if (currRecommendedIncome > 0)
                                            {
                                                DistributorsBrower.UpdateCalcRecommendedIncome(parentDistributor.UserId.ToString(), parentDistributor.ReferralUserId.ToString()
                                                    , OrderFromStoreId.ToString(), orderId, 0, currRecommendedIncome, 3, "推荐收入" + rodNum);
                                            }
                                            else
                                            {
                                                DistributorsBrower.UpdateCalcRecommendedIncomeLTZero(parentDistributor.UserId.ToString(), parentDistributor.ReferralUserId.ToString()
                                                    , OrderFromStoreId.ToString(), orderId, 0, currRecommendedIncome, 3, "推荐收入" + rodNum);
                                            }
                                            recommendSql.AppendLine("当前佣金计算：------当前提成者ID：" + parentDistributor.UserId + ";上级ID："
                                                + parentDistributor.ReferralUserId + "（" + parentDistributorGrade.Name + "）" + ";佣金金额：" + currRecommendedIncome
                                                + ";来自哪里：" + OrderFromStoreId + "------" + rodNum);

                                            // 发送推荐短信
                                            SendStatus sendStatus = SendStatus.Fail;
                                            string errMsg = "";
                                            string smsContent = "";
                                            MemberInfo member = MemberProcessor.GetMember(currUserId);
                                            if (null != member)
                                            {
                                                MemberInfo referralmember = MemberProcessor.GetMember(member.ReferralUserId);

                                                if (null != referralmember)
                                                {
                                                    // 发送短信验证码短信,并加入发送记录表
                                                    smsContent = siteSettings.DistributorRegSmsContent;
                                                    if (siteSettings.SiteFlag.EqualIgnoreCase("ls"))
                                                    {
                                                        smsContent = smsContent.Replace("$UserNameMobile$", member.RealName + member.CellPhone);
                                                    }
                                                    else
                                                    {
                                                        // 用户名和手机号码
                                                        smsContent = smsContent.Replace("$UserNameMobile$", member.RealName + member.CellPhone);
                                                        // 佣金
                                                        smsContent = smsContent.Replace("$CommissionMoney$", currRecommendedIncome.ToString("0.00"));
                                                    }
                                                    sendStatus = Messenger.SendSMS(referralmember.CellPhone, smsContent, siteSettings, out errMsg);
                                                }
                                            }

                                            currRecommendedIncome = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    if (oneParentUserId == currUserId)
                                    {
                                        // 是直接上级分销商，已处理
                                        continue;
                                    }
                                    else
                                    {
                                        currDistributor = DistributorsBrower.GetDistributorInfo(currUserId);
                                        if (null != currDistributor)
                                        {
                                            currDistributorGrade = DistributorGradeBrower.GetDistributorGradeInfo(currDistributor.DistriGradeId);
                                            if (null != currDistributorGrade)
                                            {
                                                if (currDistributorGrade.RecommendedIncome > 0)
                                                {
                                                    if (currDistributorGrade.GradeId.ToString().EqualIgnoreCase(siteSettings.DefaultTutorGradeId))
                                                    {
                                                        if (!isExistTwoTutor)
                                                        {
                                                            // 导师级别处理116(200)
                                                            currRecommendedIncome = currDistributorGrade.RecommendedIncome;
                                                            if (currRecommendedIncome > 0)
                                                            {
                                                                DistributorsBrower.UpdateCalcRecommendedIncome(currDistributor.UserId.ToString(), currDistributor.ReferralUserId.ToString()
                                                                    , OrderFromStoreId.ToString(), orderId, 0, currRecommendedIncome, 3, "推荐收入" + rodNum);
                                                            }
                                                            else
                                                            {
                                                                DistributorsBrower.UpdateCalcRecommendedIncomeLTZero(currDistributor.UserId.ToString(), currDistributor.ReferralUserId.ToString()
                                                                    , OrderFromStoreId.ToString(), orderId, 0, currRecommendedIncome, 3, "推荐收入" + rodNum);
                                                            }
                                                            recommendSql.AppendLine("当前佣金计算：------当前提成者ID：" + currDistributor.UserId + ";上级ID："
                                                                + currDistributor.ReferralUserId + "（" + currDistributorGrade.Name + "）" + ";佣金金额：" + currRecommendedIncome
                                                                + ";来自哪里：" + OrderFromStoreId + "------" + rodNum);

                                                            currRecommendedIncome = 0;

                                                            // 导师存在上级导师
                                                            isExistTwoTutor = true;

                                                            // 查找导师是否上面还有导师115
                                                            twoTutorDistributor = DistributorsBrower.GetDistributorInfo(currDistributor.ReferralUserId);
                                                            if (null != twoTutorDistributor && twoTutorDistributor.DistriGradeId.ToString().EqualIgnoreCase(siteSettings.DefaultTutorGradeId))
                                                            {
                                                                // 115(80)
                                                                currRecommendedIncome = siteSettings.DefaultCompanyIncomeEight;
                                                                if (currRecommendedIncome > 0)
                                                                {
                                                                    DistributorsBrower.UpdateCalcRecommendedIncome(twoTutorDistributor.UserId.ToString(), twoTutorDistributor.ReferralUserId.ToString()
                                                                        , OrderFromStoreId.ToString(), orderId, 0, currRecommendedIncome, 3, "推荐收入" + rodNum);
                                                                }
                                                                else
                                                                {
                                                                    DistributorsBrower.UpdateCalcRecommendedIncomeLTZero(twoTutorDistributor.UserId.ToString(), twoTutorDistributor.ReferralUserId.ToString()
                                                                        , OrderFromStoreId.ToString(), orderId, 0, currRecommendedIncome, 3, "推荐收入" + rodNum);
                                                                }
                                                                recommendSql.AppendLine("当前佣金计算：------当前提成者ID：" + twoTutorDistributor.UserId + ";上级ID："
                                                                    + twoTutorDistributor.ReferralUserId + "（" + twoTutorDistributor.DistriGradeId + "）" + ";佣金金额：" + currRecommendedIncome
                                                                    + ";来自哪里：" + OrderFromStoreId + "------" + rodNum);

                                                                currRecommendedIncome = 0;

                                                                // 114
                                                                thirdTutorDistributor = DistributorsBrower.GetDistributorInfo(twoTutorDistributor.ReferralUserId);
                                                                if (null != thirdTutorDistributor)
                                                                {
                                                                    if (thirdTutorDistributor.DistriGradeId.ToString().EqualIgnoreCase(siteSettings.DefaultPartnerGradeId))
                                                                    {
                                                                        // 扣减导师的直接上级合伙人对应的金额
                                                                        firstParentDeductionAmount = siteSettings.DefaultCompanyIncomeEight;
                                                                    }
                                                                    else
                                                                    {
                                                                        // 113
                                                                        fourTutorDistributor = DistributorsBrower.GetDistributorInfo(thirdTutorDistributor.ReferralUserId);
                                                                        if (null != fourTutorDistributor && fourTutorDistributor.DistriGradeId.ToString().EqualIgnoreCase(siteSettings.DefaultPartnerGradeId))
                                                                        {
                                                                            // 扣减导师的直接上级合伙人对应的金额
                                                                            firstParentDeductionAmount = siteSettings.DefaultCompanyIncomeEight;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else if (currDistributorGrade.GradeId.ToString().EqualIgnoreCase(siteSettings.DefaultPartnerGradeId))
                                                    {
                                                        // 合伙人级别处理
                                                        if (!isExistTwoParent)
                                                        {
                                                            currRecommendedIncome = currDistributorGrade.RecommendedIncome - firstParentDeductionAmount;
                                                            if (currRecommendedIncome > 0)
                                                            {
                                                                DistributorsBrower.UpdateCalcRecommendedIncome(currDistributor.UserId.ToString(), currDistributor.ReferralUserId.ToString()
                                                                    , OrderFromStoreId.ToString(), orderId, 0, currRecommendedIncome, 3, "推荐收入" + rodNum);
                                                            }
                                                            else
                                                            {
                                                                DistributorsBrower.UpdateCalcRecommendedIncomeLTZero(currDistributor.UserId.ToString(), currDistributor.ReferralUserId.ToString()
                                                                    , OrderFromStoreId.ToString(), orderId, 0, currRecommendedIncome, 3, "推荐收入" + rodNum);
                                                            }
                                                            recommendSql.AppendLine("当前佣金计算：------当前提成者ID：" + currDistributor.UserId + ";上级ID："
                                                                + currDistributor.ReferralUserId + "（" + currDistributorGrade.Name + "）" + ";佣金金额：" + currRecommendedIncome + ";来自哪里："
                                                                + OrderFromStoreId + "------" + rodNum);

                                                            currRecommendedIncome = 0;

                                                            // 合伙人存在上级合伙人
                                                            isExistTwoParent = true;

                                                            // 查找合伙人是否上面还有合伙人
                                                            twoParentDistributor = DistributorsBrower.GetDistributorInfo(currDistributor.ReferralUserId);
                                                            if (null != twoParentDistributor && twoParentDistributor.DistriGradeId.ToString().EqualIgnoreCase(siteSettings.DefaultPartnerGradeId))
                                                            {
                                                                currRecommendedIncome = siteSettings.DefaultCompanyRecommendedIncome;

                                                                if (currRecommendedIncome > 0)
                                                                {
                                                                    DistributorsBrower.UpdateCalcRecommendedIncome(twoParentDistributor.UserId.ToString(), twoParentDistributor.ReferralUserId.ToString(), OrderFromStoreId.ToString()
                                                                    , orderId, 0, currRecommendedIncome, 3, "推荐收入" + rodNum);
                                                                }
                                                                else
                                                                {
                                                                    DistributorsBrower.UpdateCalcRecommendedIncomeLTZero(twoParentDistributor.UserId.ToString(), twoParentDistributor.ReferralUserId.ToString(), OrderFromStoreId.ToString()
                                                                    , orderId, 0, currRecommendedIncome, 3, "推荐收入" + rodNum);
                                                                }
                                                                recommendSql.AppendLine("当前佣金计算：------当前提成者ID：" + twoParentDistributor.UserId + ";上级ID："
                                                                    + twoParentDistributor.ReferralUserId + "（" + twoParentDistributor.GradeId + "）" + ";佣金金额："
                                                                    + currRecommendedIncome + ";来自哪里：" + OrderFromStoreId + "------" + rodNum);

                                                                currRecommendedIncome = 0;
                                                            }

                                                            firstParentDeductionAmount = 0;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            recommendSql.AppendLine("---------------------------------计算推荐佣金结束------注册的分销商ID：" + OrderFromStoreId + ";---------------------------------");
                            // 调试：将推荐佣金计算过程输出到文本文件中
                            XTrace.WriteLine(recommendSql.ToString());
                        }
                    }

                    #endregion

                }

                XTrace.WriteLine("-----------------------------------开店流程处理结束......-----------------------------------");

                return true;
            }

            XTrace.WriteLine("-----------------------------------开店流程处理结束......-----------------------------------");

            return false;
        }

        public static void DeleteDistributorProductIds(List<int> productList)
        {
            int userId = GetCurrentDistributors().UserId;
            if ((userId > 0) && (productList.Count > 0))
            {
                new DistributorsDao().RemoveDistributorProducts(productList, userId);
            }
        }

        public static bool FrozenCommision(int userid, string ReferralStatus)
        {
            return new DistributorsDao().FrozenCommision(userid, ReferralStatus);
        }

        public static DbQueryResult GetBalanceDrawRequest(BalanceDrawRequestQuery query)
        {
            return new DistributorsDao().GetBalanceDrawRequest(query);
        }

        public static DbQueryResult GetDistributorIncome(DistributorIncomeQuery query)
        {
            return new DistributorsDao().GetDistributorIncome(query);
        }

        public static DataTable GetDistributorIncomeByExport(DistributorIncomeQuery query)
        {
            return new DistributorsDao().GetDistributorIncomeByExport(query);
        }

        public static DataTable GetBalanceDrawRequestByExport(string storeName, string requestStartTime, string requestEndTime)
        {
            return new DistributorsDao().GetBalanceDrawRequestByExport(storeName, requestStartTime, requestEndTime);
        }

        public static DataTable GetBalanceDrawRequestByBatch(string serialIDList)
        {
            return new DistributorsDao().GetBalanceDrawRequestByBatch(serialIDList);
        }

        public static DataTable GetCommissionByExport(string storeName, string oneUserName, string oneStoreName, string orderId, string requestStartTime, string requestEndTime)
        {
            return new DistributorsDao().GetCommissionByExport(storeName, oneUserName, oneStoreName, orderId, requestStartTime, requestEndTime);
        }

        public static bool GetBalanceDrawRequestIsCheck(int serialid)
        {
            return new DistributorsDao().GetBalanceDrawRequestIsCheck(serialid);
        }

        public static DbQueryResult GetCommissions(CommissionsQuery query)
        {
            return new DistributorsDao().GetCommissions(query);
        }

        public static DistributorsInfo GetCurrentDistributors()
        {
            return GetCurrentDistributors(Globals.GetCurrentDistributorId());
        }

        public static DistributorsInfo GetCurrentDistributors(int userId)
        {
            DistributorsInfo distributorInfo = HiCache.Get(string.Format("DataCache-Distributor-{0}", userId)) as DistributorsInfo;
            if ((distributorInfo == null) || (distributorInfo.UserId == 0))
            {
                distributorInfo = new DistributorsDao().GetDistributorInfo(userId);
                HiCache.Insert(string.Format("DataCache-Distributor-{0}", userId), distributorInfo, 360, CacheItemPriority.Normal);
            }
            else
            {
                DistributorsInfo checkDistributorInfo = new DistributorsDao().GetDistributorInfo(distributorInfo.UserId);
                if (null != checkDistributorInfo)
                {
                    distributorInfo = checkDistributorInfo;
                    HiCache.Insert(string.Format("DataCache-Distributor-{0}", checkDistributorInfo.UserId), checkDistributorInfo, 360, CacheItemPriority.Normal);
                }
            }
            return distributorInfo;
        }

        public static MemberInfo GetReferralMember(int userId)
        {
            return new MemberDao().GetMember(userId);
        }

        public static DataTable GetCurrentDistributorsCommosion()
        {
            return new DistributorsDao().GetDistributorsCommosion(Globals.GetCurrentDistributorId());
        }

        public static DataTable GetCurrentDistributorsCommosion(int userId)
        {
            return new DistributorsDao().GetCurrentDistributorsCommosion(userId);
        }

        public static int GetDistributorGrades(string ReferralUserId)
        {
            DistributorsInfo userIdDistributors = GetUserIdDistributors(int.Parse(ReferralUserId));
            List<DistributorGradeInfo> distributorGrades = new DistributorsDao().GetDistributorGrades() as List<DistributorGradeInfo>;
            foreach (DistributorGradeInfo info2 in from item in distributorGrades
                                                   orderby item.CommissionsLimit descending
                                                   select item)
            {
                if (userIdDistributors.DistriGradeId == info2.GradeId)
                {
                    return 0;
                }
                if (info2.CommissionsLimit <= (userIdDistributors.ReferralBlance + userIdDistributors.ReferralRequestBalance))
                {
                    userIdDistributors.DistriGradeId = info2.GradeId;
                    return info2.GradeId;
                }
            }
            return 0;
        }

        public static DistributorsInfo GetDistributorInfo(int distributorid)
        {
            return new DistributorsDao().GetDistributorInfo(distributorid);
        }

        public static int GetDistributorNum(DistributorGrade grade)
        {
            return new DistributorsDao().GetDistributorNum(grade);
        }

        public static DataSet GetDistributorOrder(OrderQuery query)
        {
            return new OrderDao().GetDistributorOrder(query);
        }

        public static int GetDistributorOrderCount(OrderQuery query)
        {
            return new OrderDao().GetDistributorOrderCount(query);
        }

        public static DbQueryResult GetDistributors(DistributorsQuery query)
        {
            return new DistributorsDao().GetDistributors(query);
        }

        public static DataTable GetDistributorsCommission(DistributorsQuery query)
        {
            return new DistributorsDao().GetDistributorsCommission(query);
        }

        public static DataTable GetDistributorsCommosion(int userId, DistributorGrade distributorgrade)
        {
            return new DistributorsDao().GetDistributorsCommosion(userId, distributorgrade);
        }

        public static int GetDownDistributorNum(string userid)
        {
            return new DistributorsDao().GetDownDistributorNum(userid);
        }

        public static DataTable GetDownDistributors(DistributorsQuery query)
        {
            return new DistributorsDao().GetDownDistributors(query);
        }

        public static DataTable GetDownDistributorsDetails(DistributorsQuery query)
        {
            return new DistributorsDao().GetDownDistributorsDetails(query);
        }

        /// <summary>
        /// 获取分销商的所有上级分销商
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable GetAllParentDistributorsByUserId(int userId)
        {
            return new DistributorsDao().GetAllParentDistributorsByUserId(userId);
        }

        /// <summary>
        /// 根据用户ID获取下级人员总数及佣金总额
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="DefaultPartnerGradeId"></param>
        /// <param name="DefaultTutorGradeId"></param>
        /// <param name="DefaultStoreGradeId"></param>
        /// <returns></returns>
        public static DataTable GetDistributorAllBalanceByUserId(int userId, string DefaultPartnerGradeId, string DefaultTutorGradeId, string DefaultStoreGradeId, string searchGradeId)
        {
            return new DistributorsDao().GetDistributorAllBalanceByUserId(userId, DefaultPartnerGradeId, DefaultTutorGradeId, DefaultStoreGradeId, searchGradeId);
        }

        /// <summary>
        /// 根据用户ID获取下级人员总数及佣金总额
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="DefaultPartnerGradeId"></param>
        /// <param name="DefaultTutorGradeId"></param>
        /// <param name="DefaultStoreGradeId"></param>
        /// <returns></returns>
        public static DataTable GetDistributorDetailBalanceByUserId(int userId, string DefaultPartnerGradeId, string DefaultTutorGradeId, string DefaultStoreGradeId, string searchGradeId)
        {
            return new DistributorsDao().GetDistributorDetailBalanceByUserId(userId, DefaultPartnerGradeId, DefaultTutorGradeId, DefaultStoreGradeId, searchGradeId);
        }

        /// <summary>
        /// 获取我的团队中的销售数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable GetDistributorTeamData(DistributorsQuery query)
        {
            return new DistributorsDao().GetDistributorTeamData(query);
        }

        public static int GetNotDescDistributorGrades(string ReferralUserId)
        {
            DistributorsInfo userIdDistributors = GetUserIdDistributors(int.Parse(ReferralUserId));
            decimal num2 = userIdDistributors.ReferralBlance + userIdDistributors.ReferralRequestBalance;
            DistributorGradeInfo distributorGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(userIdDistributors.DistriGradeId);
            if ((distributorGradeInfo != null) && (num2 < distributorGradeInfo.CommissionsLimit)) return userIdDistributors.DistriGradeId;
            List<DistributorGradeInfo> distributorGrades = new DistributorsDao().GetDistributorGrades() as List<DistributorGradeInfo>;
            foreach (DistributorGradeInfo info3 in distributorGrades.OrderByDescending(n => n.CommissionsLimit))
            {
                if (userIdDistributors.DistriGradeId == info3.GradeId) return 0;
                if (info3.CommissionsLimit <= (userIdDistributors.ReferralBlance + userIdDistributors.ReferralRequestBalance)) return info3.GradeId;
            }
            return 0;
        }

        public static DataTable GetNotSendRedpackRecord(int balancedrawrequestid)
        {
            return new SendRedpackRecordDao().GetNotSendRedpackRecord(balancedrawrequestid);
        }

        public static int GetRedPackTotalAmount(int balancedrawrequestid, int userid)
        {
            return new SendRedpackRecordDao().GetRedPackTotalAmount(balancedrawrequestid, userid);
        }

        public static SendRedpackRecordInfo GetSendRedpackRecordByID(int id)
        {
            return new SendRedpackRecordDao().GetSendRedpackRecordByID(id);
        }

        public static DbQueryResult GetSendRedpackRecordRequest(SendRedpackRecordQuery query)
        {
            return new SendRedpackRecordDao().GetSendRedpackRecordRequest(query);
        }

        public static decimal GetUserCommissions(int userid, DateTime fromdatetime)
        {
            return new DistributorsDao().GetUserCommissions(userid, fromdatetime);
        }

        public static decimal GetUserCommissionsByCond(int userid, int commorderstatus, DateTime fromdatetime, bool isByMonth = false)
        {
            return new DistributorsDao().GetUserCommissionsByCond(userid, commorderstatus, fromdatetime, isByMonth);
        }

        public static decimal GetUserBalanceDrawRequesByCond(int userid, int checkstatus, DateTime fromdatetime, bool isByMonth = false)
        {
            return new DistributorsDao().GetUserBalanceDrawRequesByCond(userid, checkstatus, fromdatetime, isByMonth);
        }

        public static DistributorsInfo GetUserIdDistributors(int userid)
        {
            return new DistributorsDao().GetDistributorInfo(userid);
        }

        public static DataSet GetUserRanking(int userid)
        {
            return new DistributorsDao().GetUserRanking(userid);
        }

        public static bool HasDrawRequest(int serialid)
        {
            return new SendRedpackRecordDao().HasDrawRequest(serialid);
        }

        private static int IsExiteDistributorsByStoreName(string stroname)
        {
            return new DistributorsDao().IsExiteDistributorsByStoreName(stroname);
        }

        public static bool IsExitsCommionsRequest()
        {
            return new DistributorsDao().IsExitsCommionsRequest(Globals.GetCurrentDistributorId());
        }

        public static bool CheckOrderIsPay(int userId)
        {
            return new DistributorsDao().CheckOrderIsPay(userId);
        }

        public static DataTable OrderIDGetCommosion(string orderid)
        {
            return new DistributorsDao().OrderIDGetCommosion(orderid);
        }

        public static void RemoveDistributorCache(int userId)
        {
            HiCache.Remove(string.Format("DataCache-Distributor-{0}", userId));
        }

        public static string SendRedPackToBalanceDrawRequest(int serialid)
        {
            return new DistributorsDao().SendRedPackToBalanceDrawRequest(serialid);
        }

        ///// <summary>
        ///// 付款后计算提成（账户余额）
        ///// </summary>
        ///// <param name="order"></param>
        ///// <returns></returns>
        //public static bool CalcCommissionByBuy(OrderInfo order)
        //{
        //    bool flag = true;

        //    // 如果订单中有分销商默认的推广商品时，不计算佣金
        //    if (DistributorsBrower.CheckIsDistributoBuyProduct(order.OrderId))
        //    {
        //        return false;
        //    }

        //    XTrace.WriteLine("付款后订单时的处理");

        //    decimal firstCommission = 0;
        //    decimal secondCommission = 0;
        //    decimal thirdCommission = 0;

        //    bool firstFlag = false;
        //    bool secondFlag = false;
        //    bool thirdFlag = false;

        //    // 计算订单商品利润
        //    //decimal orderProfit = order.LineItems.Values.Sum(n => (n.ItemListPrice * n.Quantity) - (n.ItemCostPrice * n.Quantity));
        //    decimal orderProfit = order.GetTotal() - order.LineItems.Values.Sum(n => (n.ItemCostPrice * n.Quantity));
        //    // 计算订单销售总金额
        //    //decimal orderAmount = order.LineItems.Values.Sum(n => (n.ItemListPrice * n.Quantity));
        //    decimal orderAmount = order.GetTotal();
        //    // 判断当前订单是否已计算提成
        //    //bool isCalcCommissionFlag = new DistributorsDao().IsSetCalculationCommission(order.OrderId);

        //    XTrace.WriteLine("订单利润：" + orderProfit + " 订单金额：" + orderAmount);

        //    // 订单没有计算提成且订单利润大于0
        //    if (orderProfit > 0)
        //    {
        //        XTrace.WriteLine("开始计算佣金");
        //        // 根据订单ID获取直接销售的店铺信息(ReferralUserId)
        //        DistributorsInfo firstDistributorInfo = DistributorsBrower.GetDistributorInfo(order.ReferralUserId);
        //        DistributorsInfo secondDistributorInfo = null;
        //        DistributorsInfo thirdDistributorInfo = null;

        //        // 根据分销商的等级ID获取对应的分销商等级数据
        //        DistributorGradeInfo firstGradeInfo = null;
        //        DistributorGradeInfo secondGradeInfo = null;
        //        DistributorGradeInfo thirdGradeInfo = null;

        //        // 根据直接销售店铺获取上级分销商(我的上级）
        //        if (null != firstDistributorInfo)
        //        {
        //            // 获取直接销售商的等级
        //            firstGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(firstDistributorInfo.DistriGradeId);
        //            // 获取上级分销商
        //            secondDistributorInfo = DistributorsBrower.GetDistributorInfo(firstDistributorInfo.ReferralUserId);
        //            // 计算直接销售佣金提成
        //            firstCommission = orderProfit * (firstGradeInfo.FirstCommissionRise / 100);
        //            // 更新佣金提成到提成表
        //            firstFlag = new DistributorsDao().UpdateCalculationCommissionByBuy(firstDistributorInfo.UserId.ToString(), firstDistributorInfo.ReferralUserId.ToString(), order.OrderId, orderAmount, firstCommission
        //                , string.Format("{0} * ( {1} / 100 )", orderProfit.ToString("f2"), firstGradeInfo.FirstCommissionRise));
        //            XTrace.WriteLine("计算直接销售佣金" + firstFlag);
        //        }
        //        // 根据直接销售店铺的上级分销商获取上上级分销商（我的上上级）
        //        if (null != secondDistributorInfo)
        //        {
        //            // 获取直接销售店铺上级分销商的等级
        //            secondGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(secondDistributorInfo.DistriGradeId);
        //            // 获取上上级分销商
        //            thirdDistributorInfo = DistributorsBrower.GetDistributorInfo(secondDistributorInfo.ReferralUserId);
        //            // 计算上级销售佣金提成
        //            secondCommission = orderProfit * (secondGradeInfo.SecondCommissionRise / 100);
        //            // 更新佣金提成到提成表
        //            secondFlag = new DistributorsDao().UpdateCalculationCommissionByBuy(secondDistributorInfo.UserId.ToString(), secondDistributorInfo.ReferralUserId.ToString(), order.OrderId, orderAmount, secondCommission
        //                , string.Format("{0} * ( {1} / 100 )", orderProfit.ToString("f2"), secondGradeInfo.SecondCommissionRise));
        //            XTrace.WriteLine("计算我的上级销售佣金" + secondFlag);
        //        }
        //        if (null != thirdDistributorInfo)
        //        {
        //            // 获取直接销售店铺上上级分销商的等级
        //            thirdGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(thirdDistributorInfo.DistriGradeId);
        //            // 计算上上级销售佣金提成
        //            thirdCommission = orderProfit * (thirdGradeInfo.ThirdCommissionRise / 100);
        //            // 更新佣金提成到提成表
        //            thirdFlag = new DistributorsDao().UpdateCalculationCommissionByBuy(thirdDistributorInfo.UserId.ToString(), thirdDistributorInfo.ReferralUserId.ToString(), order.OrderId, orderAmount, thirdCommission
        //                , string.Format("{0} * ( {1} / 100 )", orderProfit.ToString("f2"), thirdGradeInfo.ThirdCommissionRise));
        //            XTrace.WriteLine("计算我的上上级销售佣金" + thirdFlag);
        //        }

        //        // 计算额外提成
        //        DataTable distributorDatatable = DistributorsBrower.GetAllParentDistributorsByUserId(order.ReferralUserId);
        //        if (null != distributorDatatable && distributorDatatable.Rows.Count > 0)
        //        {
        //            DistributorsInfo currDistributor = null;
        //            DistributorGradeInfo currDistributorGrade = null;
        //            int currUserId = 0;
        //            decimal currCommission = 0;

        //            foreach (DataRow dr in distributorDatatable.Rows)
        //            {
        //                currUserId = int.Parse(dr["UserId"].ToString());
        //                // 获取分销商
        //                currDistributor = DistributorsBrower.GetDistributorInfo(currUserId);
        //                if (null != currDistributor)
        //                {
        //                    // 获取分销商对应的等级
        //                    currDistributorGrade = DistributorGradeBrower.GetDistributorGradeInfo(currDistributor.DistriGradeId);
        //                    if (null != currDistributorGrade)
        //                    {
        //                        currCommission = orderProfit * (currDistributorGrade.AdditionalFees / 100);
        //                        if (currCommission > 0)
        //                        {
        //                            flag = new DistributorsDao().UpdateCalculationCommissionByBuy(currUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, currCommission
        //                                                                                          , string.Format("{0} * ( {1} / 100 )", orderProfit.ToString("f2"), currDistributorGrade.AdditionalFees));
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return flag;
        //}

        /// <summary>
        /// 计算推荐收益
        /// </summary>
        /// <returns></returns>
        public static bool UpdateCalcRecommendedIncome(string UserId, string ReferralUserId, string OrderFromStoreId, string OrderId, decimal OrderTotal, decimal resultCommTatal, int commType, string remark = "")
        {
            return new DistributorsDao().UpdateCalcRecommendedIncome(UserId, ReferralUserId, OrderFromStoreId, OrderId, OrderTotal, resultCommTatal, commType, remark);
        }

        public static bool UpdateCalcRecommendedIncomeLTZero(string UserId, string ReferralUserId, string OrderFromStoreId, string OrderId, decimal OrderTotal, decimal resultCommTatal, int commType, string remark = "")
        {
            return new DistributorsDao().UpdateCalcRecommendedIncomeLTZero(UserId, ReferralUserId, OrderFromStoreId, OrderId, OrderTotal, resultCommTatal, commType, remark);
        }

        public static bool CalcCommissionNew(OrderInfo order)
        {
            bool flag = true;

            decimal firstCommission = 0;
            decimal secondCommission = 0;
            decimal thirdCommission = 0;
            decimal firstItemCommission = 0;
            decimal secondItemCommission = 0;
            decimal thirdItemCommission = 0;

            bool firstFlag = false;
            bool secondFlag = false;
            bool thirdFlag = false;

            int firstUserId = 0;
            int secondUserId = 0;
            int thirdUserId = 0;

            // 计算订单商品利润
            decimal orderProfit = order.OrderTotal - order.LineItems.Values.Sum(n => (n.ItemCostPrice * n.Quantity));
            // 计算订单销售总金额
            decimal orderAmount = order.OrderTotal;
            // 判断当前订单是否已计算提成
            bool isCalcCommissionFlag = new DistributorsDao().IsSetCalculationCommission(order.OrderId);

            StringBuilder saleSql = new StringBuilder();

            saleSql.AppendLine("");
            saleSql.AppendLine("---------------------------------计算销售佣金开始------购买者ID：" + order.UserId + ";来自店铺ID：" + order.ReferralUserId + ";订单ID：" + order.OrderId + ";---------------------------------");

            // 订单没有计算提成且订单利润大于0
            if (!isCalcCommissionFlag && orderProfit > 0)
            {
                // 根据订单ID获取直接销售的店铺信息(ReferralUserId)
                DistributorsInfo firstDistributorInfo = DistributorsBrower.GetDistributorInfo(order.ReferralUserId);
                DistributorsInfo secondDistributorInfo = null;
                DistributorsInfo thirdDistributorInfo = null;

                // 根据分销商的等级ID获取对应的分销商等级数据
                DistributorGradeInfo firstGradeInfo = null;
                DistributorGradeInfo secondGradeInfo = null;
                DistributorGradeInfo thirdGradeInfo = null;

                SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);


                #region "计算三级分销商的佣金"

                // 根据直接销售店铺获取上级分销商(我的上级）
                if (null != firstDistributorInfo)
                {
                    // 获取直接分销商的ID
                    firstUserId = firstDistributorInfo.UserId;
                    // 获取直接销售商的等级
                    firstGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(firstDistributorInfo.DistriGradeId);
                    // 获取上级分销商
                    secondDistributorInfo = DistributorsBrower.GetDistributorInfo(firstDistributorInfo.ReferralUserId);
                    // 计算直接销售佣金提成
                    firstCommission = orderProfit * (firstGradeInfo.FirstCommissionRise / 100);
                    // 更新佣金提成到提成表
                    firstFlag = new DistributorsDao().UpdateCalculationCommission(firstDistributorInfo.UserId.ToString(), firstDistributorInfo.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, firstCommission, 1
                        , string.Format("{0} * ( {1} / 100 )", orderProfit.ToString("f2"), firstGradeInfo.FirstCommissionRise));

                    saleSql.AppendLine("当前佣金计算(直接佣金)：------当前提成者ID：" + firstDistributorInfo.UserId + ";上级ID：" + firstDistributorInfo.ReferralUserId + ";佣金金额：" + firstCommission + ";来自哪里：" + order.ReferralUserId + "------" + firstGradeInfo.FirstCommissionRise);

                }
                // 根据直接销售店铺的上级分销商获取上上级分销商（我的上上级）
                if (null != secondDistributorInfo)
                {
                    // 获取上级分销商的ID
                    secondUserId = secondDistributorInfo.UserId;
                    // 获取直接销售店铺上级分销商的等级
                    secondGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(secondDistributorInfo.DistriGradeId);
                    // 获取上上级分销商
                    thirdDistributorInfo = DistributorsBrower.GetDistributorInfo(secondDistributorInfo.ReferralUserId);
                    // 计算上级销售佣金提成
                    secondCommission = orderProfit * (secondGradeInfo.SecondCommissionRise / 100);
                    // 更新佣金提成到提成表
                    secondFlag = new DistributorsDao().UpdateCalculationCommissionNoUpdateOrderTotal(secondDistributorInfo.UserId.ToString(), secondDistributorInfo.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, secondCommission, 1
                        , string.Format("{0} * ( {1} / 100 )", orderProfit.ToString("f2"), secondGradeInfo.SecondCommissionRise));

                    saleSql.AppendLine("当前佣金计算(上级佣金)：------当前提成者ID：" + secondDistributorInfo.UserId + ";上级ID：" + secondDistributorInfo.ReferralUserId + ";佣金金额：" + secondCommission + ";来自哪里：" + order.ReferralUserId + "------" + secondGradeInfo.SecondCommissionRise);

                }
                if (null != thirdDistributorInfo)
                {
                    // 获取上上级分销商的ID
                    thirdUserId = thirdDistributorInfo.UserId;
                    // 获取直接销售店铺上上级分销商的等级
                    thirdGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(thirdDistributorInfo.DistriGradeId);
                    // 计算上上级销售佣金提成
                    thirdCommission = orderProfit * (thirdGradeInfo.ThirdCommissionRise / 100);
                    // 更新佣金提成到提成表
                    thirdFlag = new DistributorsDao().UpdateCalculationCommissionNoUpdateOrderTotal(thirdDistributorInfo.UserId.ToString(), thirdDistributorInfo.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, thirdCommission, 1
                        , string.Format("{0} * ( {1} / 100 )", orderProfit.ToString("f2"), thirdGradeInfo.ThirdCommissionRise));

                    saleSql.AppendLine("当前佣金计算(顶层佣金)：------当前提成者ID：" + thirdDistributorInfo.UserId + ";上级ID：" + thirdDistributorInfo.ReferralUserId + ";佣金金额：" + thirdCommission + ";来自哪里：" + order.ReferralUserId + "------" + thirdGradeInfo.ThirdCommissionRise);

                }

                #endregion

                #region "计算订单明细上的三级分销商佣金"

                OrderDao orderDao = new OrderDao();
                LineItemDao lineItemDao = new LineItemDao();

                // 获取订单的商品信息
                foreach (LineItemInfo lineItemInfo in order.LineItems.Values)
                {
                    firstItemCommission = 0;
                    secondItemCommission = 0;
                    thirdItemCommission = 0;

                    if (null != firstGradeInfo)
                    {
                        // 计算直接销售佣金提成(((销售价－成本价) * 数量 ) * 提成比率)
                        firstItemCommission = ((lineItemInfo.ItemListPrice - lineItemInfo.ItemCostPrice) * lineItemInfo.Quantity) * (firstGradeInfo.FirstCommissionRise / 100);
                    }
                    if (null != secondGradeInfo)
                    {
                        // 计算上级销售佣金提成(((销售价－成本价) * 数量 ) * 提成比率)
                        secondItemCommission = ((lineItemInfo.ItemListPrice - lineItemInfo.ItemCostPrice) * lineItemInfo.Quantity) * (secondGradeInfo.SecondCommissionRise / 100);
                    }
                    if (null != thirdGradeInfo)
                    {
                        // 计算上上级销售佣金提成(((销售价－成本价) * 数量 ) * 提成比率)
                        thirdItemCommission = ((lineItemInfo.ItemListPrice - lineItemInfo.ItemCostPrice) * lineItemInfo.Quantity) * (thirdGradeInfo.ThirdCommissionRise / 100);
                    }
                    // 更新订单对应商品上的直接销售佣金、上级销售佣金、上上级销售佣金
                    lineItemDao.UpdateLineItemCommission(order.OrderId, lineItemInfo.ProductId, lineItemInfo.SkuId, firstItemCommission, secondItemCommission, thirdItemCommission, firstUserId, secondUserId, thirdUserId);
                }

                #endregion

                #region "计算分销商的额外提成"

                // 计算额外提成
                decimal managerCommission = 0;
                DataTable distributorDatatable = DistributorsBrower.GetAllParentDistributorsByUserId(order.ReferralUserId);
                if (null != distributorDatatable && distributorDatatable.Rows.Count > 0)
                {
                    DistributorsInfo currDistributor = null;
                    DistributorGradeInfo currDistributorGrade = null;

                    // 是否存在二级合伙人
                    bool isExistTwoParent = false;
                    // 是否存在二级导师
                    bool isExistTwoTutor = false;

                    int currUserId = 0;
                    decimal currCommission = 0;

                    foreach (DataRow dr in distributorDatatable.Rows)
                    {
                        currUserId = int.Parse(dr["UserId"].ToString());
                        // 获取分销商
                        currDistributor = DistributorsBrower.GetDistributorInfo(currUserId);
                        if (null != currDistributor)
                        {
                            // 获取分销商对应的等级
                            currDistributorGrade = DistributorGradeBrower.GetDistributorGradeInfo(currDistributor.DistriGradeId);

                            if (null != currDistributorGrade)
                            {
                                if (currDistributorGrade.GradeId.ToString().EqualIgnoreCase(siteSettings.DefaultTutorGradeId))
                                {
                                    // 导师的情况

                                    if (!isExistTwoTutor)
                                    {
                                        // 处理最近的导师额外提成收入
                                        isExistTwoTutor = true;

                                        currCommission = orderProfit * (currDistributorGrade.AdditionalFees / 100);

                                        managerCommission += currCommission;

                                        flag = new DistributorsDao().UpdateCalculationCommissionNoUpdateOrderTotal(currUserId.ToString(), order.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, currCommission, 2
                                                                                             , string.Format("{0} * ( {1} / 100 ) 管理收入", orderProfit.ToString("f2"), currDistributorGrade.AdditionalFees));
                                        saleSql.AppendLine("当前佣金计算(管理佣金)：------当前提成者ID：" + currUserId + ";上级ID：" + order.ReferralUserId + ";佣金金额：" + currCommission + ";来自哪里：" + order.ReferralUserId + "------" + currDistributorGrade.AdditionalFees);
                                    }
                                }
                                else if (currDistributorGrade.GradeId.ToString().EqualIgnoreCase(siteSettings.DefaultPartnerGradeId))
                                {
                                    // 合伙人的情况
                                    if (!isExistTwoParent)
                                    {
                                        // 处理最近的合伙人额外提成收入
                                        isExistTwoParent = true;

                                        currCommission = orderProfit * (currDistributorGrade.AdditionalFees / 100);

                                        managerCommission += currCommission;

                                        flag = new DistributorsDao().UpdateCalculationCommissionNoUpdateOrderTotal(currUserId.ToString(), order.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, currCommission, 2
                                                                                             , string.Format("{0} * ( {1} / 100 ) 管理收入", orderProfit.ToString("f2"), currDistributorGrade.AdditionalFees));
                                        saleSql.AppendLine("当前佣金计算(管理佣金)：------当前提成者ID：" + currUserId + ";上级ID：" + order.ReferralUserId + ";佣金金额：" + currCommission + ";来自哪里：" + order.ReferralUserId + "------" + currDistributorGrade.AdditionalFees);
                                    }
                                }
                            }

                            //if (null != currDistributorGrade && ( currDistributorGrade.GradeId.ToString().EqualIgnoreCase(siteSettings.DefaultPartnerGradeId) || currDistributorGrade.GradeId.ToString().EqualIgnoreCase(siteSettings.DefaultTutorGradeId) ) )
                            //{
                            //    currCommission = orderProfit * (currDistributorGrade.AdditionalFees / 100);
                            //    if (currCommission > 0)
                            //    {
                            //        managerCommission += currCommission;
                            //        flag = new DistributorsDao().UpdateCalculationCommissionNoUpdateOrderTotal(currUserId.ToString(), order.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, currCommission, 2
                            //                                                             , string.Format("{0} * ( {1} / 100 ) 管理收入", orderProfit.ToString("f2"), currDistributorGrade.AdditionalFees));
                            //        saleSql.AppendLine("当前佣金计算(管理佣金)：------当前提成者ID：" + currUserId + ";上级ID：" + order.ReferralUserId + ";佣金金额：" + currCommission + ";来自哪里：" + order.ReferralUserId + "------" + currDistributorGrade.AdditionalFees);
                            //    }
                            //}
                        }
                    }
                }

                #endregion

                // 更新订单上的直接销售佣金、上级销售佣金、上上级销售佣金
                orderDao.UpdateOrderCommission(order.OrderId, firstCommission, secondCommission, thirdCommission, firstUserId, secondUserId, thirdUserId, managerCommission);
            }
            else
            {
                DistributorsBrower.UpdateCalcRecommendedIncomeLTZero(order.UserId.ToString(), order.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, 0, 1, "订单利润：" + orderProfit.ToString());
                saleSql.AppendLine("当前订单无法计算佣金：------订单利润：" + orderProfit);
            }

            #region "虚拟币赠送处理"

            // 处理虚拟币赠送
            bool updateFlag;

            // 1.店主赠送
            if (order.StoreGiftMoney > 0)
            {
                updateFlag = new MemberDao().AddUserVirtualPoints(order.ReferralUserId, order.StoreGiftMoney);
                XTrace.WriteLine("虚拟币首单赠送（店主）：" + order.ReferralUserId + "------" + order.StoreGiftMoney);
            }

            // 2.会员赠送
            if (order.MemberGiftMoney > 0)
            {
                updateFlag = new MemberDao().AddUserVirtualPoints(order.UserId, order.MemberGiftMoney);
                XTrace.WriteLine("虚拟币购买赠送（会员）：" + order.UserId + "------" + order.MemberGiftMoney);
            }

            #endregion "虚拟币赠送处理"


            saleSql.AppendLine("---------------------------------计算销售佣金结束------购买者ID：" + order.UserId + ";来自店铺ID：" + order.ReferralUserId + ";订单ID：" + order.OrderId + ";---------------------------------");
            // 调试：将推荐佣金计算过程输出到文本文件中
            XTrace.WriteLine(saleSql.ToString());

            return flag;
        }

        ///// <summary>
        ///// 计算对应的提成
        ///// </summary>
        ///// <param name="order"></param>
        ///// <param name="DisInfo"></param>
        ///// <returns></returns>
        //public static bool CalcCommission(OrderInfo order, DistributorsInfo DisInfo)
        //{
        //    bool flag = true;
        //    ArrayList gradeIdList = new ArrayList();
        //    ArrayList referralUserIdList = new ArrayList();

        //    decimal firstCommission = 0;
        //    decimal secondCommission = 0;
        //    decimal thirdCommission = 0;

        //    decimal firstItemCommission = 0;
        //    decimal secondItemCommission = 0;
        //    decimal thirdItemCommission = 0;

        //    bool firstFlag = false;
        //    bool secondFlag = false;
        //    bool thirdFlag = false;

        //    // 计算订单商品利润
        //    //decimal orderProfit = order.LineItems.Values.Sum(n => (n.ItemListPrice * n.Quantity) - (n.ItemCostPrice * n.Quantity));
        //    decimal orderProfit = order.OrderTotal - order.LineItems.Values.Sum(n => (n.ItemCostPrice * n.Quantity));
        //    // 计算订单销售总金额
        //    //decimal orderAmount = order.LineItems.Values.Sum(n => (n.ItemListPrice * n.Quantity));
        //    decimal orderAmount = order.OrderTotal;
        //    // 判断当前订单是否已计算提成
        //    bool isCalcCommissionFlag = new DistributorsDao().IsSetCalculationCommission(order.OrderId);
        //    // 订单没有计算提成且订单利润大于0
        //    if (!isCalcCommissionFlag && orderProfit > 0)
        //    {
        //        // 根据订单ID获取直接销售的店铺信息(ReferralUserId)
        //        DistributorsInfo firstDistributorInfo = DistributorsBrower.GetDistributorInfo(order.ReferralUserId);
        //        DistributorsInfo secondDistributorInfo = null;
        //        DistributorsInfo thirdDistributorInfo = null;

        //        // 根据分销商的等级ID获取对应的分销商等级数据
        //        DistributorGradeInfo firstGradeInfo = null;
        //        DistributorGradeInfo secondGradeInfo = null;
        //        DistributorGradeInfo thirdGradeInfo = null;

        //        // 根据直接销售店铺获取上级分销商(我的上级）
        //        if (null != firstDistributorInfo)
        //        {
        //            // 获取直接销售商的等级
        //            firstGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(firstDistributorInfo.DistriGradeId);
        //            // 获取上级分销商
        //            secondDistributorInfo = DistributorsBrower.GetDistributorInfo(firstDistributorInfo.ReferralUserId);
        //            // 计算直接销售佣金提成
        //            firstCommission = orderProfit * (firstGradeInfo.FirstCommissionRise / 100);
        //            // 更新佣金提成到提成表
        //            firstFlag = new DistributorsDao().UpdateCalculationCommission(firstDistributorInfo.UserId.ToString(), firstDistributorInfo.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, firstCommission, 1
        //                , string.Format("{0} * ( {1} / 100 )", orderProfit.ToString("f2"), firstGradeInfo.FirstCommissionRise));
        //        }
        //        // 根据直接销售店铺的上级分销商获取上上级分销商（我的上上级）
        //        if (null != secondDistributorInfo)
        //        {
        //            // 获取直接销售店铺上级分销商的等级
        //            secondGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(secondDistributorInfo.DistriGradeId);
        //            // 获取上上级分销商
        //            thirdDistributorInfo = DistributorsBrower.GetDistributorInfo(secondDistributorInfo.ReferralUserId);
        //            // 计算上级销售佣金提成
        //            secondCommission = orderProfit * (secondGradeInfo.SecondCommissionRise / 100);
        //            // 更新佣金提成到提成表
        //            secondFlag = new DistributorsDao().UpdateCalculationCommissionNoUpdateOrderTotal(secondDistributorInfo.UserId.ToString(), secondDistributorInfo.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, secondCommission, 1
        //                , string.Format("{0} * ( {1} / 100 )", orderProfit.ToString("f2"), secondGradeInfo.SecondCommissionRise));
        //        }
        //        if (null != thirdDistributorInfo)
        //        {
        //            // 获取直接销售店铺上上级分销商的等级
        //            thirdGradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(thirdDistributorInfo.DistriGradeId);
        //            // 计算上上级销售佣金提成
        //            thirdCommission = orderProfit * (thirdGradeInfo.ThirdCommissionRise / 100);
        //            // 更新佣金提成到提成表
        //            thirdFlag = new DistributorsDao().UpdateCalculationCommissionNoUpdateOrderTotal(thirdDistributorInfo.UserId.ToString(), thirdDistributorInfo.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, thirdCommission, 1
        //                , string.Format("{0} * ( {1} / 100 )", orderProfit.ToString("f2"), thirdGradeInfo.ThirdCommissionRise));
        //        }

        //        OrderDao orderDao = new OrderDao();
        //        LineItemDao lineItemDao = new LineItemDao();

        //        // 更新订单上的直接销售佣金、上级销售佣金、上上级销售佣金
        //        orderDao.UpdateOrderCommission(order.OrderId, firstCommission, secondCommission, thirdCommission, 0, 0, 0, 0);

        //        // 获取订单的商品信息
        //        foreach (LineItemInfo lineItemInfo in order.LineItems.Values)
        //        {
        //            firstItemCommission = 0;
        //            secondItemCommission = 0;
        //            thirdItemCommission = 0;

        //            if (null != firstGradeInfo)
        //            {
        //                // 计算直接销售佣金提成(((销售价－成本价) * 数量 ) * 提成比率)
        //                firstItemCommission = ((lineItemInfo.ItemListPrice - lineItemInfo.ItemCostPrice) * lineItemInfo.Quantity) * (firstGradeInfo.FirstCommissionRise / 100);
        //            }
        //            if (null != secondGradeInfo)
        //            {
        //                // 计算上级销售佣金提成(((销售价－成本价) * 数量 ) * 提成比率)
        //                secondItemCommission = ((lineItemInfo.ItemListPrice - lineItemInfo.ItemCostPrice) * lineItemInfo.Quantity) * (secondGradeInfo.SecondCommissionRise / 100);
        //            }
        //            if (null != thirdGradeInfo)
        //            {
        //                // 计算上上级销售佣金提成(((销售价－成本价) * 数量 ) * 提成比率)
        //                thirdItemCommission = ((lineItemInfo.ItemListPrice - lineItemInfo.ItemCostPrice) * lineItemInfo.Quantity) * (thirdGradeInfo.ThirdCommissionRise / 100);
        //            }
        //            // 更新订单对应商品上的直接销售佣金、上级销售佣金、上上级销售佣金
        //            lineItemDao.UpdateLineItemCommission(order.OrderId, lineItemInfo.ProductId, lineItemInfo.SkuId, firstItemCommission, secondItemCommission, thirdItemCommission, 0, 0, 0);
        //        }

        //        // 计算额外提成
        //        DataTable distributorDatatable = DistributorsBrower.GetAllParentDistributorsByUserId(order.ReferralUserId);
        //        if (null != distributorDatatable && distributorDatatable.Rows.Count > 0)
        //        {
        //            DistributorsInfo currDistributor = null;
        //            DistributorGradeInfo currDistributorGrade = null;
        //            int currUserId = 0;
        //            decimal currCommission = 0;

        //            foreach (DataRow dr in distributorDatatable.Rows)
        //            {
        //                currUserId = int.Parse(dr["UserId"].ToString());
        //                // 获取分销商
        //                currDistributor = DistributorsBrower.GetDistributorInfo(currUserId);
        //                if (null != currDistributor)
        //                {
        //                    // 获取分销商对应的等级
        //                    currDistributorGrade = DistributorGradeBrower.GetDistributorGradeInfo(currDistributor.DistriGradeId);
        //                    if (null != currDistributorGrade)
        //                    {
        //                        currCommission = orderProfit * (currDistributorGrade.AdditionalFees / 100);
        //                        if (currCommission > 0)
        //                        {
        //                            flag = new DistributorsDao().UpdateCalculationCommissionNoUpdateOrderTotal(currUserId.ToString(), order.ReferralUserId.ToString(), order.ReferralUserId.ToString(), order.OrderId, orderAmount, currCommission, 2
        //                                                                                 , string.Format("{0} * ( {1} / 100 ) 管理收入", orderProfit.ToString("f2"), currDistributorGrade.AdditionalFees));
        //                        }

        //                    }
        //                }
        //            }
        //        }

        //    }
        //    else
        //    {
        //        // 没有利润的情况下，更新分销商的销售金额
        //        new DistributorsDao().UpdateDistributorOrderTotals(order.ReferralUserId.ToString(), orderAmount);
        //    }

        //    //int notDescDistributorGrades = GetNotDescDistributorGrades(order.UserId.ToString());
        //    //if (notDescDistributorGrades > 0)
        //    //{
        //    //    gradeIdList.Add(notDescDistributorGrades);
        //    //    referralUserIdList.Add(order.UserId.ToString());
        //    //    flag = new DistributorsDao().UpdateGradeId(gradeIdList, referralUserIdList);
        //    //}

        //    return flag;
        //}

        public static bool setCommission(OrderInfo order, DistributorsInfo DisInfo)
        {
            bool flag = false;
            decimal resultCommTatal = 0M;
            decimal orderTotal = order.LineItems.Values.Sum(n => (n.ItemListPrice * n.Quantity) - (n.ItemCostPrice * n.Quantity));//商品利润
            decimal orderTotalAll = order.LineItems.Values.Sum(n => (n.ItemListPrice * n.Quantity));
            ArrayList gradeIdList = new ArrayList();
            ArrayList referralUserIdList = new ArrayList();

            if (!new DistributorsDao().IsSetCalculationCommission(order.OrderId) && orderTotal > 0)//是否已生产佣金
            {
                #region 完成订单生成佣金

                DistributorGradeInfo gradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(DisInfo.DistriGradeId);
                MemberInfo member = null;

                if (gradeInfo != null)
                {
                    for (int i = (int)DisInfo.DistributorGradeId; i > 0; i--)
                    {
                        switch (DisInfo.DistributorGradeId)
                        {
                            case DistributorGrade.OneDistributor:
                                member = MemberProcessor.GetMember(DisInfo.UserId);
                                if (member != null && DisInfo != null)
                                    gradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(DisInfo.DistriGradeId);

                                resultCommTatal = orderTotal * (gradeInfo.FirstCommissionRise / 100);
                                flag = new DistributorsDao().UpdateCalculationCommission(member.UserId.ToString(), member.ReferralUserId.ToString(), "", order.OrderId, orderTotalAll, resultCommTatal, 1, string.Format("{0} * ({1} / 100)", orderTotal.ToString("f2"), gradeInfo.FirstCommissionRise));
                                break;
                            case DistributorGrade.TowDistributor:

                                if (member == null)
                                    member = MemberProcessor.GetMember(order.UserId);
                                member = MemberProcessor.GetMember(member.ReferralUserId);

                                if (member != null && member.ReferralUserId > 0 && DisInfo != null)
                                    gradeInfo = DistributorGradeBrower.GetDistributorGradeInfo(DisInfo.DistriGradeId);

                                resultCommTatal = orderTotal * (gradeInfo.SecondCommissionRise / 100);
                                flag = new DistributorsDao().UpdateCalculationCommission(member.UserId.ToString(), member.ReferralUserId.ToString(), "", order.OrderId, orderTotalAll, resultCommTatal, 1, string.Format("{0} * ({1} / 100)", orderTotal.ToString("f2"), gradeInfo.SecondCommissionRise));

                                DisInfo = new DistributorsDao().GetDistributorInfo(member.ReferralUserId);

                                break;
                            case DistributorGrade.ThreeDistributor:
                                member = MemberProcessor.GetMember(DisInfo.UserId);
                                DisInfo = new DistributorsDao().GetDistributorInfo(member.ReferralUserId);

                                resultCommTatal = orderTotal * (gradeInfo.ThirdCommissionRise / 100);
                                flag = new DistributorsDao().UpdateCalculationCommission(member.UserId.ToString(), member.ReferralUserId.ToString(), "", order.OrderId, orderTotalAll, resultCommTatal, 1, string.Format("{0} * ({1} / 100)", orderTotal.ToString("f2"), gradeInfo.ThirdCommissionRise));
                                break;
                            case DistributorGrade.All:
                            default:
                                resultCommTatal = 0;
                                break;
                        }
                    }
                }

                #endregion
            }

            int notDescDistributorGrades = GetNotDescDistributorGrades(order.UserId.ToString());
            if (notDescDistributorGrades > 0)
            {
                gradeIdList.Add(notDescDistributorGrades);
                referralUserIdList.Add(order.UserId.ToString());
                flag = new DistributorsDao().UpdateGradeId(gradeIdList, referralUserIdList);
            }
            return flag;
        }

        public static bool SetRedpackRecordIsUsed(int id, bool issend)
        {
            return new SendRedpackRecordDao().SetRedpackRecordIsUsed(id, issend);
        }

        public static bool UpdateCalculationCommissionNew(OrderInfo order)
        {
            XTrace.WriteLine("------------------------------订单佣金计算流程处理开始.------------------------------");
            // 订单中如果有默认的分销商推广商品时，不进行佣金计算
            if (DistributorsBrower.CheckIsDistributoBuyProduct(order.OrderId))
            {
                return false;
            }
            DistributorsInfo userIdDistributors = GetUserIdDistributors(order.ReferralUserId);//获取推荐店铺
            SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);//获得站点信息
            bool flag = false;
            SiteSettings siteSettings = SettingsManager.GetMasterSettings(false);
            // 判断是否要处理收益
            if (siteSettings.IsProcessCommissions)
            {
                if (userIdDistributors != null)
                {
                    //if (userIdDistributors.ReferralStatus == 0)
                    //{
                    XTrace.WriteLine("------订单信息->订单ID：" + order.OrderId + "->订单金额：" + order.GetAmount() + "->订单实际金额：" + order.OrderTotal + "->订单成本：" + order.GetCostPrice() + "->订单利润：" + order.GetProfit() + "->优惠金额：" + order.DiscountAmount + "+" + order.RedPagerAmount);
                    flag = CalcCommissionNew(order);
                    //}
                    RemoveDistributorCache(userIdDistributors.UserId);
                }
                OrderRedPagerBrower.CreateOrderRedPager(order.OrderId, order.GetTotal(), order.UserId);
            }
            else
            {
                flag = true;
            }
            XTrace.WriteLine("------------------------------订单佣金计算流程处理结束.------------------------------");
            return flag;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order">订单</param>
        /// <returns></returns>
        //public static bool UpdateCalculationCommission(OrderInfo order)
        //{
        //    // 订单中如果有默认的分销商推广商品时，不进行佣金计算
        //    if (DistributorsBrower.CheckIsDistributoBuyProduct(order.OrderId))
        //    {
        //        return false;
        //    }

        //    DistributorsInfo userIdDistributors = GetUserIdDistributors(order.ReferralUserId);//获取推荐店铺
        //    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);//获得站点信息
        //    bool flag = false;
        //    if (userIdDistributors != null)
        //    {
        //        if (!masterSettings.EnableCommission)
        //        {
        //            if (userIdDistributors.ReferralStatus == 0)
        //            {
        //                //flag = setCommission(order, userIdDistributors);
        //                //flag = CalcCommission(order, userIdDistributors);
        //                flag = CalcCommissionNew(order);
        //            }
        //        }
        //        else
        //        {
        //            if (userIdDistributors.ReferralStatus == 0)
        //            {
        //                //flag = setCommission(order, userIdDistributors);
        //                //flag = CalcCommission(order, userIdDistributors);
        //                flag = CalcCommissionNew(order);
        //            }
        //            if (!string.IsNullOrEmpty(userIdDistributors.ReferralPath))
        //            {
        //                ArrayList commTatalList = new ArrayList();
        //                decimal num = 0M;
        //                ArrayList userIdList = new ArrayList();
        //                string referralUserId = order.ReferralUserId.ToString();
        //                string orderId = order.OrderId;
        //                ArrayList orderTotalList = new ArrayList();
        //                decimal num2 = 0M;
        //                ArrayList gradeIdList = new ArrayList();
        //                string[] strArray = userIdDistributors.ReferralPath.Split(new char[] { '|' });
        //                if (strArray.Length == 1)
        //                {
        //                    DistributorsInfo info2 = GetUserIdDistributors(int.Parse(strArray[0]));
        //                    if (null != info2)
        //                    {
        //                        if (info2.ReferralStatus == 0)
        //                        {
        //                            foreach (LineItemInfo info3 in order.LineItems.Values)
        //                            {
        //                                if (info3.OrderItemsStatus.ToString() == OrderStatus.SellerAlreadySent.ToString())
        //                                {
        //                                    num += info3.SecondItemsCommission;
        //                                    num2 += info3.GetSubTotal();
        //                                }
        //                            }
        //                            commTatalList.Add(num);
        //                            orderTotalList.Add(num2);
        //                            userIdList.Add(info2.UserId);
        //                        }
        //                    }
        //                }
        //                if (strArray.Length == 2)
        //                {
        //                    DistributorsInfo info4 = GetUserIdDistributors(int.Parse(strArray[0]));
        //                    if (null != info4)
        //                    {
        //                        if (info4.ReferralStatus == 0)
        //                        {
        //                            foreach (LineItemInfo info3 in order.LineItems.Values)
        //                            {
        //                                if (info3.OrderItemsStatus.ToString() == OrderStatus.SellerAlreadySent.ToString())
        //                                {
        //                                    num += info3.ThirdItemsCommission;
        //                                    num2 += info3.GetSubTotal();
        //                                }
        //                            }
        //                            commTatalList.Add(num);
        //                            orderTotalList.Add(num2);
        //                            userIdList.Add(info4.UserId);
        //                        }
        //                    }
        //                    DistributorsInfo info5 = GetUserIdDistributors(int.Parse(strArray[1]));
        //                    num = 0M;
        //                    num2 = 0M;
        //                    if (null != info5)
        //                    {
        //                        if (info5.ReferralStatus == 0)
        //                        {
        //                            foreach (LineItemInfo info3 in order.LineItems.Values)
        //                            {
        //                                if (info3.OrderItemsStatus.ToString() == OrderStatus.SellerAlreadySent.ToString())
        //                                {
        //                                    num += info3.SecondItemsCommission;
        //                                    num2 += info3.GetSubTotal();
        //                                }
        //                            }
        //                            commTatalList.Add(num);
        //                            orderTotalList.Add(num2);
        //                            userIdList.Add(info5.UserId);
        //                        }
        //                    }
        //                }
        //                //flag = new DistributorsDao().UpdateTwoCalculationCommission(userIdList, referralUserId, orderId, orderTotalList, commTatalList);
        //                for (int i = 0; i < userIdList.Count; i++)
        //                {
        //                    int notDescDistributorGrades = GetNotDescDistributorGrades(userIdList[i].ToString());
        //                    gradeIdList.Add(notDescDistributorGrades);
        //                }
        //                // 2015-10-21 注销更新分销商等级
        //                //flag = new DistributorsDao().UpdateGradeId(gradeIdList, userIdList);
        //            }
        //        }
        //        RemoveDistributorCache(userIdDistributors.UserId);
        //    }
        //    OrderRedPagerBrower.CreateOrderRedPager(order.OrderId, order.GetTotal(), order.UserId);
        //    return flag;
        //}

        public static bool UpdateDistributor(DistributorsInfo query)
        {
            int num = IsExiteDistributorsByStoreName(query.StoreName);
            if ((num != 0) && (num != query.UserId))
            {
                return false;
            }
            return new DistributorsDao().UpdateDistributor(query);
        }

        public static bool UpdateDistributorMessage(DistributorsInfo query)
        {
            int num = IsExiteDistributorsByStoreName(query.StoreName);
            if ((num != 0) && (num != query.UserId))
            {
                return false;
            }
            return new DistributorsDao().UpdateDistributorMessage(query);
        }

        public static bool IsExistDistributor()
        {
            int num = new DistributorsDao().GetDistributorCnt();

            if (num > 0)
            {
                return true;
            }
            return false;
        }

        public static bool UpdateIsBindMobile(int userId, int isBindMobile)
        {
            return new DistributorsDao().UpdateIsBindMobile(userId, isBindMobile);
        }

        public static bool CheckIsDistributoBuyProduct(string orderId)
        {
            int num = new DistributorsDao().CheckIsDistributoBuyProduct(orderId);
            if (num > 0)
            {
                return true;
            }
            return false;
        }

        public static bool CheckCartIsDistributoBuyProduct(string productId)
        {
            int num = new DistributorsDao().CheckCartIsDistributoBuyProduct(productId);
            if (num > 0)
            {
                return true;
            }
            return false;
        }

        public static bool CheckProductIsCrossByProductId(string productId)
        {
            int num = new DistributorsDao().CheckProductIsCrossByProductId(productId);
            if (num > 0)
            {
                return true;
            }
            return false;
        }

        public static bool CheckIsFirstOrder(string referralUserId)
        {
            int num = new DistributorsDao().CheckIsFirstOrder(referralUserId);
            if (num > 0)
            {
                return false;
            }
            return true;
        }

        public static string GetProductId(IList<ShoppingCartItemInfo> infoList)
        {
            string rtnValue = "";

            foreach (ShoppingCartItemInfo info in infoList)
            {
                rtnValue += info.ProductId + ",";
            }

            if (!string.IsNullOrEmpty(rtnValue))
            {
                rtnValue = rtnValue.Substring(0, rtnValue.Length - 1);
            }
            else
            {
                rtnValue = "0";
            }

            return rtnValue;
        }

        public static decimal CalProductVPRateInOrder(ShoppingCartInfo shoppingCart)
        {
            decimal sumVP = 0;
            decimal exchangeRate = 0;
            decimal productPrice = 0;

            if (null != shoppingCart)
            {
                IList<ShoppingCartItemInfo> itemInfo = shoppingCart.LineItems;
                if (null != itemInfo && itemInfo.Count > 0)
                {
                    foreach (ShoppingCartItemInfo info in itemInfo)
                    {
                        // 得到商品的抵扣比率
                        exchangeRate = info.VirtualPointRate / 100;
                        // 得到商品的总价
                        productPrice = (decimal)(info.Quantity * info.AdjustedPrice);
                        XTrace.WriteLine("------处理中的商品ID：" + info.ProductId + " ------商品上的虚拟币使用率：" + info.VirtualPointRate + " ------商品能使用的虚拟币金额：" + productPrice);
                        sumVP += productPrice * exchangeRate;
                    }
                }
            }
            return sumVP;
        }

        public static bool UpdateCommissionOrderStatus(string orderId, int commOrderStatus)
        {
            StringBuilder sql = new StringBuilder();

            // 订单已确认收货后处理，将订单的收益体现在各分销商上可提现金额中
            OrderInfo order = ShoppingProcessor.GetOrderInfo(orderId);
            if (null != order)
            {
                // 获取提成数据
                DataTable commissionData = new DistributorsDao().GetCommosionByOrderId(orderId);
                if (null != commissionData && commissionData.Rows.Count > 0)
                {
                    int currUserId = 0;
                    decimal currCommission = 0;

                    sql.AppendLine("begin try  ");
                    sql.AppendLine("  begin tran TranUpdate ");

                    foreach (DataRow dr in commissionData.Rows)
                    {
                        currUserId = int.Parse(dr["UserId"].ToString());
                        currCommission = decimal.Parse(dr["CommTotal"].ToString());

                        sql.AppendLine(" UPDATE aspnet_Distributors SET ReferralBlance = ISNULL(ReferralBlance, 0) + " + currCommission + " WHERE UserId = " + currUserId + " ; ");
                    }

                    sql.AppendLine(" UPDATE Hishop_Commissions SET CommOrderStatus = " + commOrderStatus + " WHERE OrderId = '" + orderId + "' ;");
                    sql.AppendLine("  COMMIT TRAN TranUpdate ");
                    sql.AppendLine("  end try \r\n                    begin catch \r\n                        ROLLBACK TRAN TranUpdate\r\n                    end catch ");
                }
            }
            XTrace.WriteLine("订单确认收货时更新SQL：" + sql.ToString());

            return new DistributorsDao().UpdateCommissionOrderStatus(sql.ToString());
        }

        public static bool UpdateVisitCounts(int userId)
        {
            return new DistributorsDao().UpdateVisitCounts(userId);
        }

        public static bool UpdateGoodCounts(int userId)
        {
            return new DistributorsDao().UpdateGoodCounts(userId);
        }

        public static bool UpdateProductGoodCounts(int productId)
        {
            return new DistributorsDao().UpdateProductGoodCounts(productId);
        }

        public static string GetOrderIdByUserIdAndProductId(int userId, string productId)
        {
            return new DistributorsDao().GetOrderIdByUserIdAndProductId(userId, productId);
        }

        public static ProductInfo GetQRCodeDistProductByPTTypeId(int ptTypeId) 
        {
            return new ProductDao().GetQRCodeDistProductByPTTypeId(ptTypeId);
        }

        public static OrderInfo GetSJOrderInfo(int userId)
        {
            return new OrderDao().GetSJOrderInfo(userId);
        }

        public static bool AddUserVirtualPoints(int userId, decimal points)
        {
            return new MemberDao().AddUserVirtualPoints(userId, points);
        }

        public static bool UpdateDistributorIsTempStoreAndDeadlineTimeById(int distributorId, int isTempStore, DateTime decasualizationTime, DateTime deadlineTime)
        {
            return new DistributorsDao().UpdateDistributorIsTempStoreAndDeadlineTimeById(distributorId, isTempStore, decasualizationTime, deadlineTime);
        }

        public static bool UpdateOrderStatus(string orderId, int orderStatus, DateTime updateDate)
        {
            return new OrderDao().UpdateOrderStatus(orderId, orderStatus, updateDate);
        }
    }
}

