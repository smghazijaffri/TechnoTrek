USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CreateGoodReceipt]    Script Date: 5/11/2024 4:12:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[CreateGoodReceipt]
@GoodReceiptID varchar(50),
@GoodReceiptName Nvarchar(50),
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
                           FROM   GoodReceipt  
                           WHERE  GoodReceiptID = @GoodReceiptID) 
begin
DECLARE @CountNo VARCHAR(10) = '';
            DECLARE @data INT;
           set @GoodReceiptID = 'GR';
            SELECT @data = ISNULL(MAX(CAST(SUBSTRING(GoodReceiptID, CHARINDEX('-', GoodReceiptID) + 1, LEN(GoodReceiptID) - CHARINDEX('-', GoodReceiptID)) AS INT)), 0) + 1
     FROM GoodReceipt;
            IF (@data > 0)
            BEGIN
                
                DECLARE @itr VARCHAR(10) = CAST(@data AS VARCHAR(10));
                DECLARE @itNum INT = LEN(@itr);
                
                IF (@itNum = 1)
                BEGIN
                    SET @CountNo = '00' + @itr;
                    SET @GoodReceiptID = @GoodReceiptID + '-' + @CountNo;
                END
                ELSE IF (@itNum = 2)
                BEGIN
                    SET @CountNo = '0' + @itr;
                    SET @GoodReceiptID = @GoodReceiptID + '-' + @CountNo;
                END
                ELSE
                BEGIN
                    SET @GoodReceiptID = @GoodReceiptID + '-' + @itr;
                END
            END
            ELSE
            BEGIN
                SET @GoodReceiptID = @GoodReceiptID + '-001';
            END
INSERT INTO [dbo].GoodReceipt
           ([GoodReceiptID]
           ,[GoodReceiptName]
           ,[VendorID]
           ,[TotalAmount]
           ,[TotalQuantity]
		   ,[Status]
           ,[RefrenceDocument]
           ,[DocumentDate]
           ,[CreationDate])
     VALUES
	 (@GoodReceiptID,
	 @GoodReceiptName,
	 @VendorID,
	 @TotalAmount,
	 @TotalQuantity,
	 @Status,
	 @RefrenceDocument,
	 @DocumentDate,
	 @CreationDate)
end
UPDATE [dbo].[GoodReceipt]
   SET [GoodReceiptID] = @GoodReceiptID
      ,[GoodReceiptName] =@GoodReceiptName
	  ,[VendorID] = @VendorID
      ,[TotalAmount] = @TotalAmount
      ,[TotalQuantity] = @TotalQuantity
	  ,[Status] = @Status
      ,[RefrenceDocument] = @RefrenceDocument
      ,[DocumentDate] = @DocumentDate
      ,[CreationDate] = @CreationDate
 WHERE GoodReceiptID = @GoodReceiptID
 if(@Status = 'To Bill')
 begin
 Declare @PORefrence Varchar(50)
 Declare @QURefrence Varchar(50)
 Declare @RFQRefrence Varchar(50)
 update PurchaseOrder set Status = 'To Bill' where PurchaseOrderID = @RefrenceDocument
 Select @PORefrence = RefrenceDocument from PurchaseOrder where PurchaseOrderID = @RefrenceDocument
 update PurchaseRequest set Status = 'Received' where PRnumber = @PORefrence
 Select @QURefrence = RefrenceDocument from Quotation where QuotationID = @PORefrence
 Select @RFQRefrence = RefrenceDocument from RequestForQuotation where RFQNumber = @QURefrence
 update PurchaseRequest set Status  = 'Received' where PRnumber =  @RFQRefrence


 end
end
else
begin
Delete from GoodReceipt where GoodReceiptID = @GoodReceiptID
end
end
 set @Output = @GoodReceiptID
GO

