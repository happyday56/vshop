
-- 1206
ALTER VIEW [dbo].[vw_Hishop_BrowseProductListNoBuy]
AS
SELECT     p.CategoryId, p.TypeId, p.BrandId, p.ProductId, p.ProductName, p.ProductCode, p.ShortDescription, p.MarketPrice, p.ThumbnailUrl40, p.ThumbnailUrl60, 
                      p.ThumbnailUrl100, p.ThumbnailUrl160, p.ThumbnailUrl180, p.ThumbnailUrl220, p.ThumbnailUrl310, p.SaleStatus, p.DisplaySequence, p.MainCategoryPath, 
                      p.ExtendCategoryPath, p.SaleCounts, p.ShowSaleCounts, p.AddedDate, p.VistiCounts,
                          (SELECT     MIN(SalePrice) AS Expr1
                            FROM          dbo.Hishop_SKUs
                            WHERE      (ProductId = p.ProductId)) AS SalePrice,
                          (SELECT     TOP (1) SkuId
                            FROM          dbo.Hishop_SKUs AS Hishop_SKUs_3
                            WHERE      (ProductId = p.ProductId)
                            ORDER BY SalePrice) AS SkuId,
                          (SELECT     SUM(Stock) AS Expr1
                            FROM          dbo.Hishop_SKUs AS Hishop_SKUs_2
                            WHERE      (ProductId = p.ProductId)) AS Stock,
                          (SELECT     TOP (1) Weight
                            FROM          dbo.Hishop_SKUs AS Hishop_SKUs_1
                            WHERE      (ProductId = p.ProductId)
                            ORDER BY SalePrice) AS Weight,
                          (SELECT     COUNT(*) AS Expr1
                            FROM          dbo.Taobao_Products
                            WHERE      (ProductId = p.ProductId)) AS IsMakeTaobao,
                          (SELECT     COUNT(*) AS Expr1
                            FROM          dbo.Hishop_ProductReviews
                            WHERE      (ProductId = p.ProductId)) AS ReviewsCount, p.HomePicUrl, p.IsDisplayHome, p.AddUserId, p.VirtualPointRate, p.IsCross, p.MaxCross, 
                      category.Name AS CategoryName, type.TypeName, brand.BrandName
FROM         dbo.Hishop_Products AS p LEFT OUTER JOIN
                      dbo.Hishop_BrandCategories AS brand ON p.BrandId = brand.BrandId LEFT OUTER JOIN
                      dbo.Hishop_Categories AS category ON p.CategoryId = category.CategoryId LEFT OUTER JOIN
                      dbo.Hishop_ProductTypes AS type ON p.TypeId = type.TypeId

GO


