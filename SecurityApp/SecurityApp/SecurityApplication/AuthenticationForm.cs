using Contracts;
using Storage;

namespace SecurityApplication
{
	public partial class AuthenticationForm : Form
	{
		private readonly Storage.Storage _storage = new();
		private int _failedAttempts = 0;
		private const int MAX_ATTEMPTS = 3;

		public AuthenticationForm()
		{
			InitializeComponent();
			SetupEventHandlers();
		}

		private void SetupEventHandlers()
		{
			btnAuthenticate.Click += AuthenticateUser;
			btnHelp.Click += ShowHelpDocumentation;
			txtPassword.KeyPress += OnPasswordKeyPress;
		}

		private void OnPasswordKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				AuthenticateUser(sender, e);
			}
		}

		private void AuthenticateUser(object sender, EventArgs e)
		{
			var username = txtUsername.Text.Trim();
			var password = txtPassword.Text;

			if (string.IsNullOrEmpty(username))
			{
				ShowErrorMessage("Введите имя пользователя");
				txtUsername.Focus();
				return;
			}

			var passwordHash = Hasher.GenerateMd4Hash(password);
			var user = _storage.GetElement(new UserSearchModel
			{
				Username = username,
				Password = passwordHash
			});

			if (user == null)
			{
				HandleFailedAttempt();
				return;
			}

			ProcessSuccessfulLogin(user);
		}

		private void HandleFailedAttempt()
		{
			_failedAttempts++;
			var remainingAttempts = MAX_ATTEMPTS - _failedAttempts;

			if (_failedAttempts >= MAX_ATTEMPTS)
			{
				ShowSecurityAlert();
				Application.Exit();
				return;
			}

			ShowErrorMessage($"Неверные учетные данные. Осталось попыток: {remainingAttempts}");
			txtPassword.Clear();
			txtPassword.Focus();
		}

		private void ProcessSuccessfulLogin(UserViewModel user)
		{
			if (user.IsAccountLocked)
			{
				ShowErrorMessage("Учетная запись заблокирована. Обратитесь к администратору.");
				return;
			}

			if (IsPasswordExpired(user))
			{
				ForcePasswordChange(user);
				return;
			}

			OpenUserInterface(user);
		}

		private bool IsPasswordExpired(UserViewModel user)
		{
			if (user.PasswordExpirationPeriodMonths <= 0)
				return false;

			var expiryDate = user.LastPasswordUpdate.AddMonths(user.PasswordExpirationPeriodMonths);
			return DateTime.UtcNow > expiryDate;
		}

		private void ForcePasswordChange(UserViewModel user)
		{
			var passwordForm = new SecurityPasswordUpdateForm(user);
			passwordForm.FormClosed += (s, e) => this.Show();
			passwordForm.Show();
			this.Hide();
		}

		private void OpenUserInterface(UserViewModel user)
		{
			Form nextForm = user.HasAdministratorRights
				? new AdminDashboard(user)
				: new UserProfileForm(user);

			nextForm.FormClosed += (s, e) => this.Show();
			nextForm.Show();
			this.Hide();
		}

		private void ShowHelpDocumentation(object sender, EventArgs e)
		{
			var helpForm = new DocumentationForm();
			helpForm.FormClosed += (s, e) => this.Show();
			helpForm.Show();
			this.Hide();
		}

		private void ShowErrorMessage(string message)
		{
			MessageBox.Show(message, "Ошибка аутентификации",
						  MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void ShowSecurityAlert()
		{
			MessageBox.Show("Превышено максимальное количество попыток входа. Приложение будет закрыто.",
						  "Безопасность системы",
						  MessageBoxButtons.OK,
						  MessageBoxIcon.Error);
		}
	}
}
