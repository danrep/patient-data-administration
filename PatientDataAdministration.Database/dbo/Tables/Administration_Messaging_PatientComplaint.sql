CREATE TABLE [dbo].[Administration_Messaging_PatientComplaint] (
    [Id]                BIGINT        IDENTITY (1, 1) NOT NULL,
    [MessageId]         VARCHAR (MAX) NOT NULL,
    [SentDate]          DATETIME      NOT NULL,
    [SentByNumber]      VARCHAR (20)  NOT NULL,
    [SentByPatient]     VARCHAR (20)  NULL,
    [MessageText]       VARCHAR (MAX) NOT NULL,
    [CurrentCaseStatus] INT           NOT NULL,
    [IsDeleted]         BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

