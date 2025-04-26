using Services.Shared.Models;

namespace Module.Patient.Shared.Features.Person;

public class PersonRecord : DataModelBase<long>
{
    public long PersonIdentifier { get; set; }
    public short ExBritishArmedForcesIndicatorKey { get; set; }
    public short LookedAfterChildIndicatorKey { get; set; }
    public string? NationalInsuranceNumber { get; set; }
    public short NewSexPartnersInTheLastThreeMonthsIndicatorKey { get; set; }
    public short NumberOfDaughtersUnderEighteen { get; set; }
    public short NumberOfSexPartnersInTheLastThreeMonthsCodeKey { get; set; }
    public short ParentalResponsibilitiesIndicatorKey { get; set; }
    public short PersonAge { get; set; }
    public DateOnly? PersonBirthDate { get; set; }
    public TimeOnly? PersonBirthTime { get; set; }
    public short PersonCount { get; set; }
    public short PrisonerIndicatorKey { get; set; }
    public short SexWorkerIndicatorKey { get; set; }
}
