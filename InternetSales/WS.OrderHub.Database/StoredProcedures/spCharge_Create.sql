CREATE PROCEDURE [dbo].[spCharge_Create]
	@Id UNIQUEIDENTIFIER OUTPUT,
    @Name VARCHAR(25),
	@Description VARCHAR(255),
	@CreatedByNodeId UNIQUEIDENTIFIER,
	@ForceUpdate BIT = NULL -- NULL - throw an error, 0 - Suppress error, 1 - Perform update
AS
BEGIN TRY
	SET @Id = (SELECT Id FROM Charge WHERE [Name] = @Name);
	IF @Id IS NULL
	BEGIN
		BEGIN TRAN CreateCharge
		SET @Id = NEWID();
		INSERT INTO Charge
			(Id, [Name], [Description], DateCreated, CreatedByNodeId)
		VALUES
			(@Id, @Name, @Description, GETDATE(), @CreatedByNodeId);
		COMMIT TRAN CreateCharge;
		RETURN @@ROWCOUNT;
	END
	ELSE IF @ForceUpdate = 1
		EXEC spCharge_Update @Id, @Name, @Description, @CreatedByNodeId
	ELSE IF @ForceUpdate IS NULL
		THROW 50001, 'Charge name is already in use', 1;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	THROW;
END CATCH
