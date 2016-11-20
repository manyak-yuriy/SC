
---------------------------------------------------------------------
-- Create Views and Functions
---------------------------------------------------------------------

CREATE VIEW Sales.OrderValues
  WITH SCHEMABINDING
AS

SELECT O.OrderId, O.CustomerId, O.EmployeeId, O.ShipperId, O.OrderDate, O.RequiredDate, O.ShippedDate,
  SUM(OD.Quantity) AS Quantity,
  CAST(SUM(OD.Quantity * OD.UnitPrice * (1 - OD.Discount))
       AS NUMERIC(12, 2)) AS val
FROM Sales.Orders AS O
  JOIN Sales.OrderDetails AS OD
    ON O.OrderId = OD.OrderId
GROUP BY O.OrderId, O.CustomerId, O.EmployeeId, O.ShipperId, O.OrderDate, O.RequiredDate, O.ShippedDate;
