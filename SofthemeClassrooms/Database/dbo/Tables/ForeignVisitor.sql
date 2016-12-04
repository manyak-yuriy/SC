CREATE TABLE [dbo].[ForeignVisitor] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Email]   NVARCHAR (100) NOT NULL,
    [EventId] BIGINT         NOT NULL,
    CONSTRAINT [PK_dbo.ForeignVisitor] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ForeignVisitor_dbo.Event_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Event] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EventId]
    ON [dbo].[ForeignVisitor]([EventId] ASC);

