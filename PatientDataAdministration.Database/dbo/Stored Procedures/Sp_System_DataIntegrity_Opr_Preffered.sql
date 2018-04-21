CREATE PROCEDURE [dbo].[Sp_System_DataIntegrity_Opr_Preffered]
	@rowId INT
AS
	DECLARE @oldPepId VARCHAR(20);

	SELECT @oldPepId = PepId FROM Patient_PatientInformation 
	WHERE id = @rowId;

	WITH CTE AS(
		SELECT PepId, Surname, Othername, SiteId, RN = ROW_NUMBER()OVER(PARTITION BY PepId, Surname, Othername, SiteId ORDER BY PepId)
		FROM dbo.Patient_PatientInformation WHERE PepId = @oldPepId AND Id <> @rowId
	)
	DELETE FROM CTE WHERE RN >= 1;

	SELECT @oldPepId 'LastPepId', 
	(SELECT Surname + ' ' + Othername FROM Patient_PatientInformation WHERE id = @rowId) 'PatientName';