CREATE TABLE [dbo].[Job]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[TaskId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Task]([Id]) ON DELETE CASCADE,
    [DateStarted] DATETIME NOT NULL, 
    [StartedByNodeId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Node]([Id]) NOT NULL,
	[Progress] INT NULL,
	[Message] VARCHAR(255) NULL,
	[DateProgressed] DATETIME NULL,
	[IsFinished] BIT NULL,
    [DateEnded] DATETIME NULL,
	[EndedByNodeId] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [dbo].[Node]([Id]) NULL,

	-- If IsFinished is false and has DateEnded value means the job was interrupted by the user
)
