USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[GoodReceiptItems]    Script Date: 5/11/2024 4:14:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[GoodReceiptItems]
@Selected bit, 
@GoodReceiptID varchar(50),
@Item nvarchar(50),
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
                           FROM   GR_Items  
                           WHERE  GoodReceiptID = @GoodReceiptID and RowID = @RowID) 
begin
INSERT INTO [dbo].[GR_Items]
           ([GoodReceiptID]
           ,[Item]
           ,[RowID]
           ,[Rate]
		   ,[AcceptedQuantity]
           ,[Amount]
           ,[CreationDate])
     VALUES
	 (@GoodReceiptID,
	  @Item,
	  @RowID,
	  @Rate,
	  @AcceptedQuantity,
	  @Amount,
	  @CreationDate
	 )
end
else
begin
UPDATE [dbo].[GR_Items]
   SET [GoodReceiptID] = @GoodReceiptID
      ,[Item] = @Item
      ,[RowID] = @RowID
      ,[AcceptedQuantity] = @AcceptedQuantity
      ,[Rate] = @Rate
      ,[Amount] = @Amount
      ,[CreationDate] = @CreationDate
 WHERE GoodReceiptID = @GoodReceiptID and RowID = @RowID
end
end
else 
begin
Delete from GR_Items where GoodReceiptID = @GoodReceiptID and RowID = @RowID
end
end
GO

