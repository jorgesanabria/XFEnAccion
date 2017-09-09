using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Core.Models
{
    public class TokenResponseModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty(".issused")]
        public string IssusedAt { get; set; }
        [JsonProperty(".expires")]
        public string ExpiresAt { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
    }
}
