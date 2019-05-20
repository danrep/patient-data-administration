CREATE PROCEDURE [dbo].[Sp_Administration_GetPatientDataManifest]
	@stateId INT = 0, 
	@siteId INT = 0, 
	@restrictOnBiometrics BIT = 'false', 
	@startDate DATETIME, 
	@endDate DATETIME
AS
	if (@stateId = 0)
	begin
		set @stateId = null
	end

	if (@siteId = 0)
	begin
		set @siteId = null
	end

	if (@restrictOnBiometrics = 'true')
	begin
		select ppi.*, ppbd.DateRegistered BiometricRegistrationDate, ppbd.FingerDataHash,
		CONVERT(bit, 1) HasBioMetrics 
		from Patient_PatientInformation ppi 
		inner join Patient_PatientBiometricData ppbd on ppi.PepId = ppbd.PepId where ppi.SiteId in 
		(select Id from Administration_SiteInformation where StateId = ISNULL(@stateId, Administration_SiteInformation.StateId) and 
		Id = ISNULL(@siteId, Administration_SiteInformation.Id)) and 
		ppi.WhenCreated > @startDate and ppi.WhenCreated < @endDate
	end
	else
	begin
		select ppi.*, ISNULL(ppbd.DateRegistered, N'1900-01-01') BiometricRegistrationDate, ISNULL(ppbd.FingerDataHash, NULL) FingerDataHash,
		CONVERT(bit, (CASE WHEN ISNULL(ppbd.DateRegistered, N'1900-01-01') = N'1900-01-01' THEN 0 ELSE 1 END)) HasBioMetrics 
		from Patient_PatientInformation ppi 
		left join Patient_PatientBiometricData ppbd on ppi.PepId = ppbd.PepId where ppi.SiteId in 
		(select Id from Administration_SiteInformation where StateId = ISNULL(@stateId, Administration_SiteInformation.StateId) and 
		Id = ISNULL(@siteId, Administration_SiteInformation.Id)) and 
		ppi.WhenCreated > @startDate and ppi.WhenCreated < @endDate
	end