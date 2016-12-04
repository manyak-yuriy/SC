CREATE TABLE [dbo].[Event] (
    [Id]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [Title]             NVARCHAR (100) NULL,
    [DateStart]         DATETIME       NOT NULL,
    [DateEnd]           DATETIME       NOT NULL,
    [Description]       NVARCHAR (500) NULL,
    [ClassroomId]       INT            NOT NULL,
    [IsPublic]          BIT            NOT NULL,
    [OrganizerName]     NVARCHAR (100) NULL,
    [AllowSubscription] BIT            NULL,
    [Organizer_Id]      NVARCHAR (128) NULL,
    CONSTRAINT [PK_dbo.Event] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Event_dbo.AspNetUsers_Organizer_Id] FOREIGN KEY ([Organizer_Id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.Event_dbo.ClassRoom_ClassroomId] FOREIGN KEY ([ClassroomId]) REFERENCES [dbo].[ClassRoom] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ClassroomId]
    ON [dbo].[Event]([ClassroomId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Organizer_Id]
    ON [dbo].[Event]([Organizer_Id] ASC);

