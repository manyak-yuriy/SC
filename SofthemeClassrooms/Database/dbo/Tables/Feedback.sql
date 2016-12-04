CREATE TABLE [dbo].[Feedback] (
    [FirstName] NVARCHAR (50)  NOT NULL,
    [LastName]  NVARCHAR (50)  NOT NULL,
    [Email]     NVARCHAR (50)  NOT NULL,
    [Contents]  NVARCHAR (500) NOT NULL,
    [Id]        BIGINT         NOT NULL,
    CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED ([Id] ASC)
);

