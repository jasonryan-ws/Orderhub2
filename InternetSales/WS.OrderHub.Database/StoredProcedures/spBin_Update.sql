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
    DECLARE @TargetId UNIQUEIDENTIFIER = (SELECT Id FROM Bin WHERE [Name] = @Name)
    IF (@TargetId IS NULL OR @TargetId = @Id)
    BEGIN
        BEGIN TRAN UpdateBin
        IF @IsDefault = 1
        BEGIN
            UPDATE Bin
            SET IsDefault = 0
        END
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
    ELSE IF @TargetId IS NOT NULL
        THROW 50001, 'Bin NAME is already in use', 1;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH