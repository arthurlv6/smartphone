using System;
using Xamarin.Forms;
using System.IO;

[assembly:Dependency(typeof(SmartPhone.Droid.SQLite_Android))]
namespace SmartPhone.Droid
{
	public class SQLite_Android: SmartPhone.ISQLite
    {
		public SQLite_Android ()
		{
		}
		#region ISQLite implementation
		public SQLite.Net.SQLiteConnection GetConnection ()
		{
			var fileName = "inventory.db3";
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var path = Path.Combine (documentsPath, fileName);

			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var connection = new SQLite.Net.SQLiteConnection (platform, path);

			return connection;
		}


		#endregion
	}
}

