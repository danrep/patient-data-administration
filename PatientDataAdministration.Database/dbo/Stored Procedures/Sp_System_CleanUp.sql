CREATE PROCEDURE [dbo].[Sp_System_CleanUp]
AS
	--Regularize Sex
	UPDATE Patient_PatientInformation SET Sex = 'female' WHERE Sex = 'f';
	UPDATE Patient_PatientInformation SET Sex = 'male' WHERE Sex = 'm';	
	UPDATE Patient_PatientInformation SET Sex = 'female' WHERE Sex <> 'male';
	UPDATE Patient_PatientInformation SET Sex = 'male' WHERE Sex = 'male';

	--Remove Operational Duplicates
	WITH CTE_1 AS(
		SELECT PepId, Surname, Othername, SiteId, RN = ROW_NUMBER()OVER(PARTITION BY PepId, Surname, Othername, SiteId ORDER BY PepId)
		FROM dbo.Patient_PatientInformation
	)
	DELETE FROM CTE_1 WHERE RN > 1;

	--Sanitize PepIds
	UPDATE Patient_PatientInformation SET PepId = LTRIM(RTRIM(REPLACE(PepId, '?', ' '))) WHERE LEN(PepId) > 10;
	UPDATE Patient_PatientInformation SET PepId = LTRIM(RTRIM(REPLACE(PepId, '=', '-'))) WHERE PepId LIKE '%=%';

	--Remove Duplicates from BioCases
	WITH CTE_2 AS(
		SELECT PivotPepId, SuspectPepId, RN = ROW_NUMBER()OVER(PARTITION BY PivotPepId, SuspectPepId ORDER BY PivotPepId)
		FROM dbo.Patient_PatientBiometricIntegrityCaseMember
		WHERE IsTreated = 'false' or IsDeleted = 'false'
	)
	DELETE FROM CTE_2 WHERE RN > 1;
RETURN 0