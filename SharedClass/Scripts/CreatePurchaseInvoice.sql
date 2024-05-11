USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CreatePurchaseInvoice]    Script Date: 5/11/2024 4:12:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[CreatePurchaseInvoice]
@PurchaseInvoiceID varchar(50),
@PurchaseInvoiceName nvarchar(50),
@Status nvarchar(50),
@VendorID varchar(50),
@TotalQuantity int,
@TotalAmount int,
@DueDate date,
@DocumentDate date,
@IsPaid bit,
@IsReturn bit,
@RefrenceDocument varchar(50),
@VendorInvoiceNumber nvarchar(50),
@VendorInvoiceDate datetime,
@CreationDate datetime,
@ForInsert int,
@Output nvarchar(50) output
as
begin
if(@ForInsert = 1)
begin
 IF NOT EXISTS (SELECT 1
                           FROM   PurchaseInvoice
                           WHERE  PurchaseInvoiceID = @PurchaseInvoiceID) 
begin
SET NOCOUNT ON;



DECLARE @CountNo VARCHAR(10) = '';
            DECLARE @data INT;
           set @PurchaseInvoiceID = 'PI';
            SELECT @data = ISNULL(MAX(CAST(SUBSTRING(PurchaseInvoiceID, CHARINDEX('-', PurchaseInvoiceID) + 1, LEN(PurchaseInvoiceID) - CHARINDEX('-', PurchaseInvoiceID)) AS INT)), 0) + 1
     FROM PurchaseInvoice;

            IF (@data > 0)
            BEGIN
                
                DECLARE @itr VARCHAR(10) = CAST(@data AS VARCHAR(10));
                DECLARE @itNum INT = LEN(@itr);
                
                IF (@itNum = 1)
                BEGIN
                    SET @CountNo = '00' + @itr;
                    SET @PurchaseInvoiceID = @PurchaseInvoiceID + '-' + @CountNo;
                END
                ELSE IF (@itNum = 2)
                BEGIN
                    SET @CountNo = '0' + @itr;
                    SET @PurchaseInvoiceID = @PurchaseInvoiceID + '-' + @CountNo;
                END
                ELSE
                BEGIN
                    SET @PurchaseInvoiceID = @PurchaseInvoiceID + '-' + @itr;
                END
            END
            ELSE
            BEGIN
                SET @PurchaseInvoiceID = @PurchaseInvoiceID + '-001';
            END
INSERT INTO [dbo].[PurchaseInvoice]
           ([PurchaseInvoiceID]
           ,[PurchaseInvoiceName]
           ,[Status]
           ,[VendorID]
           ,[TotalQuantity]
           ,[TotalAmount]
           ,[DueDate]
           ,[DocumentDate]
           ,[IsPaid]
           ,[IsReturn]
           ,[CreationDate]
           ,[RefrenceDocument]
		   ,[VendorInvoiceNumber]
		   ,[VendorInvoiceDate])
     VALUES
           (@PurchaseInvoiceID, 
           @PurchaseInvoiceName, 
           @Status, 
           @VendorID, 
           @TotalQuantity, 
           @TotalAmount, 
           @DueDate, 
           @DocumentDate, 
           @IsPaid, 
           @IsReturn, 
           @CreationDate, 
           @RefrenceDocument,
		  @VendorInvoiceNumber,
		  @VendorInvoiceDate)
end
else
begin
UPDATE [dbo].[PurchaseInvoice]
   SET 
      [PurchaseInvoiceName] = @PurchaseInvoiceName 
      ,[Status] = @Status
      ,[VendorID] = @VendorID 
      ,[TotalQuantity] = @TotalQuantity
      ,[TotalAmount] = @TotalAmount
      ,[DueDate] = @DueDate
      ,[DocumentDate] = @DocumentDate
      ,[IsPaid] = @IsPaid
      ,[IsReturn] = @IsReturn
      ,[CreationDate] = @CreationDate
      ,[RefrenceDocument] = @RefrenceDocument
	  ,[VendorInvoiceNumber] = @VendorInvoiceNumber
	  ,[VendorInvoiceDate] = @VendorInvoiceDate
 WHERE PurchaseInvoiceID = @PurchaseInvoiceID
  if(@IsPaid = 1)
 begin
 Declare @PORefrence Varchar(50)
 Declare @GRRefrence Varchar(50)
 Declare @RFQRefrence Varchar(50)
 update GoodReceipt set Status = 'Completed' where GoodReceiptID= @RefrenceDocument
 Select @GRRefrence = RefrenceDocument from GoodReceipt where GoodReceiptID = @RefrenceDocument
 update PurchaseOrder set Status = 'Completed' where PurchaseOrderID = @GRRefrence
 



 end
end
end
else
begin
Delete from PurchaseInvoice where PurchaseInvoiceID = @PurchaseInvoiceID
end
end
 set @Output = @PurchaseInvoiceID
GO

