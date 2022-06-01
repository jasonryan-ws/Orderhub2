CREATE PROCEDURE [dbo].[spChannel_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(25),
    @Code VARCHAR(5),
    @ColorCode INT,
	@ModifiedByNodeId UNIQUEIDENTIFIER
AS
BEGIN TRY
    DECLARE @TargetId UNIQUEIDENTIFIER = (SELECT Id FROM Channel WHERE [Name] = @Name)
    IF (@TargetId IS NULL OR @TargetId = @Id)
    BEGIN
        BEGIN TRAN UpdateChannel
        UPDATE Channel
        SET
            [Name] = @Name,
            Code = @Code,
            ColorCode = @ColorCode,
            DateModified = GETDATE(),
            ModifiedByNodeId = @ModifiedByNodeId
        WHERE
            Id = @Id;
        COMMIT TRAN UpdateChannel;
        RETURN @@ROWCOUNT;
    END
    ELSE IF @TargetId IS NOT NULL
        THROW 50001, 'Channel name is already in use', 1;

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH
