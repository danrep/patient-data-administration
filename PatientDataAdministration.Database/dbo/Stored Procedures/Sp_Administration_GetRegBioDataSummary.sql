CREATE PROCEDURE [dbo].[Sp_Administration_GetRegBioDataSummary]
	@startDate datetime = null,
	@endDate datetime = null
AS
	if (@startDate is null and @endDate is null)
	begin
		select ss.StateName, ss.StateAbbreviation, asi.SiteNameOfficial, asi.SiteCode, count(*) PatientPopulation from System_State ss 
		inner join Administration_SiteInformation asi on ss.Id = asi.StateId 
		inner join Patient_PatientInformation ppi on ppi.SiteId = asi.Id
		inner join Patient_PatientBiometricData ppbd on ppi.PepId = ppbd.PepId
		where ss.IsDeleted = 'false' and ppi.IsDeleted = 'false' and ppbd.IsDeleted = 'false'
		group by ppi.SiteId, ss.StateName, ss.StateAbbreviation, asi.SiteNameOfficial, asi.SiteCode
		order by ss.StateName, asi.SiteCode, asi.SiteNameOfficial
	end
	else
	begin
		select ss.StateName, ss.StateAbbreviation, asi.SiteNameOfficial, asi.SiteCode, count(*) PatientPopulation from System_State ss 
		inner join Administration_SiteInformation asi on ss.Id = asi.StateId 
		inner join Patient_PatientInformation ppi on ppi.SiteId = asi.Id
		inner join Patient_PatientBiometricData ppbd on ppi.PepId = ppbd.PepId
		where ss.IsDeleted = 'false' and ppi.IsDeleted = 'false' and ppbd.IsDeleted = 'false'
		and ppbd.DateRegistered > @startDate
		and ppbd.DateRegistered < @endDate
		group by ppi.SiteId, ss.StateName, ss.StateAbbreviation, asi.SiteNameOfficial, asi.SiteCode
		order by ss.StateName, asi.SiteCode, asi.SiteNameOfficial
	end