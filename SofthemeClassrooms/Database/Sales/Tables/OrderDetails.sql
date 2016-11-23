CREATE TABLE [Sales].[OrderDetails] (
    [OrderId]   INT            NOT NULL,
    [ProductId] INT            NOT NULL,
    [UnitPrice] MONEY          CONSTRAINT [DFT_OrderDetails_UnitPrice] DEFAULT ((0)) NOT NULL,
    [Quantity]  SMALLINT       CONSTRAINT [DFT_OrderDetails_Quantity] DEFAULT ((1)) NOT NULL,
    [Discount]  NUMERIC (4, 3) CONSTRAINT [DFT_OrderDetails_Discount] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED ([OrderId] ASC, [ProductId] ASC),
    CONSTRAINT [CHK_Discount] CHECK ([Discount]>=(0) AND [Discount]<=(1)),
    CONSTRAINT [CHK_Quantity] CHECK ([Quantity]>(0)),
    CONSTRAINT [CHK_UnitPrice] CHECK ([UnitPrice]>=(0)),
    CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Sales].[Orders] ([OrderId]),
    CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY ([ProductId]) REFERENCES [Production].[Products] ([ProductId])
);


GO
CREATE NONCLUSTERED INDEX [idx_nc_OrderId]
    ON [Sales].[OrderDetails]([OrderId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_ProductId]
    ON [Sales].[OrderDetails]([ProductId] ASC);

