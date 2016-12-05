using Hidistro.ControlPanel.Commodities;
using Hidistro.Core;
using Hidistro.Entities.Commodities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
namespace Hidistro.UI.Web.API
{
	public class TaobaoProductHandler : System.Web.IHttpHandler
	{
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
		private void DownloadImage(ProductInfo product, string imageUrls, System.Web.HttpContext context)
		{
			imageUrls = System.Web.HttpUtility.UrlDecode(imageUrls);
			string[] strArray = imageUrls.Split(new char[]
			{
				';'
			});
			int num = 1;
			string[] array = strArray;
			for (int i = 0; i < array.Length; i++)
			{
				string str = array[i];
				string str2 = string.Format("/Storage/master/product/images/{0}", System.Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.InvariantCulture) + str.Substring(str.LastIndexOf('.')));
				string str3 = str2.Replace("/images/", "/thumbs40/40_");
				string str4 = str2.Replace("/images/", "/thumbs60/60_");
				string str5 = str2.Replace("/images/", "/thumbs100/100_");
				string str6 = str2.Replace("/images/", "/thumbs160/160_");
				string str7 = str2.Replace("/images/", "/thumbs180/180_");
				string str8 = str2.Replace("/images/", "/thumbs220/220_");
				string str9 = str2.Replace("/images/", "/thumbs310/310_");
				string str10 = str2.Replace("/images/", "/thumbs410/410_");
				string fileName = System.Web.HttpContext.Current.Request.MapPath(Globals.ApplicationPath + str2);
				WebClient client = new WebClient();
				try
				{
					client.DownloadFile(str, fileName);
					ResourcesHelper.CreateThumbnail(fileName, context.Request.MapPath(Globals.ApplicationPath + str3), 40, 40);
					ResourcesHelper.CreateThumbnail(fileName, context.Request.MapPath(Globals.ApplicationPath + str4), 60, 60);
					ResourcesHelper.CreateThumbnail(fileName, context.Request.MapPath(Globals.ApplicationPath + str5), 100, 100);
					ResourcesHelper.CreateThumbnail(fileName, context.Request.MapPath(Globals.ApplicationPath + str6), 160, 160);
					ResourcesHelper.CreateThumbnail(fileName, context.Request.MapPath(Globals.ApplicationPath + str7), 180, 180);
					ResourcesHelper.CreateThumbnail(fileName, context.Request.MapPath(Globals.ApplicationPath + str8), 220, 220);
					ResourcesHelper.CreateThumbnail(fileName, context.Request.MapPath(Globals.ApplicationPath + str9), 310, 310);
					ResourcesHelper.CreateThumbnail(fileName, context.Request.MapPath(Globals.ApplicationPath + str10), 410, 410);
					switch (num)
					{
					case 1:
						product.ImageUrl1 = str2;
						product.ThumbnailUrl40 = str3;
						product.ThumbnailUrl60 = str4;
						product.ThumbnailUrl100 = str5;
						product.ThumbnailUrl160 = str6;
						product.ThumbnailUrl180 = str7;
						product.ThumbnailUrl220 = str8;
						product.ThumbnailUrl310 = str9;
						product.ThumbnailUrl410 = str10;
						break;
					case 2:
						product.ImageUrl2 = str2;
						break;
					case 3:
						product.ImageUrl3 = str2;
						break;
					case 4:
						product.ImageUrl4 = str2;
						break;
					case 5:
						product.ImageUrl5 = str2;
						break;
					}
					num++;
				}
				catch
				{
				}
			}
		}
		private System.Collections.Generic.Dictionary<string, SKUItem> GetSkus(ProductInfo product, decimal weight, System.Web.HttpContext context)
		{
			string str = context.Request.Form["SkuString"];
			System.Collections.Generic.Dictionary<string, SKUItem> result;
			if (string.IsNullOrEmpty(str))
			{
				product.HasSKU = false;
				System.Collections.Generic.Dictionary<string, SKUItem> dictionary2 = new System.Collections.Generic.Dictionary<string, SKUItem>();
				SKUItem item = new SKUItem
				{
					SkuId = "0",
					SKU = product.ProductCode,
					SalePrice = decimal.Parse(context.Request.Form["SalePrice"]),
					CostPrice = 0m,
					Stock = int.Parse(context.Request.Form["Stock"]),
					Weight = weight
				};
				dictionary2.Add("0", item);
				result = dictionary2;
			}
			else
			{
				product.HasSKU = true;
				System.Collections.Generic.Dictionary<string, SKUItem> dictionary3 = new System.Collections.Generic.Dictionary<string, SKUItem>();
				string[] array = System.Web.HttpUtility.UrlDecode(str).Split(new char[]
				{
					'|'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string str2 = array[i];
					string[] strArray = str2.Split(new char[]
					{
						','
					});
					SKUItem item2 = new SKUItem
					{
						SKU = strArray[0],
						Weight = weight,
						Stock = int.Parse(strArray[1]),
						SalePrice = decimal.Parse(strArray[2])
					};
					string str3 = strArray[3];
					string str4 = "";
					string[] array2 = str3.Split(new char[]
					{
						';'
					});
					for (int j = 0; j < array2.Length; j++)
					{
						string str5 = array2[j];
						string[] strArray2 = str5.Split(new char[]
						{
							':'
						});
						int specificationId = ProductTypeHelper.GetSpecificationId(product.TypeId.Value, strArray2[0]);
						int specificationValueId = ProductTypeHelper.GetSpecificationValueId(specificationId, strArray2[1]);
						str4 = str4 + specificationValueId + "_";
						item2.SkuItems.Add(specificationId, specificationValueId);
					}
					item2.SkuId = str4.Substring(0, str4.Length - 1);
					dictionary3.Add(item2.SkuId, item2);
				}
				result = dictionary3;
			}
			return result;
		}
		private TaobaoProductInfo GetTaobaoProduct(System.Web.HttpContext context)
		{
			TaobaoProductInfo info = new TaobaoProductInfo
			{
				Cid = long.Parse(context.Request.Form["Cid"]),
				StuffStatus = context.Request.Form["StuffStatus"],
				LocationState = context.Request.Form["LocationState"],
				LocationCity = context.Request.Form["LocationCity"],
				FreightPayer = context.Request.Form["FreightPayer"]
			};
			if (!string.IsNullOrEmpty(context.Request.Form["PostFee"]))
			{
				info.PostFee = decimal.Parse(context.Request.Form["PostFee"]);
			}
			if (!string.IsNullOrEmpty(context.Request.Form["ExpressFee"]))
			{
				info.ExpressFee = decimal.Parse(context.Request.Form["ExpressFee"]);
			}
			if (!string.IsNullOrEmpty(context.Request.Form["EMSFee"]))
			{
				info.EMSFee = decimal.Parse(context.Request.Form["EMSFee"]);
			}
			info.HasInvoice = bool.Parse(context.Request.Form["HasInvoice"]);
			info.HasWarranty = bool.Parse(context.Request.Form["HasWarranty"]);
			info.HasDiscount = false;
			info.ListTime = System.DateTime.Now;
			info.PropertyAlias = context.Request.Form["PropertyAlias"];
			info.InputPids = context.Request.Form["InputPids"];
			info.InputStr = context.Request.Form["InputStr"];
			info.SkuProperties = context.Request.Form["SkuProperties"];
			info.SkuQuantities = context.Request.Form["SkuQuantities"];
			info.SkuPrices = context.Request.Form["SkuPrices"];
			info.SkuOuterIds = context.Request.Form["SkuOuterIds"];
			return info;
		}
		private string LoadImage(string path)
		{
			byte[] buffer = null;
			using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
			{
				buffer = new byte[stream.Length];
				stream.Read(buffer, 0, (int)stream.Length);
			}
			return System.Convert.ToBase64String(buffer);
		}
		public void ProcessRequest(System.Web.HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			GzipExtention.Gzip(context);
			string text = context.Request["action"];
			if (text != null)
			{
				if (text == "TaobaoProductIsExit")
				{
					this.ProcessTaobaoProductIsExit(context);
					return;
				}
				if (text == "TaobaoProductDown")
				{
					this.ProcessTaobaoProductDown(context);
					return;
				}
			}
			context.Response.Write("error");
		}
		private void ProcessTaobaoProductDown(System.Web.HttpContext context)
		{
			ProductInfo product = new ProductInfo
			{
				CategoryId = 0,
				BrandId = new int?(0),
				ProductName = System.Web.HttpUtility.UrlDecode(context.Request.Form["ProductName"]),
				ProductCode = context.Request.Form["ProductCode"],
				Description = System.Web.HttpUtility.UrlDecode(context.Request.Form["Description"])
			};
			if (context.Request.Form["SaleStatus"] == "onsale")
			{
				product.SaleStatus = ProductSaleStatus.OnSale;
			}
			else
			{
				product.SaleStatus = ProductSaleStatus.OnStock;
			}
			product.AddedDate = System.DateTime.Parse(context.Request.Form["AddedDate"]);
			product.TaobaoProductId = long.Parse(context.Request.Form["TaobaoProductId"]);
			string str = context.Request.Form["ImageUrls"];
			if (!string.IsNullOrEmpty(str))
			{
				this.DownloadImage(product, str, context);
			}
			product.TypeId = new int?(ProductTypeHelper.GetTypeId(context.Request.Form["TypeName"]));
			decimal weight = decimal.Parse(context.Request.Form["Weight"]);
			System.Collections.Generic.Dictionary<string, SKUItem> skus = this.GetSkus(product, weight, context);
			ProductActionStatus status = ProductHelper.AddProduct(product, skus, null, null);
			if (status == ProductActionStatus.Success)
			{
				TaobaoProductInfo taobaoProduct = this.GetTaobaoProduct(context);
				taobaoProduct.ProductId = product.ProductId;
				taobaoProduct.ProTitle = product.ProductName;
				taobaoProduct.Num = (long)product.Stock;
				ProductHelper.UpdateToaobProduct(taobaoProduct);
			}
			context.Response.Write(status.ToString());
		}
		private void ProcessTaobaoProductIsExit(System.Web.HttpContext context)
		{
			bool flag = ProductHelper.IsExitTaobaoProduct(long.Parse(context.Request.Form["taobaoProductId"]));
			context.Response.Write(flag.ToString());
		}
	}
}
