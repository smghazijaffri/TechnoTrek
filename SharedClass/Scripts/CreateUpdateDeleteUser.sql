USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CreateUpdateDeleteUser]    Script Date: 01-Jul-24 6:30:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateUpdateDeleteUser]
    @UserID varchar(50),
    @UserName nvarchar(max),
    @FirstName nvarchar(max),
    @LastName nvarchar(max),
    @UserPassword nvarchar(max),
    @UserEmail nvarchar(max),
    @UserPhone nvarchar(max),
    @Role nvarchar(max),
    @Address nvarchar(max),
    @UserIdentity nvarchar(max),
    @Gender nvarchar(max),
    @Birthday datetime,
    @CreationDate datetime,
    @Status nvarchar(max),
    @ForInsert int,
    @Output nvarchar(max) OUTPUT,
    @ErrorMessage nvarchar(2000) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Ensure UserID does not exist in Customer table if not deleting
        IF @ForInsert <> 0 AND EXISTS (SELECT 1 FROM Customer WHERE CustomerID = @UserID)
        BEGIN
            SET @ErrorMessage = 'UserID already exists in Customer table.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Ensure uniqueness of UserName, UserEmail, UserPhone, and UserIdentity
        IF EXISTS (SELECT 1 FROM Users WHERE (UserName = @UserName OR UserEmail = @UserEmail OR UserPhone = @UserPhone OR UserIdentity = @UserIdentity) AND UserID <> @UserID)
        BEGIN
            SET @ErrorMessage = 'UserName, UserEmail, UserPhone, or UserIdentity already exists.';
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Determine prefix based on Role
        IF @Role = 'Customer'
        BEGIN
            SET @UserID = 'C';
        END
        ELSE
        BEGIN
            SET @UserID = 'U';
        END

        -- Handle UserID generation if inserting
        IF @ForInsert = 1
        BEGIN
            DECLARE @data INT;
            SELECT @data = ISNULL(MAX(CAST(SUBSTRING(UserID, CHARINDEX('-', UserID) + 1, LEN(UserID) - CHARINDEX('-', UserID)) AS INT)), 0) + 1
            FROM Users
            WHERE UserID LIKE @UserID + '%';

            DECLARE @CountNo VARCHAR(10) = CAST(@data AS VARCHAR(10));
            DECLARE @itNum INT = LEN(@CountNo);

            IF (@itNum = 1)
                SET @CountNo = '00' + @CountNo;
            ELSE IF (@itNum = 2)
                SET @CountNo = '0' + @CountNo;

            SET @UserID = @UserID + '-' + @CountNo;
        END

        -- Insert or update Users table
        IF @ForInsert = 1
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM Users WHERE UserID = @UserID)
            BEGIN
                INSERT INTO Users
                (
                    UserID, UserName, FirstName, LastName, UserPassword,
                    UserEmail, UserPhone, Role, Address, UserIdentity,
                    Gender, Birthday, CreationDate, Status, Authorized
                )
                VALUES
                (
                    @UserID, @UserName, @FirstName, @LastName, @UserPassword,
                    @UserEmail, @UserPhone, @Role, @Address, @UserIdentity,
                    @Gender, @Birthday, @CreationDate, @Status, 'false'
                );
            END
            ELSE
            BEGIN
                SET @ErrorMessage = 'User already exists.';
                ROLLBACK TRANSACTION;
                RETURN;
            END
        END
        ELSE
        BEGIN
            UPDATE Users
            SET
                UserName = @UserName,
                FirstName = @FirstName,
                LastName = @LastName,
                UserPassword = @UserPassword,
                UserEmail = @UserEmail,
                UserPhone = @UserPhone,
                Role = @Role,
                Address = @Address,
                UserIdentity = @UserIdentity,
                Gender = @Gender,
                Birthday = @Birthday,
                CreationDate = @CreationDate,
                Status = @Status,
                Authorized = 'false'
            WHERE UserID = @UserID;
        END

        -- Handle Customer table if Role is Customer
        IF @Role = 'Customer'
        BEGIN
            IF @ForInsert = 1
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM Customer WHERE CustomerID = @UserID)
                BEGIN
                    INSERT INTO Customer
                    (
                        CustomerID, Name, Address, Contact, CreationDate
                    )
                    VALUES
                    (
                        @UserID, @FirstName + ' ' + @LastName, @Address, @UserPhone, @CreationDate
                    );
                END
            END
            ELSE
            BEGIN
                UPDATE Customer
                SET
                    Name = @FirstName + ' ' + @LastName,
                    Address = @Address,
                    Contact = @UserPhone,
                    CreationDate = @CreationDate
                WHERE CustomerID = @UserID;
            END
        END

        IF @ForInsert = 0
        BEGIN
            DELETE FROM Users WHERE UserID = @UserID;

            IF @Role = 'Customer'
            BEGIN
                DELETE FROM Customer WHERE CustomerID = @UserID;
            END
        END

        COMMIT TRANSACTION;
        SET @Output = @UserID;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @ErrorMessage = ERROR_MESSAGE();
    END CATCH;
END
GO


