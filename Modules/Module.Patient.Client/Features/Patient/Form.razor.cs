using Module.Patient.Shared.Features.Patient;
using Services.Client.Forms;

namespace Module.Patient.Client.Features.Patient;

public class FormBase : GenericMudBlazorFormComponentBase<PatientRecord, PatientValidator, long>
{
    protected override string GetApiRoute()
    {
        throw new NotImplementedException();
    }
}
