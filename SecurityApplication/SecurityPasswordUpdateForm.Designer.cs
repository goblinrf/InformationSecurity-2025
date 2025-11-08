namespace SecurityApplication
{
	partial class SecurityPasswordUpdateForm
	{
		private System.ComponentModel.IContainer components = null;

		// Main Controls
		private Panel headerPanel;
		private Panel contentPanel;
		private Panel footerPanel;

		// Header Controls
		private Label lblHeader;
		private Label lblUserInfo;
		private Label lblUsername; // ДОБАВЛЕН

		// Content Controls
		private TextBox txtNewPassword;
		private TextBox txtConfirmPassword;
		private Label lblPasswordStrength;
		private Label lblConfirmationStatus;
		private Panel panelRequirements;
		private TextBox txtRequirements;

		// Footer Controls
		private Button btnUpdatePassword;
		private Button btnCancel;

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
			this.components = new System.ComponentModel.Container();
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(500, 550);
			this.Text = "Обновление пароля безопасности";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);

			InitializeHeaderPanel();
			InitializeContentPanel();
			InitializeFooterPanel();
			LayoutForm();
		}

		private void InitializeHeaderPanel()
		{
			headerPanel = new Panel();
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 120; // Увеличил высоту для нового лейбла
			headerPanel.BackColor = Color.FromArgb(220, 80, 80);
			headerPanel.Padding = new Padding(20);

			lblHeader = new Label();
			lblHeader.Text = "ТРЕБУЕТСЯ ОБНОВЛЕНИЕ ПАРОЛЯ";
			lblHeader.ForeColor = Color.White;
			lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblHeader.Dock = DockStyle.Top;
			lblHeader.Height = 40;
			lblHeader.TextAlign = ContentAlignment.MiddleCenter;

			lblUsername = new Label(); // ДОБАВЛЕН
			lblUsername.Text = "";
			lblUsername.ForeColor = Color.White;
			lblUsername.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblUsername.Dock = DockStyle.Top;
			lblUsername.Height = 30;
			lblUsername.TextAlign = ContentAlignment.MiddleCenter;

			headerPanel.Controls.Add(lblUsername); // ДОБАВЛЕН
			headerPanel.Controls.Add(lblUserInfo);
			headerPanel.Controls.Add(lblHeader);
		}

		private void InitializeContentPanel()
		{
			contentPanel = new Panel();
			contentPanel.Dock = DockStyle.Fill;
			contentPanel.BackColor = Color.White;
			contentPanel.Padding = new Padding(30);

			// New Password Section
			var lblNewPassword = new Label();
			lblNewPassword.Text = "Новый пароль:";
			lblNewPassword.Location = new Point(0, 20);
			lblNewPassword.Size = new Size(150, 25);
			lblNewPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

			txtNewPassword = new TextBox();
			txtNewPassword.Location = new Point(160, 20);
			txtNewPassword.Size = new Size(320, 30);
			txtNewPassword.UseSystemPasswordChar = true;
			txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
			txtNewPassword.PlaceholderText = "Введите новый пароль";

			lblPasswordStrength = new Label();
			lblPasswordStrength.Location = new Point(160, 55);
			lblPasswordStrength.Size = new Size(350, 20);
			lblPasswordStrength.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

			// Confirm Password Section
			var lblConfirmPassword = new Label();
			lblConfirmPassword.Text = "Подтверждение:";
			lblConfirmPassword.Location = new Point(0, 90);
			lblConfirmPassword.Size = new Size(150, 25);
			lblConfirmPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

			txtConfirmPassword = new TextBox();
			txtConfirmPassword.Location = new Point(160, 90);
			txtConfirmPassword.Size = new Size(320, 30);
			txtConfirmPassword.UseSystemPasswordChar = true;
			txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
			txtConfirmPassword.PlaceholderText = "Повторите новый пароль";

			lblConfirmationStatus = new Label();
			lblConfirmationStatus.Location = new Point(160, 125);
			lblConfirmationStatus.Size = new Size(250, 20);
			lblConfirmationStatus.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

			// Requirements Panel
			panelRequirements = new Panel();
			panelRequirements.Location = new Point(25, 160);
			panelRequirements.Size = new Size(440, 120);
			panelRequirements.BorderStyle = BorderStyle.FixedSingle;
			panelRequirements.BackColor = Color.FromArgb(250, 250, 250);

			var lblReqTitle = new Label();
			lblReqTitle.Text = "Требования к паролю:";
			lblReqTitle.Location = new Point(10, 10);
			lblReqTitle.Size = new Size(200, 20);
			lblReqTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

			txtRequirements = new TextBox();
			txtRequirements.Location = new Point(40, 35);
			txtRequirements.Size = new Size(420, 75);
			txtRequirements.Multiline = true;
			txtRequirements.ReadOnly = true;
			txtRequirements.BorderStyle = BorderStyle.None;
			txtRequirements.BackColor = Color.FromArgb(250, 250, 250);
			txtRequirements.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
			txtRequirements.ScrollBars = ScrollBars.Vertical;

			panelRequirements.Controls.Add(txtRequirements);
			panelRequirements.Controls.Add(lblReqTitle);

			contentPanel.Controls.AddRange(new Control[] {
				lblNewPassword, txtNewPassword, lblPasswordStrength,
				lblConfirmPassword, txtConfirmPassword, lblConfirmationStatus,
				panelRequirements
			});
		}

		private void InitializeFooterPanel()
		{
			footerPanel = new Panel();
			footerPanel.Dock = DockStyle.Bottom;
			footerPanel.Height = 80;
			footerPanel.BackColor = Color.FromArgb(240, 240, 240);
			footerPanel.Padding = new Padding(20);

			btnCancel = new Button();
			btnCancel.Text = "Отмена";
			btnCancel.Size = new Size(120, 40);
			btnCancel.Location = new Point(260, 20);
			btnCancel.BackColor = Color.FromArgb(200, 200, 200);
			btnCancel.FlatStyle = FlatStyle.Flat;
			btnCancel.Name = "btnCancel";

			btnUpdatePassword = new Button();
			btnUpdatePassword.Text = "Обновить пароль";
			btnUpdatePassword.Size = new Size(150, 40);
			btnUpdatePassword.Location = new Point(100, 20);
			btnUpdatePassword.BackColor = Color.FromArgb(70, 130, 180);
			btnUpdatePassword.ForeColor = Color.White;
			btnUpdatePassword.FlatStyle = FlatStyle.Flat;
			btnUpdatePassword.Name = "btnUpdatePassword";

			footerPanel.Controls.Add(btnCancel);
			footerPanel.Controls.Add(btnUpdatePassword);
		}

		private void LayoutForm()
		{
			this.Controls.AddRange(new Control[] {
				contentPanel,
				footerPanel,
				headerPanel
			});
		}
	}
}