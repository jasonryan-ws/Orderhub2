--Global Configuration
CREATE TABLE [dbo].[Configuration]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Name] VARCHAR(25) NOT NULL,
	[Value] VARCHAR(MAX) NULL,
	[Description] VARCHAR(255) NULL,
	[FullDescription] VARCHAR(MAX) NULL,
	[DateModified] DATETIME NULL,
	[ModifiedByNodeId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [dbo].[Node](Id)
)
