CREATE TABLE [dbo].[ClassRoomProperties] (
    [Id]          INT      NOT NULL,
    [ClassRoomId] SMALLINT NOT NULL,
    [EquipmentId] INT      NOT NULL,
    [Quantity]    INT      NOT NULL,
    CONSTRAINT [PK_ClassRoomProp] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClassRoomProp_ClassRooms] FOREIGN KEY ([ClassRoomId]) REFERENCES [dbo].[ClassRooms] ([Id]),
    CONSTRAINT [FK_ClassRoomProp_Equipment] FOREIGN KEY ([EquipmentId]) REFERENCES [dbo].[Equipment] ([Id])
);

