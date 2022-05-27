CREATE PROCEDURE [dbo].[spNode_DeleteUndelete]
    @Id UNIQUEIDENTIFIER,
    @IsDeleted BIT,
    @DateDeleted DATETIME,
    @DeletedByNodeId UNIQUEIDENTIFIER
AS
BEGIN TRY
    BEGIN TRAN DeleteUndeleteNode
    IF @IsDeleted = 0
    BEGIN
        SET @DateDeleted = NULL;
        SET @DeletedByNodeId = NULL;
    END 
    UPDATE [Node]
    SET
        IsDeleted = @IsDeleted,
        DateDeleted = @DateDeleted,
        DeletedByNodeId = @DeletedByNodeId
    WHERE
        Id = @Id;
    COMMIT TRAN;
    RETURN @@ROWCOUNT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH