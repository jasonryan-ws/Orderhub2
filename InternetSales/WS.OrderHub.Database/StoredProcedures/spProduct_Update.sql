CREATE PROCEDURE [dbo].[spProduct_Update]
	@Id UNIQUEIDENTIFIER,
    @SKU VARCHAR(25),
    @UPC VARCHAR(25),
    @Name NVARCHAR(255),
    @ImageURL NVARCHAR(MAX),
    @ModifiedByNodeId UNIQUEIDENTIFIER
AS
BEGIN TRY
    DECLARE @TargetId UNIQUEIDENTIFIER = (SELECT Id FROM Product WHERE SKU = @SKU);
    IF (@TargetId IS NULL OR @TargetId = @Id)
    BEGIN
        BEGIN TRAN UpdateProduct
        UPDATE Product
        SET
            SKU = @SKU,
            UPC = @UPC,
            [Name] = @Name,
            ImageURL = @ImageURL,
            DateModified = GETDATE(),
            ModifiedByNodeId = @ModifiedByNodeId
        WHERE
            Id = @Id;
        COMMIT TRAN UpdateProduct;
        RETURN @@ROWCOUNT;
    END
    ELSE IF @TargetId IS NOT NULL
        THROW 50001, 'Product SKU is already in use', 1;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH