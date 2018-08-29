using System;
using SQLite.Net.Attributes;
namespace SmartPhone
{
	public class UserInfo
	{
		[PrimaryKey,AutoIncrement]
		public int Id { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Mobile { get; set; }
		public bool ReceiveEmail{ get; set; }
		public bool ReceiveNotification{ get; set; }
        public string Token { get; set; }
        public string ServerEndPoint { get; set; }
    }
}

