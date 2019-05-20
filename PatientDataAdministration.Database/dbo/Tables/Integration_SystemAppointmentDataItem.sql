CREATE TABLE [dbo].[Integration_SystemAppointmentDataItem] (
    [Id]                     BIGINT         IDENTITY (1, 1) NOT NULL,
    [DateLogged]             DATETIME       NOT NULL,
    [AppointmentOffice]      VARCHAR (10)   NOT NULL,
    [AppointmentData]        VARCHAR (100)  NOT NULL,
    [PepId]                  VARCHAR (20)   NOT NULL,
    [GeneratedMessage]       VARCHAR (100)  NOT NULL,
    [OperationStatus]        BIT            NOT NULL,
    [IsDeleted]              BIT            NOT NULL,
    [AppointmentDate]        DATETIME       NOT NULL,
    [PhoneNumber]            VARCHAR (20)   NULL,
    [MessageId]              VARCHAR (100)  NULL,
    [InitialResponsePayload] VARCHAR (1000) NULL,
    [FinalResponsePayload]   VARCHAR (1000) NULL,
    [MessageStatus]          INT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

