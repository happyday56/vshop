-- 会员等级
DELETE FROM aspnet_MemberGrades

INSERT INTO aspnet_MemberGrades([Name], [Description], [Points], [IsDefault], [Discount]) VALUES(N'普通会员', N'普通会员', 0, 1, 100)
INSERT INTO aspnet_MemberGrades([Name], [Description], [Points], [IsDefault], [Discount]) VALUES(N'高级会员', N'高级会员', 1000, 0, 90)
INSERT INTO aspnet_MemberGrades([Name], [Description], [Points], [IsDefault], [Discount]) VALUES(N'VIP会员', N'VIP会员', 5000, 0, 80)

/****** 对象:  Table [dbo].[Hishop_ExpressTemplates]    脚本日期: 11/17/2010 17:47:26 ******/
SET IDENTITY_INSERT [dbo].[Hishop_ExpressTemplates] ON
INSERT [dbo].[Hishop_ExpressTemplates] ([ExpressId], [ExpressName], [XmlFile], [IsUse]) VALUES (2, N'圆通速递', N'201046133312.xml', 1)
INSERT [dbo].[Hishop_ExpressTemplates] ([ExpressId], [ExpressName], [XmlFile], [IsUse]) VALUES (3, N'申通物流', N'20101117152320.xml', 1)
INSERT [dbo].[Hishop_ExpressTemplates] ([ExpressId], [ExpressName], [XmlFile], [IsUse]) VALUES (4, N'邮政平邮', N'20101117155058.xml', 1)
INSERT [dbo].[Hishop_ExpressTemplates] ([ExpressId], [ExpressName], [XmlFile], [IsUse]) VALUES (5, N'海航天天快递', N'20101117155351.xml', 1)
INSERT [dbo].[Hishop_ExpressTemplates] ([ExpressId], [ExpressName], [XmlFile], [IsUse]) VALUES (6, N'宅急送', N'20101117155840.xml', 1)
INSERT [dbo].[Hishop_ExpressTemplates] ([ExpressId], [ExpressName], [XmlFile], [IsUse]) VALUES (8, N'韵达快运', N'20101117161058.xml', 1)
INSERT [dbo].[Hishop_ExpressTemplates] ([ExpressId], [ExpressName], [XmlFile], [IsUse]) VALUES (9, N'EMS', N'20101117162921.xml', 1)
INSERT [dbo].[Hishop_ExpressTemplates] ([ExpressId], [ExpressName], [XmlFile], [IsUse]) VALUES (10, N'顺丰快递', N'20101117163245.xml', 1)
INSERT [dbo].[Hishop_ExpressTemplates] ([ExpressId], [ExpressName], [XmlFile], [IsUse]) VALUES (11, N'中通速递', N'20101117163638.xml', 1)
SET IDENTITY_INSERT [dbo].[Hishop_ExpressTemplates] OFF