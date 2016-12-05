CREATE TABLE [dbo].[Equipment] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Title]     NVARCHAR (100) NOT NULL,
    [ImagePath] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.Equipment] PRIMARY KEY CLUSTERED ([Id] ASC)
);

