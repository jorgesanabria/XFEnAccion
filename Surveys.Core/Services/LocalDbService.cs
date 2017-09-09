using Surveys.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveys.Core.Models;
using SQLite;
using Xamarin.Forms;
using Surveys.Entities;

namespace Surveys.Core.Services
{
    public class LocalDbService : ILocalDbService
    {
        private readonly SQLiteConnection connection = null;
        public LocalDbService()
        {
            connection = DependencyService.Get<ISqliteService>().GetConnection();
            CreateDatabase();
        }
        private void CreateDatabase()
        {
            if (connection.TableMappings.All(t => t.TableName != nameof(Survey)))
            {
                connection.CreateTable<Survey>();
            }
            if (connection.TableMappings.All(t => t.TableName != nameof(Team)))
            {
                connection.CreateTable<Team>();
            }
        }
        public Task DeleteSurveyAsync(Survey survey)
        {
            return Task.Run(()=>
            {
                var query = $"DELETE FROM Survey WHERE Id = '{survey.Id}'";
                var command = connection.CreateCommand(query);
                var result = command.ExecuteNonQuery();
                return result > 0;
            });
        }

        public Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            return Task.Run(() => (IEnumerable<Survey>)connection.Table<Survey>().ToArray());
        }

        public Task InsertSurveyAsync(Survey survey)
        {
            return Task.Run(() => connection.Insert(survey));
        }

        public Task DeleteAllSurveysAsync()
        {
            return Task.Run(() => connection.DeleteAll<Survey>());
        }

        public Task DeleteAllTeamsAync()
        {
            return Task.Run(() => connection.DeleteAll<Team>());
        }

        public Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return Task.Run(() => (IEnumerable<Team>)connection.Table<Team>().ToArray());
        }

        public Task InsertTeamsAsync(IEnumerable<Team> teams)
        {
            return Task.Run(() => connection.InsertAll(teams));
        }
    }
}
