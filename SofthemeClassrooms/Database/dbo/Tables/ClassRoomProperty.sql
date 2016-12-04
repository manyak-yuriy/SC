CREATE TABLE [dbo].[ClassRoomProperty] (
    [Id]          INT NOT NULL,
    [ClassRoomId] INT NOT NULL,
    [EquipmentId] INT NOT NULL,
    [Quantity]    INT NOT NULL,
    CONSTRAINT [PK_ClassRoomProp] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClassRoomProp_ClassRooms] FOREIGN KEY ([ClassRoomId]) REFERENCES [dbo].[ClassRoom] ([Id]),
    CONSTRAINT [FK_ClassRoomProp_Equipment] FOREIGN KEY ([EquipmentId]) REFERENCES [dbo].[Equipment] ([Id])
);

