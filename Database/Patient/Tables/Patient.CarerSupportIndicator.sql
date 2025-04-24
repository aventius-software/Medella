CREATE TABLE [Patient].[CarerSupportIndicator] (
    [InternalKey]         SMALLINT      NOT NULL,
    [DateTimeCreated]     DATETIME      CONSTRAINT [DF_CarerSupportIndicator_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    [DateTimeUpdated]     DATETIME      NULL,
    [DateTimeDeleted]     DATETIME      NULL,
    [ValidFromDate]       DATE          CONSTRAINT [DF_CarerSupportIndicator_ValidFromDate] DEFAULT ('1900-01-01') NOT NULL,
    [ValidToDate]         DATE          CONSTRAINT [DF_CarerSupportIndicator_ValidToDate] DEFAULT ('9999-12-31') NOT NULL,
    [NationalCode]        CHAR (2)      NOT NULL,
    [NationalDescription] VARCHAR (500) NOT NULL,
    [ShortDescription]    VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_CarerSupportIndicator] PRIMARY KEY CLUSTERED ([InternalKey] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UniqueRow]
    ON [Patient].[CarerSupportIndicator]([NationalCode] ASC, [ValidFromDate] ASC, [ValidToDate] ASC)
    INCLUDE([DateTimeCreated], [DateTimeUpdated], [DateTimeDeleted], [NationalDescription]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/carer_support_indicator.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'CarerSupportIndicator';

