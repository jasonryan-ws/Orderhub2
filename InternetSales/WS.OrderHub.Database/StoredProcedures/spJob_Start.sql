CREATE PROCEDURE [dbo].[spJob_Start]
	@Id UNIQUEIDENTIFIER OUTPUT,
	@TaskId UNIQUEIDENTIFIER,
	@StartedByNodeId UNIQUEIDENTIFIER,
	@MaxCount INT = 100,
	@ForceStart BIT = 0 --If true, start a new job regardless of another computer running on similar task 
AS
BEGIN TRY
	DECLARE @NodeName VARCHAR(25);
	SELECT TOP 1
		@Id = j.Id,
		@NodeName = n.Name
	FROM Job j
	JOIN [Node] n ON n.Id = j.StartedByNodeId 
	WHERE
		TaskId = @TaskId AND
		DateEnded IS NULL;

	IF @Id IS NOT NULL AND @ForceStart = 1
	BEGIN
		DECLARE @Message VARCHAR(255) = CONCAT('Terminated by ' , @NodeName);
		EXEC spJob_End @Id, @StartedByNodeId, @Message, 0;
		SET @Id = NULL;
	END

	IF @Id IS NULL
	BEGIN
		SET @Id = NEWID();
		BEGIN TRAN StartJob
		INSERT INTO Job
			(Id, TaskId, DateStarted, StartedByNodeId, MaxCount)
		VALUES
			(@Id, @TaskId, GETDATE(), @StartedByNodeId, @MaxCount);
		COMMIT TRAN StartJob;
		RETURN @@ROWCOUNT;
	END
	DECLARE @ErrorMessage VARCHAR(255) = @NodeName + ' is currently running this task';
	THROW 50001, @ErrorMessage, 1;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	THROW;
END CATCH