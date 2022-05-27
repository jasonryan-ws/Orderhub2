﻿CREATE TABLE [dbo].[Order]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [ChannelId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES dbo.Channel(Id),
    [DateOrdered] DATETIME NOT NULL,
    [ChannelOrderNumber] VARCHAR(25) NOT NULL,
    [IsLocked] BIT NOT NULL DEFAULT 0,
    [LockedByNodeId] VARCHAR(50) NULL, 
    [BillAddressId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES dbo.Address(Id), 
    [ShipAddressId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES dbo.Address(Id), 
    [Status] NVARCHAR(50) NULL, 
    --[IsPicked] BIT NOT NULL DEFAULT 0,
    --[DatePicked] DATETIME NULL,
    --[PickedByNodeId] UNIQUEIDENTIFIER NULL,
    [IsVerified] BIT NOT NULL DEFAULT 0,
    [DateVerified] DATETIME NULL,
    [VerifiedByNodeId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [dbo].[Node](Id), 
    [ShipMethod] NVARCHAR(100) NULL, 
    [IsShipped] BIT NOT NULL DEFAULT 0, 
    [DateShipped] DATETIME NULL,
    [ShipCost] MONEY NOT NULL, 
    [IsCancelled] BIT NOT NULL DEFAULT 0,
    [DateCancelled] DATETIME NULL,
    [CancelledByNodeId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [IsSkipped] BIT NOT NULL DEFAULT 0,
    [DateSkipped] DATETIME NULL, 
    [SkippedByNodeId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [Comments] NVARCHAR(MAX) NULL,  
    [DateCreated] DATETIME NOT NULL, 
    [CreatedByNodeId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [DateModified] DATETIME NULL,
    [ModifiedByNodeId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [dbo].[Node](Id),
    [ExternalRowVersion] BINARY(8) NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [DateDeleted] DATETIME NULL,
    [DeletedByNodeId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [dbo].[Node](Id)
    UNIQUE([ChannelId], [ChannelOrderNumber])
)
