CREATE PROCEDURE uspGetProductData
(
    @Command Varchar(100)=NULL,

    @ProductIds Varchar(MAX)=NULL
)
AS
    IF @Command='Get-Products'
        BEGIN

            SELECT 
                P.ProductID,
                P.Name,
                P.ProductNumber
            FROM 
                Production.Product As P
            WHERE
                P.ProductID IN 
                (
                    SELECT 
                        value
                    FROM 
                        STRING_SPLIT(@ProductIds,',')
                )

        END
GO