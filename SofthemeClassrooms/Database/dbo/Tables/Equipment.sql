CREATE TABLE [dbo].[Equipment] (
    [Id]   INT           NOT NULL,
    [Name] NVARCHAR (35) NOT NULL,
    [Path] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED ([Id] ASC)
);

