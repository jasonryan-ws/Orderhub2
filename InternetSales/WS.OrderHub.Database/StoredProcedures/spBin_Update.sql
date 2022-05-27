CREATE PROCEDURE [dbo].[spBin_Update]
	@Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(50),
    @IsReserved BIT,
    @IsDefault BIT,
    @ModifiedByNodeId UNIQUEIDENTIFIER
    --@IsDeleted BIT,
    --@DateDeleted DATETIME,
    --@DeletedByNodeId UNIQUEIDENTIFIER,
AS
BEGIN TRY
    -- Check if this name already exist
    IF (SELECT Id FROM Bin WHERE [Name] = @Name AND Id <> @Id) IS NULL
    BEGIN
        BEGIN TRAN UpdateBin
        UPDATE Bin
        SET
            [Name] = @Name,
            IsReserved = @IsReserved,
            IsDefault = @IsDefault,
            DateModified = GETDATE(),
            ModifiedByNodeId = @ModifiedByNodeId
        WHERE
            Id = @Id
        COMMIT TRAN;
        RETURN @@ROWCOUNT;
    END
    ELSE
        THROW 50001, 'Bin NAME is already in use', 1;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH