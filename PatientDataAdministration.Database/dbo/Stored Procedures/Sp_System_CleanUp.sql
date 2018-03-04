CREATE PROCEDURE [dbo].[Sp_System_CleanUp]
AS
	UPDATE Patient_PatientInformation SET sex = 'female' WHERE sex = 'f'
	UPDATE Patient_PatientInformation SET sex = 'male' WHERE sex = 'm'
	
	UPDATE Patient_PatientInformation SET sex = 'female' WHERE sex <> 'male'
	UPDATE Patient_PatientInformation SET sex = 'male' WHERE sex = 'male'
RETURN 0