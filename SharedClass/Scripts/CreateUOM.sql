USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CreateUOM]    Script Date: 5/12/2024 2:04:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[CreateUOM]
@UOMID nvarchar(50),
@UOMName nvarchar(50),
@CreationDate datetime,
@Status nvarchar(50),
@ForInsert int,
@Output nvarchar(50) output
as
begin
if(@ForInsert = 1)
begin
    IF NOT EXISTS (SELECT 1 FROM UOM WHERE UOMID = @UOMID) 
    begin
        DECLARE @CountNo VARCHAR(10) = '';
        DECLARE @data INT;
        set @UOMID = 'UOM';
        SELECT @data = ISNULL(MAX(CAST(SUBSTRING(UOMID, CHARINDEX('-', UOMID) + 1, LEN(UOMID) - CHARINDEX('-', UOMID)) AS INT)), 0) + 1
        FROM UOM;
        IF (@data > 0)
        BEGIN
            DECLARE @itr VARCHAR(10) = CAST(@data AS VARCHAR(10));
            DECLARE @itNum INT = LEN(@itr);
            
            IF (@itNum = 1)
            BEGIN
                SET @CountNo = '00' + @itr;
                SET @UOMID = @UOMID + '-' + @CountNo;
            END
            ELSE IF (@itNum = 2)
            BEGIN
                SET @CountNo = '0' + @itr;
                SET @UOMID = @UOMID + '-' + @CountNo;
            END
            ELSE
            BEGIN
                SET @UOMID = @UOMID + '-' + @itr;
            END
        END
        ELSE
        BEGIN
            SET @UOMID = @UOMID + '-001';
        END

        -- Set CreationDate to current date and time
        SET @CreationDate = GETDATE();

        INSERT INTO [dbo].UOM
                   ([UOMID]
                   ,[UOMName]
                   ,[CreationDate]
                   ,[Status])
             VALUES
             (@UOMID,
             @UOMName,
             @CreationDate,
             @Status)
    end
    ELSE
    begin
        UPDATE [dbo].UOM
           SET [UOMID] = @UOMID
              ,[UOMName] =@UOMName
              ,[Status] = @Status
              ,[CreationDate] = @CreationDate
         WHERE UOMID = @UOMID
    end
end
else
begin
    Delete from UOM where UOMID = @UOMID
end

set @Output = @UOMID
end

GO

