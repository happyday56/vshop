using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Core.Enums;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Orders;
using Hidistro.Entities.VShop;
using Hidistro.SaleSystem.Vshop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace Hidistro.UI.Web.Admin
{
    public class VsiteHandler : System.Web.IHttpHandler
    {
        private class EnumJson
        {
            public string Name
            {
                get;
                set;
            }
            public string Value
            {
                get;
                set;
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public void ProcessRequest(System.Web.HttpContext context)
        {
            string str = context.Request.Form["actionName"];
            string s = string.Empty;
            string str2 = str;
            if (str2 != null)
            {
                if (!(str2 == "Topic"))
                {
                    if (str2 == "Vote")
                    {
                        s = JsonConvert.SerializeObject(StoreHelper.GetVoteList());
                    }
                    else if (str2 == "Product")
                    {
                        s = JsonConvert.SerializeObject(VShopHelper.GetProductList());
                    }
                    else
                    {
                        if (str2 == "Category")
                        {
                            s = JsonConvert.SerializeObject(
                                from item in CatalogHelper.GetMainCategories()
                                select new
                                {
                                    CateId = item.CategoryId,
                                    CateName = item.Name
                                });
                        }
                        else
                        {
                            if (str2 == "Activity")
                            {
                                System.Array values = System.Enum.GetValues(typeof(LotteryActivityType));
                                System.Collections.Generic.List<VsiteHandler.EnumJson> list3 = new System.Collections.Generic.List<VsiteHandler.EnumJson>();
                                foreach (System.Enum enum2 in values)
                                {
                                    VsiteHandler.EnumJson json = new VsiteHandler.EnumJson
                                    {
                                        Name = enum2.ToShowText(),
                                        Value = enum2.ToString()
                                    };
                                    list3.Add(json);
                                }
                                s = JsonConvert.SerializeObject(list3);
                            }
                            else
                            {
                                if (str2 == "ActivityList")
                                {
                                    string str3 = context.Request.Form["acttype"];
                                    if (!str3.EqualIgnoreCase("ÇëÑ¡Ôñ»î¶¯"))
                                    {
                                        LotteryActivityType type = (LotteryActivityType)System.Enum.Parse(typeof(LotteryActivityType), str3);
                                        if (type == LotteryActivityType.SignUp)
                                        {
                                            s = JsonConvert.SerializeObject(
                                                from item in VShopHelper.GetAllActivity()
                                                select new
                                                {
                                                    ActivityId = item.ActivityId,
                                                    ActivityName = item.Name
                                                });
                                        }
                                        else
                                        {
                                            s = JsonConvert.SerializeObject(VShopHelper.GetLotteryActivityByType(type));
                                        }
                                    }
                                }
                                else
                                {
                                    if (str2 == "AccountTime")
                                    {
                                        s += "{";
                                        BalanceDrawRequestQuery entity = new BalanceDrawRequestQuery
                                        {
                                            RequestTime = "",
                                            CheckTime = "",
                                            StoreName = "",
                                            PageIndex = 1,
                                            PageSize = 1,
                                            SortOrder = SortAction.Desc,
                                            SortBy = "RequestTime",
                                            RequestEndTime = "",
                                            RequestStartTime = "",
                                            IsCheck = "1",
                                            UserId = context.Request.Form["UserID"]
                                        };
                                        Globals.EntityCoding(entity, true);
                                        System.Data.DataTable data = (System.Data.DataTable)DistributorsBrower.GetBalanceDrawRequest(entity).Data;
                                        if (data.Rows.Count > 0)
                                        {
                                            if (data.Rows[0]["MerchantCode"].ToString().Trim() != context.Request.Form["merchantcode"].Trim())
                                            {
                                                s = s + "\"Time\":\"" + data.Rows[0]["RequestTime"].ToString() + "\"";
                                            }
                                            else
                                            {
                                                s += "\"Time\":\"\"";
                                            }
                                        }
                                        else
                                        {
                                            s += "\"Time\":\"\"";
                                        }
                                        s += "}";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    s = JsonConvert.SerializeObject(VShopHelper.Gettopics());
                }
            }
            context.Response.Write(s);
        }
    }
}
