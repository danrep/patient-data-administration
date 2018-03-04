CREATE PROCEDURE [dbo].[Sp_System_Indicators_PopulationDistro_SexSiteState]
	
AS
	select COUNT(*) AS 'PatientPopulation', p_pi.Sex, p_pi.SiteId, s_s.StateAbbreviation, s_s.StateCode 
	from Patient_PatientInformation p_pi 
	INNER JOIN Administration_SiteInformation a_si
	ON p_pi.SiteId = a_si.Id
	INNER JOIN System_State s_s  
	ON a_si.StateId = s_s.Id
	WHERE p_pi.IsDeleted = 'false'
	GROUP BY p_pi.SiteId, p_pi.Sex, s_s.StateAbbreviation, s_s.StateCode
	ORDER BY p_pi.SiteId