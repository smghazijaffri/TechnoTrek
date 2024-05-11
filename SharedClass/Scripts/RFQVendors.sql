USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[RFQVendors]    Script Date: 5/11/2024 4:15:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[RFQVendors]
@Selected bit,
@RFQNumber varchar(50),
@VendorID varchar(50),
@RowID int,
@SendEmail bit,
@CreationDate datetime,
@ForInsert int,
@Output nvarchar(50) output
as
begin
if(@ForInsert = 1)
begin
 IF NOT EXISTS (SELECT 1   
                           FROM   RFQVendor  
                           WHERE  RFQNumber = @RFQNumber and RowID = @RowID)
begin
INSERT INTO [dbo].[RFQVendor]
           ([RFQNumber]
           ,[VendorID]
           ,[RowID]
           ,[SendEmail]
           ,[CreationDate])
     VALUES
           (@RFQNumber
           ,@VendorID
           ,@RowID
           ,@SendEmail
           ,@CreationDate)
end
else
begin
UPDATE [dbo].[RFQVendor]
   SET [RFQNumber] = @RFQNumber
      ,[VendorID] = @VendorID
      ,[RowID] = @RowID
      ,[SendEmail] = @SendEmail
      ,[CreationDate] = @CreationDate
 WHERE RFQNumber = @RFQNumber and RowID = @RowID
end
end
else
begin
Delete from RFQVendor where RFQNumber = @RFQNumber and RowID = @RowID
end
end
GO

