CREATE PROCEDURE [dbo].[spNode_Create]
	@Id UNIQUEIDENTIFIER OUTPUT,
	@Name VARCHAR(100),
	@Description VARCHAR(255)
AS
BEGIN TRY
	SET @Id = (SELECT Id FROM [Node] WHERE [Name] = @Name);
	IF @Id IS NULL
	BEGIN
		BEGIN TRAN CreateNode
		SET @Id = NEWID();
		INSERT INTO [Node]
			(Id, [Name], [Description], DateCreated)
		VALUES
			(@Id, @Name, @Description, GETDATE())
		COMMIT TRAN CreateNode;
		RETURN @@ROWCOUNT;
	END
-- Doesn't do anything if the name is already in use.
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	THROW;
END CATCH