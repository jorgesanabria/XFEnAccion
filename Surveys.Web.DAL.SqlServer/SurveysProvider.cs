using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Surveys.Entities;

namespace Surveys.Web.DAL.SqlServer
{
    public class SurveysProvider : SqlServerProvider
    {
        public override string ConnectionString { get; set; } = System.Configuration.ConfigurationManager.ConnectionStrings["Surveys"].ConnectionString;
        public async Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            var result = new List<Survey>();
            var query = "SELECT * FROM Survey";
            using (var reader = await ExecuteReaderAsync(query))
            {
                while (reader.Read())
                {
                    result.Add(GetSurveyFromReader(reader));
                }
                return result;
            }
        }

        public async Task<int> InsertSurveyAsync(Survey survey)
        {
            if (survey == null)
            {
                return 0;
            }
            var query = @"INSERT INTO Survey (Id, Name, BirthDate, TeamId, Lat, Lon)
                          VALUES
                          (@Id, @Name, @BirthDate, @TeamId, @Lat, @Lon)";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", survey.Id),
                new SqlParameter("@Name", survey.Name),
                new SqlParameter("@BirthDate", survey.BirthDate),
                new SqlParameter("@TeamId", survey.TeamId),
                new SqlParameter("@Lat", survey.Lat),
                new SqlParameter("@Lon", survey.Lon)
            };
            var result = await ExecuteNonQueryAsync(query, parameters.ToArray());
            return result;
        }

        private Survey GetSurveyFromReader(SqlDataReader reader)
        {
            return new Survey
            {
                Id = reader[nameof(Survey.Id)].ToString(),
                Name = reader[nameof(Survey.Name)].ToString(),
                BirthDate = (DateTime)reader[nameof(Survey.BirthDate)],
                TeamId = (int)reader[nameof(Survey.TeamId)],
                Lat = (double)reader[nameof(Survey.Lat)],
                Lon = (double)reader[nameof(Survey.Lon)]
            };
        }
    }
}
