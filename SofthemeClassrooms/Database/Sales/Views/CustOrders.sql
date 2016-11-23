
CREATE VIEW Sales.CustOrders
  WITH SCHEMABINDING
AS

SELECT
  O.CustomerId, 
  DATEADD(month, DATEDIFF(month, 0, O.OrderDate), 0) AS ordermonth,
  SUM(OD.Quantity) AS Quantity
FROM Sales.Orders AS O
  JOIN Sales.OrderDetails AS OD
    ON OD.OrderId = O.OrderId
GROUP BY CustomerId, DATEADD(month, DATEDIFF(month, 0, O.OrderDate), 0);
