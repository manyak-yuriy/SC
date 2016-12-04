CREATE TABLE [dbo].[Event] (
    [Id]                BIGINT         NOT NULL,
    [Title]             NVARCHAR (100) NULL,
    [DateStart]         DATETIME       NOT NULL,
    [DateEnd]           DATETIME       NOT NULL,
    [Description]       NVARCHAR (500) NULL,
    [ClassroomId]       INT            NOT NULL,
    [OrganizerId]       BIGINT         NOT NULL,
    [IsPublic]          BIT            NOT NULL,
    [OrganizerName]     NVARCHAR (100) NULL,
    [AllowSubscription] BIT            NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Events_ClassRooms] FOREIGN KEY ([ClassroomId]) REFERENCES [dbo].[ClassRoom] ([Id]),
    CONSTRAINT [FK_Events_User] FOREIGN KEY ([OrganizerId]) REFERENCES [dbo].[User] ([Id])
);

