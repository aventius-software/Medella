CREATE TABLE [Person].[NumberOfSexPartnersInTheLastThreeMonthsCode] (
    [InternalKey]         SMALLINT      IDENTITY (1, 1) NOT NULL,
    [DateTimeCreated]     DATETIME      CONSTRAINT [DF_NumberOfSexPartnersInTheLastThreeMonthsCode_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    [DateTimeUpdated]     DATETIME      NULL,
    [DateTimeDeleted]     DATETIME      NULL,
    [ValidFromDate]       DATE          CONSTRAINT [DF_NumberOfSexPartnersInTheLastThreeMonthsCode_ValidFromDate] DEFAULT ('1900-01-01') NOT NULL,
    [ValidToDate]         DATE          CONSTRAINT [DF_NumberOfSexPartnersInTheLastThreeMonthsCode_ValidToDate] DEFAULT ('9999-12-31') NOT NULL,
    [NationalCode]        CHAR (2)      NOT NULL,
    [NationalDescription] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_NumberOfSexPartnersInTheLastThreeMonthsCode] PRIMARY KEY CLUSTERED ([InternalKey] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'https://www.datadictionary.nhs.uk/attributes/number_of_sex_partners_in_last_three_months_code.html', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'NumberOfSexPartnersInTheLastThreeMonthsCode';

