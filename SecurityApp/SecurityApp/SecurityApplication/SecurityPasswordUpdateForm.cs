using Contracts;
using Storage;

namespace SecurityApplication
{
	public partial class SecurityPasswordUpdateForm : Form
	{
		private UserViewModel _currentUser;
		private readonly Storage.Storage _storage = new();

		public SecurityPasswordUpdateForm(UserViewModel user)
		{
			_currentUser = user;
			InitializeComponent();
			InitializeFormData();
			SetupEventHandlers();
		}

		private void InitializeFormData()
		{
			lblUsername.Text = _currentUser.Username;
			UpdatePasswordRequirements();
		}

		private void SetupEventHandlers()
		{
			btnUpdatePassword.Click += UpdateUserPassword;
			txtNewPassword.TextChanged += ValidateNewPassword;
			txtConfirmPassword.TextChanged += ValidatePasswordConfirmation;
			btnCancel.Click += CancelOperation;
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
				requirements.Add($"• Срок действия: {_currentUser.PasswordExpirationPeriodMonths} месяцев");
			}

			if (requirements.Count > 0)
			{
				txtRequirements.Lines = requirements.ToArray();
				panelRequirements.Visible = true;
			}
			else
			{
				panelRequirements.Visible = false;
			}
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

			// Убрана проверка сложности, оставлена только базовая валидация
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
				LastPasswordUpdate = DateTime.UtcNow
			};

			var result = _storage.Update(updateModel);
			if (result != null)
			{
				ShowSuccessMessage();
				NavigateToUserForm(result);
			}
			else
			{
				ShowErrorMessage("Ошибка при обновлении пароля");
			}
		}

		private bool ValidateInput()
		{
			if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
			{
				ShowErrorMessage("Введите новый пароль");
				return false;
			}

			if (txtNewPassword.Text != txtConfirmPassword.Text)
			{
				ShowErrorMessage("Пароли не совпадают");
				return false;
			}

			if (_currentUser.HasStrongPassword)
			{
				if (txtNewPassword.Text == _currentUser.Username)
				{
					ShowErrorMessage("Пароль не должен совпадать с логином");
					return false;
				}

				if (txtNewPassword.Text.Length < _currentUser.MinimumPasswordLength)
				{
					ShowErrorMessage($"Минимальная длина пароля: {_currentUser.MinimumPasswordLength} символов");
					return false;
				}
			}

			return true;
		}

		private void CancelOperation(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ShowSuccessMessage()
		{
			MessageBox.Show("Пароль успешно обновлен!\nТеперь вы можете войти в систему.",
						  "Обновление пароля",
						  MessageBoxButtons.OK,
						  MessageBoxIcon.Information);
		}

		private void ShowErrorMessage(string message)
		{
			MessageBox.Show(message,
						  "Ошибка",
						  MessageBoxButtons.OK,
						  MessageBoxIcon.Error);
		}

		private void NavigateToUserForm(UserViewModel updatedUser)
		{
			var userForm = new UserProfileForm(updatedUser);
			userForm.Show();
			this.Close();
		}
	}
}
