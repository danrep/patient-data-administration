CREATE TABLE [dbo].[Patient_PatientBiometricData] (
    [Id]                      INT           IDENTITY (1, 1) NOT NULL,
    [PepId]                   VARCHAR (100) NOT NULL,
    [FingerPrimary]           VARCHAR (MAX) NOT NULL,
    [FingerSecondary]         VARCHAR (MAX) NOT NULL,
    [FingerPrimaryPosition]   VARCHAR (MAX) DEFAULT ('Left Thumb') NOT NULL,
    [FingerSecondaryPosition] VARCHAR (MAX) DEFAULT ('Right Thumb') NOT NULL,
    [FingerDataHash]          VARCHAR (MAX) NULL,
    [DateRegistered]          DATETIME      NOT NULL,
    [IsValid]                 BIT           NOT NULL,
    [IsDeleted]               BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);





