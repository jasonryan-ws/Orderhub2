CREATE PROCEDURE [dbo].[spProductBin_Update]
    @Id UNIQUEIDENTIFIER,
    @Quantity INT,
    @ModifiedByNodeId UNIQUEIDENTIFIER
AS
BEGIN TRY
    BEGIN TRAN UpdateProductBin
    UPDATE ProductBin
    SET
        Quantity = @Quantity,
        DateModified = GETDATE(),
        ModifiedByNodeId = @ModifiedByNodeId
    WHERE
        Id = @Id;
    COMMIT TRAN UpdateProductBin;
    RETURN @@ROWCOUNT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH