USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[PurhcaseRequestItems]    Script Date: 5/11/2024 4:15:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[PurhcaseRequestItems]
@selected bit,
@PRNumber nvarchar(50),
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
                           FROM   PR_Items  
                           WHERE  PRnumber = @PRNumber and RowID = @RowID)
begin
INSERT INTO [dbo].[PR_Items]
           ([PRNumber]
           ,[RowID]
           ,[Item]
           ,[Quantity]
           ,[UOM]
           ,[RequiredBy]
           ,[CreationDate])
		   Values
		   (@PRNumber,
		   @RowID,
		   @Item,
		   @Quantity,
		   @UOM,
		   @RequiredBy,
		   @CreationDate
		   )
end
else 
begin
UPDATE [dbo].[PR_Items]
   SET [PRNumber] = @PRNumber
      ,[Item] = @Item
      ,[Quantity] = @Quantity
      ,[UOM] = @UOM
      ,[RequiredBy] = @RequiredBy
 WHERE PRNumber = @PRNumber and RowID = @RowID
end
end
else
begin
Delete from PR_Items where PRNumber = @PRNumber and RowID = @RowID
end
end
GO

