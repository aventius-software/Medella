CREATE TABLE [Patient].[Record] (
    [InternalKey]                                                             BIGINT    IDENTITY (1, 1) NOT NULL,
    [DateTimeCreated]                                                         DATETIME  CONSTRAINT [DF_Record_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    [DateTimeUpdated]                                                         DATETIME  NULL,
    [DateTimeDeleted]                                                         DATETIME  NULL,
    [PersonRecordKey]                                                         BIGINT    NOT NULL,
    [CarerSupportIndicatorKey]                                                SMALLINT  NOT NULL,
    [CommunityHealthIndexNumber]                                              CHAR (10) NULL,
    [HealthAndCareNumber]                                                     CHAR (10) NULL,
    [NHSNumber]                                                               CHAR (10) NULL,
    [NHSNumberStatusIndicatorCodeKey]                                         SMALLINT  NOT NULL,
    [OverseasVisitorUKArrivalDate]                                            DATE      NULL,
    [PatientConsentObtainedIndicatorForCareProfessionalContactKey]            SMALLINT  NOT NULL,
    [PatientConsentObtainedIndicatorForNationalJointRegistryRecordingDataKey] SMALLINT  NOT NULL,
    [ReasonPatientDoesNotHaveIndependentMentalCapacityAdvocateKey]            SMALLINT  NOT NULL,
    [ReasonPatientDoesNotHaveIndependentMentalHealthAdvocateKey]              SMALLINT  NOT NULL,
    [ReasonableAdjustmentRequiredIndicatorKey]                                SMALLINT  NOT NULL,
    CONSTRAINT [PK_Record] PRIMARY KEY CLUSTERED ([InternalKey] ASC),
    CONSTRAINT [FK_Record_CarerSupportIndicator] FOREIGN KEY ([CarerSupportIndicatorKey]) REFERENCES [Patient].[CarerSupportIndicator] ([InternalKey]),
    CONSTRAINT [FK_Record_ConsentObtainedIndicatorForCareProfessionalContact] FOREIGN KEY ([PatientConsentObtainedIndicatorForCareProfessionalContactKey]) REFERENCES [Patient].[ConsentObtainedIndicatorForCareProfessionalContact] ([InternalKey]),
    CONSTRAINT [FK_Record_ConsentObtainedIndicatorForNationalJointRegistryRecordingData] FOREIGN KEY ([PatientConsentObtainedIndicatorForNationalJointRegistryRecordingDataKey]) REFERENCES [Patient].[ConsentObtainedIndicatorForNationalJointRegistryRecordingData] ([InternalKey]),
    CONSTRAINT [FK_Record_NHSNumberStatusIndicatorCode] FOREIGN KEY ([NHSNumberStatusIndicatorCodeKey]) REFERENCES [Patient].[NHSNumberStatusIndicatorCode] ([InternalKey]),
    CONSTRAINT [FK_Record_ReasonableAdjustmentRequiredIndicator] FOREIGN KEY ([ReasonableAdjustmentRequiredIndicatorKey]) REFERENCES [Patient].[ReasonableAdjustmentRequiredIndicator] ([InternalKey]),
    CONSTRAINT [FK_Record_ReasonPatientDoesNotHaveIndependentMentalCapacityAdvocate] FOREIGN KEY ([ReasonPatientDoesNotHaveIndependentMentalCapacityAdvocateKey]) REFERENCES [Patient].[ReasonPatientDoesNotHaveIndependentMentalCapacityAdvocate] ([InternalKey]),
    CONSTRAINT [FK_Record_ReasonPatientDoesNotHaveIndependentMentalHealthAdvocate] FOREIGN KEY ([ReasonPatientDoesNotHaveIndependentMentalHealthAdvocateKey]) REFERENCES [Patient].[ReasonPatientDoesNotHaveIndependentMentalHealthAdvocate] ([InternalKey]),
    CONSTRAINT [FK_Record_Record] FOREIGN KEY ([PersonRecordKey]) REFERENCES [Person].[Record] ([InternalKey])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/classes/patient.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record';


GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/overseas_visitor_uk_arrival_date.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'OverseasVisitorUKArrivalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/nhs_number_status_indicator_code.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'NHSNumberStatusIndicatorCodeKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/nhs_number.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'NHSNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/health_and_care_number.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'HealthAndCareNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/community_health_index_number.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'CommunityHealthIndexNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/carer_support_indicator.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'CarerSupportIndicatorKey';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/classes/person.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'PersonRecordKey';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UniqueRow]
    ON [Patient].[Record]([NHSNumber] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/reason_patient_does_not_have_independent_mental_health_advocate.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'ReasonPatientDoesNotHaveIndependentMentalHealthAdvocateKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/reason_patient_does_not_have_independent_mental_capacity_advocate.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'ReasonPatientDoesNotHaveIndependentMentalCapacityAdvocateKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/reasonable_adjustment_required_indicator.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'ReasonableAdjustmentRequiredIndicatorKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/patient_consent_obtained_indicator_for_national_joint_registry_recording_data.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'PatientConsentObtainedIndicatorForNationalJointRegistryRecordingDataKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/patient_consent_obtained_indicator_for_care_professional_contact.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'PatientConsentObtainedIndicatorForCareProfessionalContactKey';

