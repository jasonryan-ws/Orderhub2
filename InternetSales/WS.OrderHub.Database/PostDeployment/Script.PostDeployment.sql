/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\DefaultData\Nodes.sql
:r .\DefaultData\Countries.sql
:r .\DefaultData\States.sql
:r .\DefaultData\Addresses.sql
:r .\DefaultData\Configurations.sql
:r .\DefaultData\Bins.sql
:r .\DefaultData\Channels.sql
:r .\DefaultData\Charges.sql
:r .\DefaultData\Orders.sql
:r .\DefaultData\Products.sql
:r .\DefaultData\OrderItems.sql
