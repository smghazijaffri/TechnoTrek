USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[CancelAll]    Script Date: 5/11/2024 4:12:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[CancelAll]
@ID varchar(50),
@type nvarchar(MAX),
@CreationDate datetime,
@ForInsert int,
@Output nvarchar(50) output
as
begin
 Declare @PORefrence Varchar(50)
 Declare @RFQRefrence Varchar(50)
 Declare @QURefrence Varchar(50)
 Declare @GRRefrence varchar(50)
 Declare @PIRefrence varchar(50)
if(@type = 'Purhcase Request')
begin
update PurchaseRequest set Status = 'Cancelled' where PRnumber = @ID
update PurchaseOrder set Status = 'Cancelled' where RefrenceDocument = @ID
update RequestForQuotation set Status = 'Cancelled' where RefrenceDocument = @ID
Select @PORefrence = PurchaseOrderID from PurchaseOrder where RefrenceDocument  = @ID
update GoodReceipt set Status = 'Cancelled' where RefrenceDocument = @PORefrence
Select @RFQRefrence = RFQNumber from RequestForQuotation where RefrenceDocument = @ID
update Quotation set Status = 'Cancelled' where RefrenceDocument = @RFQRefrence
Select @QURefrence = QuotationID from Quotation where RefrenceDocument = @RFQRefrence 
update PurchaseOrder set Status = 'Cancelled' where RefrenceDocument = @QURefrence
Select @PORefrence = PurchaseOrderID from PurchaseOrder where RefrenceDocument  = @QURefrence
update GoodReceipt set Status = 'Cancelled' where RefrenceDocument = @PORefrence

end
else if(@type = 'Purchase Order')
begin
update PurchaseOrder set Status = 'Cancelled' where PurchaseOrderID = @ID
update GoodReceipt set Status = 'Cancelled' where RefrenceDocument = @ID
Select @PORefrence = RefrenceDocument from PurchaseOrder where PurchaseOrderID  = @ID
update PurchaseRequest set Status = 'Pending' where PRnumber = @PORefrence
end
else if (@type = 'Request For Quotation')
begin
update RequestForQuotation set Status = 'Cancelled' where RFQNumber = @ID
update Quotation  set Status = 'Cancelled' where RefrenceDocument = @ID
end
else if(@type = 'Quotation')
begin
update Quotation set Status = 'Cancelled' where QuotationID = @ID
end
else if(@type = 'Good Receipt')
begin
update GoodReceipt set Status = 'Cancelled' where GoodReceiptID = @ID
Select @GRRefrence = RefrenceDocument from GoodReceipt where GoodReceiptID  = @ID
update PurchaseOrder set Status = 'To Receive and Bill' where PurchaseOrderID = @GRRefrence
Select @PORefrence = RefrenceDocument from PurchaseOrder where PurchaseOrderID  = @GRRefrence
update PurchaseRequest set Status = 'Ordered' where PRnumber = @PORefrence
end
else if(@type = 'Purchase Invoice')
begin
update PurchaseInvoice set Status = 'Cancelled' where PurchaseInvoiceID = @ID
select @PIRefrence = RefrenceDocument from PurchaseInvoice where PurchaseInvoiceID = @ID
update GoodReceipt set Status = 'To Bill' where GoodReceiptID = @PIRefrence
Select @GRRefrence = RefrenceDocument from GoodReceipt where GoodReceiptID  = @ID
update PurchaseOrder set Status = 'To Bill' where PurchaseOrderID = @GRRefrence
Select @PORefrence = RefrenceDocument from PurchaseOrder where PurchaseOrderID  = @GRRefrence
update PurchaseRequest set Status = 'Ordered' where PRnumber = @PORefrence
end
end
GO

