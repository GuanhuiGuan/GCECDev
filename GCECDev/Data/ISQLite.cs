using System;
using SQLite;

namespace GCECDev.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
