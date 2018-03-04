CREATE PROCEDURE [dbo].[Sp_System_Indicators_PopulationDistro_BioCount]
	@abbrevation VARCHAR(10)
AS
	select COUNT(*) AS 'PatientPopulation', p_pi.Sex, p_pi.SiteId, s_s.StateAbbreviation, s_s.StateCode, 
	CONVERT(BIT, IIF ( p_pi.LastUpdated > p_pi.WhenCreated, 'true', 'false' )) IsUpdated
	from Patient_PatientInformation p_pi 
	INNER JOIN Patient_PatientBiometricData p_pbd ON p_pi.PepId = p_pbd.PepId
	INNER JOIN Administration_SiteInformation a_si ON p_pi.SiteId = a_si.Id
	INNER JOIN System_State s_s ON a_si.StateId = s_s.Id
	WHERE p_pi.IsDeleted = 'false' AND s_s.StateAbbreviation =  ISNULL(@abbrevation, s_s.StateAbbreviation)
	GROUP BY p_pi.SiteId, p_pi.Sex, s_s.StateAbbreviation, s_s.StateCode, p_pi.LastUpdated, p_pi.WhenCreated
	ORDER BY p_pi.SiteId