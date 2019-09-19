using Vonk.Facade.Relational;
using Microsoft.EntityFrameworkCore;
using myfacade.Models;
using System;
using Vonk.Core.Repository;
using System.Linq;

namespace myfacade
{
  public class BloodPressureQuery : RelationalQuery<ViSiBloodPressure>
  {
  }

  public class BPQueryFactory : RelationalQueryFactory<ViSiBloodPressure, BloodPressureQuery>
  {
    public BPQueryFactory(DbContext onContext) : base("Observation", onContext) { }

    public override BloodPressureQuery AddValueFilter(string parameterName, TokenValue value)
    {
      if (parameterName == "_id")
      {
        if (!int.TryParse(value.Code, out int observationId))
        {
          throw new ArgumentException("Observation Id must be an integer value.");
        }
        else
        {
          return PredicateQuery(vp => vp.Id == observationId);
        }
      }
      return base.AddValueFilter(parameterName, value);
    }

    public override BloodPressureQuery AddValueFilter(string parameterName, ReferenceToValue value)
    {
      if (parameterName == "subject" && value.Targets.Contains("Patient"))
      {
        var patientQuery = value.CreateQuery(new PatientQueryFactory(OnContext));
        var patIds = patientQuery.Execute(OnContext).Select(p => p.Id);
        Console.WriteLine($"ids: {String.Join(", ", patIds.ToArray())}");

        return PredicateQuery(bp => patIds.Contains(bp.PatientId));
      }
      return base.AddValueFilter(parameterName, value);
    }
  }
}

