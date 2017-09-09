using Surveys.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveys.Entities;

namespace Surveys.Core.ServiceInterface
{
    public interface ILocalDbService
    {
        Task<IEnumerable<Survey>> GetAllSurveysAsync();
        Task InsertSurveyAsync(Survey survey);
        Task DeleteSurveyAsync(Survey survey);
        Task DeleteAllSurveysAsync();
        Task DeleteAllTeamsAync();
        Task<IEnumerable<Team>> GetAllTeamsAsync();
        Task InsertTeamsAsync(IEnumerable<Team> teams);
    }
}
