using Vonk.Facade.Relational;
using Microsoft.EntityFrameworkCore;
using myfacade.Models;
using System;
using Vonk.Core.Repository;
using System.Linq;

namespace myfacade
{
  public class PatientQuery : RelationalQuery<ViSiPatient>
  {
  }

  public class PatientQueryFactory : RelationalQueryFactory<ViSiPatient, PatientQuery>
  {
    public PatientQueryFactory(DbContext onContext) : base("Patient", onContext) { }

    public override PatientQuery AddValueFilter(string parameterName, TokenValue value)
    {
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

    public override PatientQuery AddValueFilter(string parameterName, ReferenceFromValue value)
    {
      if (parameterName == "subject" && value.Source == "Observation")
      {
        var obsQuery = value.CreateQuery(new BPQueryFactory(OnContext));
        var obsIds = obsQuery.Execute(OnContext).Select(bp => bp.PatientId);

        return PredicateQuery(p => obsIds.Contains(p.Id));
      }
      return base.AddValueFilter(parameterName, value);
    }
  }
}

