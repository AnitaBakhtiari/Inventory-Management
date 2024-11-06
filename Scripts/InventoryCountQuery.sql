-- Inventory All Products Count -----------------------------------------------------------------
SELECT 
    p.BrandName AS BrandName,
    COUNT(pi.Id) AS InventoryProductsCount
FROM 
    Inventory.dbo.Products AS p
LEFT JOIN 
    Inventory.dbo.ProductInstances AS pi ON p.Id = pi.ProductId
GROUP BY 
    p.BrandName;


-- Available Inventory Products Count -----------------------------------------------------------
SELECT 
    p.BrandName AS BrandName,
    COUNT(*) AS AvailableProductCount
FROM 
    Inventory.dbo.Products AS p
INNER JOIN 
    Inventory.dbo.ProductInstances AS pi ON p.Id = pi.ProductId and pi.IsAvailable = 1  
GROUP BY 
    p.BrandName
ORDER BY 
    p.BrandName;