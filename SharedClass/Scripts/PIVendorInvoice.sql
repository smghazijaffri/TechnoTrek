USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[PIVendorInvoice]    Script Date: 5/11/2024 4:14:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[PIVendorInvoice]
@PurchaseInvoiceID varchar(50),
@VendorInvoiceNumber varchar(50),
@VendorInvoiceDate date,
@CreationDate datetime,
@ForInsert int,
@Output nvarchar(50) output
as
begin
if(@ForInsert = 1)
begin
 IF NOT EXISTS (SELECT 1   
                           FROM   PI_VendorInvoice  
                           WHERE  PurchaseInvoiceID = @PurchaseInvoiceID) 
begin
INSERT INTO [dbo].[PI_VendorInvoice]
           ([PurchaseInvoiceID]
           ,[VendorInvoiceNumber]
           ,[VendorInvoiceDate]
           ,[CreationDate])
     VALUES
           (@PurchaseInvoiceID 
           ,@VendorInvoiceNumber
           ,@VendorInvoiceDate
           ,@CreationDate)
end
else 
begin
UPDATE [dbo].[PI_VendorInvoice]
   SET 
      [VendorInvoiceNumber] = @VendorInvoiceNumber
      ,[VendorInvoiceDate] = @VendorInvoiceDate
      ,[CreationDate] = @CreationDate
 WHERE PurchaseInvoiceID = @PurchaseInvoiceID
end
end
else 
begin
Delete from PI_VendorInvoice where PurchaseInvoiceID = @PurchaseInvoiceID
end
end
GO

