CREATE TABLE [dbo].[Bin]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL UNIQUE, 
    [IsReserved] BIT NOT NULL DEFAULT 0, 
    [IsDefault] BIT NOT NULL DEFAULT 0, -- is primary receiving location if true
    [DateCreated] DATETIME NOT NULL,
    [CreatedByNodeId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [DateModified] DATETIME NULL,
    [ModifiedByNodeId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [DateDeleted] DATETIME NULL,
    [DeletedByNodeId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [dbo].[Node](Id)
)
