CREATE PROCEDURE [DatabaseAdministration].[uspRunPostDeploymentProcesses] AS
BEGIN
	-- Patient schema reference tables (keep in alphabetical order!)
	EXEC [DatabaseAdministration].[uspSeedPatientCarerSupportIndicator]
	EXEC [DatabaseAdministration].[uspSeedPatientConsentObtainedIndicatorForCareProfessionalContact]
	EXEC [DatabaseAdministration].[uspSeedPatientConsentObtainedIndicatorForNationalJointRegistryRecordingData]
	EXEC [DatabaseAdministration].[uspSeedPatientNHSNumberStatusIndicatorCode]
END