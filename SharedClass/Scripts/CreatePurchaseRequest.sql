USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CreatePurchaseRequest]    Script Date: 5/11/2024 4:13:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[CreatePurchaseRequest]
@PRnumber varchar(50),
@PRname nvarchar(50),
@Status nvarchar(50),
@DocumentDate Date,
@CreationDate Datetime,
@ForInsert int,
@Output nvarchar(50) output
as
begin
if(@ForInsert = 1)
begin
 IF NOT EXISTS (SELECT 1
                           FROM   PurchaseRequest
                           WHERE  PRnumber = @PRNumber) 
begin
SET NOCOUNT ON;



DECLARE @CountNo VARCHAR(10) = '';
            DECLARE @data INT;
           set @PRnumber = 'PR';
            SELECT @data = ISNULL(MAX(CAST(SUBSTRING(PRnumber, CHARINDEX('-', PRnumber) + 1, LEN(PRnumber) - CHARINDEX('-', PRnumber)) AS INT)), 0) + 1
     FROM PurchaseRequest;

            IF (@data > 0)
            BEGIN
                
                DECLARE @itr VARCHAR(10) = CAST(@data AS VARCHAR(10));
                DECLARE @itNum INT = LEN(@itr);
                
                IF (@itNum = 1)
                BEGIN
                    SET @CountNo = '00' + @itr;
                    SET @PRnumber = @PRnumber + '-' + @CountNo;
                END
                ELSE IF (@itNum = 2)
                BEGIN
                    SET @CountNo = '0' + @itr;
                    SET @PRnumber = @PRnumber + '-' + @CountNo;
                END
                ELSE
                BEGIN
                    SET @PRnumber = @PRnumber + '-' + @itr;
                END
            END
            ELSE
            BEGIN
                SET @PRnumber = @PRnumber + '-001';
            END

INSERT INTO [dbo].[PurchaseRequest]
           ([PRnumber]
           ,[PRname]
           ,[Status]
           ,[DocumentDate]
           ,[CreationDate])
     VALUES
     (
     @PRnumber,
     @PRname,
     @Status,
     @DocumentDate,
     @CreationDate
     )
	 
end
else
begin
UPDATE [dbo].[PurchaseRequest]
   SET 
      [PRname] = @PRname
      ,[Status] = @Status 
      ,[DocumentDate] = @DocumentDate
 WHERE PRnumber = @PRnumber
end
end
else 
begin
Delete from PurchaseRequest where PRnumber= @PRnumber
end
end
 set @Output = @PRnumber
GO

