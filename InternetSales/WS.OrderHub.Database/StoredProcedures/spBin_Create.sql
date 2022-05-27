CREATE PROCEDURE [dbo].[spBin_Create]
	@Id UNIQUEIDENTIFIER OUTPUT,
    @Name NVARCHAR(50),
    @IsReserved BIT,
    @IsDefault BIT,
    @CreatedByNodeId UNIQUEIDENTIFIER
AS
BEGIN TRY
    -- Checks if this name already exist
    SET @Id = (SELECT Id FROM Bin WHERE [Name] = @Name)
    IF @Id IS NULL
    BEGIN
        BEGIN TRAN CreateBin
        SET @Id = NEWID();
        INSERT INTO Bin
            (Id, [Name], IsReserved, IsDefault, DateCreated, CreatedByNodeId)
        VALUES
            (@Id, @Name, @IsReserved, @IsDefault, GETDATE(), @CreatedByNodeId)
        COMMIT TRAN;
        RETURN @@ROWCOUNT;
    END
    -- Doesn't do anything if name is already in use.

    --ELSE
    --    THROW 50001, 'Bin NAME is already in use', 1;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH