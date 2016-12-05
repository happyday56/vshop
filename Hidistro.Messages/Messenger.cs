using System;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using Hidistro.Core;
using Hidistro.Core.Configuration;
using Hidistro.Core.Entities;
using Hidistro.Entities.Members;
using Hidistro.Entities.Orders;
using Hidistro.Entities.VShop;
using Hishop.Plugins;
using Hishop.Weixin.MP.Api;
using Hishop.Weixin.MP.Domain;
using Hidistro.SqlDal.Orders;
using NewLife.Log;

namespace Hidistro.Messages
{
    public static class Messenger
    {
        internal static EmailSender CreateEmailSender(SiteSettings settings)
        {
            string str;
            return CreateEmailSender(settings, out str);
        }

        internal static EmailSender CreateEmailSender(SiteSettings settings, out string msg)
        {
            try
            {
                msg = "";
                if (!settings.EmailEnabled)
                {
                    return null;
                }
                return EmailSender.CreateInstance(settings.EmailSender, HiCryptographer.Decrypt(settings.EmailSettings));
            }
            catch (Exception exception)
            {
                msg = exception.Message;
                return null;
            }
        }

        internal static SMSSender CreateSMSSender(SiteSettings settings)
        {
            string str;
            return CreateSMSSender(settings, out str);
        }

        internal static SMSSender CreateSMSSender(SiteSettings settings, out string msg)
        {
            try
            {
                msg = "";
                if (!settings.SMSEnabled)
                {
                    return null;
                }
                return SMSSender.CreateInstance(settings.SMSSender, HiCryptographer.Decrypt(settings.SMSSettings));
            }
            catch (Exception exception)
            {
                msg = exception.Message;
                return null;
            }
        }

        private static TemplateMessage GenerateWeixinMessageWhenFindPassword(string templateId, SiteSettings settings, MemberInfo user, string password)
        {
            if (string.IsNullOrWhiteSpace(user.OpenId))
            {
                return null;
            }
            string weixinToken = settings.WeixinToken;
            TemplateMessage message2 = new TemplateMessage
            {
                Url = "",
                TemplateId = templateId,
                Touser = user.OpenId
            };
            TemplateMessage.MessagePart[] partArray = new TemplateMessage.MessagePart[4];
            TemplateMessage.MessagePart part = new TemplateMessage.MessagePart
            {
                Name = "first",
                Value = "您好,您的账号信息如下"
            };
            partArray[0] = part;
            TemplateMessage.MessagePart part2 = new TemplateMessage.MessagePart
            {
                Name = "keyword1",
                Value = user.UserName
            };
            partArray[1] = part2;
            TemplateMessage.MessagePart part3 = new TemplateMessage.MessagePart
            {
                Name = "keyword2",
                Value = password
            };
            partArray[2] = part3;
            TemplateMessage.MessagePart part4 = new TemplateMessage.MessagePart
            {
                Name = "remark",
                Value = "请妥善保管。"
            };
            partArray[3] = part4;
            message2.Data = partArray;
            return message2;
        }

        private static TemplateMessage GenerateWeixinMessageWhenOrderClose(string templateId, SiteSettings settings, MemberInfo user, OrderInfo order, string reason)
        {
            if (string.IsNullOrWhiteSpace(user.OpenId))
            {
                return null;
            }
            string weixinToken = settings.WeixinToken;
            TemplateMessage message2 = new TemplateMessage
            {
                Url = "",
                TemplateId = templateId,
                Touser = user.OpenId
            };
            TemplateMessage.MessagePart[] partArray = new TemplateMessage.MessagePart[5];
            TemplateMessage.MessagePart part = new TemplateMessage.MessagePart
            {
                Name = "first",
                Value = "您好,您的订单已关闭，请核对"
            };
            partArray[0] = part;
            TemplateMessage.MessagePart part2 = new TemplateMessage.MessagePart
            {
                Name = "transid",
                Value = order.OrderId
            };
            partArray[1] = part2;
            TemplateMessage.MessagePart part3 = new TemplateMessage.MessagePart
            {
                Name = "fee",
                Color = "#ff3300",
                Value = "￥" + order.GetTotal().ToString("F2")
            };
            partArray[2] = part3;
            TemplateMessage.MessagePart part4 = new TemplateMessage.MessagePart
            {
                Name = "pay_time",
                Value = (order.PayDate.ToString() != "") ? DateTime.Parse(order.PayDate.ToString()).ToString("M月d日 HH:mm:ss") : DateTime.Parse(order.OrderDate.ToString()).ToString("M月d日 HH:mm:ss")
            };
            partArray[3] = part4;
            TemplateMessage.MessagePart part5 = new TemplateMessage.MessagePart
            {
                Name = "remark",
                Color = "#000000",
                Value = "关闭原因：" + reason
            };
            partArray[4] = part5;
            message2.Data = partArray;
            return message2;
        }

        /// <summary>
        /// 订单创建时发送微信信息
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="settings"></param>
        /// <param name="order"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private static TemplateMessage GenerateWeixinMessageWhenOrderCreate(string templateId, SiteSettings settings, OrderInfo order, MemberInfo user)
        {
            //XTrace.WriteLine("订单创建微信消息：－－模板ID：" + templateId + " -- 站点名称： " + settings.SiteName + " -- 发送对象：" + user.OpenId
            //    + " -- 订单ID： " + order.OrderId + " -- 订单数量：" + order.GetTotalQuantity().ToString() + " -- 订单金额：" + order.GetTotal());
            TemplateMessage message = null;
            if (!string.IsNullOrWhiteSpace(user.OpenId))
            {
                string weixinToken = settings.WeixinToken;
                TemplateMessage message2 = new TemplateMessage
                {
                    Url = "",
                    TemplateId = templateId,
                    Touser = user.OpenId
                };

                TemplateMessage.MessagePart[] partArray = new TemplateMessage.MessagePart[5];
                TemplateMessage.MessagePart part = new TemplateMessage.MessagePart
                {
                    Name = "first",
                    Value = "您的订单已创建成功，请关注【" + settings.SiteName + "】服务号，打开【我】-【我的订单】点击进入完成付款。"
                };
                partArray[0] = part;
                // 订单号
                TemplateMessage.MessagePart part2 = new TemplateMessage.MessagePart
                {
                    Name = "orderno",
                    Value = order.OrderId
                };
                partArray[1] = part2;
                TemplateMessage.MessagePart part3 = new TemplateMessage.MessagePart
                {
                    Name = "refundno",
                    Value = order.GetTotalQuantity().ToString()
                };
                partArray[2] = part3;
                TemplateMessage.MessagePart part4 = new TemplateMessage.MessagePart
                {
                    Name = "refundproduct",
                    Color = "#ff3300",
                    Value = "￥" + order.GetTotal().ToString("F2")
                };
                partArray[3] = part4;
                TemplateMessage.MessagePart part5 = new TemplateMessage.MessagePart
                {
                    Name = "remark",
                    Value = "如您还有疑问，请联系你的客服。"
                };
                partArray[4] = part5;
                message2.Data = partArray;
                message = message2;
            }
            return message;
        }

        private static TemplateMessage GenerateWeixinMessageWhenOrderCreateManage(string templateId, SiteSettings settings, OrderInfo order, MemberInfo user)
        {
            TemplateMessage message = null;
            if (!string.IsNullOrWhiteSpace(user.OpenId))
            {
                string weixinToken = settings.WeixinToken;
                TemplateMessage message2 = new TemplateMessage
                {
                    Url = "",
                    TemplateId = templateId,
                    Touser = user.OpenId
                };
                TemplateMessage.MessagePart[] partArray = new TemplateMessage.MessagePart[4];
                TemplateMessage.MessagePart part = new TemplateMessage.MessagePart
                {
                    Name = "first",
                    Value = settings.SiteName + "平台有订单提交,请注意及时处理!"
                };
                partArray[0] = part;
                TemplateMessage.MessagePart part2 = new TemplateMessage.MessagePart
                {
                    Name = "orderID",
                    Value = order.OrderId
                };
                partArray[1] = part2;
                TemplateMessage.MessagePart part3 = new TemplateMessage.MessagePart
                {
                    Name = "orderMoneySum",
                    Color = "#ff3300",
                    Value = "￥" + order.GetTotal().ToString("F2")
                };
                partArray[2] = part3;
                TemplateMessage.MessagePart part4 = new TemplateMessage.MessagePart
                {
                    Name = "remark",
                    Value = "提交人：" + order.Username + ",提交时间：" + DateTime.Now.ToString() + ",联系方式：" + order.CellPhone
                };
                partArray[3] = part4;
                message2.Data = partArray;
                message = message2;
            }
            return message;
        }

        /// <summary>
        /// 微信订单支付成功消息推送
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="settings"></param>
        /// <param name="user"></param>
        /// <param name="orderId"></param>
        /// <param name="fee"></param>
        /// <returns></returns>
        private static TemplateMessage GenerateWeixinMessageWhenOrderPay(string templateId, SiteSettings settings, MemberInfo user, string orderId, decimal fee)
        {
            if (string.IsNullOrWhiteSpace(user.OpenId))
            {
                return null;
            }

            string weixinToken = settings.WeixinToken;

            TemplateMessage templateMessage = new TemplateMessage();

            templateMessage.Url = "";//单击URL
            templateMessage.TemplateId = templateId;//消息模板ID
            templateMessage.Touser = user.OpenId;//用户OPENID

            #region 读取订单详情
            //StringBuilder prostr = new StringBuilder();
            OrderInfo orderInfo = new OrderDao().GetOrderInfo(orderId);
            //foreach (LineItemInfo item in orderInfo.LineItems.Values)
            //{
            //    prostr.AppendFormat(",{0}", item.ItemDescription);
            //}
            //if (prostr.Length > 0) prostr.Remove(0, 1);
            #endregion

            TemplateMessage.MessagePart[] messateParts = new TemplateMessage.MessagePart[]{
                 new TemplateMessage.MessagePart{Name = "first",Value = "您好,您的订单已付款成功"},
                 new TemplateMessage.MessagePart{Name = "keyword1", Value = orderId},
                 new TemplateMessage.MessagePart{Name = "keyword2", Value = orderInfo.PayDate.ToString()},
                 //new TemplateMessage.MessagePart{Name = "keyword3", Value = orderInfo.GetAmount().ToString("F2")},
                 new TemplateMessage.MessagePart{Name = "keyword3", Value = orderInfo.OrderTotal.ToString("F2")},
                 new TemplateMessage.MessagePart{Name = "keyword4", Value = orderInfo.PaymentType},
                  new TemplateMessage.MessagePart{Name = "remark",Value = "感谢您的惠顾."}};

            templateMessage.Data = messateParts;

            return templateMessage;
        }

        //private static TemplateMessage GenerateWeixinMessageWhenOrderPay(string templateId, SiteSettings settings, MemberInfo user, OrderInfo order, decimal fee)
        //{
        //    if (string.IsNullOrWhiteSpace(user.OpenId))
        //    {
        //        return null;
        //    }

        //    string weixinToken = settings.WeixinToken;

        //    TemplateMessage templateMessage = new TemplateMessage();

        //    templateMessage.Url = "";//单击URL
        //    templateMessage.TemplateId = templateId;//消息模板ID
        //    templateMessage.Touser = user.OpenId;//用户OPENID

        //    StringBuilder prostr = new StringBuilder();
        //    foreach (LineItemInfo item in order.LineItems.Values)
        //    {
        //        prostr.AppendFormat(",{0}", item.ItemDescription);
        //    }
        //    if (prostr.Length > 0) prostr.Remove(0, 1);

        //    TemplateMessage.MessagePart[] messateParts = new TemplateMessage.MessagePart[]{
        //                                                                                new TemplateMessage.MessagePart{Name = "first",Value = "您好,您的订单" + order.OrderId + "支付成功"},
        //                                                                                new TemplateMessage.MessagePart{Name = "orderMoneySum",Color = "#ff3300",Value = "￥" + fee.ToString("F2")},
        //                                                                                new TemplateMessage.MessagePart{Name = "orderProductName",Value = prostr.ToString()},
        //                                                                                new TemplateMessage.MessagePart{Name = "remark",Value = ""}};

        //    templateMessage.Data = messateParts;

        //    return templateMessage;
        //}

        private static TemplateMessage GenerateWeixinMessageWhenOrderRefund(string templateId, SiteSettings settings, MemberInfo user, string orderId, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(user.OpenId))
            {
                return null;
            }
            string weixinToken = settings.WeixinToken;
            TemplateMessage message2 = new TemplateMessage
            {
                Url = "",
                TemplateId = templateId,
                Touser = user.OpenId
            };
            TemplateMessage.MessagePart[] partArray = new TemplateMessage.MessagePart[4];
            TemplateMessage.MessagePart part = new TemplateMessage.MessagePart
            {
                Name = "first",
                Value = "您好,您的订单号为" + orderId + "的订单已经退款"
            };
            partArray[0] = part;
            TemplateMessage.MessagePart part2 = new TemplateMessage.MessagePart
            {
                Name = "reason",
                Value = "-"
            };
            partArray[1] = part2;
            TemplateMessage.MessagePart part3 = new TemplateMessage.MessagePart
            {
                Name = "refund",
                Color = "#ff3300",
                Value = "￥" + amount.ToString("F2")
            };
            partArray[2] = part3;
            TemplateMessage.MessagePart part4 = new TemplateMessage.MessagePart
            {
                Name = "remark",
                Value = ""
            };
            partArray[3] = part4;
            message2.Data = partArray;
            return message2;
        }

        private static TemplateMessage GenerateWeixinMessageWhenOrderSend(string templateId, SiteSettings settings, MemberInfo user, OrderInfo order)
        {
            if (string.IsNullOrWhiteSpace(user.OpenId))
            {
                return null;
            }
            string weixinToken = settings.WeixinToken;
            TemplateMessage message2 = new TemplateMessage
            {
                Url = "",
                TemplateId = templateId,
                Touser = user.OpenId
            };
            TemplateMessage.MessagePart[] partArray = new TemplateMessage.MessagePart[5];
            TemplateMessage.MessagePart part = new TemplateMessage.MessagePart
            {
                Name = "first",
                Value = "您好,您的订单已发货"
            };
            partArray[0] = part;
            TemplateMessage.MessagePart part2 = new TemplateMessage.MessagePart
            {
                Name = "keyword1",
                Value = order.OrderId
            };
            partArray[1] = part2;
            TemplateMessage.MessagePart part3 = new TemplateMessage.MessagePart
            {
                Name = "keyword2",
                Value = order.ExpressCompanyName
            };
            partArray[2] = part3;
            TemplateMessage.MessagePart part4 = new TemplateMessage.MessagePart
            {
                Name = "keyword3",
                Value = order.ShipOrderNumber
            };
            partArray[3] = part4;
            TemplateMessage.MessagePart part5 = new TemplateMessage.MessagePart
            {
                Name = "remark",
                Value = "点击查看订单详情"
            };
            partArray[4] = part5;
            message2.Data = partArray;
            return message2;
        }

        private static TemplateMessage GenerateWeixinMessageWhenPasswordChange(string templateId, SiteSettings settings, MemberInfo user, string passowordType)
        {
            if (string.IsNullOrWhiteSpace(user.OpenId))
            {
                return null;
            }
            string weixinToken = settings.WeixinToken;
            TemplateMessage message2 = new TemplateMessage
            {
                Url = "",
                TemplateId = templateId,
                Touser = user.OpenId
            };
            TemplateMessage.MessagePart[] partArray = new TemplateMessage.MessagePart[4];
            TemplateMessage.MessagePart part = new TemplateMessage.MessagePart
            {
                Name = "first",
                Value = "您好"
            };
            partArray[0] = part;
            TemplateMessage.MessagePart part2 = new TemplateMessage.MessagePart
            {
                Name = "productName",
                Value = passowordType + "密码"
            };
            partArray[1] = part2;
            TemplateMessage.MessagePart part3 = new TemplateMessage.MessagePart
            {
                Name = "time",
                Value = DateTime.Now.ToString("M月d日 HH:mm")
            };
            partArray[2] = part3;
            TemplateMessage.MessagePart part4 = new TemplateMessage.MessagePart
            {
                Name = "remark",
                Value = ""
            };
            partArray[3] = part4;
            message2.Data = partArray;
            return message2;
        }

        private static MailMessage GenericOrderEmail(MessageTemplate template, SiteSettings settings, string UserName, string userEmail, string orderId, decimal total, string memo, string shippingType, string shippingName, string shippingAddress, string shippingZip, string shippingPhone, string shippingCell, string shippingEmail, string shippingBillno, decimal refundMoney, string closeReason, string servicePhone, string expressCompanyName, string shipOrderNumber)
        {
            MailMessage emailTemplate = MessageTemplateHelper.GetEmailTemplate(template, userEmail);
            if (emailTemplate == null)
            {
                return null;
            }
            emailTemplate.Subject = GenericOrderMessageFormatter(settings, UserName, emailTemplate.Subject, orderId, total, memo, shippingType, shippingName, shippingAddress, shippingZip, shippingPhone, shippingCell, shippingEmail, shippingBillno, refundMoney, closeReason, servicePhone, expressCompanyName, shipOrderNumber);
            emailTemplate.Body = GenericOrderMessageFormatter(settings, UserName, emailTemplate.Body, orderId, total, memo, shippingType, shippingName, shippingAddress, shippingZip, shippingPhone, shippingCell, shippingEmail, shippingBillno, refundMoney, closeReason, servicePhone, expressCompanyName, shipOrderNumber);
            return emailTemplate;
        }

        private static string GenericOrderMessageFormatter(SiteSettings settings, string UserName, string stringToFormat, string orderId, decimal total, string memo, string shippingType, string shippingName, string shippingAddress, string shippingZip, string shippingPhone, string shippingCell, string shippingEmail, string shippingBillno, decimal refundMoney, string closeReason, string servicePhone, string expressCompanyName, string shipOrderNumber)
        {
            stringToFormat = stringToFormat.Replace("$SiteName$", settings.SiteName.Trim());
            stringToFormat = stringToFormat.Replace("$UserName$", UserName);
            stringToFormat = stringToFormat.Replace("$OrderId$", orderId);
            stringToFormat = stringToFormat.Replace("$Total$", total.ToString("F"));
            stringToFormat = stringToFormat.Replace("$Memo$", memo);
            stringToFormat = stringToFormat.Replace("$Shipping_Type$", shippingType);
            stringToFormat = stringToFormat.Replace("$Shipping_Name$", shippingName);
            stringToFormat = stringToFormat.Replace("$Shipping_Addr$", shippingAddress);
            stringToFormat = stringToFormat.Replace("$Shipping_Zip$", shippingZip);
            stringToFormat = stringToFormat.Replace("$Shipping_Phone$", shippingPhone);
            stringToFormat = stringToFormat.Replace("$Shipping_Cell$", shippingCell);
            stringToFormat = stringToFormat.Replace("$Shipping_Email$", shippingEmail);
            stringToFormat = stringToFormat.Replace("$Shipping_Billno$", shippingBillno);
            stringToFormat = stringToFormat.Replace("$RefundMoney$", refundMoney.ToString("F"));
            stringToFormat = stringToFormat.Replace("$CloseReason$", closeReason);
            // 客服号码
            stringToFormat = stringToFormat.Replace("$ServicePhone$", servicePhone);
            // 物流公司
            stringToFormat = stringToFormat.Replace("$ExpressCompanyName$", expressCompanyName);
            // 物流单号
            stringToFormat = stringToFormat.Replace("$ShipOrderNumber$", shipOrderNumber);
            return stringToFormat;
        }

        private static void GenericOrderMessages(SiteSettings settings, string UserName, string userEmail, string orderId, decimal total, string memo, string shippingType, string shippingName, string shippingAddress, string shippingZip, string shippingPhone, string shippingCell, string shippingEmail, string shippingBillno, decimal refundMoney, string closeReason, MessageTemplate template, string servicePhone, string expressCompanyName, string shipOrderNumber, out MailMessage email, out string smsMessage, out string innerSubject, out string innerMessage)
        {
            email = null;
            smsMessage = null;
            innerSubject = (string)(innerMessage = null);
            if ((template != null) && (settings != null))
            {
                if (template.SendEmail && settings.EmailEnabled)
                {
                    email = GenericOrderEmail(template, settings, UserName, userEmail, orderId, total, memo, shippingType, shippingName, shippingAddress, shippingZip, shippingPhone, shippingCell, shippingEmail, shippingBillno, refundMoney, closeReason, servicePhone, expressCompanyName, shipOrderNumber);
                }
                if (template.SendSMS && settings.SMSEnabled)
                {
                    smsMessage = GenericOrderMessageFormatter(settings, UserName, template.SMSBody, orderId, total, memo, shippingType, shippingName, shippingAddress, shippingZip, shippingPhone, shippingCell, shippingEmail, shippingBillno, refundMoney, closeReason, servicePhone, expressCompanyName, shipOrderNumber);
                }
                if (template.SendInnerMessage)
                {
                    innerSubject = GenericOrderMessageFormatter(settings, UserName, template.InnerMessageSubject, orderId, total, memo, shippingType, shippingName, shippingAddress, shippingZip, shippingPhone, shippingCell, shippingEmail, shippingBillno, refundMoney, closeReason, servicePhone, expressCompanyName, shipOrderNumber);
                    innerMessage = GenericOrderMessageFormatter(settings, UserName, template.InnerMessageBody, orderId, total, memo, shippingType, shippingName, shippingAddress, shippingZip, shippingPhone, shippingCell, shippingEmail, shippingBillno, refundMoney, closeReason, servicePhone, expressCompanyName, shipOrderNumber);
                }
            }
        }

        private static MailMessage GenericUserEmail(MessageTemplate template, SiteSettings settings, string UserName, string userEmail, string password, string dealPassword)
        {
            MailMessage emailTemplate = MessageTemplateHelper.GetEmailTemplate(template, userEmail);
            if (emailTemplate == null)
            {
                return null;
            }
            emailTemplate.Subject = GenericUserMessageFormatter(settings, emailTemplate.Subject, UserName, userEmail, password, dealPassword);
            emailTemplate.Body = GenericUserMessageFormatter(settings, emailTemplate.Body, UserName, userEmail, password, dealPassword);
            return emailTemplate;
        }

        private static string GenericUserMessageFormatter(SiteSettings settings, string stringToFormat, string UserName, string userEmail, string password, string dealPassword)
        {
            stringToFormat = stringToFormat.Replace("$SiteName$", settings.SiteName.Trim());
            stringToFormat = stringToFormat.Replace("$UserName$", UserName.Trim());
            stringToFormat = stringToFormat.Replace("$Email$", userEmail.Trim());
            stringToFormat = stringToFormat.Replace("$Password$", password);
            stringToFormat = stringToFormat.Replace("$DealPassword$", dealPassword);
            return stringToFormat;
        }

        private static void GenericUserMessages(SiteSettings settings, string UserName, string userEmail, string password, string dealPassword, MessageTemplate template, out MailMessage email, out string smsMessage, out string innerSubject, out string innerMessage)
        {
            email = null;
            smsMessage = null;
            innerSubject = (string)(innerMessage = null);
            if ((template != null) && (settings != null))
            {
                if (template.SendEmail && settings.EmailEnabled)
                {
                    email = GenericUserEmail(template, settings, UserName, userEmail, password, dealPassword);
                }
                if (template.SendSMS && settings.SMSEnabled)
                {
                    smsMessage = GenericUserMessageFormatter(settings, template.SMSBody, UserName, userEmail, password, dealPassword);
                }
                if (template.SendInnerMessage)
                {
                    innerSubject = GenericUserMessageFormatter(settings, template.InnerMessageSubject, UserName, userEmail, password, dealPassword);
                    innerMessage = GenericUserMessageFormatter(settings, template.InnerMessageBody, UserName, userEmail, password, dealPassword);
                }
            }
        }

        private static string GetUserCellPhone(MemberInfo user)
        {
            if (user == null)
            {
                return null;
            }
            return user.CellPhone;
        }

        public static void OrderClosed(MemberInfo user, OrderInfo order, string reason)
        {
            if (user != null)
            {
                MessageTemplate template = MessageTemplateHelper.GetTemplate("OrderClosed");
                if (template != null)
                {
                    MailMessage email = null;
                    string innerSubject = null;
                    string innerMessage = null;
                    string smsMessage = null;
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    GenericOrderMessages(masterSettings, user.UserName, user.Email, order.OrderId, 0M, null, null, null, null, null, null, null, null, null, 0M, reason, template, masterSettings.ServicePhone, order.ExpressCompanyName, order.ShipOrderNumber, out email, out smsMessage, out innerSubject, out innerMessage );
                    TemplateMessage templateMessage = GenerateWeixinMessageWhenOrderClose(template.WeixinTemplateId, masterSettings, user, order, reason);
                    Send(template, masterSettings, user, false, email, innerSubject, innerMessage, smsMessage, templateMessage);
                }
            }
        }

        /// <summary>
        /// 订单创建时发送相关信息
        /// </summary>
        /// <param name="order"></param>
        /// <param name="user"></param>
        public static void OrderCreated(OrderInfo order, MemberInfo user)
        {
            if ((order != null) && (user != null))
            {
                MessageTemplate template = MessageTemplateHelper.GetTemplate("OrderCreated");
                if (template != null)
                {
                    MailMessage email = null;
                    string innerSubject = null;
                    string innerMessage = null;
                    string smsMessage = null;
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    GenericOrderMessages(masterSettings, user.UserName, user.Email, order.OrderId, order.GetTotal(), order.Remark, order.ModeName, order.ShipTo, order.Address, order.ZipCode, order.TelPhone, order.CellPhone, order.EmailAddress, order.ShipOrderNumber, order.RefundAmount, order.CloseReason, template, masterSettings.ServicePhone, order.ExpressCompanyName, order.ShipOrderNumber, out email, out smsMessage, out innerSubject, out innerMessage);
                    TemplateMessage templateMessage = GenerateWeixinMessageWhenOrderCreate(template.WeixinTemplateId, masterSettings, order, user);
                    Send(template, masterSettings, user, false, email, innerSubject, innerMessage, smsMessage, templateMessage);
                }
            }
        }

        public static void OrderCreatedSendManage(OrderInfo order, MemberInfo user)
        {
            if ((order != null) && (user != null))
            {
                MessageTemplate template = MessageTemplateHelper.GetTemplate("OrderCreated");
                if (template != null)
                {
                    MailMessage email = null;
                    string innerSubject = null;
                    string innerMessage = null;
                    string smsMessage = null;
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    GenericOrderMessages(masterSettings, user.UserName, user.Email, order.OrderId, order.GetTotal(), order.Remark, order.ModeName, order.ShipTo, order.Address, order.ZipCode, order.TelPhone, order.CellPhone, order.EmailAddress, order.ShipOrderNumber, order.RefundAmount, order.CloseReason, template, masterSettings.ServicePhone, order.ExpressCompanyName, order.ShipOrderNumber, out email, out smsMessage, out innerSubject, out innerMessage);
                    TemplateMessage templateMessage = GenerateWeixinMessageWhenOrderCreateManage(template.WeixinTemplateId, masterSettings, order, user);
                    Send(template, masterSettings, user, false, email, innerSubject, innerMessage, smsMessage, templateMessage);
                }
            }
        }

        public static void OrderPayment(MemberInfo user, string orderId, decimal amount)
        {
            if (user != null)
            {
                MessageTemplate template = MessageTemplateHelper.GetTemplate("OrderPayment");
                if (template != null)
                {
                    MailMessage email = null;
                    string innerSubject = null;
                    string innerMessage = null;
                    string smsMessage = null;
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    GenericOrderMessages(masterSettings, user.UserName, user.Email, orderId, amount, null, null, null, null, null, null, null, null, null, 0M, null, template, masterSettings.ServicePhone, "", "", out email, out smsMessage, out innerSubject, out innerMessage);
                    TemplateMessage templateMessage = GenerateWeixinMessageWhenOrderPay(template.WeixinTemplateId, masterSettings, user, orderId, amount);
                    Send(template, masterSettings, user, false, email, innerSubject, innerMessage, smsMessage, templateMessage);
                }
            }
        }

        public static void OrderRefund(MemberInfo user, string orderId, decimal amount)
        {
            if (user != null)
            {
                MessageTemplate template = MessageTemplateHelper.GetTemplate("OrderRefund");
                if (template != null)
                {
                    MailMessage email = null;
                    string innerSubject = null;
                    string innerMessage = null;
                    string smsMessage = null;
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    GenericOrderMessages(masterSettings, user.UserName, user.Email, orderId, 0M, null, null, null, null, null, null, null, null, null, amount, null, template, masterSettings.ServicePhone, "", "", out email, out smsMessage, out innerSubject, out innerMessage);
                    TemplateMessage templateMessage = GenerateWeixinMessageWhenOrderRefund(template.WeixinTemplateId, masterSettings, user, orderId, amount);
                    Send(template, masterSettings, user, false, email, innerSubject, innerMessage, smsMessage, templateMessage);
                }
            }
        }

        public static void OrderShipping(OrderInfo order, MemberInfo user)
        {
            if ((order != null) && (user != null))
            {
                MessageTemplate template = MessageTemplateHelper.GetTemplate("OrderShipping");
                if (template != null)
                {
                    MailMessage email = null;
                    string innerSubject = null;
                    string innerMessage = null;
                    string smsMessage = null;
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    GenericOrderMessages(masterSettings, user.UserName, user.Email, order.OrderId, order.GetTotal(), order.Remark, order.RealModeName, order.ShipTo, order.Address, order.ZipCode, order.TelPhone, order.CellPhone, order.EmailAddress, order.ShipOrderNumber, order.RefundAmount, order.CloseReason, template, masterSettings.ServicePhone, order.ExpressCompanyName, order.ShipOrderNumber, out email, out smsMessage, out innerSubject, out innerMessage);
                    TemplateMessage templateMessage = GenerateWeixinMessageWhenOrderSend(template.WeixinTemplateId, masterSettings, user, order);
                    Send(template, masterSettings, user, false, email, innerSubject, innerMessage, smsMessage, templateMessage);
                }
            }
        }

        private static void Send(MessageTemplate template, SiteSettings settings, MemberInfo user, bool sendFirst, MailMessage email, string innerSubject, string innerMessage, string smsMessage, TemplateMessage templateMessage)
        {
            if (template.SendEmail && (email != null))
            {
                if (sendFirst)
                {
                    EmailSender sender = CreateEmailSender(settings);
                    if (!((sender != null) && SendMail(email, sender)))
                    {
                        Emails.EnqueuEmail(email, settings);
                    }
                }
                else
                {
                    Emails.EnqueuEmail(email, settings);
                }
            }
            if (template.SendSMS)
            {
                string userCellPhone = GetUserCellPhone(user);
                if (!string.IsNullOrEmpty(userCellPhone))
                {
                    string str2;
                    SendSMS(userCellPhone, smsMessage, settings, out str2);
                }
            }
            if (template.SendInnerMessage)
            {

            }
            if ((template.SendWeixin && !string.IsNullOrWhiteSpace(template.WeixinTemplateId)) && (templateMessage != null))
            {
                //XTrace.WriteLine("调用微信发送接口------APPID:" + settings.WeixinAppId + "  ------ APPSECRET:" + settings.WeixinAppSecret);
                TemplateApi.SendMessage(TokenApi.GetToken_Message(settings.WeixinAppId, settings.WeixinAppSecret), templateMessage);
            }
        }

        internal static bool SendMail(MailMessage email, EmailSender sender)
        {
            string str;
            return SendMail(email, sender, out str);
        }

        internal static bool SendMail(MailMessage email, EmailSender sender, out string msg)
        {
            try
            {
                msg = "";
                return sender.Send(email, Encoding.GetEncoding(HiConfiguration.GetConfig().EmailEncoding));
            }
            catch (Exception exception)
            {
                msg = exception.Message;
                return false;
            }
        }

        public static SendStatus SendMail(string subject, string body, string emailTo, SiteSettings settings, out string msg)
        {
            msg = "";
            if ((((string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body)) || (string.IsNullOrEmpty(emailTo) || (subject.Trim().Length == 0))) || (body.Trim().Length == 0)) || (emailTo.Trim().Length == 0))
            {
                return SendStatus.RequireMsg;
            }
            if (!((settings != null) && settings.EmailEnabled))
            {
                return SendStatus.NoProvider;
            }
            EmailSender sender = CreateEmailSender(settings, out msg);
            if (sender == null)
            {
                return SendStatus.ConfigError;
            }
            MailMessage email = new MailMessage
            {
                IsBodyHtml = true,
                Priority = MailPriority.High,
                Body = body.Trim(),
                Subject = subject.Trim()
            };
            email.To.Add(emailTo);
            return (SendMail(email, sender, out msg) ? SendStatus.Success : SendStatus.Fail);
        }

        public static SendStatus SendMail(string subject, string body, string[] cc, string[] bcc, SiteSettings settings, out string msg)
        {
            msg = "";
            if (((string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body)) || ((subject.Trim().Length == 0) || (body.Trim().Length == 0))) || (((cc == null) || (cc.Length == 0)) && ((bcc == null) || (bcc.Length == 0))))
            {
                return SendStatus.RequireMsg;
            }
            if (!((settings != null) && settings.EmailEnabled))
            {
                return SendStatus.NoProvider;
            }
            EmailSender sender = CreateEmailSender(settings, out msg);
            if (sender == null)
            {
                return SendStatus.ConfigError;
            }
            MailMessage email = new MailMessage
            {
                IsBodyHtml = true,
                Priority = MailPriority.High,
                Body = body.Trim(),
                Subject = subject.Trim()
            };
            if ((cc != null) && (cc.Length > 0))
            {
                foreach (string str in cc)
                {
                    email.CC.Add(str);
                }
            }
            if ((bcc != null) && (bcc.Length > 0))
            {
                foreach (string str in bcc)
                {
                    email.Bcc.Add(str);
                }
            }
            return (SendMail(email, sender, out msg) ? SendStatus.Success : SendStatus.Fail);
        }

        public static SendStatus SendSMS(string phoneNumber, string message, SiteSettings settings, out string msg)
        {
            msg = "";
            if (((string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(message)) || (phoneNumber.Trim().Length == 0)) || (message.Trim().Length == 0))
            {
                return SendStatus.RequireMsg;
            }
            if (!((settings != null) && settings.SMSEnabled))
            {
                return SendStatus.NoProvider;
            }
            SMSSender sender = CreateSMSSender(settings, out msg);
            if (sender == null)
            {
                return SendStatus.ConfigError;
            }
            XTrace.WriteLine("短信发送对象：" + phoneNumber + "  短信发送内容：－－－" + message + "－－－");
            bool sendResultFlag = sender.Send(phoneNumber, message, out msg);
            XTrace.WriteLine("短信发送结果：" + phoneNumber + "  发送返回结果：－－－" + msg + "－－－");

            if (sendResultFlag)
            {
                return SendStatus.Success;
            }
            return SendStatus.Fail;
            //return (sender.Send(phoneNumber, message, out msg) ? SendStatus.Success : SendStatus.Fail);
        }

        public static SendStatus SendSMS(string[] phoneNumbers, string message, SiteSettings settings, out string msg)
        {
            msg = "";
            if ((((phoneNumbers == null) || string.IsNullOrEmpty(message)) || (phoneNumbers.Length == 0)) || (message.Trim().Length == 0))
            {
                return SendStatus.RequireMsg;
            }
            if (!((settings != null) && settings.SMSEnabled))
            {
                return SendStatus.NoProvider;
            }
            SMSSender sender = CreateSMSSender(settings, out msg);
            if (sender == null)
            {
                return SendStatus.ConfigError;
            }
            return (sender.Send(phoneNumbers, message, out msg) ? SendStatus.Success : SendStatus.Fail);
        }

        public static void UserDealPasswordChanged(MemberInfo user, string changedDealPassword)
        {
            if (user != null)
            {
                MessageTemplate template = MessageTemplateHelper.GetTemplate("ChangedDealPassword");
                if (template != null)
                {
                    MailMessage email = null;
                    string innerSubject = null;
                    string innerMessage = null;
                    string smsMessage = null;
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    GenericUserMessages(masterSettings, user.UserName, user.Email, null, changedDealPassword, template, out email, out smsMessage, out innerSubject, out innerMessage);
                    TemplateMessage templateMessage = GenerateWeixinMessageWhenPasswordChange(template.WeixinTemplateId, masterSettings, user, "交易");
                    Send(template, masterSettings, user, false, email, innerSubject, innerMessage, smsMessage, templateMessage);
                }
            }
        }

        public static void UserPasswordChanged(MemberInfo user, string changedPassword)
        {
            if (user != null)
            {
                MessageTemplate template = MessageTemplateHelper.GetTemplate("ChangedPassword");
                if (template != null)
                {
                    MailMessage email = null;
                    string innerSubject = null;
                    string innerMessage = null;
                    string smsMessage = null;
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    GenericUserMessages(masterSettings, user.UserName, user.Email, changedPassword, null, template, out email, out smsMessage, out innerSubject, out innerMessage);
                    TemplateMessage templateMessage = GenerateWeixinMessageWhenPasswordChange(template.WeixinTemplateId, masterSettings, user, "登录");
                    Send(template, masterSettings, user, false, email, innerSubject, innerMessage, smsMessage, templateMessage);
                }
            }
        }

        public static void UserPasswordForgotten(MemberInfo user, string resetPassword)
        {
            if (user != null)
            {
                MessageTemplate template = MessageTemplateHelper.GetTemplate("ForgottenPassword");
                if (template != null)
                {
                    MailMessage email = null;
                    string innerSubject = null;
                    string innerMessage = null;
                    string smsMessage = null;
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    GenericUserMessages(masterSettings, user.UserName, user.Email, resetPassword, null, template, out email, out smsMessage, out innerSubject, out innerMessage);
                    TemplateMessage templateMessage = GenerateWeixinMessageWhenFindPassword(template.WeixinTemplateId, masterSettings, user, resetPassword);
                    Send(template, masterSettings, user, true, email, innerSubject, innerMessage, smsMessage, templateMessage);
                }
            }
        }

        public static void UserRegister(MemberInfo user, string createPassword)
        {
            if (user != null)
            {
                MessageTemplate template = MessageTemplateHelper.GetTemplate("NewUserAccountCreated");
                if (template != null)
                {
                    MailMessage email = null;
                    string innerSubject = null;
                    string innerMessage = null;
                    string smsMessage = null;
                    SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);
                    GenericUserMessages(masterSettings, user.UserName, user.Email, createPassword, null, template, out email, out smsMessage, out innerSubject, out innerMessage);
                    Send(template, masterSettings, user, true, email, innerSubject, innerMessage, smsMessage, null);
                }
            }
        }
    }
}

