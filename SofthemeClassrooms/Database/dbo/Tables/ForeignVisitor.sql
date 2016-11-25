CREATE TABLE [dbo].[ForeignVisitor] (
    [Id]      BIGINT        NOT NULL,
    [Email]   NVARCHAR (50) NOT NULL,
    [EventId] BIGINT        NOT NULL,
    CONSTRAINT [FK_ForeignVisitor_Events] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id])
);

