CREATE PROCEDURE [dbo].[spCharge_Update]
	@Id UNIQUEIDENTIFIER,
    @Name VARCHAR(25),
	@Description VARCHAR(255),
	@ModifiedByNodeId UNIQUEIDENTIFIER
AS
BEGIN TRY
	DECLARE @TargetId UNIQUEIDENTIFIER = (SELECT Id FROM Charge WHERE [Name] = @Name)
	IF (@TargetId IS NULL OR @TargetId = @Id)
	BEGIN
		BEGIN TRAN UpdateCharge;
		UPDATE Charge
		SET
			[Name] = @Name,
			[Description] = @Description,
			DateModified = GETDATE(),
			ModifiedByNodeId = @ModifiedByNodeId
		WHERE
			Id = @Id;
		COMMIT TRAN UpdateCharge;
		RETURN @@ROWCOUNT;
	END
	IF @TargetId IS NOT NULL
		THROW 50001, 'Charge name is already in use', 1;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	THROW;
END CATCH