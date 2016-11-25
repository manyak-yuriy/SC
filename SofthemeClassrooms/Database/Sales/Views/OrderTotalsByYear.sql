
CREATE VIEW Sales.OrderTotalsByYear
  WITH SCHEMABINDING
AS

SELECT
  YEAR(O.OrderDate) AS orderyear,
  SUM(OD.Quantity) AS Quantity
FROM Sales.Orders AS O
  JOIN Sales.OrderDetails AS OD
    ON OD.OrderId = O.OrderId
GROUP BY YEAR(OrderDate);
