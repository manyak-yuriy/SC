CREATE TABLE [Production].[Categories] (
    [CategoryId]   INT            IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (15)  NOT NULL,
    [description]  NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [CategoryName]
    ON [Production].[Categories]([CategoryName] ASC);

