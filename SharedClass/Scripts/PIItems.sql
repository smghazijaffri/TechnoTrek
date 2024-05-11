USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[PIItems]    Script Date: 5/11/2024 4:14:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[PIItems]
@Selected bit,
@PurchaseInvoiceID varchar(50),
@Item varchar(50),
@RowID int,
@AcceptedQuantity int,
@Rate int,
@Amount int,
@CreationDate datetime,
@ForInsert int,
@Output nvarchar(50) output
as
begin
if(@ForInsert = 1)
begin
 IF NOT EXISTS (SELECT 1   
                           FROM   PI_Items  
                           WHERE  PurchaseInvoiceID = @PurchaseInvoiceID and RowID = @RowID) 
begin
INSERT INTO [dbo].[PI_Items]
           ([PurchaseInvoiceID]
           ,[Item]
           ,[RowID]
           ,[AcceptedQuantity]
           
           ,[Rate]
           ,[Amount]
           ,[CreationDate])
     VALUES
           (@PurchaseInvoiceID
           ,@Item
           ,@RowID
           ,@AcceptedQuantity
           ,@Rate
           ,@Amount
           ,@CreationDate)
end
else 
begin
UPDATE [dbo].[PI_Items]
   SET 
      [Item] = @Item
      ,[RowID] = @RowID
      ,[AcceptedQuantity] = @AcceptedQuantity
      ,[Rate] = @Rate
      ,[Amount] = @Amount
      ,[CreationDate] = @CreationDate
 WHERE PurchaseInvoiceID = @PurchaseInvoiceID
end
end
else 
begin
Delete from PurchaseInvoice where PurchaseInvoiceID = @PurchaseInvoiceID
end
end
GO

