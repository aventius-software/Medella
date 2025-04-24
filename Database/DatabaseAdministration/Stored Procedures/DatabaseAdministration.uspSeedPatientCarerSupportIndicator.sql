CREATE PROCEDURE DatabaseAdministration.uspSeedPatientCarerSupportIndicator AS
BEGIN
	-- See https://www.datadictionary.nhs.uk/attributes/carer_support_indicator.html
	WITH [cteValues] AS (
		SELECT
		[InternalKey] = 1
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = '01'
		,[NationalDescription] = 'Yes - carer support is available'
		,[ShortDescription] = 'Yes'

		UNION ALL

		SELECT
		[InternalKey] = 2
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = '02'
		,[NationalDescription] = 'No - carer support is not available'
		,[ShortDescription] = 'No'
	)

	-- Update data
	MERGE
	[Patient].[CarerSupportIndicator] [trg]

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