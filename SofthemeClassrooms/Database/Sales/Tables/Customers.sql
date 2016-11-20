CREATE TABLE [Sales].[Customers] (
    [CustomerId]   INT           IDENTITY (1, 1) NOT NULL,
    [CompanyName]  NVARCHAR (40) NOT NULL,
    [ContactName]  NVARCHAR (30) NOT NULL,
    [ContactTitle] NVARCHAR (30) NOT NULL,
    [Address]      NVARCHAR (60) NOT NULL,
    [City]         NVARCHAR (15) NOT NULL,
    [Region]       NVARCHAR (15) NULL,
    [PostalCode]   NVARCHAR (10) NULL,
    [Country]      NVARCHAR (15) NOT NULL,
    [Phone]        NVARCHAR (24) NOT NULL,
    [Fax]          NVARCHAR (24) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [idx_nc_City]
    ON [Sales].[Customers]([City] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_CompanyName]
    ON [Sales].[Customers]([CompanyName] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_PostalCode]
    ON [Sales].[Customers]([PostalCode] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_Region]
    ON [Sales].[Customers]([Region] ASC);

