CREATE TABLE [dbo].[ClassRooms] (
    [Id]         SMALLINT      NOT NULL,
    [Title]      NVARCHAR (50) NOT NULL,
    [IsBookable] BIT           NOT NULL,
    [Capacity]   INT           NOT NULL,
    CONSTRAINT [PK_ClassRooms] PRIMARY KEY CLUSTERED ([Id] ASC)
);

