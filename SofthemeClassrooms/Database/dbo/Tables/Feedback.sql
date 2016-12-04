CREATE TABLE [dbo].[Feedback] (
    [Id]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (100)  NOT NULL,
    [LastName]  NVARCHAR (100)  NOT NULL,
    [Email]     NVARCHAR (100)  NOT NULL,
    [Contents]  NVARCHAR (1000) NOT NULL,
    CONSTRAINT [PK_dbo.Feedback] PRIMARY KEY CLUSTERED ([Id] ASC)
);

