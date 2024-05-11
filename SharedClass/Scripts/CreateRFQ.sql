USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CreateRFQ]    Script Date: 5/11/2024 4:13:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[CreateRFQ]
@RFQNumber varchar(50),
@RFQName Nvarchar(50),
@Status varchar(50),
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
                           FROM   RequestForQuotation  
                           WHERE  RFQNumber = @RFQNumber) 
begin
DECLARE @CountNo VARCHAR(10) = '';
            DECLARE @data INT;
           set @RFQNumber = 'RFQ';
            SELECT @data = ISNULL(MAX(CAST(SUBSTRING(RFQNumber, CHARINDEX('-', RFQNumber) + 1, LEN(RFQNumber) - CHARINDEX('-', RFQNumber)) AS INT)), 0) + 1
     FROM RequestForQuotation;
            IF (@data > 0)
            BEGIN
                
                DECLARE @itr VARCHAR(10) = CAST(@data AS VARCHAR(10));
                DECLARE @itNum INT = LEN(@itr);
                
                IF (@itNum = 1)
                BEGIN
                    SET @CountNo = '00' + @itr;
                    SET @RFQNumber = @RFQNumber + '-' + @CountNo;
                END
                ELSE IF (@itNum = 2)
                BEGIN
                    SET @CountNo = '0' + @itr;
                    SET @RFQNumber = @RFQNumber + '-' + @CountNo;
                END
                ELSE
                BEGIN
                    SET @RFQNumber = @RFQNumber + '-' + @itr;
                END
            END
            ELSE
            BEGIN
                SET @RFQNumber = @RFQNumber + '-001';
            END
INSERT INTO [dbo].[RequestForQuotation]
           ([RFQNumber]
           ,[RFQName]
           ,[Status]
           ,[RefrenceDocument]
           ,[DocumentDate]
           ,[CreationDate])
     VALUES
           (@RFQNumber
			,@RFQName
			,@Status 
			,@RefrenceDocument
			,@DocumentDate
			,@CreationDate)
end
else
begin
UPDATE [dbo].[RequestForQuotation]
   SET [RFQNumber] = @RFQNumber
      ,[RFQName] = @RFQName
      ,[Status] = @Status
      ,[RefrenceDocument] = @RefrenceDocument
      ,[DocumentDate] = @DocumentDate
      ,[CreationDate] = @CreationDate
 WHERE RFQNumber = @RFQNumber
end
end
else
begin
delete from RequestForQuotation where RFQNumber = @RFQNumber
end
end
set @Output = @RFQNumber
GO

