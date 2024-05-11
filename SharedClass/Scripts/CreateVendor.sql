USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CreateVendor]    Script Date: 5/11/2024 4:14:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[CreateVendor]
@VendorID varchar(50),
@VendorName nvarchar(50),
@VendorGroup nvarchar(50),
@VendorType nvarchar(50),
@Address nvarchar(50),
@Contact nvarchar(50),
@Email nvarchar(50),
@CreationDate datetime, -- Remove redundant declaration
@Status nvarchar(50),
@ForInsert int,
@Output nvarchar(50) output
as
begin
if(@ForInsert = 1)
begin
    IF NOT EXISTS (SELECT 1 FROM Vendor WHERE VendorID = @VendorID) 
    begin
        DECLARE @CountNo VARCHAR(10) = '';
        DECLARE @data INT;
        set @VendorID = 'V';
        SELECT @data = ISNULL(MAX(CAST(SUBSTRING(VendorID, CHARINDEX('-', VendorID) + 1, LEN(VendorID) - CHARINDEX('-', VendorID)) AS INT)), 0) + 1
        FROM Vendor;
        IF (@data > 0)
        BEGIN
            DECLARE @itr VARCHAR(10) = CAST(@data AS VARCHAR(10));
            DECLARE @itNum INT = LEN(@itr);
            
            IF (@itNum = 1)
            BEGIN
                SET @CountNo = '00' + @itr;
                SET @VendorID = @VendorID + '-' + @CountNo;
            END
            ELSE IF (@itNum = 2)
            BEGIN
                SET @CountNo = '0' + @itr;
                SET @VendorID = @VendorID + '-' + @CountNo;
            END
            ELSE
            BEGIN
                SET @VendorID = @VendorID + '-' + @itr;
            END
        END
        ELSE
        BEGIN
            SET @VendorID = @VendorID + '-001';
        END

        -- Set CreationDate to current date and time
        SET @CreationDate = GETDATE();

        INSERT INTO [dbo].Vendor
                   ([VendorID]
                   ,[VendorName]
                   ,[VendorGroup]
                   ,[VendorType]
                   ,[Address]
                   ,[Contact]
                   ,[Email]
                   ,[CreationDate]
                   ,[Status])
             VALUES
             (@VendorID,
             @VendorName,
             @VendorGroup,
             @VendorType,
             @Address,
             @Contact,
             @Email,
             @CreationDate,
             @Status)
    end
    ELSE
    begin
        -- VendorID already exists, update the existing record
        UPDATE [dbo].Vendor
           SET [VendorID] = @VendorID
              ,[VendorName] =@VendorName
              ,[VendorGroup] = @VendorGroup
              ,[VendorType] = @VendorType
              ,[Address] = @Address
              ,[Contact] = @Contact
              ,[Email] = @Email
              ,[Status] = @Status
              ,[CreationDate] = @CreationDate
         WHERE VendorID = @VendorID
    end
end
else
begin
    -- Delete the vendor record
    Delete from Vendor where VendorID = @VendorID
end

-- Set Output parameter to VendorID
set @Output = @VendorID
end
GO

