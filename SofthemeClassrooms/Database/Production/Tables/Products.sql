CREATE TABLE [Production].[Products] (
    [ProductId]    INT           IDENTITY (1, 1) NOT NULL,
    [ProductName]  NVARCHAR (40) NOT NULL,
    [SupplierId]   INT           NOT NULL,
    [CategoryId]   INT           NOT NULL,
    [UnitPrice]    MONEY         CONSTRAINT [DFT_Products_UnitPrice] DEFAULT ((0)) NOT NULL,
    [Discontinued] BIT           CONSTRAINT [DFT_Products_Discontinued] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [CHK_Products_UnitPrice] CHECK ([UnitPrice]>=(0)),
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Production].[Categories] ([CategoryId]),
    CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY ([SupplierId]) REFERENCES [Production].[Suppliers] ([SupplierId])
);


GO
CREATE NONCLUSTERED INDEX [idx_nc_CategoryId]
    ON [Production].[Products]([CategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_ProductName]
    ON [Production].[Products]([ProductName] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_SupplierId]
    ON [Production].[Products]([SupplierId] ASC);

