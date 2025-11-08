namespace Contracts
{
	public class UserSearchModel
	{
		public string? Id { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public bool? HasAdministratorRights { get; set; }
		public bool? IsAccountLocked { get; set; }
		public bool? HasStrongPassword { get; set; }
		public int? MinimumPasswordLength { get; set; }
	}
}
