namespace Hidistro.Entities.Commodities
{
    using Hidistro.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ProductInfo
    {
        private SKUItem defaultSku;
        private Dictionary<string, SKUItem> skus;

        public DateTime AddedDate { get; set; }

        public int? BrandId { get; set; }

        public int CategoryId { get; set; }

        public decimal CostPrice
        {
            get
            {
                return this.DefaultSku.CostPrice;
            }
        }

        public SKUItem DefaultSku
        {
            get
            {
                return (this.defaultSku ?? (this.defaultSku = this.Skus.Values.First<SKUItem>()));
            }
        }

        public string Description { get; set; }

        public int DisplaySequence { get; set; }

        public string ExtendCategoryPath { get; set; }

        public bool HasSKU { get; set; }

        public string ImageUrl1 { get; set; }

        public string ImageUrl2 { get; set; }

        public string ImageUrl3 { get; set; }

        public string ImageUrl4 { get; set; }

        public string ImageUrl5 { get; set; }

        public bool IsfreeShipping { get; set; }

        public string MainCategoryPath { get; set; }

        public decimal? MarketPrice { get; set; }

        public decimal MaxSalePrice
        {
            get
            {
                decimal[] maxSalePrice = new decimal[1];
                foreach (SKUItem item in from sku in this.Skus.Values
                    where sku.SalePrice > maxSalePrice[0]
                    select sku)
                {
                    maxSalePrice[0] = item.SalePrice;
                }
                return maxSalePrice[0];
            }
        }

        public decimal MinSalePrice
        {
            get
            {
                decimal[] minSalePrice = new decimal[] { 79228162514264337593543950335M };
                foreach (SKUItem item in from sku in this.Skus.Values
                    where sku.SalePrice < minSalePrice[0]
                    select sku)
                {
                    minSalePrice[0] = item.SalePrice;
                }
                return minSalePrice[0];
            }
        }

        public string ProductCode { get; set; }

        public int ProductId { get; set; }

        [HtmlCoding]
        public string ProductName { get; set; }

        public int SaleCounts { get; set; }

        public decimal SalePrice { get; set; }

        public ProductSaleStatus SaleStatus { get; set; }

        [HtmlCoding]
        public string ShortDescription { get; set; }

        public int ShowSaleCounts { get; set; }

        public string SKU
        {
            get
            {
                return this.DefaultSku.SKU;
            }
        }

        public string SkuId
        {
            get
            {
                return this.DefaultSku.SkuId;
            }
        }

        public Dictionary<string, SKUItem> Skus
        {
            get
            {
                return (this.skus ?? (this.skus = new Dictionary<string, SKUItem>()));
            }
        }

        public int Stock
        {
            get
            {
                return this.Skus.Values.Sum<SKUItem>(((Func<SKUItem, int>) (sku => sku.Stock)));
            }
        }

        public long TaobaoProductId { get; set; }

        public string ThumbnailUrl100 { get; set; }

        public string ThumbnailUrl160 { get; set; }

        public string ThumbnailUrl180 { get; set; }

        public string ThumbnailUrl220 { get; set; }

        public string ThumbnailUrl310 { get; set; }

        public string ThumbnailUrl40 { get; set; }

        public string ThumbnailUrl410 { get; set; }

        public string ThumbnailUrl60 { get; set; }

        public int? TypeId { get; set; }

        public string Unit { get; set; }

        public int VistiCounts { get; set; }

        public decimal Weight
        {
            get
            {
                return this.DefaultSku.Weight;
            }
        }

        [HtmlCoding]
        public string ProductShortLetter { get; set; }

        public string ProductLetter { get; set; }

        public string LetterImgUrl1 { get; set; }

        public string LetterImgUrl2 { get; set; }

        public string LetterImgUrl3 { get; set; }

        public string LetterImgUrl4 { get; set; }

        public string LetterImgUrl5 { get; set; }

        public string LetterTbUrl40 { get; set; }

        public string LetterTbUrl60 { get; set; }

        public string LetterTbUrl100 { get; set; }

        public string LetterTbUrl160 { get; set; }

        public string LetterTbUrl180 { get; set; }

        public string LetterTbUrl220 { get; set; }

        public string LetterTbUrl310 { get; set; }

        public string LetterTbUrl410 { get; set; }

        /// <summary>
        /// 是否分销商订购购买商品标识
        /// </summary>
        public int IsDistributorBuy { get; set; }

        /// <summary>
        /// 商品的虚拟币使用比率
        /// </summary>
        public decimal? VirtualPointRate { get; set; }

        /// <summary>
        /// 商品方案的添加时间
        /// </summary>
        public DateTime LetterAddDate { get; set; }

        public string HomePicUrl { get; set; }

        public int IsDisplayHome { get; set; }

        public int AddUserId { get; set; }

        public int GoodCounts { get; set; }

        public int IsCross { get; set; }

        public int MaxCross { get; set; }

        public int PTTypeId { get; set; }

    }
}

