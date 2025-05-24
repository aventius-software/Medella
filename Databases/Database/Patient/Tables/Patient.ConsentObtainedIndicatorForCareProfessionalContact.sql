CREATE TABLE [Patient].[ConsentObtainedIndicatorForCareProfessionalContact] (
    [InternalKey]         SMALLINT      NOT NULL,
    [DateTimeCreated]     DATETIME      CONSTRAINT [DF_ConsentObtainedIndicatorForCareProfessionalContact_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    [DateTimeUpdated]     DATETIME      NULL,
    [DateTimeDeleted]     DATETIME      NULL,
    [ValidFromDate]       DATE          CONSTRAINT [DF_ConsentObtainedIndicatorForCareProfessionalContact_ValidFromDate] DEFAULT ('1900-01-01') NOT NULL,
    [ValidToDate]         DATE          CONSTRAINT [DF_ConsentObtainedIndicatorForCareProfessionalContact_ValidToDate] DEFAULT ('9999-12-31') NOT NULL,
    [NationalCode]        CHAR (1)      NOT NULL,
    [NationalDescription] VARCHAR (500) NOT NULL,
    [ShortDescription]    VARCHAR (50)  NULL,
    CONSTRAINT [PK_ConsentObtainedIndicatorForCareProfessionalContact] PRIMARY KEY CLUSTERED ([InternalKey] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/patient_consent_obtained_indicator_for_care_professional_contact.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'ConsentObtainedIndicatorForCareProfessionalContact';

