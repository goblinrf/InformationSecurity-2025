namespace Contracts
{
	public class UserBindingModel : IUser
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string Username { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;

		public bool HasAdministratorRights { get; set; }

		public bool IsAccountLocked { get; set; }

		public bool HasStrongPassword { get; set; }

		public int MinimumPasswordLength { get; set; }
		public int PasswordExpirationPeriodMonths { get; set; }
		public DateTime LastPasswordUpdate { get; set; }
	}
}
