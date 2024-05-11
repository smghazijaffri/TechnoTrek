USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[SIItem]    Script Date: 11/05/2024 8:24:39 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[SIItem]  
@Selected bit,  
@SalesInvoiceID varchar(50),  
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
                           FROM   SI_Item    
                           WHERE  SalesInvoiceID = @SalesInvoiceID and RowID = @RowID)   
begin  
INSERT INTO [dbo].[SI_Item]  
           ([SalesInvoiceID]  
           ,[Item]  
           ,[RowID]  
           ,[Quantity]  
           ,[Rate]  
           ,[Amount]  
           ,[CreationDate])  
     VALUES  
           (@SalesInvoiceID  
           ,@Item  
           ,@RowID  
           ,@Quantity  
           ,@Rate  
           ,@Amount   
           ,@CreationDate)  
       
end  
else   
begin  
UPDATE [dbo].[SI_Item]  
   SET [SalesInvoiceID] = @SalesInvoiceID  
      ,[Item] = @Item  
      ,[RowID] = @RowID  
      ,[Quantity] = @Quantity  
      ,[Rate] = @Rate  
      ,[Amount] = @Amount  
      ,[CreationDate] = @CreationDate  
   where SalesInvoiceID = @SalesInvoiceID and RowID = @RowID  
end  
end  
else   
begin  
Delete SI_Item where SalesInvoiceID = @SalesInvoiceID and RowID = @RowID  
end  
end
GO

