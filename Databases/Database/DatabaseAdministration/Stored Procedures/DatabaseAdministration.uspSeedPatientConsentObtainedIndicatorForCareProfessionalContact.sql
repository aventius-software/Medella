CREATE PROCEDURE [DatabaseAdministration].[uspSeedPatientConsentObtainedIndicatorForCareProfessionalContact] AS
BEGIN
	-- See https://www.datadictionary.nhs.uk/attributes/patient_consent_obtained_indicator_for_care_professional_contact.html
	WITH [cteValues] AS (
		SELECT
		[InternalKey] = 1
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = 'Y'
		,[NationalDescription] = 'Yes - the PATIENT has consented for their CARE PROFESSIONAL to be contacted about their care'
		,[ShortDescription] = 'Yes'

		UNION ALL

		SELECT
		[InternalKey] = 2
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = 'N'
		,[NationalDescription] = 'No - the PATIENT has not consented for their CARE PROFESSIONAL to be contacted about their care'
		,[ShortDescription] = 'No'
	)

	-- Update data
	MERGE
	[Patient].[ConsentObtainedIndicatorForCareProfessionalContact] [trg]

	USING
	[cteValues] [src]
	ON
	[trg].[InternalKey] = [src].[InternalKey]

	WHEN NOT MATCHED BY TARGET THEN INSERT (
		[InternalKey]
		,[DateTimeCreated]
		,[ValidFromDate]
		,[ValidToDate]
		,[NationalCode]
		,[NationalDescription]
		,[ShortDescription]
	) VALUES (
		[src].[InternalKey]
		,[src].[DateTimeCreated]
		,[src].[ValidFromDate]
		,[src].[ValidToDate]
		,[src].[NationalCode]
		,[src].[NationalDescription]
		,[src].[ShortDescription]
	);
END