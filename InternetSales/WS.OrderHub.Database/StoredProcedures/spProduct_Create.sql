CREATE PROCEDURE [dbo].[spProduct_Create]
	@Id UNIQUEIDENTIFIER OUTPUT,
    @SKU VARCHAR(25),
    @UPC VARCHAR(25),
    @Name NVARCHAR(255),
    @ImageURL NVARCHAR(MAX),
    @CreatedByNodeId UNIQUEIDENTIFIER,
    @ForceUpdate BIT = NULL
AS
BEGIN TRY
    SET @Id = (SELECT Id FROM Product WHERE SKU = @SKU)
    IF @Id IS NULL
    BEGIN
        BEGIN TRAN CreateProduct
        SET @Id = NEWID();
        INSERT INTO Product
            (Id, SKU, UPC, [Name], ImageURL, DateCreated, CreatedByNodeId)
        VALUES
            (@Id, @SKU, @UPC, @Name, @ImageURL, GETDATE(), @CreatedByNodeId);
        COMMIT TRAN CreateProduct;
        RETURN @@ROWCOUNT;
    END
    IF @ForceUpdate = 1
        EXEC spProduct_Update @Id, @SKU, @UPC, @Name, @ImageURL, @CreatedByNodeId
    ELSE IF @ForceUpdate IS NULL
        THROW 50001, 'SKU is already in use', 1;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH