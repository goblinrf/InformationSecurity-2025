namespace Contracts
{
	public interface IUser
	{
		string Id { get; }
		string Username { get; }
		string Password { get; }
		bool HasAdministratorRights { get; }
		bool IsAccountLocked { get; }
		bool HasStrongPassword { get; }
		int MinimumPasswordLength { get; }
		int PasswordExpirationPeriodMonths { get; }
		DateTime LastPasswordUpdate { get; }
	}
}
