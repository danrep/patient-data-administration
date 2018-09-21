CREATE PROCEDURE [dbo].[Sp_System_Reporting_PopulationDataSite]
	@siteId INT, 
	@lowerDate DATETIME, 
	@upperDate DATETIME
AS
BEGIN
	DECLARE @patientInFocus TABLE(
		PepId VARCHAR(100), 
		HasBio BIT, 
		HasNfc BIT
	)

	INSERT INTO @patientInFocus
	SELECT PepId, dbo.Function_GetPatientBioRegStatus(PepId), dbo.Function_GetPatientNfcRegStatus(PepId)
	FROM Patient_PatientInformation 
	WHERE SiteId = @siteId AND LastUpdated >= @lowerDate AND LastUpdated < @upperDate AND IsDeleted = 'false'

	DECLARE @populationData TABLE(
		PopulationCount BIGINT NOT NULL, 
		PopulationCountBio BIGINT NOT NULL, 
		PopulationCountNfc BIGINT NOT NULL
	)

	INSERT INTO @populationData
	SELECT 
	(SELECT COUNT(*) FROM @patientInFocus),
	(SELECT COUNT(*) FROM @patientInFocus WHERE HasBio = 'true'),
	(SELECT COUNT(*) FROM @patientInFocus WHERE HasNfc = 'true')

	SELECT * FROM @populationData
END