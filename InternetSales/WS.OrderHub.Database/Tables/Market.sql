﻿CREATE TABLE [dbo].[Market]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(25) NOT NULL UNIQUE, 
    [Code] VARCHAR(5) NOT NULL, 
    [ColorCode] INT NULL, 
    [DateCreated] DATETIME NOT NULL, 
    [DateModified] DATETIME NULL
)
