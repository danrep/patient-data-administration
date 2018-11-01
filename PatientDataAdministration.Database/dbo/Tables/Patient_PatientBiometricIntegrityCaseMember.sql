CREATE TABLE [dbo].[Patient_PatientBiometricIntegrityCaseMember] (
    [Id]                              INT           IDENTITY (1, 1) NOT NULL,
    [PatientBiometricIntegrityCaseId] INT           NOT NULL,
    [MatchingScore]                   INT           NOT NULL,
    [PivotPepId]                      VARCHAR (20)  NOT NULL,
    [SuspectPepId]                    VARCHAR (20)  NOT NULL,
    [MemberTreatmentNote]             VARCHAR (100) NULL,
    [MemberTreatmentTypeId]           INT           NOT NULL,
    [DateTreated]                     DATETIME      NOT NULL,
    [IsTreated]                       BIT           NOT NULL,
    [IsDeleted]                       BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

