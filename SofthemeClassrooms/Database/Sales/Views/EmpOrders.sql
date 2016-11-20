
CREATE VIEW Sales.EmpOrders  
  WITH SCHEMABINDING  
AS  
  
SELECT  
  O.EmployeeId,
  DATEADD(month, DATEDIFF(month, 0, O.OrderDate), 0) AS ordermonth,  
  SUM(OD.Quantity) AS Quantity,
  CAST(SUM(OD.Quantity * OD.UnitPrice * (1 - Discount))  
       AS NUMERIC(12, 2)) AS val,
  COUNT(*) AS numorders  
FROM Sales.Orders AS O  
  JOIN Sales.OrderDetails AS OD  
    ON OD.OrderId = O.OrderId  
GROUP BY EmployeeId, DATEADD(month, DATEDIFF(month, 0, O.OrderDate), 0);  
