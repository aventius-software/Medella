CREATE PROCEDURE [DatabaseAdministration].[uspSeedPatientNHSNumberStatusIndicatorCode] AS
BEGIN
	-- See https://www.datadictionary.nhs.uk/attributes/nhs_number_status_indicator_code.html
	WITH [cteValues] AS (
		SELECT
		[InternalKey] = 1
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = '01'
		,[NationalDescription] = 'Number present and verified'
		,[ShortDescription] = NULL

		UNION ALL

		SELECT
		[InternalKey] = 2
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = '02'
		,[NationalDescription] = 'Number present but not traced'
		,[ShortDescription] = NULL

		UNION ALL

		SELECT
		[InternalKey] = 3
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = '03'
		,[NationalDescription] = 'Trace required'
		,[ShortDescription] = NULL

		UNION ALL

		SELECT
		[InternalKey] = 4
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = '04'
		,[NationalDescription] = 'Trace attempted - No match or multiple match found'
		,[ShortDescription] = NULL

		UNION ALL

		SELECT
		[InternalKey] = 5
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = '05'
		,[NationalDescription] = 'Trace needs to be resolved - (NHS NUMBER or PATIENT detail conflict)'
		,[ShortDescription] = NULL

		UNION ALL

		SELECT
		[InternalKey] = 6
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = '06'
		,[NationalDescription] = 'Trace in progress'
		,[ShortDescription] = NULL

		UNION ALL

		SELECT
		[InternalKey] = 7
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = '07'
		,[NationalDescription] = 'Number not present and trace not required'
		,[ShortDescription] = NULL

		UNION ALL

		SELECT
		[InternalKey] = 8
		,[DateTimeCreated] = '19000101'
		,[ValidFromDate] = '19000101'
		,[ValidToDate] = '99991231'
		,[NationalCode] = '08'
		,[NationalDescription] = 'Trace postponed (baby under six weeks old)'
		,[ShortDescription] = NULL
	)

	-- Update data
	MERGE
	[Patient].[NHSNumberStatusIndicatorCode] [trg]

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