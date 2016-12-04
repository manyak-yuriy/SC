CREATE TABLE [dbo].[Feedback] (
    [FirstName] NVARCHAR (100)  NOT NULL,
    [LastName]  NVARCHAR (100)  NOT NULL,
    [Email]     NVARCHAR (100)  NOT NULL,
    [Contents]  NVARCHAR (1000) NOT NULL,
    [Id]        BIGINT          NOT NULL,
    CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED ([Id] ASC)
);

