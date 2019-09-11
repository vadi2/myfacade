using Vonk.Core.Common;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using myfacade.Models;

namespace myfacade
{
    class ResourceMapper
    {
        public IResource MapPatient(ViSiPatient source)
        {
            var patient = new Patient
            {
                Id = source.Id.ToString(),
                BirthDate = source.DateOfBirth.ToFhirDate()
            };
            patient.Identifier.Add(new Identifier("http://mycompany.org/patientnumber",
                                                  source.PatientNumber));
            // etc.

            return patient as IResource;
        }
    }
}