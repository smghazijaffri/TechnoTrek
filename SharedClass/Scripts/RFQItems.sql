USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[RFQItems]    Script Date: 5/11/2024 4:15:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[RFQItems]
@selected bit,
@RFQNumber nvarchar(50),
@Item nvarchar(50),
@Quantity int,
@UOM nvarchar(50),
@RequiredBy Date,
@RowID int,
@CreationDate datetime,
@ForInsert int,
@Output nvarchar(50) output
as
begin
if(@ForInsert = 1)
begin
 IF NOT EXISTS (SELECT 1   
                           FROM   RFQ_Items  
                           WHERE  RFQNumber = @RFQNumber and RowID = @RowID)
begin
INSERT INTO [dbo].[RFQ_Items]
           ([RFQNumber]
           ,[Item]
           ,[RowID]
           ,[Quantity]
           ,[UOM]
           ,[RequiredBy]
           ,[CreationDate])
     VALUES
           (@RFQNumber 
			,@Item 
			,@RowID
			,@Quantity 
			,@UOM 
			,@RequiredBy 
			,@CreationDate)
end
else 
begin
UPDATE [dbo].[RFQ_Items]
   SET [RFQNumber] = @RFQNumber
      ,[Item] = @Item
      ,[RowID] = @RowID
      ,[Quantity] = @Quantity
      ,[UOM] = @UOM
      ,[RequiredBy] = @RequiredBy
      ,[CreationDate] = @CreationDate
 WHERE RFQNumber = @RFQNumber and RowID = @RowID
end
end
else 
begin
Delete from RFQ_Items where RFQNumber = RFQNumber and RowID = @RowID
end
end
GO

