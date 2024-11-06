-- The Most Refunded Products -----------------------------------------------------------------
SELECT 
    p.BrandName AS BrandName,
    COUNT(*) AS TotalRefunds
FROM 
    Inventory.dbo.IssuanceDocuments AS id
INNER JOIN 
    Inventory.dbo.IssuanceDocumentProductInstance AS idpi ON id.Id = idpi.IssuanceDocumentsId
INNER JOIN 
    Inventory.dbo.ProductInstances AS pi ON pi.Id = idpi.ProductInstancesId
INNER JOIN 
    Inventory.dbo.Products AS p ON p.Id = pi.ProductId
WHERE 
    id.Type = 2  --Refund Type
	
	GROUP BY 
    p.BrandName
ORDER BY 
    TotalRefunds DESC;

