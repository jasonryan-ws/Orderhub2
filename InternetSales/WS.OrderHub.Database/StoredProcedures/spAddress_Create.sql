CREATE PROCEDURE [dbo].[spAddress_Create]
	@Id UNIQUEIDENTIFIER OUTPUT,
    @FirstName NVARCHAR(100), 
    @MiddleName NVARCHAR(100), 
    @LastName NVARCHAR(100), 
    @Company NVARCHAR(60), 
    @Street1 NVARCHAR(120), 
    @Street2 NVARCHAR(120), 
    @Street3 NVARCHAR(120), 
    @City NVARCHAR(120), 
    @State NVARCHAR(50), 
    @PostalCode NVARCHAR(20), 
    @CountryCode VARCHAR(5), 
    @Phone NVARCHAR(25), 
    @Fax NVARCHAR(25), 
    @Email NVARCHAR(100),
    @CreatedByNodeId UNIQUEIDENTIFIER
AS

BEGIN TRY
    BEGIN TRAN CreateAddress
    SET @Id = NEWID();
    INSERT INTO [Address]
        (Id, FirstName, MiddleName, LastName, Company, Street1, Street2, Street3, City, State, PostalCode, CountryCode, Phone, Fax, Email, DateCreated, CreatedByNodeId)
	VALUES
		(@Id, @FirstName, @MiddleName, @LastName, @Company, @Street1, @Street2, @Street3, @City, @State, @PostalCode, @CountryCode, @Phone, @Fax, @Email, GETDATE(), @CreatedByNodeId);
		
	COMMIT TRAN CreateAddress;
	RETURN @@ROWCOUNT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH
