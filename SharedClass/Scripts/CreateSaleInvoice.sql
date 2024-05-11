USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CreateSaleInvoice]    Script Date: 11/05/2024 8:28:16 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[CreateSaleInvoice]     
@SalesInvoiceID varchar(50),    
@SalesInvoiceName nvarchar(max),    
@CustomerID varchar(50),    
@Status NVARCHAR(MAX),    
@TotalAmount int,    
@TotalQuantity int,    
@IsPaid bit,    
@IsPartiallyPaid bit,    
@IsReturn bit,    
@RefrenceDocument nvarchar(max),    
@DueDate date,    
@DocumentDate date,    
@CreationDate date,    
@ForInsert INT,    
@Output nvarchar(max) output   
as     
begin    
if(@ForInsert = 1)    
begin    
 IF NOT EXISTS (SELECT 1    
                           FROM   SaleInvoice    
                           WHERE  SalesInvoiceID = @SalesInvoiceID)     
begin    
SET NOCOUNT ON;    
    
    
    
DECLARE @CountNo VARCHAR(10) = '';    
            DECLARE @data INT;    
           set @SalesInvoiceID = 'SI';    
            SELECT @data = ISNULL(MAX(CAST(SUBSTRING(SalesInvoiceID, CHARINDEX('-', SalesInvoiceID) + 1, LEN(SalesInvoiceID) - CHARINDEX('-', SalesInvoiceID)) AS INT)), 0) + 1    
     FROM SaleInvoice;    
    
            IF (@data > 0)    
            BEGIN    
                    
                DECLARE @itr VARCHAR(10) = CAST(@data AS VARCHAR(10));    
                DECLARE @itNum INT = LEN(@itr);    
                    
                IF (@itNum = 1)    
                BEGIN    
                    SET @CountNo = '00' + @itr;    
                    SET @SalesInvoiceID = @SalesInvoiceID + '-' + @CountNo;    
                END    
                ELSE IF (@itNum = 2)    
                BEGIN    
                    SET @CountNo = '0' + @itr;    
                    SET @SalesInvoiceID = @SalesInvoiceID + '-' + @CountNo;    
                END    
                ELSE    
                BEGIN    
                    SET @SalesInvoiceID = @SalesInvoiceID + '-' + @itr;    
                END    
            END    
            ELSE    
            BEGIN    
                SET @SalesInvoiceID = @SalesInvoiceID + '-001';    
            END    
INSERT INTO [dbo].[SaleInvoice]    
           ([SalesInvoiceID]    
           ,[SalesInvoiceName]    
           ,[CustomerID]    
           ,[Status]    
           ,[TotalAmount]    
           ,[TotalQuantity]    
           ,[IsPaid]    
           ,[IsPartiallyPaid]    
           ,[IsReturn]    
           ,[RefrenceDocument]    
           ,[DueDate]    
           ,[DocumentDate]    
           ,[CreationDate])    
     VALUES    
           (@SalesInvoiceID    
           ,@SalesInvoiceName    
           ,@CustomerID    
           ,@Status    
           ,@TotalAmount    
           ,@TotalQuantity    
           ,@IsPaid    
           ,@IsPartiallyPaid    
           ,@IsReturn    
           ,@RefrenceDocument    
           ,@DueDate    
           ,@DocumentDate    
           ,@CreationDate)    
    
end    
else     
begin    
UPDATE [dbo].[SaleInvoice]    
   SET [SalesInvoiceID] = @SalesInvoiceID    
      ,[SalesInvoiceName] = @SalesInvoiceName    
      ,[CustomerID] = @CustomerID    
      ,[Status] = @Status    
      ,[TotalAmount] = @TotalAmount    
      ,[TotalQuantity] = @TotalQuantity    
      ,[IsPaid] = @IsPaid    
      ,[IsPartiallyPaid] = @IsPartiallyPaid    
      ,[IsReturn] = @IsReturn    
      ,[RefrenceDocument] = @RefrenceDocument    
      ,[DueDate] = @DueDate    
      ,[DocumentDate] = @DocumentDate    
      ,[CreationDate] = @CreationDate    
 WHERE SalesInvoiceID = @SalesInvoiceID    
end    
end    
else     
begin    
Delete from SaleInvoice where SalesInvoiceID = @SalesInvoiceID    
end    
end    
 set @Output = @SalesInvoiceID
GO

