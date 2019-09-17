using Vonk.Facade.Relational;
using Vonk.Core.Repository;
using myfacade.Models;
using System;
using System.Threading.Tasks;
using Vonk.Core.Context;
using System.Collections.Generic;
using Vonk.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace myfacade
{
  public class ViSiRepository : SearchRepository
  {
    private readonly ViSiContext _visiContext;
    private readonly ResourceMapper _resourceMapper;

    public ViSiRepository(QueryContext queryContext, ViSiContext visiContext, ResourceMapper resourceMapper) : base(queryContext)
    {
      _visiContext = visiContext;
      _resourceMapper = resourceMapper;
    }

    protected override async Task<SearchResult> Search(string resourceType, IArgumentCollection arguments, SearchOptions options)
    {
      Console.WriteLine($"XXX Searching for: {resourceType}");
      switch (resourceType)
      {
        case "Patient":
          return await SearchPatient(arguments, options);
        default:
          throw new NotImplementedException($"ResourceType {resourceType} is not supported.");
      }
    }

    private async Task<SearchResult> SearchPatient(IArgumentCollection arguments, SearchOptions options)
    {
      Console.WriteLine($"XXX SearchPatient {arguments.ToString()}");
      var query = _queryContext.CreateQuery(new PatientQueryFactory(_visiContext), arguments, options);

      var count = await query.ExecuteCount(_visiContext);
      var patientResources = new List<IResource>();

      if (count > 0)
      {
        var visiPatients = await query.Execute(_visiContext).ToListAsync();

        foreach (var visiPatient in visiPatients)
        {
          Console.WriteLine($"XXX got a patient back, mapping and adding...");
          patientResources.Add(_resourceMapper.MapPatient(visiPatient));
        }
      }
      Console.WriteLine($"{patientResources[0].ToString()}");
      Console.WriteLine($"XXX pagesize {query.GetPageSize()}, count {count}");
      return new SearchResult(patientResources, query.GetPageSize(), count);
    }
  }
}
