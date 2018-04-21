CREATE PROCEDURE [dbo].[Sp_System_DataIntegrity_Opr_Delete]
	@rowId INT
AS
	DECLARE @oldPepId VARCHAR(20)
	DECLARE @fullName VARCHAR(100)
	
	SELECT @oldPepId = PepId, @fullName = Surname + ' ' + Othername FROM Patient_PatientInformation 
	WHERE id = @rowId

	DELETE FROM Patient_PatientInformation WHERE Id = @rowId

	SELECT @oldPepId 'LastPepId', @fullName 'PatientName'