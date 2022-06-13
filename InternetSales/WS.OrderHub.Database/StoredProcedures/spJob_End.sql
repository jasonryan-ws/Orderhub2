CREATE PROCEDURE [dbo].[spJob_End]
	@Id UNIQUEIDENTIFIER,
	@EndedByNodeId UNIQUEIDENTIFIER,
	@Message VARCHAR(255),
	@IsFinished BIT = 1
AS
BEGIN TRY
	BEGIN TRAN EndJob
	UPDATE Job
	SET
		DateEnded = GETDATE(),
		EndedByNodeId = @EndedByNodeId,
		Message = @Message,
		IsFinished = @IsFinished
	WHERE
		Id = @Id;
	COMMIT TRAN EndJob;
	RETURN @@ROWCOUNT;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	THROW;
END CATCH