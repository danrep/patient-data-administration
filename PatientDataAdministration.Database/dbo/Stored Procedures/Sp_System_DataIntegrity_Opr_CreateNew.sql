CREATE PROCEDURE [dbo].[Sp_System_DataIntegrity_Opr_CreateNew]
	@rowId INT
AS
	DECLARE @oldPepId VARCHAR(20)
	DECLARE @siteId VARCHAR(10)
	DECLARE @siteCode VARCHAR(10)
	DECLARE @nextNumber VARCHAR(10)
	
	SELECT @siteId = SiteId FROM Patient_PatientInformation WHERE Id = @rowId
	SELECT @siteCode = siteCode FROM Administration_SiteInformation WHERE Id = @siteId

	SELECT @oldPepId = PepId FROM Patient_PatientInformation 
	WHERE id = (SELECT MAX(Id) FROM Patient_PatientInformation WHERE SiteId = @siteId) AND IsDeleted = 'false'
	SET @nextNumber = CONVERT(VARCHAR, CONVERT(INT, RTRIM(LTRIM(SUBSTRING(@oldPepId, 7, 6)))) + 1)

	DECLARE @newPepId VARCHAR(20)
	SET @newPepId = @siteCode + '-' + FORMAT(GETDATE(), 'yy') + '-' + REPLICATE('0', 4 - LEN(@nextNumber)) + @nextNumber

	UPDATE Patient_PatientInformation SET PepId =  @newPepId
	WHERE Id = @rowId

	SELECT @oldPepId 'LastPepId', @newPepId 'NewPepId', 
	(SELECT Surname + ' ' + Othername FROM Patient_PatientInformation WHERE id = @rowId) 'PatientName'