USE [Computer]
GO

/****** Object:  StoredProcedure [dbo].[GetReportData]    Script Date: 01-Jul-24 6:29:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetReportData]
    @ReportName NVARCHAR(50),
    @ID NVARCHAR(50) = NULL,
    @CreationDate DATETIME,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @ErrorMessage NVARCHAR(2000) OUTPUT
AS
BEGIN
BEGIN TRY
        BEGIN TRANSACTION;
    SET NOCOUNT ON;

    IF @ReportName = 'Purchase Invoice'
    BEGIN
        SELECT 
            ROW_NUMBER() OVER (ORDER BY i.ItemName) AS Row,
            pi.PurchaseInvoiceID,
            pi.AcceptedQuantity,
            pi.Rate AS Rate,
            pi.Amount AS Amount,
            i.ItemName AS Item,
            v.VendorName AS Vendor,
            v.Address AS VendorAddress,
            v.Contact AS VendorContact,
            v.Email AS VendorEmail,
            u.UOMName AS UOM,
            FORMAT(p.DueDate, 'yyyy-MM-dd') AS DueDate,
            p.TotalQuantity AS TotalQuantity,
            p.TotalAmount AS TotalAmount,
            FORMAT(p.DocumentDate, 'yyyy-MM-dd') AS DocumentDate,
            p.RefrenceDocument
        FROM 
            PI_Items pi
        INNER JOIN 
            PurchaseInvoice p ON pi.PurchaseInvoiceID = p.PurchaseInvoiceID
        INNER JOIN 
            Vendor v ON p.VendorID = v.VendorID
        INNER JOIN 
            Items i ON pi.Item = i.ItemCode
        INNER JOIN 
            UOM u ON pi.UOM = u.UOMID
        WHERE
            pi.PurchaseInvoiceID = @ID;
    END
    ELSE IF @ReportName = 'Sales Invoice'
    BEGIN
        SELECT
            ROW_NUMBER() OVER (ORDER BY i.ItemName) AS Row,
            SI.SalesInvoiceID,
            C.Name AS CustomerName,
            C.Address,
            C.Contact,
            I.ItemName,
            SII.Rate,
            SII.UOM,
            SII.Amount,
            SII.Quantity,
            SI.TotalAmount,
            SI.TotalQuantity,
            CASE 
                WHEN SI.IsPartiallyPaid = 'true' THEN SI.TotalAmount / 2 
                ELSE 0 
            END AS OutstandingAmount,
            SI.IsPartiallyPaid,
            FORMAT(SI.DueDate, 'yyyy-MM-dd') AS DueDate,
            FORMAT(SI.DocumentDate, 'yyyy-MM-dd') AS DocumentDate
        FROM 
            SaleInvoice SI
        JOIN 
            SI_Item SII ON SI.SalesInvoiceID = SII.SalesInvoiceID
        JOIN 
            Customer C ON SI.CustomerID = C.CustomerID
        JOIN 
            Items I ON SII.Item = I.ItemCode
        WHERE
            SI.SalesInvoiceID = @ID;
    END
    ELSE IF @ReportName = 'Inventory Report'
    BEGIN
        SELECT 
            I.ItemCode,
            I.ItemName,
            I.ItemType,
            S.Quantity,
            S.Rate,
            FORMAT(S.CreationDate, 'yyyy-MM-dd') AS StockUpdated,
            S.Quantity * S.Rate AS TotalValue,
            FORMAT(@CreationDate, 'yyyy-MM-dd') AS ReportGenerated
        FROM 
            Items I
        LEFT JOIN 
            Stock S ON I.ItemCode = S.ItemID
        ORDER BY 
            I.ItemCode;
    END
    ELSE IF @ReportName = 'Request for Quotation'
    BEGIN
        SELECT 
            ROW_NUMBER() OVER (ORDER BY i.ItemName) AS No, 
            r.RFQNumber, 
            ItemName AS Item, 
            Quantity, 
            UOMName AS UOM, 
            FORMAT(RequiredBy, 'yyyy-MM-dd') AS RequiredBy, 
            FORMAT(rq.DocumentDate, 'yyyy-MM-dd') AS DocumentDate 
        FROM 
            RFQ_Items r 
        INNER JOIN 
            Items i ON r.Item = i.ItemCode 
        INNER JOIN 
            UOM u ON r.UOM = u.UOMID 
        INNER JOIN 
            RequestForQuotation rq ON r.RFQNumber = rq.RFQNumber 
        WHERE 
            r.RFQNumber = @ID;
    END
    ELSE IF @ReportName = 'Purchase Order Analysis'
    BEGIN
        WITH PO_Status_CTE AS (
            SELECT
                PO.PurchaseOrderID,
                PO.PurchaseOrderName,
                V.VendorName,
                PO.TotalAmount,
                PO.TotalQuantity AS TotalQuantityOrdered,
                PO.Status AS PO_Status,
                FORMAT(PO.DocumentDate, 'yyyy-MM-dd') AS DocumentDate,
                ISNULL(SUM(PO.TotalAmount), 0) AS TotalBilledAmount,
                ISNULL(SUM(CASE WHEN PO.Status IN ('To Bill', 'To Receive and Bill') THEN PI.Amount ELSE 0 END), 0) AS TotalAmountToBill,
                ISNULL(SUM(CASE WHEN PO.Status IN ('To Bill', 'Completed') THEN PI.Quantity ELSE 0 END), 0) AS TotalQuantityReceived
            FROM [Computer].[dbo].[PurchaseOrder] PO
            LEFT JOIN [Computer].[dbo].[PO_Items] PI ON PO.PurchaseOrderID = PI.PurchaseOrderID
            LEFT JOIN [Computer].[dbo].[Vendor] V ON PO.VendorID = V.VendorID
            WHERE PO.DocumentDate BETWEEN @StartDate AND @EndDate
            GROUP BY
                PO.PurchaseOrderID,
                PO.PurchaseOrderName,
                V.VendorName,
                PO.TotalAmount,
                PO.TotalQuantity,
                PO.Status,
                PO.DocumentDate
        )

        SELECT
            PS.PurchaseOrderID,
            PS.PurchaseOrderName,
            PS.VendorName,
            PS.TotalAmount,
            PS.TotalQuantityOrdered,
            PS.PO_Status,
            PS.DocumentDate,
            PS.TotalBilledAmount,
            PS.TotalAmountToBill,
            PS.TotalQuantityReceived,
            PS.TotalQuantityOrdered - PS.TotalQuantityReceived AS TotalQuantityToReceive,
            I.ItemCode,
            I.ItemName,
            PI.Quantity AS ItemQuantity,
            U.UOMName,
            PI.Rate,
            PI.Amount AS ItemAmount,
			@CreationDate AS Generated
        FROM PO_Status_CTE PS
        LEFT JOIN [Computer].[dbo].[PO_Items] PI ON PS.PurchaseOrderID = PI.PurchaseOrderID
        LEFT JOIN [Computer].[dbo].[Items] I ON PI.Item = I.ItemCode
        LEFT JOIN [Computer].[dbo].[UOM] U ON PI.UOM = U.UOMID
        WHERE PS.PO_Status IN ('To Receive and Bill', 'To Receive', 'To Bill', 'Completed', 'Cancelled')
        ORDER BY PS.PurchaseOrderID;
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


