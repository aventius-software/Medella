CREATE TABLE [Patient].[ReasonPatientDoesNotHaveIndependentMentalHealthAdvocate] (
    [InternalKey]         SMALLINT      IDENTITY (1, 1) NOT NULL,
    [DateTimeCreated]     DATETIME      CONSTRAINT [DF_ReasonPatientDoesNotHaveIndependentMentalHealthAdvocate_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    [DateTimeUpdated]     DATETIME      NULL,
    [DateTimeDeleted]     DATETIME      NULL,
    [ValidFromDate]       DATE          CONSTRAINT [DF_ReasonPatientDoesNotHaveIndependentMentalHealthAdvocate_ValidFromDate] DEFAULT ('1900-01-01') NOT NULL,
    [ValidToDate]         DATE          CONSTRAINT [DF_ReasonPatientDoesNotHaveIndependentMentalHealthAdvocate_ValidToDate] DEFAULT ('9999-12-31') NOT NULL,
    [NationalCode]        CHAR (2)      NOT NULL,
    [NationalDescription] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_ReasonPatientDoesNotHaveIndependentMentalHealthAdvocate] PRIMARY KEY CLUSTERED ([InternalKey] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/reason_patient_does_not_have_independent_mental_health_advocate.html', @level0type = N'SCHEMA', @level0name = N'Patient', @level1type = N'TABLE', @level1name = N'ReasonPatientDoesNotHaveIndependentMentalHealthAdvocate';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UniqueRow]
    ON [Patient].[ReasonPatientDoesNotHaveIndependentMentalHealthAdvocate]([NationalCode] ASC, [ValidFromDate] ASC, [ValidToDate] ASC)
    INCLUDE([DateTimeCreated], [DateTimeUpdated], [DateTimeDeleted], [NationalDescription]);

