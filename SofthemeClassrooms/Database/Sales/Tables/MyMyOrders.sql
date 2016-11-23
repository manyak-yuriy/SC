CREATE TABLE [Sales].[MyMyOrders] (
    [OrderId]        INT           IDENTITY (1, 1) NOT NULL,
    [CustomerId]     INT           NULL,
    [EmployeeId]     INT           NOT NULL,
    [OrderDate]      DATETIME      NOT NULL,
    [RequiredDate]   DATETIME      NOT NULL,
    [ShippedDate]    DATETIME      NULL,
    [ShipperId]      INT           NOT NULL,
    [Freight]        MONEY         CONSTRAINT [DFT_MyMyOrders_Freight] DEFAULT ((0)) NOT NULL,
    [ShipName]       NVARCHAR (40) NOT NULL,
    [ShipAddress]    NVARCHAR (60) NOT NULL,
    [ShipCity]       NVARCHAR (15) NOT NULL,
    [ShipRegion]     NVARCHAR (15) NULL,
    [ShipPostalCode] NVARCHAR (10) NULL,
    [ShipCountry]    NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_MyMyOrders] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_MyOrders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [Sales].[Customers] ([CustomerId])
);

