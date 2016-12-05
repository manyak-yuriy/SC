CREATE TABLE [dbo].[ClassRoomProperty] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [ClassRoomId] INT NOT NULL,
    [EquipmentId] INT NOT NULL,
    [Quantity]    INT NOT NULL,
    CONSTRAINT [PK_dbo.ClassRoomProperty] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ClassRoomProperty_dbo.ClassRoom_ClassRoomId] FOREIGN KEY ([ClassRoomId]) REFERENCES [dbo].[ClassRoom] ([Id]),
    CONSTRAINT [FK_dbo.ClassRoomProperty_dbo.Equipment_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [dbo].[Equipment] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ClassRoomId]
    ON [dbo].[ClassRoomProperty]([ClassRoomId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EquipmentId]
    ON [dbo].[ClassRoomProperty]([EquipmentId] ASC);

