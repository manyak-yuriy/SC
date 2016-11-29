CREATE TABLE [dbo].[Events] (
    [Id]                BIGINT         NOT NULL,
    [Title]             NVARCHAR (50)  NULL,
    [DateStart]         DATETIME       NOT NULL,
    [DateEnd]           DATETIME       NOT NULL,
    [Description]       NVARCHAR (500) NULL,
    [ClassroomId]       SMALLINT       NOT NULL,
    [OrganizerId]       INT            NULL,
    [IsPublic]          BIT            NOT NULL,
    [OrganizerName]     NVARCHAR (50)  NULL,
    [AllowSubscription] BIT            NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Events_ClassRooms] FOREIGN KEY ([ClassroomId]) REFERENCES [dbo].[ClassRooms] ([Id]),
    CONSTRAINT [FK_Events_User] FOREIGN KEY ([OrganizerId]) REFERENCES [dbo].[User] ([Id])
);

