USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CreatePurchaseOrder]    Script Date: 5/11/2024 4:13:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[CreatePurchaseOrder]
@PurchaseOrderID varchar(50),
@PurchaseOrderName Nvarchar(50),
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
                           FROM   PurchaseOrder  
                           WHERE  PurchaseOrderID = @PurchaseOrderID) 
						   

begin
DECLARE @CountNo VARCHAR(10) = '';
            DECLARE @data INT;
           set @PurchaseOrderID = 'PO';
            SELECT @data = ISNULL(MAX(CAST(SUBSTRING(PurchaseOrderID, CHARINDEX('-', PurchaseOrderID) + 1, LEN(PurchaseOrderID) - CHARINDEX('-', PurchaseOrderID)) AS INT)), 0) + 1
     FROM PurchaseOrder;
            IF (@data > 0)
            BEGIN
                SET @data = @data;
                DECLARE @itr VARCHAR(10) = CAST(@data AS VARCHAR(10));
                DECLARE @itNum INT = LEN(@itr);
                
                IF (@itNum = 1)
                BEGIN
                    SET @CountNo = '00' + @itr;
                    SET @PurchaseOrderID = @PurchaseOrderID + '-' + @CountNo;
                END
                ELSE IF (@itNum = 2)
                BEGIN
                    SET @CountNo = '0' + @itr;
                    SET @PurchaseOrderID = @PurchaseOrderID + '-' + @CountNo;
                END
                ELSE
                BEGIN
                    SET @PurchaseOrderID = @PurchaseOrderID + '-' + @itr;
                END
            END
            ELSE
            BEGIN
                SET @PurchaseOrderID = @PurchaseOrderID + '-001';
            END
INSERT INTO [dbo].[PurchaseOrder]
           ([PurchaseOrderID]
           ,[PurchaseOrderName]
           ,[VendorID]
           ,[TotalAmount]
           ,[TotalQuantity]
		   ,[Status]
           ,[RefrenceDocument]
           ,[DocumentDate]
           ,[CreationDate])
     VALUES
	 (@PurchaseOrderID,
	 @PurchaseOrderName,
	 @VendorID,
	 @TotalAmount,
	 @TotalQuantity,
	 @Status,
	 @RefrenceDocument,
	 @DocumentDate,
	 @CreationDate)
end
else 
begin 
UPDATE [dbo].[PurchaseOrder]
   SET [PurchaseOrderID] = @PurchaseOrderID
      ,[PurchaseOrderName] =@PurchaseOrderName
	  ,[VendorID] = @VendorID
      ,[TotalAmount] = @TotalAmount
      ,[TotalQuantity] = @TotalQuantity
	  ,[Status] = @Status
      ,[RefrenceDocument] = @RefrenceDocument
      ,[DocumentDate] = @DocumentDate
      ,[CreationDate] = @CreationDate
 WHERE PurchaseOrderID = @PurchaseOrderID
 if(@Status = 'To Receive and Bill')
 begin
  Declare @QURefrence Varchar(50)
  Declare @RFQRefrence Varchar(50)
 update PurchaseRequest set Status  = 'Ordered' where PRnumber =  @RefrenceDocument
 Select @QURefrence = RefrenceDocument from Quotation where QuotationID = @RefrenceDocument
 Select @RFQRefrence = RefrenceDocument from RequestForQuotation where RFQNumber = @QURefrence
 update PurchaseRequest set Status  = 'Ordered' where PRnumber =  @RFQRefrence

 end
end
end
else
begin 
Delete from PurchaseOrder where PurchaseOrderID = @PurchaseOrderID
end
end
 set @Output = @PurchaseOrderID
GO

