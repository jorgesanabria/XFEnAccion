using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Core.ServiceInterface
{
    public interface ISqliteService
    {
        SQLiteConnection GetConnection();
    }
}
