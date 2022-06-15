CREATE PROCEDURE [dbo].[spJob_SetProgression]
	@Id UNIQUEIDENTIFIER,
	@Count INT,
	@Message VARCHAR(255),
	@DateEnded DATETIME OUTPUT, -- If not null, it means that this job has been terminated by user while in progress,
	@EndedByNodeId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN TRY
	DECLARE @MaxCount INT = (SELECT ISNULL(MaxCount, 0) FROM Job WHERE Id = @Id);
	SELECT
		@DateEnded = DateEnded,
		@EndedByNodeId = EndedByNodeId
	FROM Job
	WHERE Id = @Id;

	IF @DateEnded IS NULL
	BEGIN
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
	END
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK  TRAN;
	THROW;
END CATCH