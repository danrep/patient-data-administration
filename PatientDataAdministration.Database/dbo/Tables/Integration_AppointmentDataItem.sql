CREATE TABLE [dbo].[Integration_AppointmentDataItem] (
    [Id]                        BIGINT        IDENTITY (1, 1) NOT NULL,
    [AppointmentDataManifestId] BIGINT        NOT NULL,
    [PepId]                     VARCHAR (10)  NOT NULL,
    [DateVisit]                 DATETIME      NOT NULL,
    [DateAppointment]           DATETIME      NOT NULL,
    [AppointmentOffice]         VARCHAR (5)   NOT NULL,
    [AppointmentData]           VARCHAR (MAX) NOT NULL,
    [IsDeleted]                 BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

