using Vonk.Core.Common;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using myfacade.Models;
using Vonk.Fhir.R3;
using System.Collections.Generic;
using System;

namespace myfacade
{
  public class ResourceMapper
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

      if (source.EmailAddress != null)
      {
        patient.Telecom.Add(new ContactPoint
        {
          System = ContactPoint.ContactPointSystem.Email,
          Value = source.EmailAddress
        });
      }

      var patientName = new HumanName { Use = HumanName.NameUse.Nickname };
      patientName.Given = new List<string> { source.FirstName };
      patientName.Family = source.FamilyName;
      patient.Name.Add(patientName);

      return patient.ToIResource();
    }

    public IResource MapBloodPressure(ViSiBloodPressure source)
    {
        var offset = new DateTimeOffset(source.MeasuredAt);
        Console.WriteLine($"datetime - {source.MeasuredAt}, offset - {offset}");
        var observation = new Observation{
            Effective = new FhirDateTime(new DateTimeOffset(source.MeasuredAt).ToFhirDateTime())
        };

        return observation.ToIResource();
    }
  }
}
