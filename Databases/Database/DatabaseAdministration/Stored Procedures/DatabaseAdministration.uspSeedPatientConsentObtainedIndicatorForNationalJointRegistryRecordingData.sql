CREATE PROCEDURE [DatabaseAdministration].[uspSeedPatientConsentObtainedIndicatorForNationalJointRegistryRecordingData] AS
BEGIN
	-- See https://www.datadictionary.nhs.uk/attributes/patient_consent_obtained_indicator_for_national_joint_registry_recording_data.html
	WITH [cteValues] AS (
		SELECT
		[InternalKey] = 1
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = 0
		,[NationalDescription] = 'No - the PATIENT has not consented to have their details recorded'
		,[ShortDescription] = 'No'

		UNION ALL

		SELECT
		[InternalKey] = 2
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = 1
		,[NationalDescription] = 'Yes - the PATIENT has consented to have their details recorded'
		,[ShortDescription] = 'Yes'

		UNION ALL

		SELECT
		[InternalKey] = 3
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = 3
		,[NationalDescription] = 'Not Recorded'
		,[ShortDescription] = 'Not Recorded'
	)

	-- Update data
	MERGE
	[Patient].[ConsentObtainedIndicatorForNationalJointRegistryRecordingData] [trg]

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