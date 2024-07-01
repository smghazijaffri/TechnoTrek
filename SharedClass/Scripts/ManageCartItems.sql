USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[ManageCartItems]    Script Date: 01-Jul-24 6:44:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ManageCartItems]
    @UserID VARCHAR(50),
    @CartItemsJson NVARCHAR(MAX) = NULL,
    @CreationDate datetime,
    @ForInsert int,
    @Action NVARCHAR(50),
    @Output nvarchar(MAX) output,
    @ErrorMessage nvarchar(2000) output
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        SET NOCOUNT ON;
        SET @Output = NULL;

        IF @Action = 'Save'
        BEGIN
            IF @CartItemsJson = '[]'
            BEGIN
                -- Delete the cart items if the JSON is an empty array
                DELETE FROM [dbo].[Cart]
                WHERE [UserID] = @UserID;
                SET @Output = 'Cart emptied successfully.';
            END
            ELSE
            BEGIN
                -- Insert or update the cart items
                IF EXISTS (SELECT 1 FROM [dbo].[Cart] WHERE [UserID] = @UserID)
                BEGIN
                    UPDATE [dbo].[Cart]
                    SET [CartItemsJson] = @CartItemsJson,
                        [LastUpdated] = GETDATE()
                    WHERE [UserID] = @UserID;
                    SET @Output = 'Cart updated successfully.';
                END
                ELSE
                BEGIN
                    INSERT INTO [dbo].[Cart] ([UserID], [CartItemsJson], [CreationDate], [LastUpdated])
                    VALUES (@UserID, @CartItemsJson, @CreationDate, GETDATE());
                    SET @Output = 'Item added successfully.';
                END
            END
        END
        ELSE IF @Action = 'Retrieve'
        BEGIN
            -- Retrieve the cart items
            SELECT @Output = [CartItemsJson]
            FROM [dbo].[Cart]
            WHERE [UserID] = @UserID;
        END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        SET @ErrorMessage = ERROR_MESSAGE();
        THROW;
    END CATCH;
END
GO


