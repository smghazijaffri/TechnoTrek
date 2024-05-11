USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[SOItem]    Script Date: 11/05/2024 8:25:49 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create Procedure [dbo].[SOItem]  
@Selected bit,  
@SaleOrderID varchar(50),  
@Item varchar(50),  
@RowID nvarchar(50),  
@Quantity int,  
@Rate int,  
@Amount int,  
@CreationDate datetime,  
@ForInsert int,  
@Output nvarchar(max)  output
as  
begin  
if(@ForInsert = 1)  
begin  
 IF NOT EXISTS (SELECT 1     
                           FROM   SO_Item    
                           WHERE  SaleOrderID = @SaleOrderID and RowID = @RowID)   
begin  
INSERT INTO [dbo].[SO_Item]
           ([SaleOrderID]
           ,[Item]
           ,[RowID]
           ,[Quantity]
           ,[Rate]
           ,[Amount]
           ,[CreationDate])  
     VALUES  
           (@SaleOrderID  
           ,@Item  
           ,@RowID  
           ,@Quantity  
           ,@Rate  
           ,@Amount   
           ,@CreationDate)  
       
end  
else   
begin  
UPDATE [dbo].[SO_Item]  
   SET [SaleOrderID] = @SaleOrderID  
      ,[Item] = @Item  
      ,[RowID] = @RowID  
      ,[Quantity] = @Quantity  
      ,[Rate] = @Rate  
      ,[Amount] = @Amount  
      ,[CreationDate] = @CreationDate  
   where SaleOrderID = @SaleOrderID and RowID = @RowID  
end  
end  
else   
begin  
Delete SO_Item where SaleOrderID = @SaleOrderID and RowID = @RowID  
end  
end
GO

