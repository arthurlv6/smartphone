using System;
using SQLite.Net;

namespace SmartPhone
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

