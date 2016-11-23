﻿CREATE TABLE [Sales].[Shippers] (
    [ShipperId]   INT           IDENTITY (1, 1) NOT NULL,
    [CompanyName] NVARCHAR (40) NOT NULL,
    [Phone]       NVARCHAR (24) NOT NULL,
    CONSTRAINT [PK_Shippers] PRIMARY KEY CLUSTERED ([ShipperId] ASC)
);

