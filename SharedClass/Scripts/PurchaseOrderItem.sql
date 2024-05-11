USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[PurchaseOrderItem]    Script Date: 5/11/2024 4:14:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[PurchaseOrderItem]
@Selected bit, 
@PurchaseOrderID varchar(50),
@Item nvarchar(50),
@RowID int,
@Quantity int,
@UOM nvarchar(50),
@Rate int,
@Amount int,
@RequiredBy date,
@CreationDate datetime,
@ForInsert int,
@Output nvarchar(50) output
as
begin
if(@ForInsert = 1)
begin
 IF NOT EXISTS (SELECT 1   
                           FROM   PO_Items  
                           WHERE  PurchaseOrderID = @PurchaseOrderID and RowID = @RowID) 
begin

INSERT INTO [dbo].[PO_Items]
           ([PurchaseOrderID]
           ,[Item]
           ,[RowID]
           ,[Quantity]
           ,[UOM]
           ,[Rate]
           ,[Amount]
           ,[RequiredBy]
           ,[CreationDate])
     VALUES
	 (@PurchaseOrderID,
	  @Item,
	  @RowID,
	  @Quantity,
	  @UOM,
	  @Rate,
	  @Amount,
	  @RequiredBy,
	  @CreationDate
	 )
end
else
begin
UPDATE [dbo].[PO_Items]
   SET [PurchaseOrderID] = @PurchaseOrderID
      ,[Item] = @Item
      ,[RowID] = @RowID
      ,[Quantity] = @Quantity
      ,[UOM] = @UOM
      ,[Rate] = @Rate
      ,[Amount] = @Amount
      ,[RequiredBy] = @RequiredBy
      ,[CreationDate] = @CreationDate
 WHERE PurchaseOrderID = @PurchaseOrderID and RowID = @RowID
end
end
else
Delete from PO_Items where PurchaseOrderID = @PurchaseOrderID and RowID = @RowID
end
GO

