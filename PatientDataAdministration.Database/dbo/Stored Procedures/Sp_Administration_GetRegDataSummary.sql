CREATE PROCEDURE [dbo].[Sp_Administration_GetRegDataSummary]
	@startDate datetime = null,
	@endDate datetime = null
AS
	if (@startDate is null and @endDate is null)
	begin
		select ss.StateName, ss.StateAbbreviation, asi.SiteNameOfficial, asi.SiteCode, count(*) PatientPopulation from System_State ss 
		inner join Administration_SiteInformation asi
		on ss.Id = asi.StateId 
		inner join Patient_PatientInformation ppi 
		on ppi.SiteId = asi.Id
		where ss.IsDeleted = 'false' and ppi.IsDeleted = 'false'
		group by ppi.SiteId, ss.StateName, ss.StateAbbreviation, asi.SiteNameOfficial, asi.SiteCode
		order by ss.StateName, asi.SiteCode, asi.SiteNameOfficial
	end
	else
	begin
		select ss.StateName, ss.StateAbbreviation, asi.SiteNameOfficial, asi.SiteCode, count(*) PatientPopulation from System_State ss 
		inner join Administration_SiteInformation asi
		on ss.Id = asi.StateId 
		inner join Patient_PatientInformation ppi 
		on ppi.SiteId = asi.Id
		where ss.IsDeleted = 'false' and ppi.IsDeleted = 'false'
		and ppi.WhenCreated > @startDate
		and ppi.WhenCreated < @endDate
		group by ppi.SiteId, ss.StateName, ss.StateAbbreviation, asi.SiteNameOfficial, asi.SiteCode
		order by ss.StateName, asi.SiteCode, asi.SiteNameOfficial
	end