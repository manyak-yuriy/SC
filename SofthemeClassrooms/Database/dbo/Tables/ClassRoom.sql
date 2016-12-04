CREATE TABLE [dbo].[ClassRoom] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (100) NOT NULL,
    [IsBookable] BIT            NOT NULL,
    [Capacity]   INT            NOT NULL,
    CONSTRAINT [PK_dbo.ClassRoom] PRIMARY KEY CLUSTERED ([Id] ASC)
);

