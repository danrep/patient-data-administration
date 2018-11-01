CREATE TABLE [dbo].[Patient_PatientBiometricIntegrityCase] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [DateGenerated]   DATETIME      DEFAULT (getdate()) NOT NULL,
    [PivotPepId]      VARCHAR (100) NULL,
    [CaseName]        VARCHAR (100) NULL,
    [CaseDescription] VARCHAR (100) NULL,
    [CaseStatus]      INT           NOT NULL,
    [IsDeleted]       BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

