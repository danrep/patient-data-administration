CREATE PROCEDURE [dbo].[Sp_System_Indicators_PopulationDistro_30DayStatePlotData]
	@abbreviation VARCHAR(10) 
AS
BEGIN
	DECLARE @minDate DATETIME
	SET @minDate = CAST(DATEADD(DAY, -30,  GETDATE()) AS date)

	DECLARE @plotData TABLE(
		PlotDate DATETIME NOT NULL, 
		CreatedCount BIGINT NOT NULL, 
		UpdatedCount BIGINT NOT NULL
	)

	DECLARE @patientsInState TABLE(
		PepId varchar(20) NOT NULL,
		WhenCreated DATETIME NOT NULL,
		LastUpdated DATETIME NOT NULL
	)
	
	DECLARE @countNew BIGINT
	DECLARE @countUpdated BIGINT

   INSERT INTO @patientsInState 
   SELECT p_pi.PepId, CAST(p_pi.WhenCreated AS date) WhenCreated, CAST(p_pi.LastUpdated AS date) LastUpdated FROM Patient_PatientInformation p_pi 
		INNER JOIN Administration_SiteInformation a_si ON p_pi.SiteId = a_si.Id
		INNER JOIN System_State s_s ON a_si.StateId = s_s.Id
		WHERE p_pi.IsDeleted = 'false' AND s_s.StateAbbreviation = @abbreviation

	WHILE CAST(GETDATE() AS date) >= @minDate
	BEGIN
		SELECT @countNew = COUNT(*) FROM @patientsInState 
		WHERE WhenCreated = @minDate and WhenCreated = LastUpdated

		SELECT @countUpdated = COUNT(*) FROM @patientsInState 
		WHERE LastUpdated = @minDate and WhenCreated <> LastUpdated

		INSERT INTO @plotData VALUES(@minDate, @countNew, @countUpdated)

		SET @minDate = DATEADD(DAY, 1, @minDate)
	END

	SELECT * FROM @plotData
END