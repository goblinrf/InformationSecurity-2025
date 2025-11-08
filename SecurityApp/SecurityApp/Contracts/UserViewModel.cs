using System.ComponentModel;

namespace Contracts
{
	public class UserViewModel : IUser
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		[DisplayName("Имя пользователя")]
		public string Username { get; set; } = string.Empty;
		[DisplayName("Имя пользователя")]
		public string Password { get; set; } = string.Empty;
		[DisplayName("Админ?")]
		public bool HasAdministratorRights { get; set; }
		public bool IsAccountLocked { get; set; }
		public bool HasStrongPassword { get; set; }

		[DisplayName("Минимальная длина пароля")]
		public int MinimumPasswordLength { get; set; }
		[DisplayName("Период активности пароля")]
		public int PasswordExpirationPeriodMonths { get; set; }
		[DisplayName("Последняя смена пароля")]
		public DateTime LastPasswordUpdate { get; set; }
	}
}
