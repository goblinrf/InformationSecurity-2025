using Contracts;
using Storage;

namespace SecurityApplication
{
	public partial class UserProfileForm : Form
	{
		private readonly Storage.Storage _storage = new();
		private UserViewModel _currentUser;

		public UserProfileForm(UserViewModel user)
		{
			_currentUser = user;
			InitializeComponent();
			InitializeFormData();
			SetupEventHandlers();
		}

		private void InitializeFormData()
		{
			lblWelcome.Text = $"Добро пожаловать, {_currentUser.Username}!";
			UpdatePasswordRequirements();
		}

		private void SetupEventHandlers()
		{
			btnUpdatePassword.Click += UpdateUserPassword;
			txtCurrentPassword.TextChanged += ClearValidationMessages;
			txtNewPassword.TextChanged += ValidateNewPassword;
			txtConfirmPassword.TextChanged += ValidatePasswordConfirmation;
		}

		private void UpdatePasswordRequirements()
		{
			var requirements = new List<string>();

			if (_currentUser.HasStrongPassword)
			{
				requirements.Add("• Пароль не должен совпадать с логином");
				requirements.Add($"• Минимальная длина: {_currentUser.MinimumPasswordLength} символов");
			}

			if (_currentUser.PasswordExpirationPeriodMonths > 0)
			{
				var expiryDate = _currentUser.LastPasswordUpdate.AddMonths(_currentUser.PasswordExpirationPeriodMonths);
				requirements.Add($"• Следующая смена: {expiryDate:dd.MM.yyyy}");
			}

			txtRequirements.Lines = requirements.ToArray();
		}

		private void ClearValidationMessages(object sender, EventArgs e)
		{
			lblValidationMessage.Text = "";
		}

		private void ValidateNewPassword(object sender, EventArgs e)
		{
			var newPassword = txtNewPassword.Text;

			if (string.IsNullOrEmpty(newPassword))
			{
				lblPasswordStrength.Text = "Введите новый пароль";
				lblPasswordStrength.ForeColor = Color.Gray;
				return;
			}

			if (_currentUser.HasStrongPassword)
			{
				if (newPassword == _currentUser.Username)
				{
					lblPasswordStrength.Text = "⚠ Пароль совпадает с логином";
					lblPasswordStrength.ForeColor = Color.Orange;
					return;
				}

				if (newPassword.Length < _currentUser.MinimumPasswordLength)
				{
					lblPasswordStrength.Text = $"⚠ Минимум {_currentUser.MinimumPasswordLength} символов";
					lblPasswordStrength.ForeColor = Color.Orange;
					return;
				}
			}

			lblPasswordStrength.Text = "✓ Пароль соответствует требованиям";
			lblPasswordStrength.ForeColor = Color.Green;
		}

		private void ValidatePasswordConfirmation(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtConfirmPassword.Text))
			{
				lblConfirmationStatus.Text = "Подтвердите пароль";
				lblConfirmationStatus.ForeColor = Color.Gray;
				return;
			}

			if (txtNewPassword.Text == txtConfirmPassword.Text)
			{
				lblConfirmationStatus.Text = "✓ Пароли совпадают";
				lblConfirmationStatus.ForeColor = Color.Green;
			}
			else
			{
				lblConfirmationStatus.Text = "✗ Пароли не совпадают";
				lblConfirmationStatus.ForeColor = Color.Red;
			}
		}

		private void UpdateUserPassword(object sender, EventArgs e)
		{
			if (!ValidateInput())
				return;

			var newPassword = txtNewPassword.Text;
			var passwordHash = Hasher.GenerateMd4Hash(newPassword);

			var updateModel = new UserBindingModel
			{
				Id = _currentUser.Id,
				Username = _currentUser.Username,
				Password = passwordHash,
				HasAdministratorRights = _currentUser.HasAdministratorRights,
				IsAccountLocked = _currentUser.IsAccountLocked,
				HasStrongPassword = _currentUser.HasStrongPassword,
				MinimumPasswordLength = _currentUser.MinimumPasswordLength,
				PasswordExpirationPeriodMonths = _currentUser.PasswordExpirationPeriodMonths,
				LastPasswordUpdate = DateTime.UtcNow,
			};

			var result = _storage.Update(updateModel);
			if (result != null)
			{
				_currentUser = result;
				ShowSuccessMessage("Пароль успешно изменен");
				ClearPasswordFields();
			}
			else
			{
				ShowErrorMessage("Ошибка при изменении пароля");
			}
		}

		private bool ValidateInput()
		{
			if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
			{
				ShowValidationError("Введите новый пароль");
				return false;
			}

			// Если текущий пароль не пустой - проверяем его корректность
			if (!string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
			{
				var currentPasswordHash = Hasher.GenerateMd4Hash(txtCurrentPassword.Text);
				var userCheck = _storage.GetElementWithPassword(new UserSearchModel
				{
					Username = _currentUser.Username,
					Password = currentPasswordHash
				});

				if (userCheck == null)
				{
					ShowValidationError("Текущий пароль неверен");
					return false;
				}
			}
			// Если текущий пароль пустой - проверяем, что у пользователя действительно пустой пароль
			else
			{
				var emptyPasswordHash = Hasher.GenerateMd4Hash(string.Empty);
				var userCheck = _storage.GetElement(new UserSearchModel
				{
					Username = _currentUser.Username,
					Password = emptyPasswordHash
				});

				if (userCheck == null)
				{
					ShowValidationError("Для смены пароля введите текущий пароль");
					return false;
				}
			}

			if (txtNewPassword.Text != txtConfirmPassword.Text)
			{
				ShowValidationError("Новые пароли не совпадают");
				return false;
			}

			if (_currentUser.HasStrongPassword)
			{
				if (txtNewPassword.Text == _currentUser.Username)
				{
					ShowValidationError("Пароль не должен совпадать с логином");
					return false;
				}

				if (txtNewPassword.Text.Length < _currentUser.MinimumPasswordLength)
				{
					ShowValidationError($"Минимальная длина пароля: {_currentUser.MinimumPasswordLength} символов");
					return false;
				}
			}

			return true;
		}

		private void ClearPasswordFields()
		{
			txtCurrentPassword.Clear();
			txtNewPassword.Clear();
			txtConfirmPassword.Clear();
			lblPasswordStrength.Text = "";
			lblConfirmationStatus.Text = "";
			lblValidationMessage.Text = "";
		}

		private void ShowValidationError(string message)
		{
			lblValidationMessage.Text = message;
			lblValidationMessage.ForeColor = Color.Red;
		}

		private void ShowSuccessMessage(string message)
		{
			MessageBox.Show(message, "Успех",
						  MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void ShowErrorMessage(string message)
		{
			MessageBox.Show(message, "Ошибка",
						  MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}