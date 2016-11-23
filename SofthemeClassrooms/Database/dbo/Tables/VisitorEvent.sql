CREATE TABLE [dbo].[VisitorEvent] (
    [Id]        INT    NOT NULL,
    [EventId]   BIGINT NOT NULL,
    [VisitorId] INT    NOT NULL,
    CONSTRAINT [PK_VisitorEvent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VisitorEvent_Events] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]),
    CONSTRAINT [FK_VisitorEvent_User] FOREIGN KEY ([VisitorId]) REFERENCES [dbo].[User] ([Id])
);

