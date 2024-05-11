USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CreateQuotation]    Script Date: 5/11/2024 4:13:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[CreateQuotation]
@QuotationID varchar(50),
@QuotationName Nvarchar(50),
@VendorID varchar(50),
@TotalAmount int,
@TotalQuantity int,
@Status nvarchar(50),
@RefrenceDocument Nvarchar(50),
@DocumentDate date,
@CreationDate datetime,
@ForInsert int,
@Output nvarchar(50) output
as
begin
if(@ForInsert = 1)
begin
 IF NOT EXISTS (SELECT 1   
                           FROM   Quotation  
                           WHERE  QuotationID = @QuotationID) 
begin
DECLARE @CountNo VARCHAR(10) = '';
            DECLARE @data INT;
           set @QuotationID = 'QU';
            SELECT @data = ISNULL(MAX(CAST(SUBSTRING(QuotationID, CHARINDEX('-', QuotationID) + 1, LEN(QuotationID) - CHARINDEX('-', QuotationID)) AS INT)), 0) + 1
     FROM Quotation;
            IF (@data > 0)
            BEGIN
                
                DECLARE @itr VARCHAR(10) = CAST(@data AS VARCHAR(10));
                DECLARE @itNum INT = LEN(@itr);
                
                IF (@itNum = 1)
                BEGIN
                    SET @CountNo = '00' + @itr;
                    SET @QuotationID = @QuotationID + '-' + @CountNo;
                END
                ELSE IF (@itNum = 2)
                BEGIN
                    SET @CountNo = '0' + @itr;
                    SET @QuotationID = @QuotationID + '-' + @CountNo;
                END
                ELSE
                BEGIN
                    SET @QuotationID = @QuotationID + '-' + @itr;
                END
            END
            ELSE
            BEGIN
                SET @QuotationID = @QuotationID + '-001';
            END
INSERT INTO [dbo].Quotation
           ([QuotationID]
           ,[QuotationName]
           ,[VendorID]
           ,[TotalAmount]
           ,[TotalQuantity]
		   ,[Status]
           ,[RefrenceDocument]
           ,[DocumentDate]
           ,[CreationDate])
     VALUES
	 (@QuotationID,
	 @QuotationName,
	 @VendorID,
	 @TotalAmount,
	 @TotalQuantity,
	 @Status,
	 @RefrenceDocument,
	 @DocumentDate,
	 @CreationDate)
end
UPDATE [dbo].Quotation
   SET [QuotationID] = @QuotationID
      ,[QuotationName] =@QuotationName
	  ,[VendorID] = @VendorID
      ,[TotalAmount] = @TotalAmount
      ,[TotalQuantity] = @TotalQuantity
	  ,[Status] = @Status
      ,[RefrenceDocument] = @RefrenceDocument
      ,[DocumentDate] = @DocumentDate
      ,[CreationDate] = @CreationDate
 WHERE QuotationID = @QuotationID
end
else
begin
Delete from Quotation where QuotationID = @QuotationID
end
end
 set @Output = @QuotationID
GO

