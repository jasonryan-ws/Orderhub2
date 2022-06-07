CREATE PROCEDURE [dbo].[spProduct_Search]
	@Keyword NVARCHAR(4000)
AS
BEGIN
	DECLARE @searchText NVARCHAR(4000)
	SET @searchText = CONCAT('"*', TRIM(@Keyword), '*"')
	SET @searchText = REPLACE(@searchText, ' ','*" AND "*')

	SELECT
		p.*
	FROM Product p
	WHERE
		(
			p.SKU = @Keyword OR
			p.UPC = @Keyword OR
			CONTAINS(p.Name, @searchText)
		)
	ORDER BY p.Name
END