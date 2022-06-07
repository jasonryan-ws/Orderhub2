CREATE PROCEDURE [dbo].[spProductBin_Create]
	@Id UNIQUEIDENTIFIER OUTPUT,
    @ProductId UNIQUEIDENTIFIER,
    @BinId UNIQUEIDENTIFIER,
    @Quantity INT,
    @CreatedByNodeId UNIQUEIDENTIFIER,
    @ForceUpdate BIT = NULL
AS
BEGIN TRY
    SET @Id = (SELECT Id FROM ProductBin WHERE ProductId = @ProductId AND BinId = @BinId)
    IF @Id IS NULL
    BEGIN
        BEGIN TRAN CreateProductBin
        SET @Id = NEWID();
        INSERT INTO ProductBin
            (Id, ProductId, BinId, Quantity, DateCreated, CreatedByNodeId)
        VALUES
            (@Id, @ProductId, @BinId, @Quantity, GETDATE(), @CreatedByNodeId);
        COMMIT TRAN CreateProductBin;
        RETURN @@ROWCOUNT;
    END
    ELSE IF @ForceUpdate = 1
        EXEC spProductBin_Update @Id, @Quantity, @CreatedByNodeId;
    ELSE IF @ForceUpdate IS NULL
        THROW 50001, 'Product bin already exists', 1;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH