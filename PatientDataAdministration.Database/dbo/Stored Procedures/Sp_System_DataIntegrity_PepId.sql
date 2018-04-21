CREATE PROCEDURE [dbo].[Sp_System_DataIntegrity_PepId]
	
AS
	select TOP(100) PepId, count(*) NumberOfOccurences FROM Patient_PatientInformation 
	WHERE IsDeleted = 'false' GROUP BY PepId HAVING count(*) > 1
	ORDER BY NumberOfOccurences DESC