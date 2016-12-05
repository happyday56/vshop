
using Hidistro.Core;
using Hidistro.Core.Entities;
using Hidistro.Entities.Members;
using Hidistro.SaleSystem.Vshop;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace Hidistro.UI.Web.API
{
    public class GetQRCode : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        public static Image DownloadImage(string httpImg)
        {
            WebRequest req = WebRequest.Create(httpImg);

            var rsp = req.GetResponse();
            Stream stream = rsp.GetResponseStream();
            System.Drawing.Image img = null;
            using (MemoryStream ms = new MemoryStream())
            {
                int b;
                while ((b = stream.ReadByte()) != -1)
                {
                    ms.WriteByte((byte)b);
                }
                img = System.Drawing.Image.FromStream(ms);
            }

            if (null != stream)
            {
                stream.Close();
            }
            if (null != rsp)
            {
                rsp.Close();
            }
            
            return img;
        }

        public static Image CombinImage(Image imgBack, Image img, string userName, string userImg, bool isOneBuy, string siteFlag = "")
        {
            if (siteFlag.EqualIgnoreCase("las"))
            {
                if (img.Height != 65 || img.Width != 65)
                {
                    img = GetQRCode.KiResizeImage(img, 152, 152, 0);//250
                }
                Graphics graphic = Graphics.FromImage(imgBack);
                graphic.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);
                //graphic.DrawImage(img, imgBack.Width / 2 - img.Width / 2 + 10, imgBack.Width / 2 - img.Width / 2 + 85, 136, 136);
                if (isOneBuy)
                {
                    if (!string.IsNullOrEmpty(userName))
                    {
                        Font font = new Font("ËÎÌו", 28);
                        SolidBrush brush = new SolidBrush(Color.White);
                        graphic.DrawString(userName, font, brush, new PointF(80, 510));
                    }

                    graphic.DrawImage(img, 312, 215, 186, 186);
                }
                else
                {
                    graphic.DrawImage(img, imgBack.Width / 2 - img.Width / 2 + 10, imgBack.Width / 2 - img.Width / 2 + 85, 136, 136);
                }
                GC.Collect();
            }
            else if (siteFlag.EqualIgnoreCase("ls"))
            {
                if (img.Height != 65 || img.Width != 65)
                {
                    img = GetQRCode.KiResizeImage(img, 152, 152, 0);
                }
                Graphics graphic = Graphics.FromImage(imgBack);
                graphic.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);
                if (!string.IsNullOrEmpty(userName))
                {
                    Font font = new Font("ËÎÌו", 18);
                    SolidBrush brush = new SolidBrush(Color.Red);
                    graphic.DrawString(userName, font, brush, new PointF(230, 510));
                }
                if (!string.IsNullOrEmpty(userImg))
                {
                    Image imgUser = DownloadImage(userImg);
                    if (null != imgUser)
                    {
                        graphic.DrawImage(imgUser, 46, 488, 85, 85);
                    }                    
                }
                graphic.DrawImage(img, 167, 670, 145, 145);
                GC.Collect();
            }
            else
            {
                if (img.Height != 65 || img.Width != 65)
                {
                    img = GetQRCode.KiResizeImage(img, 152, 152, 0);
                }
                Graphics graphic = Graphics.FromImage(imgBack);
                graphic.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);
                graphic.DrawImage(img, 0, 0, 152, 152);
                GC.Collect();
            }
            return imgBack;
        }

        public static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            Image image;
            try
            {
                Image bitmap = new Bitmap(newW, newH);
                Graphics graphic = Graphics.FromImage(bitmap);
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                graphic.Dispose();
                image = bitmap;
            }
            catch
            {
                image = null;
            }
            return image;
        }

        public void ProcessRequest(HttpContext context)
        {
            string item = context.Request["code"];
            string referralId = context.Request["ReferralUserId"];
            string productId = context.Request["ProductId"];
            string ptTypeId = context.Request["PTTypeId"];
            int currUserId = 0;
            string currUserName = "";
            string currUserImg = "";
            bool isOneBuy = false;

            if (!string.IsNullOrEmpty(referralId))
            {
                currUserId = int.Parse(referralId);
                MemberInfo currMember = MemberProcessor.GetMember(currUserId);
                if (null != currMember)
                {
                    currUserName = currMember.UserName;
                    currUserImg = currMember.UserHead;
                }
                referralId = "&ReferralUserId=" + referralId;
                
            }
            if (!string.IsNullOrEmpty(productId))
            {
                productId = "&ProductId=" + productId;
            }
            if (!string.IsNullOrEmpty(ptTypeId))
            {
                if (ptTypeId.Equals("2"))
                {
                    isOneBuy = true;
                }
                ptTypeId = "&PTTypeId=" + ptTypeId;
            }
            if (!string.IsNullOrEmpty(item))
            {
                SiteSettings masterSettings = SettingsManager.GetMasterSettings(false);

                QRCodeEncoder qRCodeEncoder = new QRCodeEncoder()
                {
                    QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                    QRCodeScale = 4,
                    QRCodeVersion = 8,
                    QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
                };
                item += referralId + productId + ptTypeId;
                Image image = qRCodeEncoder.Encode(item);
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Png);
                //string str = context.Server.MapPath("/Storage/master/QRcord" + masterSettings.SiteFlag + ".jpg");
                string str = context.Server.MapPath("/Storage/master/QRcord.jpg");
                if (masterSettings.SiteFlag.EqualIgnoreCase("las"))
                {
                    if (isOneBuy)
                    {
                        str = context.Server.MapPath("/Storage/master/QRcord" + masterSettings.SiteFlag + ".jpg");
                    }
                }
                else
                {
                    str = context.Server.MapPath("/Storage/master/QRcord" + masterSettings.SiteFlag + ".jpg");
                }
                Image image1 = Image.FromFile(str);
                MemoryStream memoryStream1 = new MemoryStream();
                if (masterSettings.SiteFlag.EqualIgnoreCase("las"))
                {
                    if (isOneBuy)
                    {
                        GetQRCode.CombinImage(image1, image, currUserName, currUserImg, isOneBuy, masterSettings.SiteFlag).Save(memoryStream1, ImageFormat.Png);
                    }
                    else
                    {
                        GetQRCode.CombinImage(image1, image, currUserName, currUserImg, isOneBuy, "").Save(memoryStream1, ImageFormat.Png);
                    }
                }
                else if (masterSettings.SiteFlag.EqualIgnoreCase("ls"))
                {
                    GetQRCode.CombinImage(image1, image, currUserName, currUserImg, isOneBuy, masterSettings.SiteFlag).Save(memoryStream1, ImageFormat.Png);
                }
                else
                {
                    GetQRCode.CombinImage(image1, image, currUserName, currUserImg, isOneBuy, "").Save(memoryStream1, ImageFormat.Png);
                }
                context.Response.ClearContent();
                context.Response.ContentType = "image/png";
                context.Response.BinaryWrite(memoryStream1.ToArray());
                memoryStream.Dispose();
                memoryStream1.Dispose();

            }
            context.Response.Flush();
            context.Response.End();
            //context.Response.Close();
        }
    }
}