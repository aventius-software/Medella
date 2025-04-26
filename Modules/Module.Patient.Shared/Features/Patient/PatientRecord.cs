using Services.Shared.Models;

namespace Module.Patient.Shared.Features.Patient;

public class PatientRecord : DataModelBase<long>
{    
    public long PersonRecordKey { get;set; }
    public short CarerSupportIndicatorKey { get;set; }
    public string? CommunityHealthIndexNumber { get;set; }
    public string? HealthAndCareNumber { get;set; }
    public string? NHSNumber { get;set; }
    public short NHSNumberStatusIndicatorCodeKey { get;set; }
    public DateOnly OverseasVisitorUKArrivalDate { get;set; }
    public short PatientConsentObtainedIndicatorForCareProfessionalContactKey { get;set; }
    public short PatientConsentObtainedIndicatorForNationalJointRegistryRecordingDataKey { get;set; }
    public short ReasonPatientDoesNotHaveIndependentMentalCapacityAdvocateKey { get;set; }
    public short ReasonPatientDoesNotHaveIndependentMentalHealthAdvocateKey { get;set; }
    public short ReasonableAdjustmentRequiredIndicatorKey { get;set; }
}
