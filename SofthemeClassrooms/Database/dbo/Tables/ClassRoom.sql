CREATE TABLE [dbo].[ClassRoom] (
    [Id]         INT            NOT NULL,
    [Title]      NVARCHAR (100) NOT NULL,
    [IsBookable] BIT            NOT NULL,
    [Capacity]   INT            NOT NULL,
    CONSTRAINT [PK_ClassRooms] PRIMARY KEY CLUSTERED ([Id] ASC)
);

