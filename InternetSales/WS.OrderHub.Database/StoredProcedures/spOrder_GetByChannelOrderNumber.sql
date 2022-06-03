CREATE PROCEDURE [dbo].[spOrder_GetByChannelOrderNumber]
	@ChannelOrderNumber VARCHAR(25)
AS
BEGIN
	DECLARE @Id UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM [Order] WHERE ChannelOrderNumber = @ChannelOrderNumber);
	EXEC spOrder_GetById @Id
END