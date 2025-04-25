CREATE TABLE [Person].[Record] (
    [InternalKey]                                    BIGINT    IDENTITY (1, 1) NOT NULL,
    [DateTimeCreated]                                DATETIME  CONSTRAINT [DF_Record_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    [DateTimeUpdated]                                DATETIME  NULL,
    [DateTimeDeleted]                                DATETIME  NULL,
    [PersonIdentifier]                               BIGINT    NOT NULL,
    [ExBritishArmedForcesIndicatorKey]               SMALLINT  NOT NULL,
    [LookedAfterChildIndicatorKey]                   SMALLINT  NOT NULL,
    [NationalInsuranceNumber]                        CHAR (13) NULL,
    [NewSexPartnersInTheLastThreeMonthsIndicatorKey] SMALLINT  NOT NULL,
    [NumberOfDaughtersUnderEighteen]                 TINYINT   NULL,
    [NumberOfSexPartnersInTheLastThreeMonthsCodeKey] SMALLINT  NOT NULL,
    [ParentalResponsibilitiesIndicatorKey]           SMALLINT  NOT NULL,
    [PersonAge]                                      TINYINT   NULL,
    [PersonBirthDate]                                DATE      NULL,
    [PersonBirthTime]                                TIME (7)  NULL,
    [PersonCount]                                    TINYINT   NULL,
    [PrisonerIndicatorKey]                           SMALLINT  NOT NULL,
    [SexWorkerIndicatorKey]                          SMALLINT  NOT NULL,
    CONSTRAINT [PK_Record] PRIMARY KEY CLUSTERED ([InternalKey] ASC),
    CONSTRAINT [FK_Record_ExBritishArmedForcesIndicator] FOREIGN KEY ([ExBritishArmedForcesIndicatorKey]) REFERENCES [Person].[ExBritishArmedForcesIndicator] ([InternalKey]),
    CONSTRAINT [FK_Record_LookedAfterChildIndicator] FOREIGN KEY ([LookedAfterChildIndicatorKey]) REFERENCES [Person].[LookedAfterChildIndicator] ([InternalKey]),
    CONSTRAINT [FK_Record_NewSexPartnersInTheLastThreeMonthsIndicator] FOREIGN KEY ([NewSexPartnersInTheLastThreeMonthsIndicatorKey]) REFERENCES [Person].[NewSexPartnersInTheLastThreeMonthsIndicator] ([InternalKey]),
    CONSTRAINT [FK_Record_NumberOfSexPartnersInTheLastThreeMonthsCode] FOREIGN KEY ([NumberOfSexPartnersInTheLastThreeMonthsCodeKey]) REFERENCES [Person].[NumberOfSexPartnersInTheLastThreeMonthsCode] ([InternalKey]),
    CONSTRAINT [FK_Record_ParentalResponsibilitiesIndicator] FOREIGN KEY ([ParentalResponsibilitiesIndicatorKey]) REFERENCES [Person].[ParentalResponsibilitiesIndicator] ([InternalKey]),
    CONSTRAINT [FK_Record_PrisonerIndicator] FOREIGN KEY ([PrisonerIndicatorKey]) REFERENCES [Person].[PrisonerIndicator] ([InternalKey]),
    CONSTRAINT [FK_Record_SexWorkerIndicator] FOREIGN KEY ([SexWorkerIndicatorKey]) REFERENCES [Person].[SexWorkerIndicator] ([InternalKey])
);








GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UniqueRow]
    ON [Person].[Record]([PersonIdentifier] ASC)
    INCLUDE([DateTimeDeleted]);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'An individual human being about whom information is maintained, for example a PERSON may be a PATIENT , employee, CARE PROFESSIONAL or a relative of a PATIENT. See https://www.datadictionary.nhs.uk/classes/person.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/sex_worker_indicator.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'SexWorkerIndicatorKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/prisoner_indicator.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'PrisonerIndicatorKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/person_identifier.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'PersonIdentifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/person_count.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'PersonCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/person_birth_time.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'PersonBirthTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/person_birth_date.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'PersonBirthDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/person_age.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'PersonAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/parental_responsibilities_indicator.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'ParentalResponsibilitiesIndicatorKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/number_of_daughters_under_18.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'NumberOfDaughtersUnderEighteen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/new_sex_partners_in_the_last_three_months_indicator.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'NewSexPartnersInTheLastThreeMonthsIndicatorKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/national_insurance_number.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'NationalInsuranceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/ex-british_armed_forces_indicator.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'ExBritishArmedForcesIndicatorKey';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/looked_after_child_indicator.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'LookedAfterChildIndicatorKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/number_of_sex_partners_in_last_three_months_code.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Record', @level2type = N'COLUMN', @level2name = N'NumberOfSexPartnersInTheLastThreeMonthsCodeKey';

