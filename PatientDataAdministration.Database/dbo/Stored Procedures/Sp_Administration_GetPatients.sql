CREATE PROCEDURE [dbo].[Sp_Administration_GetPatients]
	@query VARCHAR(100), 
	@stateId INT, 
	@siteId INT, 
	@hasBio BIT, 
	@hasNfc BIT
AS
	declare @patientInformation TABLE(
    [Id]                INT           NOT NULL,
    [PepId]             VARCHAR (100) NOT NULL,
    [PreviousId]        VARCHAR (100) NOT NULL,
    [SiteId]            INT           NOT NULL,
    [Title]             VARCHAR (100) NOT NULL,
    [Surname]           VARCHAR (100) NOT NULL,
    [Othername]         VARCHAR (100) NOT NULL,
    [Sex]               VARCHAR (10)  NOT NULL,
    [PhoneNumber]       VARCHAR (20)  NOT NULL,
    [MaritalStatus]     VARCHAR (10)  NOT NULL,
    [DateOfBirth]       DATETIME      NULL,
    [StateOfOrigin]     INT           NOT NULL,
    [HouseAddress]      VARCHAR (MAX) NOT NULL,
    [HouseAddressState] INT           NOT NULL,
    [HouseAddresLga]    INT           NOT NULL,
    [HospitalNumber]    VARCHAR (100) NOT NULL,
    [PassportData]      IMAGE         NULL,
    [IsDeleted]         BIT           NOT NULL,
    [WhenCreated]       DATETIME      NULL,
    [LastUpdated]       DATETIME      NULL,
    [HasBio]         BIT           NOT NULL,
    [HasNfc]         BIT           NOT NULL
	);
	DECLARE @tableLimit INT

	SET @tableLimit = 100
	
	SET @hasNfc = ISNULL(@hasNfc, 'false')
	set @hasBio = ISNULL(@hasBio, 'false')
	SET @siteId = ISNULL(@siteId, 0)
	set @stateId = ISNULL(@stateId, 0)
	SET @query = ISNULL(@query, '')

	IF LEN(@query) < 1 AND @stateId = 0 AND @siteId = 0
	BEGIN	
		INSERT INTO @patientInformation
		SELECT *, 
		IIF (EXISTS(SELECT 1 FROM Patient_PatientBiometricData WHERE IsDeleted = 'False' AND Patient_PatientBiometricData.PepId = Patient_PatientInformation.PepId), 'true', 'false'), 
		IIF (EXISTS(SELECT 1 FROM Patient_PatientNearFieldCommunicationData WHERE IsDeleted = 'False' AND Patient_PatientNearFieldCommunicationData.PepId = Patient_PatientInformation.PepId), 'true', 'false')
		FROM Patient_PatientInformation WHERE IsDeleted = 'False' 
		ORDER BY NEWID()
	end
	else
	BEGIN
		DECLARE @sites TABLE(
			id INT NOT NULL, 
			stateId INT NOT null
		)

		INSERT INTO @sites
		SELECT Id, StateId FROM Administration_SiteInformation WHERE 
			StateId = (CASE WHEN @stateId = 0 THEN StateId ELSE @stateId END) and 
			Id = (CASE WHEN @siteId = 0 THEN Id ELSE @siteId END) 
            
		INSERT INTO @patientInformation
		SELECT *, 
		IIF (EXISTS(SELECT 1 FROM Patient_PatientBiometricData WHERE IsDeleted = 'False' AND Patient_PatientBiometricData.PepId = Patient_PatientInformation.PepId), 'true', 'false'), 
		IIF (EXISTS(SELECT 1 FROM Patient_PatientNearFieldCommunicationData WHERE IsDeleted = 'False' AND Patient_PatientNearFieldCommunicationData.PepId = Patient_PatientInformation.PepId), 'true', 'false')
		FROM Patient_PatientInformation WHERE IsDeleted = 'False' AND 
		(
			PepId LIKE '%'+@query+'%' OR
			PreviousId LIKE '%'+@query+'%' OR
			Surname LIKE '%'+@query+'%' OR
			Othername LIKE '%'+@query+'%' OR
			Sex LIKE '%'+@query+'%' OR
			PhoneNumber LIKE '%'+@query+'%' OR
			HouseAddress LIKE '%'+@query+'%' OR
			HospitalNumber LIKE '%'+@query+'%' 
		) AND 
		SiteId IN ( SELECT id FROM @sites WHERE stateId = (CASE WHEN @stateId = 0 THEN stateId ELSE @stateId END))
	END

	SELECT TOP(@tableLimit) * FROM @patientInformation 
	WHERE HasNfc = (CASE WHEN @hasNfc = 1 THEN @hasNfc ELSE HasNfc END) AND 
	HasBio = (CASE WHEN @hasBio = 1 THEN @hasBio ELSE HasBio END)