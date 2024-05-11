USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[QUItems]    Script Date: 5/11/2024 4:15:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[QUItems]
@Selected bit, 
@QuotationID varchar(50),
@Item nvarchar(50),
@RowID int,
@Quantity int,
@UOM nvarchar(50),
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
                           FROM   QU_Items  
                           WHERE  QuotationID = @QuotationID and RowID = @RowID) 
begin

INSERT INTO [dbo].[QU_Items]
           ([QuotationID]
           ,[Item]
           ,[RowID]
           ,[Quantity]
           ,[UOM]
           ,[Rate]
           ,[Amount]
           ,[CreationDate])
     VALUES
	 (@QuotationID,
	  @Item,
	  @RowID,
	  @Quantity,
	  @UOM,
	  @Rate,
	  @Amount,
	  @CreationDate
	 )
end
else
begin
UPDATE [dbo].[QU_Items]
   SET [QuotationID] = @QuotationID
      ,[Item] = @Item
      ,[RowID] = @RowID
      ,[Quantity] = @Quantity
      ,[UOM] = @UOM
      ,[Rate] = @Rate
      ,[Amount] = @Amount
      ,[CreationDate] = @CreationDate
 WHERE QuotationID = @QuotationID and RowID = @RowID
end
end
else
Delete from QU_Items where QuotationID = @QuotationID and RowID = @RowID
end
GO

