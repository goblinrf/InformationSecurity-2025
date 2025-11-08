using Contracts;
using Storage;

namespace SecurityApplication
{
    internal static class Program
    {
		
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();
			//Application.Run(new SecurityPasswordUpdateForm(new UserViewModel
			//{
			//	Id = "aba",
			//	Username = "egor",
			//	Password = "model.Password",
			//	HasAdministratorRights = false,
			//	IsAccountLocked = false,
			//	HasStrongPassword = false,
			//	MinimumPasswordLength = 10,
			//	PasswordExpirationPeriodMonths = 10,
			//	LastPasswordUpdate = DateTime.MinValue,
			//}));

			Application.Run(new AuthenticationForm());
		}
	}
}