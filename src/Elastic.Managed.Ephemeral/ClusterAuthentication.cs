namespace Elastic.Managed.Ephemeral
{
	public class ClusterAuthentication
	{
		public class XPackCredentials
		{
			public string Username { get; set; }
			public string Role { get; set; }
			public string Password => Username;
		}

		public static XPackCredentials Admin => new XPackCredentials { Username = "es_admin", Role = "admin" };
		public static XPackCredentials User => new XPackCredentials { Username = "es_user", Role = "user" };

		public static XPackCredentials[] AllUsers { get; } = { Admin, User };
	}
}
