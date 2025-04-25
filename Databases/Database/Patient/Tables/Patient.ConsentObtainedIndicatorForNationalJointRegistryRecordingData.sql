CREATE TABLE [Patient].[ConsentObtainedIndicatorForNationalJointRegistryRecordingData] (
    [InternalKey]         SMALLINT      IDENTITY (1, 1) NOT NULL,
    [DateTimeCreated]     DATETIME      CONSTRAINT [DF_PatientConsentObtainedIndicatorForNationalJointRegistryRecordingData_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    [DateTimeUpdated]     DATETIME      NULL,
    [DateTimeDeleted]     DATETIME      NULL,
    [ValidFromDate]       DATE          CONSTRAINT [DF_PatientConsentObtainedIndicatorForNationalJointRegistryRecordingData_ValidFromDate] DEFAULT ('1900-01-01') NOT NULL,
    [ValidToDate]         DATE          CONSTRAINT [DF_PatientConsentObtainedIndicatorForNationalJointRegistryRecordingData_ValidToDate] DEFAULT ('9999-12-31') NOT NULL,
    [NationalCode]        TINYINT       NOT NULL,
    [NationalDescription] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_ConsentObtainedIndicatorForNationalJointRegistryRecordingData] PRIMARY KEY CLUSTERED ([InternalKey] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/patient_consent_obtained_indicator_for_national_joint_registry_recording_data.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'ConsentObtainedIndicatorForNationalJointRegistryRecordingData';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UniqueRow]
    ON [Patient].[ConsentObtainedIndicatorForNationalJointRegistryRecordingData]([NationalCode] ASC, [ValidFromDate] ASC, [ValidToDate] ASC)
    INCLUDE([DateTimeCreated], [DateTimeUpdated], [DateTimeDeleted], [NationalDescription]);

