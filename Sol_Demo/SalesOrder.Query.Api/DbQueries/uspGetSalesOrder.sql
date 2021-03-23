CREATE PROCEDURE uspGetSalesOrderData
(
    @Command Varchar(100)=NULL,

    @PageNumber int=NULL,
    @RowsOfPage int=NULL
)
AS
    IF @Command='Get-SalesOrder'
        BEGIN

            SELECT 
                SOH.SalesOrderID,
                SOH.SalesOrderNumber,
                SOH.PurchaseOrderNumber,
                SOH.OrderDate,
                SOD.ProductID,
                SOD.OrderQty,
                SOD.UnitPrice
            FROM 
                Sales.SalesOrderHeader As SOH 
            INNER JOIN 
                Sales.SalesOrderDetail AS SOD
            ON
                SOH.SalesOrderID=SOD.SalesOrderID
            WHERE
                SOH.SalesOrderID IN 
                    (
                        SELECT 
                            SOH.SalesOrderID
                        FROM 
                            Sales.SalesOrderHeader As SOH 
                        GROUP BY 
                            SOH.SalesOrderID
                        ORDER BY 
                            SOH.SalesOrderID
                        OFFSET (@PageNumber-1) * @RowsOfPage ROWS
                        FETCH NEXT @RowsOfPage ROWS ONLY
                    )
            ORDER BY 
                SOH.SalesOrderID

        END
GO
