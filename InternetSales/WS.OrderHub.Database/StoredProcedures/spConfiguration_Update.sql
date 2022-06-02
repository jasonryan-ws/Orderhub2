CREATE PROCEDURE [dbo].[spConfiguration_Update]
	@Id UNIQUEIDENTIFIER,
	@Value VARCHAR(MAX),
	@ModifiedByNodeId UNIQUEIDENTIFIER
AS
BEGIN TRY
	BEGIN TRAN UpdateConfiguration
	UPDATE [Configuration]
	SET
		[Value] = @Value,
		DateModified = GETDATE(),
		ModifiedByNodeId = @ModifiedByNodeId
	WHERE
		Id = @Id;
	COMMIT TRAN UpdateConfiguration;
	RETURN @@ROWCOUNT;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	THROW;
END CATCH