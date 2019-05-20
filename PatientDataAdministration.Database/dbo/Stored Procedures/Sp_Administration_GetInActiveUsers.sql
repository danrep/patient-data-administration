CREATE PROCEDURE [dbo].[Sp_Administration_GetInActiveUsers]
	@inactiveBar int
AS
	SELECT CurrentUserId, MIN(DateLog) DateLog FROM Administration_ClientLog 
	WHERE IsDeleted = 'false' AND DATEDIFF(DAY, DateLog, GETDATE()) >= 7 and CurrentUserId in (select Id from Administration_StaffInformation where IsDeleted = 'false')
	GROUP BY CurrentUserId