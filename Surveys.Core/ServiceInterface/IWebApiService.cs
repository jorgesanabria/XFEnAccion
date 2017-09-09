﻿using Surveys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Core.ServiceInterface
{
    public interface IWebApiService
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<bool> SaveSurveysAsync(IEnumerable<Survey> surveys);
        Task<bool> LoginAsync(string userName, string password);
    }
}
