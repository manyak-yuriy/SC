CREATE TABLE [Sales].[Orders] (
    [OrderId]        INT           IDENTITY (1, 1) NOT NULL,
    [CustomerId]     INT           NULL,
    [EmployeeId]     INT           NOT NULL,
    [OrderDate]      DATETIME      NOT NULL,
    [RequiredDate]   DATETIME      NOT NULL,
    [ShippedDate]    DATETIME      NULL,
    [ShipperId]      INT           NOT NULL,
    [Freight]        MONEY         CONSTRAINT [DFT_Orders_Freight] DEFAULT ((0)) NOT NULL,
    [ShipName]       NVARCHAR (40) NOT NULL,
    [ShipAddress]    NVARCHAR (60) NOT NULL,
    [ShipCity]       NVARCHAR (15) NOT NULL,
    [ShipRegion]     NVARCHAR (15) NULL,
    [ShipPostalCode] NVARCHAR (10) NULL,
    [ShipCountry]    NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [Sales].[Customers] ([CustomerId]),
    CONSTRAINT [FK_Orders_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [HR].[Employees] ([EmployeeId]),
    CONSTRAINT [FK_Orders_Shippers] FOREIGN KEY ([ShipperId]) REFERENCES [Sales].[Shippers] ([ShipperId])
);


GO
CREATE NONCLUSTERED INDEX [idx_nc_CustomerId]
    ON [Sales].[Orders]([CustomerId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_EmployeeId]
    ON [Sales].[Orders]([EmployeeId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_ShipperId]
    ON [Sales].[Orders]([ShipperId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_OrderDate]
    ON [Sales].[Orders]([OrderDate] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_ShippedDate]
    ON [Sales].[Orders]([ShippedDate] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_ShipPostalCode]
    ON [Sales].[Orders]([ShipPostalCode] ASC);

