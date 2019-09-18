using Vonk.Facade.Relational;
using Microsoft.EntityFrameworkCore;
using myfacade.Models;
using System;
using Vonk.Core.Repository;

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
          throw new ArgumentException("Patient Id must be an integer value.");
        }
        else
        {
          return PredicateQuery(vp => vp.Id == observationId);
        }
      }
      return base.AddValueFilter(parameterName, value);
    }
  }
}

