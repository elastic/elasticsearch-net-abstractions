namespace Elastic.Managed.Ephemeral
{
	/// <summary>
	/// Authentication credentials for the cluster
	/// </summary>
	public class ClusterAuthentication
	{
		/// <summary>
		/// Authentication credentials for X-Pack
		/// </summary>
		public class XPackCredentials
		{
			public string Username { get; set; }
			public string Role { get; set; }
			public string Password => Username;
		}

		/// <summary>
		/// Administrator credentials
		/// </summary>
		public static XPackCredentials Admin => new XPackCredentials { Username = "es_admin", Role = "admin" };

		/// <summary>
		/// User credentials
		/// </summary>
		public static XPackCredentials User => new XPackCredentials { Username = "es_user", Role = "user" };

		/// <summary>
		/// Credentials for all users
		/// </summary>
		public static XPackCredentials[] AllUsers { get; } = { Admin, User };
	}
}
