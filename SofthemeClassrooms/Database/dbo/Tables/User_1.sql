CREATE TABLE [dbo].[User] (
    [Id]       BIGINT         NOT NULL,
    [Name]     NVARCHAR (100) NOT NULL,
    [Email]    NVARCHAR (100) NOT NULL,
    [Password] NCHAR (100)    NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

