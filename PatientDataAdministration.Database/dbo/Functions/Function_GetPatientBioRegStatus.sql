CREATE FUNCTION [dbo].[Function_GetPatientBioRegStatus]
(
	@pepId varchar(50)
)
RETURNS BIT
AS
BEGIN
	declare @status INT

	select @status = COUNT(Id) FROM Patient_PatientBiometricData WHERE PepId = @pepId AND IsDeleted = 'false';

	return @status
END