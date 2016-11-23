CREATE TABLE [dbo].[User] (
    [Id]       INT            NOT NULL,
    [Name]     NVARCHAR (100) NOT NULL,
    [Email]    NVARCHAR (80)  NOT NULL,
    [Password] NCHAR (100)    NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

