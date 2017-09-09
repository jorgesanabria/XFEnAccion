using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveys.Entities;
using System.Data.SqlClient;

namespace Surveys.Web.DAL.SqlServer
{
    public class TeamsProvider : SqlServerProvider
    {
        public override string ConnectionString { get; set; } = System.Configuration.ConfigurationManager.ConnectionStrings["Surveys"].ConnectionString;
        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            var result = new List<Team>();
            var query = "SELECT * FROM Team";
            using (var reader = await ExecuteReaderAsync(query))
            {
                while(reader.Read())
                {
                    result.Add(GetTeamFromReader(reader));
                }
                return result;
            }
        }

        private Team GetTeamFromReader(SqlDataReader reader)
        {
            return new Team
            {
                Id = (int)reader[nameof(Team.Id)],
                Name = reader[nameof(Team.Name)].ToString(),
                Color = reader[nameof(Team.Color)].ToString(),
                Logo = reader[nameof(Team.Logo)] is DBNull ? null : (byte[])reader[nameof(Team.Logo)]
            };
        }
    }
}
