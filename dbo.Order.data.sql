﻿SET IDENTITY_INSERT [dbo].[Order] ON
INSERT INTO [dbo].[Order] ([OrderId], [CarId], [OrderStatus], [StartMileage], [StopMileage], [TotalCost], [CustomerName], [CustomerAddress], [CustomerPhone], [CustomerEmail], [DateCreated]) VALUES (7, 88, 0, 78000, 0, CAST(0.00 AS Decimal(18, 2)), N'test', N'test', N'123123123', N'test@wp.pl', N'2021-08-11 22:22:30')
INSERT INTO [dbo].[Order] ([OrderId], [CarId], [OrderStatus], [StartMileage], [StopMileage], [TotalCost], [CustomerName], [CustomerAddress], [CustomerPhone], [CustomerEmail], [DateCreated]) VALUES (8, 84, 1, 280000, 0, CAST(0.00 AS Decimal(18, 2)), N'test', N'test', N'123123123', N'test@wp.pl', N'2021-08-11 22:23:26')
INSERT INTO [dbo].[Order] ([OrderId], [CarId], [OrderStatus], [StartMileage], [StopMileage], [TotalCost], [CustomerName], [CustomerAddress], [CustomerPhone], [CustomerEmail], [DateCreated]) VALUES (9, 83, 0, 239000, 0, CAST(0.00 AS Decimal(18, 2)), N'test', N'test', N'123123123', N'test@wp.pl', N'2021-08-11 22:34:31')
INSERT INTO [dbo].[Order] ([OrderId], [CarId], [OrderStatus], [StartMileage], [StopMileage], [TotalCost], [CustomerName], [CustomerAddress], [CustomerPhone], [CustomerEmail], [DateCreated]) VALUES (10, 85, 0, 179975, 0, CAST(0.00 AS Decimal(18, 2)), N'test', N'test', N'123123123', N'test@wp.pl', N'2021-08-11 22:35:37')
SET IDENTITY_INSERT [dbo].[Order] OFF
