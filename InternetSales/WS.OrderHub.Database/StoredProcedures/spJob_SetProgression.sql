CREATE PROCEDURE [dbo].[spJob_SetProgression]
	@Id UNIQUEIDENTIFIER,
	@Count INT,
	@Message VARCHAR(255)
AS
BEGIN TRY
	DECLARE @MaxCount INT = (SELECT ISNULL(MaxCount, 0) FROM Job WHERE Id = @Id);
	BEGIN TRAN ProgessJob
	UPDATE Job
	SET
		[Count] = @Count,
		Message = @Message,
		DateProgressionSet = GETDATE()
	WHERE
		Id = @Id;
	COMMIT TRAN ProgressJob;
	-- Returns the progression in %
	IF @MaxCount > 0
	BEGIN
		DECLARE @Progress INT = ((@Count * 100) / @MaxCount)
		IF @Progress > 100
		RETURN 100;
	END
	RETURN 0;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK  TRAN;
	THROW;
END CATCH