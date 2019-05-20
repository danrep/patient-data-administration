CREATE TABLE [dbo].[Integration_SystemPhoneNumberBlacklist] (
    [Id]                  BIGINT       IDENTITY (1, 1) NOT NULL,
    [DateLogged]          DATETIME     NOT NULL,
    [PhoneNumber]         VARCHAR (20) NULL,
    [LastOperationStatus] INT          NOT NULL,
    [IsDeleted]           BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

