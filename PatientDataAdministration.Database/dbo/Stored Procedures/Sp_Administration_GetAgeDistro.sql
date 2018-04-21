CREATE procedure [dbo].[Sp_Administration_GetAgeDistro]
AS 
BEGIN
	SELECT patient_patientInformation.RangeOfValues AS RangeOfValues, COUNT(*) AS NumberOfOccurences
	FROM (
		SELECT CASE  
			WHEN DATEDIFF(YEAR, dateofbirth, GETDATE()) BETWEEN 0 AND 12 THEN '0-12'
			WHEN DATEDIFF(YEAR, dateofbirth, GETDATE()) BETWEEN 13 AND 19 THEN '13-19'
			WHEN DATEDIFF(YEAR, dateofbirth, GETDATE()) BETWEEN 20 AND 29 THEN '20-29'
			WHEN DATEDIFF(YEAR, dateofbirth, GETDATE()) BETWEEN 30 AND 39 THEN '30-39'
			WHEN DATEDIFF(YEAR, dateofbirth, GETDATE()) BETWEEN 40 AND 49 THEN '40-49'
			WHEN DATEDIFF(YEAR, dateofbirth, GETDATE()) BETWEEN 50 AND 59 THEN '50-59'
			WHEN DATEDIFF(YEAR, dateofbirth, GETDATE()) BETWEEN 60 AND 69 THEN '60-69'
			WHEN DATEDIFF(YEAR, dateofbirth, GETDATE()) BETWEEN 70 AND 79 THEN '70-79'
			WHEN DATEDIFF(YEAR, dateofbirth, GETDATE()) BETWEEN 80 AND 89 THEN '80-89'
			ELSE '90 above' 
		END AS RangeOfValues
		FROM Patient_PatientInformation where DateOfBirth is not null) patient_patientInformation
	GROUP BY patient_patientInformation.RangeOfValues
	ORDER BY patient_patientInformation.RangeOfValues
END