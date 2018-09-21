CREATE PROCEDURE [dbo].[Sp_System_CleanUp]
AS
	--Regularize Sex
	UPDATE Patient_PatientInformation SET Sex = 'female' WHERE Sex = 'f';
	UPDATE Patient_PatientInformation SET Sex = 'male' WHERE Sex = 'm';	
	UPDATE Patient_PatientInformation SET Sex = 'female' WHERE Sex <> 'male';
	UPDATE Patient_PatientInformation SET Sex = 'male' WHERE Sex = 'male';

	--Remove Operational Duplicates
	WITH CTE AS(
		SELECT PepId, Surname, Othername, SiteId, RN = ROW_NUMBER()OVER(PARTITION BY PepId, Surname, Othername, SiteId ORDER BY PepId)
		FROM dbo.Patient_PatientInformation
	)
	DELETE FROM CTE WHERE RN > 1;

	--Sanitize PepIds
	UPDATE Patient_PatientInformation SET PepId = LTRIM(RTRIM(REPLACE(PepId, '?', ' '))) WHERE LEN(PepId) > 10
	UPDATE Patient_PatientInformation SET PepId = LTRIM(RTRIM(REPLACE(PepId, '=', '-'))) WHERE PepId LIKE '%=%'
RETURN 0