CREATE PROCEDURE [Patient].[uspGetRecordByKey]
	@key BIGINT
AS
BEGIN
	SELECT
	[pat].[InternalKey]
	,[pat].[DateTimeCreated]
	,[pat].[DateTimeUpdated]
	,[pat].[DateTimeDeleted]
	,[pat].[PersonRecordKey]
	,[pat].[CarerSupportIndicatorKey]
	,[pat].[CommunityHealthIndexNumber]
	--,[pat].[HealthAndCareNumber]
	--,[pat].[NHSNumber]
	--,[pat].[NHSNumberStatusIndicatorCodeKey]
	--,[pat].[OverseasVisitorUKArrivalDate]
	--,[pat].[PatientConsentObtainedIndicatorForCareProfessionalContactKey]
	--,[pat].[PatientConsentObtainedIndicatorForNationalJointRegistryRecordingDataKey]
	--,[pat].[ReasonPatientDoesNotHaveIndependentMentalCapacityAdvocateKey]
	--,[pat].[ReasonPatientDoesNotHaveIndependentMentalHealthAdvocateKey]
	--,[pat].[ReasonableAdjustmentRequiredIndicatorKey]

	FROM
	[Patient].[Record] [pat]

	WHERE
	[pat].[InternalKey] = @key
END