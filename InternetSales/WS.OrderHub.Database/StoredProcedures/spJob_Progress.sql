CREATE PROCEDURE [dbo].[spJob_Progress]
	@Id UNIQUEIDENTIFIER,
	@Progress INT,
	@Message VARCHAR(255)
AS
BEGIN TRY
	BEGIN TRAN ProgessJob
	UPDATE Job
	SET
		Progress = @Progress,
		Message = @Message,
		DateProgressed = GETDATE()
	WHERE
		Id = @Id;
	COMMIT TRAN ProgressJob;
	RETURN @@ROWCOUNT;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK  TRAN;
	THROW;
END CATCH