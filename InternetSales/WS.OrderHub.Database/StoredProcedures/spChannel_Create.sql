CREATE PROCEDURE [dbo].[spChannel_Create]
    @Id UNIQUEIDENTIFIER OUTPUT,
    @Name NVARCHAR(25),
    @Code VARCHAR(5),
    @ColorCode INT,
	@CreatedByNodeId UNIQUEIDENTIFIER
AS
BEGIN TRY
    SET @Id = (SELECT Id FROM Channel WHERE [Name] = @Name)
    IF @Id IS NULL
    BEGIN
        BEGIN TRAN CreateChannel
        SET @Id = NEWID();
        INSERT INTO Channel
            (Id, [Name], Code, ColorCode, DateCreated, CreatedByNodeId)
        VALUES
            (@Id, @Name, @Code, @ColorCode, GETDATE(), @CreatedByNodeId);
        COMMIT TRAN CreateChannel;
        RETURN @@ROWCOUNT;
    END
    ELSE
        THROW 50001, 'Channel name is already in use', 1;      
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH