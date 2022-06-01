CREATE PROCEDURE [dbo].[spAddress_Update]
	@Id UNIQUEIDENTIFIER,
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
    @ModifiedByNodeId UNIQUEIDENTIFIER
AS

BEGIN TRY
    BEGIN TRAN UpdateAddress
    UPDATE [Address]
    SET
        FirstName = @FirstName, 
        MiddleName = @MiddleName, 
        LastName = @LastName, 
        Company = @Company, 
        Street1 = @Street1, 
        Street2 = @Street2, 
        Street3 = @Street3, 
        City = @City, 
        State = @State, 
        PostalCode = @PostalCode, 
        CountryCode = @CountryCode, 
        Phone = @Phone, 
        Fax = @Fax, 
        Email = @Email,
		DateModified = GETDATE(),
        ModifiedByNodeId = @ModifiedByNodeId
    WHERE
        Id = @Id;
    COMMIT TRAN UpdateAddress;
    RETURN @@ROWCOUNT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
    THROW;
END CATCH