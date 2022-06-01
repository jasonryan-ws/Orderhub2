CREATE PROCEDURE [dbo].[spBin_Create]
	@Id UNIQUEIDENTIFIER OUTPUT,
    @Name NVARCHAR(50),
    @IsReserved BIT,
    @IsDefault BIT,
    @CreatedByNodeId UNIQUEIDENTIFIER,
    @ForceUpdate BIT = NULL
    -- @ForceUpdate NULL - Throw an error message
    -- @ForceUpdate 1 - Update if name exists
    -- @ForceUpdate 0 - Suppress error
AS
BEGIN TRY
    -- Checks if this name already exist
    SET @Id = (SELECT Id FROM Bin WHERE [Name] = @Name)
    IF @Id IS NULL
    BEGIN
        BEGIN TRAN CreateBin
        IF @IsDefault = 1
        BEGIN
            UPDATE Bin
            SET IsDefault = 0
        END
        SET @Id = NEWID();
        INSERT INTO Bin
            (Id, [Name], IsReserved, IsDefault, DateCreated, CreatedByNodeId)
        VALUES
            (@Id, @Name, @IsReserved, @IsDefault, GETDATE(), @CreatedByNodeId)
        COMMIT TRAN CreateBin;
        RETURN @@ROWCOUNT;
    END
    IF @ForceUpdate = 1
        EXEC spBin_Update @Id, @Name, @IsReserved, @IsDefault, @CreatedByNodeId
    ELSE IF @ForceUpdate IS NULL
        THROW 50001, 'Bin name is already in use', 1;
    -- IF @ForceUpdate = 0
       -- Suppress error
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH