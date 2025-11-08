namespace SecurityApplication
{
	partial class AdminDashboard
	{
		private System.ComponentModel.IContainer components = null;

		// Main Controls
		private DataGridView dataGridViewUsers;
		private Panel navigationPanel;
		private Panel adminPanel;
		private Panel userManagementPanel;

		// Navigation Controls
		private Button btnFirst, btnPrevious, btnNext, btnLast, btnRefresh;
		private Label lblNavigationInfo;
		private Button btnToggleStatus, btnDeleteUser;

		// Admin Password Controls
		private TextBox txtCurrentPassword, txtNewPassword, txtConfirmPassword;
		private NumericUpDown numAdminPasswordLifetime;
		private Button btnChangeAdminPassword;

		// User Management Controls
		private TextBox txtNewUsername;
		private CheckBox chkEnablePasswordRestrictions;
		private NumericUpDown numDefaultPasswordLength, numDefaultPasswordLifetime;
		private Button btnCreateUser;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			// Main Form Setup
			this.components = new System.ComponentModel.Container();
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1200, 700);
			this.Text = "Панель управления пользователями";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);

			// Initialize Components
			InitializeDataGrid();
			InitializeNavigationPanel();
			InitializeAdminPanel();
			InitializeUserManagementPanel();
			LayoutControls();

			// Привязка обработчиков событий
			AttachEventHandlers();
		}

		private void AttachEventHandlers()
		{
			// Навигация
			btnFirst.Click += NavigateToFirst;
			btnPrevious.Click += NavigateToPrevious;
			btnNext.Click += NavigateToNext;
			btnLast.Click += NavigateToLast;
			btnRefresh.Click += RefreshData;

			// Действия с пользователями
			btnToggleStatus.Click += ToggleUserStatus;
			btnDeleteUser.Click += RemoveUser;

			// Админ пароль
			btnChangeAdminPassword.Click += ChangeAdminPassword;

			// Создание пользователя
			btnCreateUser.Click += CreateNewUser;

			// События DataGridView
			dataGridViewUsers.SelectionChanged += OnUserSelectionChanged;
			dataGridViewUsers.DataBindingComplete += OnDataBindingComplete;
		}

		private void InitializeDataGrid()
		{
			dataGridViewUsers = new DataGridView();
			dataGridViewUsers.Location = new Point(20, 20);
			dataGridViewUsers.Size = new Size(800, 350);
			dataGridViewUsers.BackgroundColor = Color.White;
			dataGridViewUsers.BorderStyle = BorderStyle.Fixed3D;
			dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridViewUsers.ReadOnly = true;
			dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGridViewUsers.RowHeadersVisible = false;
			dataGridViewUsers.AllowUserToAddRows = false;
			dataGridViewUsers.AllowUserToDeleteRows = false;
		}

		private void InitializeNavigationPanel()
		{
			navigationPanel = new Panel();
			navigationPanel.Location = new Point(20, 390);
			navigationPanel.Size = new Size(800, 100);
			navigationPanel.BackColor = Color.FromArgb(240, 240, 240);
			navigationPanel.BorderStyle = BorderStyle.FixedSingle;

			// Navigation Buttons
			btnFirst = CreateNavigationButton("<<", 20, 20, "NavigateToFirst");
			btnPrevious = CreateNavigationButton("<", 80, 20, "NavigateToPrevious");
			btnNext = CreateNavigationButton(">", 140, 20, "NavigateToNext");
			btnLast = CreateNavigationButton(">>", 200, 20, "NavigateToLast");
			btnRefresh = CreateNavigationButton("Обновить", 260, 20, "RefreshData");

			// User Action Buttons
			btnToggleStatus = CreateActionButton("Изменить статус", 400, 20, Color.Orange, "ToggleUserStatus");
			btnDeleteUser = CreateActionButton("Удалить", 550, 20, Color.FromArgb(220, 80, 80), "RemoveUser");

			// Navigation Info Label
			lblNavigationInfo = new Label();
			lblNavigationInfo.Location = new Point(20, 60);
			lblNavigationInfo.Size = new Size(400, 25);
			lblNavigationInfo.TextAlign = ContentAlignment.MiddleLeft;
			lblNavigationInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblNavigationInfo.Text = "Загрузка...";

			// Add controls to panel
			navigationPanel.Controls.AddRange(new Control[] {
				btnFirst, btnPrevious, btnNext, btnLast, btnRefresh,
				btnToggleStatus, btnDeleteUser, lblNavigationInfo
			});
		}

		private void InitializeAdminPanel()
		{
			adminPanel = new Panel();
			adminPanel.Location = new Point(840, 20);
			adminPanel.Size = new Size(330, 300);
			adminPanel.BackColor = Color.FromArgb(230, 240, 255);
			adminPanel.BorderStyle = BorderStyle.FixedSingle;

			var title = new Label();
			title.Text = "Безопасность администратора";
			title.Location = new Point(10, 10);
			title.Size = new Size(300, 25);
			title.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

			// Password fields
			txtCurrentPassword = CreatePasswordField("Текущий пароль:", 50);
			txtNewPassword = CreatePasswordField("Новый пароль:", 90);
			txtConfirmPassword = CreatePasswordField("Подтверждение:", 130);

			// Password lifetime
			var lifetimeLabel = new Label();
			lifetimeLabel.Text = "Срок действия (дни):";
			lifetimeLabel.Location = new Point(10, 170);
			lifetimeLabel.Size = new Size(150, 25);

			numAdminPasswordLifetime = new NumericUpDown();
			numAdminPasswordLifetime.Location = new Point(160, 170);
			numAdminPasswordLifetime.Size = new Size(80, 25);
			numAdminPasswordLifetime.Minimum = 1;
			numAdminPasswordLifetime.Maximum = 365;
			numAdminPasswordLifetime.Value = 90;

			// Change password button
			btnChangeAdminPassword = new Button();
			btnChangeAdminPassword.Text = "Сменить пароль";
			btnChangeAdminPassword.Location = new Point(80, 210);
			btnChangeAdminPassword.Size = new Size(150, 35);
			btnChangeAdminPassword.BackColor = Color.FromArgb(70, 130, 180);
			btnChangeAdminPassword.ForeColor = Color.White;
			btnChangeAdminPassword.FlatStyle = FlatStyle.Flat;
			btnChangeAdminPassword.Name = "btnChangeAdminPassword";

			adminPanel.Controls.AddRange(new Control[] {
				title, txtCurrentPassword, txtNewPassword, txtConfirmPassword,
				lifetimeLabel, numAdminPasswordLifetime, btnChangeAdminPassword
			});
		}

		private void InitializeUserManagementPanel()
		{
			userManagementPanel = new Panel();
			userManagementPanel.Location = new Point(840, 340);
			userManagementPanel.Size = new Size(330, 300);
			userManagementPanel.BackColor = Color.FromArgb(240, 255, 240);
			userManagementPanel.BorderStyle = BorderStyle.FixedSingle;

			var title = new Label();
			title.Text = "Управление пользователями";
			title.Location = new Point(10, 10);
			title.Size = new Size(300, 25);
			title.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

			// Username field
			var usernameLabel = new Label();
			usernameLabel.Text = "Имя пользователя:";
			usernameLabel.Location = new Point(10, 50);
			usernameLabel.Size = new Size(150, 25);

			txtNewUsername = new TextBox();
			txtNewUsername.Location = new Point(160, 50);
			txtNewUsername.Size = new Size(150, 25);

			// Password restrictions
			chkEnablePasswordRestrictions = new CheckBox();
			chkEnablePasswordRestrictions.Text = "Включить ограничения пароля";
			chkEnablePasswordRestrictions.Location = new Point(10, 90);
			chkEnablePasswordRestrictions.Size = new Size(250, 25);
			chkEnablePasswordRestrictions.Checked = true;

			var lengthLabel = new Label();
			lengthLabel.Text = "Минимальная длина:";
			lengthLabel.Location = new Point(10, 130);
			lengthLabel.Size = new Size(140, 25);

			numDefaultPasswordLength = new NumericUpDown();
			numDefaultPasswordLength.Location = new Point(150, 130);
			numDefaultPasswordLength.Size = new Size(60, 25);
			numDefaultPasswordLength.Minimum = 4;
			numDefaultPasswordLength.Maximum = 20;
			numDefaultPasswordLength.Value = 8;

			var lifetimeLabel = new Label();
			lifetimeLabel.Text = "Срок действия (дни):";
			lifetimeLabel.Location = new Point(10, 170);
			lifetimeLabel.Size = new Size(140, 25);

			numDefaultPasswordLifetime = new NumericUpDown();
			numDefaultPasswordLifetime.Location = new Point(150, 170);
			numDefaultPasswordLifetime.Size = new Size(60, 25);
			numDefaultPasswordLifetime.Minimum = 1;
			numDefaultPasswordLifetime.Maximum = 365;
			numDefaultPasswordLifetime.Value = 60;

			// Create user button
			btnCreateUser = new Button();
			btnCreateUser.Text = "Создать пользователя";
			btnCreateUser.Location = new Point(80, 210);
			btnCreateUser.Size = new Size(150, 35);
			btnCreateUser.BackColor = Color.FromArgb(60, 180, 75);
			btnCreateUser.ForeColor = Color.White;
			btnCreateUser.FlatStyle = FlatStyle.Flat;
			btnCreateUser.Name = "btnCreateUser";

			userManagementPanel.Controls.AddRange(new Control[] {
				title, usernameLabel, txtNewUsername, chkEnablePasswordRestrictions,
				lengthLabel, numDefaultPasswordLength, lifetimeLabel,
				numDefaultPasswordLifetime, btnCreateUser
			});
		}

		private void LayoutControls()
		{
			this.Controls.AddRange(new Control[] {
				dataGridViewUsers,
				navigationPanel,
				adminPanel,
				userManagementPanel
			});
		}

		// Helper methods for creating controls
		private Button CreateNavigationButton(string text, int x, int y, string handlerName)
		{
			var button = new Button();
			button.Text = text;
			button.Location = new Point(x, y);
			button.Size = new Size(50, 30);
			button.FlatStyle = FlatStyle.Flat;
			button.Name = handlerName;
			return button;
		}

		private Button CreateActionButton(string text, int x, int y, Color backColor, string handlerName)
		{
			var button = new Button();
			button.Text = text;
			button.Location = new Point(x, y);
			button.Size = new Size(120, 30);
			button.BackColor = backColor;
			button.ForeColor = Color.White;
			button.FlatStyle = FlatStyle.Flat;
			button.Name = handlerName;
			return button;
		}

		private TextBox CreatePasswordField(string labelText, int y)
		{
			var label = new Label();
			label.Text = labelText;
			label.Location = new Point(10, y);
			label.Size = new Size(140, 25);

			var textBox = new TextBox();
			textBox.Location = new Point(150, y);
			textBox.Size = new Size(150, 25);
			textBox.UseSystemPasswordChar = true;

			adminPanel.Controls.Add(label);
			return textBox;
		}
	}
}