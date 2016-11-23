CREATE TABLE [dbo].[Events] (
    [Id]          BIGINT         NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Date]        DATETIME       NOT NULL,
    [Duration]    SMALLINT       NOT NULL,
    [Description] NVARCHAR (500) NOT NULL,
    [ClassroomId] SMALLINT       NOT NULL,
    [OrganizerId] INT            NOT NULL,
    [IsPublic]    BIT            NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Events_ClassRooms] FOREIGN KEY ([ClassroomId]) REFERENCES [dbo].[ClassRooms] ([Id]),
    CONSTRAINT [FK_Events_User] FOREIGN KEY ([OrganizerId]) REFERENCES [dbo].[User] ([Id])
);

