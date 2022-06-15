CREATE PROCEDURE [dbo].[spOrder_Create]
	@Id UNIQUEIDENTIFIER OUTPUT,
    @ChannelId UNIQUEIDENTIFIER,
    @ChannelOrderNumber VARCHAR(25),
	@DateOrdered DATETIME,
    @BillAddressId UNIQUEIDENTIFIER,
    @ShipAddressId UNIQUEIDENTIFIER,
    @Status NVARCHAR(50), 
    @ShipMethod NVARCHAR(100), 
    @IsShipped BIT,
    @DateShipped DATETIME,
    @ShipCost MONEY, 
    @IsCancelled BIT,
    @DateCancelled DATETIME,
    @CancelledByNodeId UNIQUEIDENTIFIER,
    @Comments NVARCHAR(MAX),  
    @CreatedByNodeId UNIQUEIDENTIFIER,
    @ExternalRowVersion BINARY(8),
    @ForceUpdate BIT = 0
AS
BEGIN TRY
    SET @Id = (SELECT Id FROM [Order] WHERE ChannelId = @ChannelId AND ChannelOrderNumber = @ChannelOrderNumber)
    IF @Id IS NULL
    BEGIN
        BEGIN TRAN CreateOrder
        SET @Id = NEWID();
		IF @IsCancelled = 0 AND @IsShipped = 0
				SET @Status = 'New'
        INSERT INTO [Order]
        (
                Id,
                ChannelId,
                ChannelOrderNumber,
                DateOrdered, BillAddressId,
                ShipAddressId,
                [Status],
                ShipMethod,
                IsShipped,
                DateShipped,
                ShipCost,
                IsCancelled,
                DateCancelled,
                CancelledByNodeId,
                Comments,
                DateCreated,
                CreatedByNodeId,
                ExternalRowVersion
        )
        VALUES
        (
                @Id,
                @ChannelId,
                @ChannelOrderNumber,
                @DateOrdered,
                @BillAddressId,
                @ShipAddressId,
                @Status,
                @ShipMethod,
                @IsShipped,
                @DateShipped,
                @ShipCost,
                @IsCancelled,
                @DateCancelled,
                @CancelledByNodeId,
                @Comments,
                GETDATE(),
                @CreatedByNodeId,
                @ExternalRowVersion
        )
        COMMIT TRAN CreateOrder;
        RETURN @@ROWCOUNT;
    END
    ELSE IF @ForceUpdate = 1
        EXEC spOrder_Update @Id, @Status, @ShipMethod, @IsShipped, @DateShipped, @ShipCost, @IsCancelled, @DateCancelled, @CancelledByNodeId, @Comments, @CreatedByNodeId, @ExternalRowVersion;
    ELSE
        THROW 50001, 'Order already exists', 1

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH
