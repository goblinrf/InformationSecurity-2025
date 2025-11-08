using Contracts;
using Storage;
using System.Data;

namespace SecurityApplication
{
	public partial class AdminDashboard : Form
	{
		private readonly Storage.Storage _storage = new();
		private readonly UserViewModel _currentAdmin;
		private List<UserViewModel> _userList = new();
		private int _selectedIndex = 0;

		public AdminDashboard(UserViewModel adminUser)
		{
			_currentAdmin = adminUser;
			InitializeComponent();
			InitializeData();
			SetupEventHandlers();
		}

		private void InitializeData()
		{
			RefreshUserData();
			InitializeDefaultValues();
		}

		private void InitializeDefaultValues()
		{
			numAdminPasswordLifetime.Value = 90;
			numDefaultPasswordLength.Value = 8;
			numDefaultPasswordLifetime.Value = 60;
		}

		private void SetupEventHandlers()
		{
			dataGridViewUsers.SelectionChanged += OnUserSelectionChanged;
			dataGridViewUsers.DataBindingComplete += OnDataBindingComplete;
		}

		private void RefreshUserData()
		{
			_userList = _storage.GetFullList();
			DisplayUserGrid();
			UpdateNavigationInfo();
		}

		private void DisplayUserGrid()
		{
			dataGridViewUsers.DataSource = _userList.Select(u => new
			{
				u.Id,
				Логин = u.Username,
				Администратор = u.HasAdministratorRights ? "Да" : "Нет",
				Статус = u.IsAccountLocked ? "Заблокирован" : "Активен",
				СложныйПароль = u.HasStrongPassword ? "Да" : "Нет",
				СрокДействия = $"{u.PasswordExpirationPeriodMonths} мес."
			}).ToList();

			dataGridViewUsers.Columns["Id"].Visible = false;
		}

		private void OnDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			dataGridViewUsers.DataBindingComplete -= OnDataBindingComplete;
			UpdateSelectionHighlight();
		}

		private void UpdateNavigationInfo()
		{
			if (_userList.Count == 0)
			{
				lblNavigationInfo.Text = "Пользователи не найдены";
				return;
			}

			if (_selectedIndex < 0) _selectedIndex = 0;
			if (_selectedIndex >= _userList.Count) _selectedIndex = _userList.Count - 1;

			var currentUser = _userList[_selectedIndex];
			lblNavigationInfo.Text = $"Запись {_selectedIndex + 1} из {_userList.Count} | {currentUser.Username}";
		}

		private void UpdateSelectionHighlight()
		{
			if (dataGridViewUsers.Rows.Count == 0 || _userList.Count == 0) return;

			foreach (DataGridViewRow row in dataGridViewUsers.Rows)
			{
				if (row.Cells["Id"].Value?.ToString() == _userList[_selectedIndex].Id)
				{
					row.Selected = true;
					dataGridViewUsers.FirstDisplayedScrollingRowIndex = row.Index;
					break;
				}
			}
		}

		private void OnUserSelectionChanged(object sender, EventArgs e)
		{
			if (dataGridViewUsers.SelectedRows.Count == 0) return;

			var selectedId = dataGridViewUsers.SelectedRows[0].Cells["Id"].Value?.ToString();
			var index = _userList.FindIndex(x => x.Id == selectedId);

			if (index >= 0)
			{
				_selectedIndex = index;
				UpdateNavigationInfo();
			}
		}

		#region Navigation Controls
		private void NavigateToFirst(object sender, EventArgs e)
		{
			_selectedIndex = 0;
			UpdateNavigationInfo();
			UpdateSelectionHighlight();
		}

		private void NavigateToPrevious(object sender, EventArgs e)
		{
			_selectedIndex = Math.Max(0, _selectedIndex - 1);
			UpdateNavigationInfo();
			UpdateSelectionHighlight();
		}

		private void NavigateToNext(object sender, EventArgs e)
		{
			_selectedIndex = Math.Min(_userList.Count - 1, _selectedIndex + 1);
			UpdateNavigationInfo();
			UpdateSelectionHighlight();
		}

		private void NavigateToLast(object sender, EventArgs e)
		{
			_selectedIndex = Math.Max(0, _userList.Count - 1);
			UpdateNavigationInfo();
			UpdateSelectionHighlight();
		}

		private void RefreshData(object sender, EventArgs e) => RefreshUserData();
		#endregion

		#region Admin Password Management
		private void ChangeAdminPassword(object sender, EventArgs e)
		{
			var currentPassword = txtCurrentPassword.Text;
			var newPassword = txtNewPassword.Text;
			var confirmPassword = txtConfirmPassword.Text;

			if (string.IsNullOrWhiteSpace(newPassword))
			{
				ShowMessage("Введите новый пароль", "Ошибка");
				return;
			}

			if (newPassword != confirmPassword)
			{
				ShowMessage("Пароли не совпадают", "Ошибка");
				return;
			}

			var hashedCurrent = Hasher.GenerateMd4Hash(currentPassword);
			var currentUser = _storage.GetElementWithPassword(new UserSearchModel
			{
				Username = _currentAdmin.Username,
				Password = hashedCurrent
			});

			if (currentUser == null)
			{
				ShowMessage("Текущий пароль неверен", "Ошибка");
				return;
			}

			var hashedNew = Hasher.GenerateMd4Hash(newPassword);
			var updateModel = new UserBindingModel
			{
				Id = _currentAdmin.Id,
				Username = _currentAdmin.Username,
				Password = hashedNew,
				HasAdministratorRights = true,
				IsAccountLocked = _currentAdmin.IsAccountLocked,
				HasStrongPassword = _currentAdmin.HasStrongPassword,
				MinimumPasswordLength = _currentAdmin.MinimumPasswordLength,
				PasswordExpirationPeriodMonths = (int)numAdminPasswordLifetime.Value,
				LastPasswordUpdate = DateTime.UtcNow,
			};

			var result = _storage.Update(updateModel);
			if (result != null)
			{
				ShowMessage("Пароль администратора успешно изменен", "Успех");
				ClearPasswordFields();
				_currentAdmin.Password = hashedNew;
				_currentAdmin.LastPasswordUpdate = updateModel.LastPasswordUpdate;
			}
			else
			{
				ShowMessage("Ошибка при изменении пароля", "Ошибка");
			}
		}

		private void ClearPasswordFields()
		{
			txtCurrentPassword.Clear();
			txtNewPassword.Clear();
			txtConfirmPassword.Clear();
		}
		#endregion

		#region User Management
		private void CreateNewUser(object sender, EventArgs e)
		{
			var username = txtNewUsername.Text.Trim();

			if (string.IsNullOrWhiteSpace(username))
			{
				ShowMessage("Введите имя пользователя", "Ошибка");
				return;
			}

			var existingUser = _storage.GetElement(new UserSearchModel { Username = username });
			if (existingUser != null)
			{
				ShowMessage("Пользователь с таким именем уже существует", "Ошибка");
				return;
			}

			var userModel = new UserBindingModel
			{
				Username = username,
				Password = Hasher.GenerateMd4Hash(string.Empty),
				HasAdministratorRights = false,
				IsAccountLocked = false,
				HasStrongPassword = chkEnablePasswordRestrictions.Checked,
				MinimumPasswordLength = (int)numDefaultPasswordLength.Value,
				PasswordExpirationPeriodMonths = (int)numDefaultPasswordLifetime.Value,
				LastPasswordUpdate = DateTime.UtcNow
			};

			var result = _storage.Insert(userModel);
			if (result != null)
			{
				ShowMessage("Пользователь успешно создан", "Успех");
				RefreshUserData();
				txtNewUsername.Clear();
			}
			else
			{
				ShowMessage("Ошибка при создании пользователя", "Ошибка");
			}
		}

		private void ToggleUserStatus(object sender, EventArgs e)
		{
			if (_userList.Count == 0) return;

			var currentUser = _userList[_selectedIndex];
			if (currentUser.HasAdministratorRights)
			{
				ShowMessage("Нельзя изменить статус администратора", "Ошибка");
				return;
			}

			currentUser.IsAccountLocked = !currentUser.IsAccountLocked;
			var updateModel = CreateUserBindingModel(currentUser);

			var result = _storage.Update(updateModel);
			if (result != null)
			{
				RefreshUserData();
				ShowMessage($"Пользователь {(currentUser.IsAccountLocked ? "заблокирован" : "разблокирован")}", "Успех");
			}
			else
			{
				ShowMessage("Ошибка при изменении статуса", "Ошибка");
			}
		}

		private void RemoveUser(object sender, EventArgs e)
		{
			if (_userList.Count == 0) return;

			var currentUser = _userList[_selectedIndex];
			var confirmation = MessageBox.Show(
				$"Вы уверены, что хотите удалить пользователя {currentUser.Username}?",
				"Подтверждение удаления",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (confirmation == DialogResult.Yes)
			{
				var result = _storage.Delete(new UserBindingModel { Id = currentUser.Id });
				if (result != null)
				{
					RefreshUserData();
					ShowMessage("Пользователь удален", "Успех");
				}
				else
				{
					ShowMessage("Ошибка при удалении пользователя", "Ошибка");
				}
			}
		}

		private UserBindingModel CreateUserBindingModel(UserViewModel user)
		{
			return new UserBindingModel
			{
				Id = user.Id,
				Username = user.Username,
				Password = user.Password,
				HasAdministratorRights = user.HasAdministratorRights,
				IsAccountLocked = user.IsAccountLocked,
				PasswordExpirationPeriodMonths = user.PasswordExpirationPeriodMonths,
				LastPasswordUpdate = user.LastPasswordUpdate,
			};
		}
		#endregion

		private void ShowMessage(string message, string title)
		{
			MessageBox.Show(message, title, MessageBoxButtons.OK,
				title == "Успех" ? MessageBoxIcon.Information : MessageBoxIcon.Error);
		}
	}
}


