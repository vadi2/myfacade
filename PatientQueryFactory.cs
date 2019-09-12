using Vonk.Facade.Relational;
using Microsoft.EntityFrameworkCore;
using myfacade.Models;
using System;
using Vonk.Core.Repository;

namespace myfacade
{
  public class PatientQuery : RelationalQuery<ViSiPatient>
  {
    PatientQuery AddValueFilter(string parameterName, TokenValue value) {
      if (parameterName == "_id")
      {
        if (!int.TryParse(value.Code, out int patientId))
        {
          throw new ArgumentException("Patient Id must be an integer value.");
        }
        else
        {
          return PredicateQuery(vp => vp.Id == patientId);
        }
      }
      return base.AddValueFilter(parameterName, value);
    }
  }

  public class PatientQueryFactory : RelationalQueryFactory<ViSiPatient, PatientQuery>
  {
    public PatientQueryFactory(DbContext onContext) : base("Patient", onContext) { }
  }
}

