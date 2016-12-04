CREATE TABLE [dbo].[ForeignVisitor] (
    [Id]      BIGINT         NOT NULL,
    [Email]   NVARCHAR (100) NOT NULL,
    [EventId] BIGINT         NOT NULL,
    CONSTRAINT [PK_ForeignVisitor] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ForeignVisitor_Events] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Event] ([Id])
);

