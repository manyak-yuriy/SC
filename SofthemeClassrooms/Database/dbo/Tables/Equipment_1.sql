CREATE TABLE [dbo].[Equipment] (
    [Id]        INT            NOT NULL,
    [Title]     NVARCHAR (100) NOT NULL,
    [ImagePath] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED ([Id] ASC)
);

