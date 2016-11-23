CREATE TABLE [HR].[Employees] (
    [EmployeeId]      INT           IDENTITY (1, 1) NOT NULL,
    [LastName]        NVARCHAR (20) NOT NULL,
    [FirstName]       NVARCHAR (10) NOT NULL,
    [Title]           NVARCHAR (30) NOT NULL,
    [TitleOfCourtesy] NVARCHAR (25) NOT NULL,
    [BirthDate]       DATETIME      NOT NULL,
    [HireDate]        DATETIME      NOT NULL,
    [Address]         NVARCHAR (60) NOT NULL,
    [City]            NVARCHAR (15) NOT NULL,
    [Region]          NVARCHAR (15) NULL,
    [PostalCode]      NVARCHAR (10) NULL,
    [Country]         NVARCHAR (15) NOT NULL,
    [Phone]           NVARCHAR (24) NOT NULL,
    [ManagerId]       INT           NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    CONSTRAINT [CHK_BirthDate] CHECK ([BirthDate]<=getdate()),
    CONSTRAINT [FK_Employees_Employees] FOREIGN KEY ([ManagerId]) REFERENCES [HR].[Employees] ([EmployeeId])
);


GO
CREATE NONCLUSTERED INDEX [idx_nc_LastName]
    ON [HR].[Employees]([LastName] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_nc_PostalCode]
    ON [HR].[Employees]([PostalCode] ASC);

