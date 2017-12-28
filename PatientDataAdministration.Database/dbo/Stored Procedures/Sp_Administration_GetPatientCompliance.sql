CREATE PROCEDURE [dbo].[Sp_Administration_GetPatientCompliance]
AS
	SELECT [pI].PepId FROM Patient_PatientInformation [pI] 
	INNER JOIN Patient_PatientBiometricData pBD
	ON [pI].PepId = pBD.PepId
	INNER JOIN Patient_PatientNearFieldCommunicationData pNFCD
	ON [pI].PepId = pNFCD.PepId
	WHERE [pI].IsDeleted = 'false' AND pBD.IsDeleted = 'false' AND pNFCD.IsDeleted = 'false'